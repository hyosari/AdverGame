using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class RoomData : MonoBehaviour {

    //외부접근을 위해 public으로 선언 하지만 inspector에 노출 x
    [HideInInspector]
    public string roomName = "";
    [HideInInspector]
    public int connectPlayer = 0;
    [HideInInspector]
    public int maxPlayers = 0;

    //룸 이름 표시할 text ui 항목
    public Text textRoomName;
    //룸 접속자 수와 최대 접속자 수를 표시할 TExt UI항목
    public Text textConnectInfo;


    //룸정보를 전달한 후 Text UI항목에 표시하는 함수
    public void DispRoomData()
    {
        textRoomName.text = roomName;
        //룸정보를 DB에 넘겨줌

        Debug.Log("textRoomName : " + textRoomName.text);
        //  StartCoroutine(GameManager.instance.roomNo(textRoomName.text));

        //DataMgr의 SaveRoomNum에 방번호 저장하고 서버로 보냄
        //StartCoroutine(DataMgr.instance.SaveRoomNum(textRoomName.text));
        //DataMgr의 SaveRoomNumD에 방번호 저장하고 서버로 보냄            //******************PhotonInit.OnClickCreateRoom()으로 옮겼다
        //StartCoroutine(DataMgr.instance.SaveRoomNumD(textRoomName.text));
        //사용자 접속 정보를 text에 담는다.
        textConnectInfo.text = "(" + connectPlayer.ToString() + "/" + maxPlayers.ToString() + ")";
        
    }
}
