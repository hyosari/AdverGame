using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameMgr : MonoBehaviour
{
    /***************************************************************************************/
    //button항목
    public Button aButton;
    public Button bButton;

    //A,B의 카운트 변수
    public int countA;
    public int countB;
    /******************************************************************************************/
    //접속된 플레이어 수를 표시할 text UI항목변수
    public Text txtConnect;

    //카운트 다운을 위한 변수
    private int timer = 0;

    public GameObject Num_A;   //1번
    public GameObject Num_B;   //2번
    public GameObject Num_C;   //3번
    public GameObject Num_D;   //4번
    public GameObject Num_E;   //5번
    public GameObject Num_GO;

    //intro에서 마스터일때, exit버튼 지우기 위한 변수
    public GameObject exitButton;


    //PlayerCtrl스크립트의 destBrake를 확인하기 위한 스크립트 참조 변수
    private PlayerCtrl script;
    //private bool boolTimer=false;


    //rpc호출을 위한 photonvies
    private PhotonView pv;
    //전체 시작을 확인하는 변수
    //private bool isStart=false;

    //scIntro의 노출시간변수
    private float introtime;


    //filled타입의 항목 연결
    //public Slider timeSlider;

    //public Transform destTr;
    //public NavMeshAgent nvAgent;

    // Use this for initialization
    void Awake()
    {
        //destTr = GameObject.FindWithTag("DEST").GetComponent<Transform>();
        //nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        //pv컴포넌트 할당
        //timeSlider.value = 0;

        pv = GetComponent<PhotonView>();
        //ID를 생성하는 함수 호출
        CreateID();
        //포톤클라우드의 네트워크 메시지 수신을 다시 연결
        PhotonNetwork.isMessageQueueRunning = true;
        //룸에 입장후 기본 접속자 정보를 출력
        GetConnectPlayerCount();

        //player = GameObject.FindWithTag("Player").GetComponent<GameObject>();

        //시작시 카운트 다운 초기화
        timer = 0;

        //퀴즈게임으로 넘어가기 전에 나타나는 숫자 카운트 변수들
        Num_A.SetActive(false);
        Num_B.SetActive(false);
        Num_C.SetActive(false);
        Num_D.SetActive(false);
        Num_E.SetActive(false);
        Num_GO.SetActive(false);

        script = GameObject.FindWithTag("Player").GetComponent<PlayerCtrl>();

        //마스터일때 화면에 exitButton보이지 않는 처리
        if (PhotonNetwork.isMasterClient)
        {
            exitButton.SetActive(false);
        }

    }

    //Start 함수를 코루틴으로 호출
    IEnumerator Start()
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //모바일일때 인트로 씬에서 16/10 비율로 고정한다.
        if(!PhotonNetwork.isMasterClient)
        {
            //Screen.SetResolution(Screen.width, Screen.width * 16 / 9, true);
            //Screen.SetResolution(Screen.width, Screen.width * 16 / 10, true);
            Screen.SetResolution(1024, 600, true);
            //Screen.SetResolution(2560,1440, true);
        }
            
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //intro에서 대기하는 시간의 변수
        introtime = 30.0f;

        Debug.Log("[" + PhotonNetwork.player.name + "] Connected");

        //룸에 있는 네트워크 객체간 통신이 완료될떄까지 대기
        yield return new WaitForSeconds(2.0f);

        //마스터이고 인트로 영상일때, 20초 기다리다가 로드씬
        if (PhotonNetwork.isMasterClient && SceneManager.GetActiveScene().name == "scIntro")
        {
            //씬넘어가기 introtime만큼 기다려
            yield return new WaitForSeconds(introtime);
            //pv.RPC("LoadScG1", PhotonTargets.All);    //현재 들어와 있는 사람만 씬넘어가기

            LoadScG1();     //**************************v3.5

        }


        //게임시작(방장이 intro가 아닌경우 계속해서 게임 진행하도록 letStart()실행)
        if (PhotonNetwork.isMasterClient && SceneManager.GetActiveScene().name != "scIntro")
        {
            //script.GameStart();
            yield return new WaitForSeconds(1.0f);
            //pv.RPC("letStart", PhotonTargets.All);  //들어온 사람만 시작

            letStart();     //*************************v3.5

            /*if (script.destIsBrake)                         //파괴확인
                pv.RPC("LoadSc", PhotonTargets.AllBuffered);    //씬넘어가기*/
            //isStart = true;
        }
    }

    void Update()
    {
        EndKey();
        if (script.destIsBrake && PhotonNetwork.isMasterClient)  //마스터인지 확인해서 마스터에서만 타이머가 작동한다->오류방지 ************v3.5
        {

            StartCoroutine("Timer");
        }

    }


    void Timer()
    {

        //if(boolTimer)
        { //게임 시작시 정지
            if (timer == 0)
            {
                Time.timeScale = 0.0f;

                //랭킹씬으로 이동
                
            }

            //Timer 가 150보다 작거나 같을경우 Timer 계속증가
            if (timer <= 160)
            {
                timer++;

                // Timer가 30보다 작을경우 3번켜기
                if (timer < 30)
                {
                    Num_E.SetActive(true);
                }
                // Timer가 30보다 클경우 3번끄고 2번켜기
                if (timer > 30)
                {
                    Num_E.SetActive(false);
                    Num_D.SetActive(true);
                }
                // Timer가 60보다 작을경우 2번끄고 1번켜기
                if (timer > 60)
                {
                    Num_D.SetActive(false);
                    Num_C.SetActive(true);
                }
                //Timer 가 90보다 크거나 같을경우 1번끄고 GO 켜기 LoadingEnd () 코루틴호출 
                if (timer > 90)
                {
                    Num_C.SetActive(false);
                    Num_B.SetActive(true);
                }
                if (timer > 120)
                {
                    Num_B.SetActive(false);
                    Num_A.SetActive(true);
                }
                //Timer 가 150보다 크거나 같을경우 1번끄고 GO 켜기 LoadingEnd () 코루틴호출 
                if (timer >= 150)
                {
                    Num_A.SetActive(false);
                    Num_GO.SetActive(true);
                    StartCoroutine(this.LoadingEnd());
                    Time.timeScale = 1.0f; //게임시작
                    if (timer == 160)
                    {
                        //모두 OX로 이동
                        if (PhotonNetwork.isMasterClient && SceneManager.GetActiveScene().name == "scGameA_1")
                        {
                            //pv.RPC("MainLevel", PhotonTargets.Others);
                            SceneManager.LoadScene("OxDesc");
                        }


                        //스크린 G2로 이동
                        //모바일 INTRO로 이동
                        /*if (PhotonNetwork.isMasterClient && SceneManager.GetActiveScene().name == "MainLevel")
                        {
                            yield return new WaitForSeconds(10.0f);     //10초기다리고 넘어감
                            pv.RPC("LoadscIntro", PhotonTargets.Others);    //모바일은 INtro
                            LoadScG2();                                     //스크린은 G2
                        }*/

                        //모두 DAD로 이동****************************************************************************////3.5.1
                        if (PhotonNetwork.isMasterClient && SceneManager.GetActiveScene().name == "scGameA_2")
                        {
                            //pv.RPC("DragandDrop", PhotonTargets.All);     //NUmberingMobile과 screen나누기 전 코드
                            //pv.RPC("NumberingMobile", PhotonTargets.Others);
                            //NumberingScreen();
                            SceneManager.LoadScene("NumberingDesc");
                        }

                        //모두 다수결로 이동
                        if (PhotonNetwork.isMasterClient && SceneManager.GetActiveScene().name == "scGameA_3")
                        {
                            //pv.RPC("DragandDrop", PhotonTargets.All);     //NUmberingMobile과 screen나누기 전 코드
                            /*pv.RPC("MajorMobile", PhotonTargets.Others);
                            SceneManager.LoadScene("MajorScreen*/
                            SceneManager.LoadScene("MajorDesc");
                        }

                        if (PhotonNetwork.isMasterClient && SceneManager.GetActiveScene().name == "scGameA_4")
                        {
                            SceneManager.LoadScene("Ranking");
                        }

                        /*
                        ////////////////////////////////////////////////////////////////////////////////////////////v3.5_1
                        if (PhotonNetwork.isMasterClient && SceneManager.GetActiveScene().name == "scGameA_1")
                            LoadScG2();         //****************************** v3.5
                        // pv.RPC("LoadScG2", PhotonTargets.All);

                        if (PhotonNetwork.isMasterClient && SceneManager.GetActiveScene().name == "scGameA_2")            //순서정하기
                            pv.RPC("DragandDrop", PhotonTargets.All);
                        /////////////////////////////////////////////////////////////////////////////////////////////////////*/

                        /*if (PhotonNetwork.isMasterClient && SceneManager.GetActiveScene().name == "scGameA_2")              //퀴즈게임
                            pv.RPC("MainLevel", PhotonTargets.All);*/

                    }
                    }
            }
        }

    }

    //1초기다린후 출발 끄기
    IEnumerator LoadingEnd()
    {

        yield return new WaitForSeconds(1.0f);
        Num_GO.SetActive(false);
    }

    [PunRPC]
    void letStart()
    {
        script.GameStart(); //네비게이션 따라가는것만
    }

    /*[PunRPC]/////////////////////////////////////////////////////////////////////////////////////////////v3.3
    void LoadSc()       //마스터 방장의 명령으로 함꼐 같은씬으로 이동한다                ///테스트
    {
        if (SceneManager.GetActiveScene().name == "scIntro")
        {
            SceneManager.LoadSceneAsync("scGameA_1");
        }
            
        if (SceneManager.GetActiveScene().name == "scGameA_1")
        {
            SceneManager.LoadSceneAsync("scGameA_2");
            
        }
            
    }*/
    /// ////////////////////////////////////////////////////////////////////////////////////////////////v.3.4

    [PunRPC]
    void LoadscIntro()
    {
        SceneManager.LoadSceneAsync("scIntro");
    }

    [PunRPC]
    void LoadScG1()
    {
        SceneManager.LoadSceneAsync("scGameA_1");
    }

    [PunRPC]
    void LoadScG2()
    {
        SceneManager.LoadSceneAsync("scGameA_2");
    }

    [PunRPC]
    void LoadScG3()
    {
        SceneManager.LoadSceneAsync("scGameA_3");
    }

    [PunRPC]
    void MainLevel()
    {
        SceneManager.LoadSceneAsync("MainLevel");
    }

    [PunRPC]
    void NumberingMobile()
    {
        SceneManager.LoadSceneAsync("NumberingMobile");
    }

    [PunRPC]
    void NumberingScreen()
    {
        SceneManager.LoadSceneAsync("NumberingScreen");
    }

    [PunRPC]
    void MajorUser()
    {
        SceneManager.LoadSceneAsync("MajorUser");
    }

    [PunRPC]
    void MajorMain()
    {
        SceneManager.LoadSceneAsync("MajorMain");
    }
    [PunRPC]
    void MajorMobile()
    {
        SceneManager.LoadSceneAsync("MajorMobile");
    }

    /* [PunRPC]
     public void Ranking()
     {
         SceneManager.LoadSceneAsync("Ranking");
     }*/


    // ID만들기
    void CreateID()
    {
        PhotonNetwork.Instantiate("ID", new Vector3(0, 0, 0), Quaternion.identity, 0);
    }


    //룸 접속자 정보를 조회하는 함수
    void GetConnectPlayerCount()
    {
        //현재 입장한 룸 정보를 받아옴
        Room currRoom = PhotonNetwork.room;

        //현재 룸의 접속자 수와 최대 접속 가능한 수를 문자열로 구성 후 TEXT UI항목에 출력
        txtConnect.text = currRoom.playerCount.ToString() + "/" + currRoom.maxPlayers.ToString();
    }

    //네트워크 플레이어가 접속했을 때 호출되는 함수
    void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        GetConnectPlayerCount();
    }

    //네트워크 플레이어가 룸을 나가거나 접속이 끊어졌을 떄 호출되는 함수
    void OnPhotonPlayerDisconnected(PhotonPlayer outPlayer)
    {
        GetConnectPlayerCount();
    }

//방장이 Exit버튼으로 종료했을때 모두 방을 떠나게 한다.
    public void OnClickExitRoom()
    {
        //로그 매시지에 출력할 문자열 생성
        //string msg = "\n<color=#ff0000>[" + PhotonNetwork.player.name + "] Disconnected</color>";

        //RPC함수 호출
        //pv.RPC("LogMsg", PhotonTargets.AllBuffered, msg);

        //현재 룸을 빠져나가며 생성한 모든 네트워크 객체를 삭제

        if (PhotonNetwork.isMasterClient)
        {
            /*if (PhotonNetwork.isMasterClient && SceneManager.GetActiveScene().name == "Ranking")
                System.Diagnostics.Process.GetCurrentProcess().Refresh();*/
            /*if (PhotonNetwork.isMasterClient && SceneManager.GetActiveScene().name == "Ranking")
                System.Diagnostics.Process.GetCurrentProcess().Refresh();*/

            pv.RPC("OnLeftRoom", PhotonTargets.All);
        }
        else
        {
            PhotonNetwork.LeaveRoom();
        }
        //SceneManager.LoadScene(0);


        
    }

    //프로그램 종료시 모두 방떠나게 하기
    public void OnApplicationQuit()
    {
        if (PhotonNetwork.isMasterClient)
        {
            //모바일에 작동을 안한다
            pv.RPC("OnLeftRoom", PhotonTargets.Others);
            Debug.Log("작동");
            OnLeftRoom();
        }
        else
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    //룸에서 접속 종료했을때 호출되는 콜백함수
    [PunRPC]
    void OnLeftRoom()
    {
        

        Debug.Log("작동1");
        PhotonNetwork.LeaveRoom();
        //로빈씬을 호출
        SceneManager.LoadScene(0);    //index로 로비씬에 접근
        Debug.Log("작동2");

        //재탕오류 해결방법인데 잘 안되네
        /*if (PhotonNetwork.isMasterClient && SceneManager.GetActiveScene().name == "Ranking")
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            System.Diagnostics.Process.GetCurrentProcess().InitializeLifetimeService();
        }*/
        /*if (PhotonNetwork.isMasterClient && SceneManager.GetActiveScene().name == "Ranking")
            System.Diagnostics.Process.GetCurrentProcess().Refresh();*/


    }

    //프로그램을 버튼으로 종료하게 하는 함수
    //PC에서 esc, 안드로이드에서 back
    void EndKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
