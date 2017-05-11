using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class RankingManager : MonoBehaviour
{

    //  public Question[] questions;
    private static List<Question> unansweredQuestions;

    private Question currentQuestion;
    private const string room_num = "praise";

    //[SerializeField]

    // private Text factText;
    // [SerializeField]
    // private float timeBetweenQuestions = 1f;

    public Text rankingText1, rankingText2, rankingText3, rankingText4;
    public Transform text1, text2, text3, text4;
    private Transform temp;

    //private string urlScoreList = "http://localhost/get_score_list.php";
    private string urlScoreList = "http://ec2-52-78-85-116.ap-northeast-2.compute.amazonaws.com/Score_optimize.php";
    private string urlScoreList1 = "http://ec2-52-78-85-116.ap-northeast-2.compute.amazonaws.com/Ranking_create.php";
    private string urlScoreList2 = "http://ec2-52-78-85-116.ap-northeast-2.compute.amazonaws.com/Set_mileage.php";

    //  [SerializeField]
    //  private Text falseAnswerText;

    public Animator animator;
    public Animation anim;

    void Start()
    {

        // rankingText.text = "1ST   21200376   50points";

        /*
        if (unansweredQuestions == null || unansweredQuestions.Count == 0) //문제가 없다더이상
        {
            unansweredQuestions = questions.ToList<Question>();
        }

        SetCurrentQuestion();*/
        // Debug.Log(currentQuestion.fact + " is " + currentQuestion.isTrue);


        StartCoroutine(this.GetScoreList());

    }


    void Awake()
    {
        // if(temp!=null)
        // text1.position = temp.position;
    }

    public IEnumerator GetScoreList()
    {

        //post방식으로 인자를 전달하기 위한 form선언
        WWWForm form = new WWWForm();
        form.AddField("room_num", room_num);

        //url 호출
        var www = new WWW(urlScoreList, form);
        //완료시점까지 대기


        yield return www;

        StartCoroutine(this.GetScoreList1());

        /*
        Debug.Log("dd");

        if (string.IsNullOrEmpty(www.error))
        {
            
            Debug.Log(www.text);
            //점수 표시 함수 호출
            DispScoreList(www.text);
        }
        else
        {
            Debug.Log("Error : " + www.error);
        }
        */

    }
    public IEnumerator GetScoreList1()
    {

        //post방식으로 인자를 전달하기 위한 form선언
        WWWForm form = new WWWForm();
        form.AddField("room_num", room_num);

        //url 호출
        var www = new WWW(urlScoreList1, form);
        //완료시점까지 대기


        yield return www;



        if (string.IsNullOrEmpty(www.error))
        {

            Debug.Log(www.text);
            //점수 표시 함수 호출

            DispScoreList(www.text);
            StartCoroutine(this.SetMileage(room_num));
        }
        else
        {
            Debug.Log("Error : " + www.error);
        }


    }

    public IEnumerator SetMileage(string strJsonData)
    {
        //post방식으로 인자를 전달하기 위한 form선언
        WWWForm form = new WWWForm();

        Debug.Log("1");
        //url 호출

        //완료시점까지 대기

        //JSON파일 파싱
        //   var N = JSON.Parse(strJsonData);
        //JSON오브젝트의 배열만큼 순회

        form.AddField("room_num", room_num);
        //form.AddField("rankings[i][score]", N[i]["score"].AsInt);
        Debug.Log("456");


        var www = new WWW(urlScoreList2, form);

        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {

            Debug.Log(www.text);

        }
        else
        {
            Debug.Log("Error : " + www.error);
        }
        Debug.Log("2");
    }
    //JSON 파일을 파싱한 후 점수를 표시하는 함수
    void DispScoreList(string strJsonData)
    {

        //JSON파일 파싱
        var N = JSON.Parse(strJsonData);
        //JSON오브젝트의 배열만큼 순회
        for (int i = 0; i < N.Count; i++)
        {

            //  int ranking = N[i]["ranking"].AsInt;
            string stu_num = N[i]["stu_num"];

            int score = N[i]["score"].AsInt;
            //결과값을 콘솔뷰에 표시
            // Debug.Log( N["score"].AsInt);

            switch (i)
            {
                case 0:
                    // StartCoroutine(ani(i));
                    rankingText1.text = "1ST  " + stu_num.ToString() + "  " + score.ToString();

                    break;
                case 1:
                    // StartCoroutine(ani(i));
                    rankingText2.text = "2ND  " + stu_num.ToString() + "  " + score.ToString();
                    break;
                case 2:
                    //  StartCoroutine(ani(i));
                    rankingText3.text = "3RD  " + stu_num.ToString() + "  " + score.ToString();
                    break;
                case 3:
                    // StartCoroutine(ani(i));
                    rankingText4.text = "4TH  " + stu_num.ToString() + "  " + score.ToString();
                    break;


            }


        }
    }

    IEnumerator ani(int i)
    {
        //  int hash = Animator.StringToHash("1");
        //  animator.Play(hash, 0, 0);
        //  animator.Play("1");
        yield return new WaitForSeconds(1);
        // animator.Play("1wait");
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("1"))
        {
            //   Debug.Log("dd");
            //   animator.speed = 0;

        }
        // yield return new WaitForSeconds(1);
        // animator.speed = 1;
        //  animator.Play("2");


        //animator["0"].speed = 1.0f;

        //  temp.position = new Vector3(text1.position.x, text1.position.y, text1.position.z);


    }

    /*
    void SetCurrentQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        factText.text = currentQuestion.fact;

        if(currentQuestion.isTrue)
        {
            trueAnswerText.text = "CORRECT";
            falseAnswerText.text = "WRONG";
        }
        else
        {
            trueAnswerText.text = "WRONG";
            falseAnswerText.text = "CORRECT";
        }
     
    }
    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UserSelectTrue()
    {
        animator.SetTrigger("True");
        if(currentQuestion.isTrue)
        {
            StartCoroutine(DataMgr.instance.SaveScore("160715", 1));
            Debug.Log("CORRECT!");

        }
        else
        {
            Debug.Log("WRONG!");
        }

        StartCoroutine(TransitionToNextQuestion());
    }

    public void UserSelectFalse()
    {
        animator.SetTrigger("False");
        if (!currentQuestion.isTrue)
        {
            StartCoroutine(DataMgr.instance.SaveScore("160715", 1));
            Debug.Log("CORRECT!");

        }
        else
        {
            Debug.Log("WRONG!");
        }
        StartCoroutine(TransitionToNextQuestion());
    }
    */
}
