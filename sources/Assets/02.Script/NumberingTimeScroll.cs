using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class NumberingTimeScroll : MonoBehaviour {
    //filled타입의 항목 연결
    public Slider timeSlider;
    //타임스크롤 오브젝트
    public GameObject SliderPanel;

    //씬 시작부터 시간 측정 변수
    private float ztot;

    //애니메이션 관리 변수
    [SerializeField]
    public Animator[] animator;
    //애니메이션으로 이동하는 이미지의 변수
    public GameObject[] DragItem;
    //힌트 이미지를 보여주기 위한 변수
    public GameObject[] HIntItem;

    //rpc호출을 위한 photonvies
    private PhotonView pv;
    /*public Image Q1Image;
    public Image Q2Image;
    public Image Q3Image;
    public Image Q4Image;*/


    void Awake()
    {
        //ChangeQuizImage();
        pv = GetComponent<PhotonView>();
    }
    
    void Start () {
        SliderPanel.SetActive(true);    //슬라이더 보이기

        //방장이 아닌 손님일 경우, 슬라이더 안보이기
        if (SceneManager.GetActiveScene().name == "NumberingMobile")
        {
            SliderPanel.SetActive(false);   
        }
    }

	void Update () {

        if ((SceneManager.GetActiveScene().name == "NumberingScreen") || (SceneManager.GetActiveScene().name == "NumberingScreen1")|| (SceneManager.GetActiveScene().name == "NumberingScreen2"))    //스크린에서만 스크롤바로 시간이 진행되고, 힌트를 보여준다
        {
            if (ztot <= 16)
                remianTime();       //16초 까지만 시간계산

            if (ztot > 3.0f)        //각 초마다 힌트 보여주기
            {
                //힌트주는 애니메이션 실행
                //animator[0].Play("NumberingHInt1");                
            }
            if (ztot > 3.5f)
            {
                //드래그로 이동하는 이미지 끄고, cell에 그 사라진 이미지를 넣는다
                DragItem[0].SetActive(false);
                HIntItem[0].SetActive(true);
            }
            if (ztot > 5.0f)
            {
                //animator[1].Play("NumberingHInt2");
                
            }
            if(ztot>5.5f)
            {
                DragItem[1].SetActive(false);
                HIntItem[1].SetActive(true);
            }
            if (ztot > 7.0f)
            {
                //animator[2].Play("NumberingHInt3");                
            }
            if(ztot>7.5f)
            {
                DragItem[2].SetActive(false);
                HIntItem[2].SetActive(true);
            }
            if (ztot > 9.0f)
            {
                //animator[3].Play("NumberingHInt4");                
            }
            if (ztot > 9.5f)
            {
                DragItem[3].SetActive(false);
                HIntItem[3].SetActive(true);
            }
            if(ztot>15f)
            {
                //Screen일때 씬넘기기
                if(SceneManager.GetActiveScene().name=="NumberingScreen")
                {
                    pv.RPC("NumberingMobile1", PhotonTargets.Others);
                    SceneManager.LoadScene("NumberingScreen1");
                }
                    
                if (SceneManager.GetActiveScene().name == "NumberingScreen1")
                {
                    pv.RPC("NumberingMobile2", PhotonTargets.Others);
                    SceneManager.LoadScene("NumberingScreen2");
                }

                if (SceneManager.GetActiveScene().name == "NumberingScreen2")
                {
                    pv.RPC("LoadscIntro", PhotonTargets.Others);
                    SceneManager.LoadScene("scGameA_3");
                }
                   
            }
        }    
        

    }

    //시간 계산 함수
    void remianTime()
    {
        //씬이 시작한 순간부터 시간을 초단위로 받는다
        ztot = Time.timeSinceLevelLoad;
        timeSlider.value = (ztot);
        //Debug.Log(ztot);              //시간초 확인
    }


    [PunRPC]
    void LoadscIntro()
    {
        SceneManager.LoadSceneAsync("scIntro");
    }

    [PunRPC]
    void NumberingMobile1()
    {
        SceneManager.LoadSceneAsync("NumberingMobile1");
    }

    [PunRPC]
    void NumberingMobile2()
    {
        SceneManager.LoadSceneAsync("NumberingMobile2");
    }
}
