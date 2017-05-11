using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class OxTimeScroll : MonoBehaviour
{

    //filled타입의 항목 연결
    public Slider timeSlider;
    public Animator animator;

    public OxMainQuestion currentQuestion;

    //함수 한번만 실행하기 위한변수
    private bool isActive=false;

    public float ztot;  // 경과 시 간 변 수 

    private PhotonView pv;

    [SerializeField]
    private Text questionText;   // Question Text ( only one display)

    [SerializeField]
    private float timeBetweenQuestions = 10.0f;

    [SerializeField]
    private Image imageA;
    [SerializeField]
    private Image imageB;

    [SerializeField]
    private Text resultA;
    [SerializeField]
    private Text resultB;

    private bool noinf=true; // 무한 루프 방 지 변 수 


    void Start()
    {

        pv = GetComponent<PhotonView>();
    }

   
      

    void Update()
    {

        if (SceneManager.GetActiveScene().name == "OxMain")
            remianTime();


    }

    void remianTime()
    {
        float ztot = Time.timeSinceLevelLoad;
        timeSlider.value = (ztot);
       // Debug.Log("ztot Value : " + ztot);
        if (ztot > 10 && noinf)  // 막대바가 다 찼을 때 
        {
            noinf = false;

            

            if(OxMainGameManager.ques_no==0)
            animator.SetTrigger("True");
            if (OxMainGameManager.ques_no == 1)
                animator.SetTrigger("False");
            if (OxMainGameManager.ques_no == 2)
                animator.SetTrigger("True");

            OxMainGameManager.ques_no++;
            Debug.Log("트루");

            //StartCoroutine(MajorResult.instance.GetScoreList("A"));
            // StartCoroutine(MajorResult.instance.GetScoreList("B"));
        }
        if (ztot >= 15)
        {

                Debug.Log("ztot 20 in ");
            if(OxMainGameManager.ques_no<3 && !isActive)
            {
                isActive = true;
                //모바일을 다시 불러주기 위해 이 스크립트를 모바일에 추가해야한다.
                pv.RPC("LoadOxMobile",PhotonTargets.Others);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 씐 다 시 불 러 오 기 
            }

            if (OxMainGameManager.ques_no == 3 && !isActive)
            {
                isActive = true;
                pv.RPC("LoadscIntro", PhotonTargets.Others);
                SceneManager.LoadSceneAsync("scGameA_2");
            }
        }
    }


   /* [PunRPC]
    void LoadScG2()
    {
        SceneManager.LoadSceneAsync("scGameA_2");
    }*/

   [PunRPC]
    void LoadOxMobile()
    {
        SceneManager.LoadSceneAsync("OxMobile");
    }


    [PunRPC]
    void LoadscIntro()
    {
        SceneManager.LoadSceneAsync("scIntro");
    }


}