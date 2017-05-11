using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

public class OxMobileGameManager : MonoBehaviour {

    public Question[] questions;
    private static List<Question> unansweredQuestions;

    private Question currentQuestion;

    [SerializeField]
    private Text factText;

    [SerializeField]
    private float timeBetweenQuestions = 0f;

	[SerializeField]
	private Button trueButton;
	[SerializeField]
	private Button falseButton;

    public static int Gnum = 0;

    //popup창을 위한 변수
    public GameObject popUp;

    //[SerializeField]
 //   private Text trueAnswerText;

 //  [SerializeField]
 //   private Text falseAnswerText;

 /*   [SerializeField]
    private Animator animator;  */ //애 니 메 이 션 없 앰 

    //DB에 넘겨줄 게임 번호 설정을 위한 변수
    //private float quizNum;

    //private string room_num="Room_309";//테스팅용 임시 방번호와 아이디
    //private int stuId = 21200376;

    public static OxMobileGameManager instance = null;
    // private PhotonView pv;                                                    //********************v3.5넘어가는거 동작완료
    void Awake()
    {
        //싱글턴 인스턴스 할당
        instance = this;
        //pv = GetComponent<PhotonView>();                                       //********************v3.5넘어가는거 동작완료

    }

    void Start()
    {
        if(unansweredQuestions == null || unansweredQuestions.Count == 0) //문제가 없다더이상
        {
            unansweredQuestions = questions.ToList<Question>();
        }
       

        //Debug.Log("현재 플레이어 학번: " + Convert.ToInt32(PhotonNetwork.player.name));
        //Debug.Log("현재 플레이가 있는 방번호 : " + PhotonNetwork.player.userId);
        SetCurrentQuestion();
        // Debug.Log(currentQuestion.fact + " is " + currentQuestion.isTrue);
        //StartCoroutine(nextSc());                                                   //********************v3.5넘어가는거 동작완료
        Gnum++;


    }



    void SetCurrentQuestion()
    {
        //int randomQuestionIndex = UnityEngine.Random.Range(0, unansweredQuestions.Count);
        int randomQuestionIndex = 0;
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        factText.text = currentQuestion.fact;

      /* if(currentQuestion.isTrue)
        {
            trueAnswerText.text = "CORRECT";
            falseAnswerText.text = "WRONG";
        }
        else
        {
            trueAnswerText.text = "WRONG";
            falseAnswerText.text = "CORRECT";
        }*/
     
    }
    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);
        //씬넘어가는 부분은 Screen에서 담당한다.
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UserSelectTrue()
    {
        Debug.Log("버튼 작동!");
        if (currentQuestion.isTrue)
        {
			if (!PhotonNetwork.isMasterClient)
			{                                         //게임 번호
				StartCoroutine (OxScoreSaveMgr.instance.SaveScore (PhotonNetwork.player.userId, Gnum, PhotonNetwork.player.name, 1));
				// StartCoroutine(OxScoreSaveMgr.instance.SaveScore(room_num, 1, stuId, 1));  테스팅용
				Debug.Log ("CORRECT!");
				trueButton.interactable = false;
				falseButton.interactable = false;

			}

        }
        else
        {
			if (!PhotonNetwork.isMasterClient) 
			{
				StartCoroutine (OxScoreSaveMgr.instance.SaveScore (PhotonNetwork.player.userId, Gnum, PhotonNetwork.player.name, 0));
				// StartCoroutine(OxScoreSaveMgr.instance.SaveScore(room_num, 1, stuId, 0));      테스팅용
				Debug.Log ("WRONG!");
				trueButton.interactable = false;
				falseButton.interactable = false;
			}
        }

        StartCoroutine(TransitionToNextQuestion());
        //popup창 호출
        popUp.SetActive(true);
    }

    public void UserSelectFalse()
    {
        Debug.Log("버튼 작동!");


        if (!currentQuestion.isTrue)
        {
			if (!PhotonNetwork.isMasterClient) 
			{
				StartCoroutine (OxScoreSaveMgr.instance.SaveScore (PhotonNetwork.player.userId, Gnum, PhotonNetwork.player.name, 1));
				// StartCoroutine(OxScoreSaveMgr.instance.SaveScore(room_num, 1, stuId, 1));
				Debug.Log ("CORRECT!");
				trueButton.interactable = false;
				falseButton.interactable = false;
			}

        }
        else
        {
			if (!PhotonNetwork.isMasterClient)
			{
                StartCoroutine(OxScoreSaveMgr.instance.SaveScore(PhotonNetwork.player.userId, Gnum, PhotonNetwork.player.name, 0));
           // StartCoroutine(OxScoreSaveMgr.instance.SaveScore(room_num, 1, stuId, 0));
				Debug.Log("WRONG!");
				trueButton.interactable = false;
				falseButton.interactable = false;
			}
        }
        StartCoroutine(TransitionToNextQuestion());
        //popup창 호출
        popUp.SetActive(true);
    }

}
