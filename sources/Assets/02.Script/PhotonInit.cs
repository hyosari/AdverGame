using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;


public class PhotonInit : MonoBehaviour
{
    string _roomName;
    public string version = "v1.0";

    //플레이어 이름을 입력하는 UI항목 연결
    public InputField userId;
    //룸 이름을 입력받을 UI항목 연결 변수
    public InputField roomName;
    //RoomItem이 차일드로 생성될 parent객체
    public GameObject scrollContents;
    //룸 목록만큼 생성될 RoomItem 프리팹
    public GameObject roomItem;
    /////////////////////////////////////////////게임입장 버튼
    //private bool enterGame=false;

    //입장 버튼

    void Awake()
    {
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings(version);
        }
        userId.text = GetUserId();
        

        //룸이름을 무작위로 설정
        roomName.text = "Room_" + UnityEngine.Random.Range(0, 999).ToString("000");
        
    }
    
    void OnJoinedLobby()
    {
        Debug.Log("Entered Lobby !");
        userId.text = GetUserId();


        //랜덤매치
        //PhotonNetwork.JoinRandomRoom();
    }

    //로컬에 저장된 플레이어 이름을 반환하거나 생성하는 함수
    string GetUserId()
    {
        string userId = PlayerPrefs.GetString("USER_ID");

        if (string.IsNullOrEmpty(userId))
        {
            userId = "USER_" + UnityEngine.Random.Range(0, 999).ToString("000");

        }
        return userId;
    }

    //무작위 룸 접속에 실패한 경우 호출되는 콜백 함수
    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("No Rooms !");
        //무작위 접속에 실패했을 경우 방을 만드는 부분이다. -> 지금은 방을 만들지 않는다.
        //PhotonNetwork.CreateRoom("MyRoom");
    }

    void OnJoinedRoom()
    {
        Debug.Log("Enter Room");
        //탱크를 생성하는 함수 호출
        //CreatID();

        //룸씬으로 이동하는 코루틴 실행
        StartCoroutine(this.LoadBattleField());
    }

    IEnumerator LoadBattleField()
    {
        //씬을 이동하는 동안 포톤 클라우드  서버로 부터 네트워크 메시지 수신 중단
        PhotonNetwork.isMessageQueueRunning = false;
        //백그라운드로 씬 로딩
        AsyncOperation ao = SceneManager.LoadSceneAsync("scIntro");
        yield return ao;
    }
    //*********************************************************************** 안씀
    public void OnClickJoineRandomRoom()
    {
        //로컬 플레이어의 이름을 설정
        PhotonNetwork.player.name = userId.text;
        //플레이어 이름을 저장
        PlayerPrefs.SetString("USER_ID", userId.text);

        //무작위로 추출된 룸으로 입장
        if(PhotonNetwork.player.name != "admin")
            PhotonNetwork.JoinRandomRoom();
    }//**********************************************************************


    public void OnClickCreateRoom()         //아이디가 "admin"인 사람만 방을 만들 수 있다
    {
        string _roomName = roomName.text;

            //로컬 플레이어의 이름을 설정
        PhotonNetwork.player.name = userId.text;
        //플레이어 이름을 저장
        PlayerPrefs.SetString("USER_ID", userId.text);

        if (PhotonNetwork.player.name == "admin")           ////////////////////////////////////////아이디가 admin인지확인
        {
            //룸 이름이 없거나 null인경우 룸 이름 지정
            if (string.IsNullOrEmpty(roomName.text))
            {
                _roomName = "ROOM_" + UnityEngine.Random.Range(0, 999).ToString("000");
            }

            //생성할 룸의 조건 설정
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.isOpen = true;
            roomOptions.isVisible = true;
            roomOptions.maxPlayers = 20;

            //DataMgr의 SaveRoomNum에 방번호 저장하고 서버로 보냄
            StartCoroutine(DataMgr.instance.SaveRoomNum(roomName.text,1));
            //DataMgr의 SaveRoomNumD에 방번호 저장하고 서버로 보냄
            //StartCoroutine(DataMgr.instance.SaveRoomNumD(roomName.text));

            //지정한 조건에 맞는 룸 생성 함수
            PhotonNetwork.CreateRoom(_roomName, roomOptions, TypedLobby.Default);
        }
        else
        {
            Debug.Log("관리자만 방을 만들 수 있습니다.");

        }

    }


    void OnPhotonCreateFailed(object[] codeAndMsg)
    {
        Debug.Log("Create Room Failed = " + codeAndMsg[1]);
    }

    void OnReceivedRoomListUpdate()
    {
        //룸 목록을 다시 받았을 때 갱신하기 위해 기존에 생성된 RoomItem을 삭제
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ROOM_ITEM"))
        {
            Destroy(obj);
        }

        //Grid Layout Group 컴포넌트의 Constraint Count 값을 증가 시킬 변수
        int rowCount = 0;
        scrollContents.GetComponent<RectTransform>().sizeDelta = Vector2.zero;

        //수신받은 룸 목록의 정보로 RoomItem을 생성
        foreach (RoomInfo _room in PhotonNetwork.GetRoomList())
        {
            Debug.Log(_room.name);
            //RoomItem 프리팹을 동적으로 생성
            GameObject room = (GameObject)Instantiate(roomItem);
            //생성한 RoomItem프리팹의 Parent지정
            room.transform.SetParent(scrollContents.transform, false);

            //생성한 RoomItem에 표시하기 위한 텍스트 정보 전달
            RoomData roomData = room.GetComponent<RoomData>();
            roomData.roomName = _room.name;
            roomData.connectPlayer = _room.playerCount;
            roomData.maxPlayers = _room.maxPlayers;
            //텍스트 정보 표시
            roomData.DispRoomData();
            //RoomItem의 Button 컴포넌트에 클릭 이벤트를 동적으로 연결
            roomData.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { OnClickRoomItem(roomData.roomName); });

            //Grid Layout Group 컴포넌트의 Constraint Count값을 증가시킴
            scrollContents.GetComponent<GridLayoutGroup>().constraintCount = ++rowCount;
            //스크롤 영역의 높이를 증가시킴
            scrollContents.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 20);   
        }

    }

    
    //RoomItem이 클릭되면 호출될 이벤트 연결 함수
    public void OnClickRoomItem(string roomName)
    {
        //프레이즈 용으로 특정 방에만 입장하게 설정
        roomName = "praise";

        //로컬 플레이어의 이름을 설정
        PhotonNetwork.player.name = userId.text;
        //플레이어 이름 저장
        PlayerPrefs.SetString("USER_ID", userId.text);
        //인자로 전달된 이름에 해당하는 룸으로 입장
        PhotonNetwork.JoinRoom(roomName);
        /*******************************************************************************************************************/
        PhotonNetwork.player.userId = roomName;
        
    }

    void OnGUI()
    {
        //화면 좌측 상단에 접속 과정에 대한 로드를 출력
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }


    public void OnclickedCheckScoreButton()
    {
        PlayerPrefs.SetString("USER_ID", userId.text);
        PlayerPrefs.Save();

        //stu id가 제대로 저장되었는지 확인
        string checkStu = PlayerPrefs.GetString("USER_ID");
        Debug.Log("c저장된 학번은 : " + checkStu);

        //체크스코어 씬으로 넘어간다
        SceneManager.LoadScene("CheckScore");
    }
    
}
