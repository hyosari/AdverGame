using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class OxMainGameManager : MonoBehaviour {

    public OxMainQuestion[] questions;

    public static int ques_no = 0;
   // public static int ques_no=0;
    private static List<OxMainQuestion> unansweredQuestions;

    private OxMainQuestion currentQuestion;

    [SerializeField]
    private Text factText;

    [SerializeField]
    private float timeBetweenQuestions = 1f;

  


    void Start()
    {
        //뉴라인 적용
        //currentQuestion.fact = currentQuestion.fact.Replace("\\n", "\n");
        //factText.text = factText.text.Replace("NEWLINE", "\n");

        if (unansweredQuestions == null || unansweredQuestions.Count == 0) //문제가 없다더이상
        {
            
            unansweredQuestions = questions.ToList<OxMainQuestion>();
        }

        SetCurrentQuestion();
        // Debug.Log(currentQuestion.fact + " is " + currentQuestion.isTrue);
        

    }

    void SetCurrentQuestion()
    {
        // int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[ques_no];
      

        factText.text = currentQuestion.fact;

        if(currentQuestion.isTrue)
        {
           // trueAnswerText.text = "CORRECT";
           // falseAnswerText.text = "WRONG";
        }
        else
        {
           // trueAnswerText.text = "WRONG";
           // falseAnswerText.text = "CORRECT";
        }
     
    }
    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
