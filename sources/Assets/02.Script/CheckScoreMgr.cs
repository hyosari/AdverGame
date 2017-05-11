using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using SimpleJSON;

public class CheckScoreMgr : MonoBehaviour {

    public Text stuIdScoreText;
    public Text t_score;

    private string StuId;
    // Use this for initialization

    private string urlScoreList = "http://ec2-52-78-85-116.ap-northeast-2.compute.amazonaws.com/Get_mileage.php";
    void Start () {
        //모바일에서 저장된 StuId를 받아온다
        StuId = PlayerPrefs.GetString("USER_ID");
        Debug.Log("StuId 확인 : " + StuId);

        //학번 표시
        StartCoroutine(DispStuIdScoreText());
       

    }
	
	// Update is called once per frame
	void Update () {

        EndKey();

    }

    public void OnclickedBackButton()
    {
        //Back버튼을 누르면 다시 모바일 로비 화면으로 돌아간다
        SceneManager.LoadScene("scLobbyMobile");

    }

    IEnumerator DispStuIdScoreText()
    {
        stuIdScoreText.text = StuId + "'s Mileage";

        //post방식으로 인자를 전달하기 위한 form선언
        WWWForm form = new WWWForm();
        form.AddField("student_id", StuId);

        //url 호출
        var www = new WWW(urlScoreList, form);
        //완료시점까지 대기


        yield return www;
        Debug.Log("인터넷 및 서버 연결 완료");

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

    }
    //JSON 파일을 파싱한 후 점수를 표시하는 함수
    void DispScoreList(string strJsonData)
    {

        //JSON파일 파싱
        var N = JSON.Parse(strJsonData);
      
        //JSON오브젝트의 배열만큼 순회
       
             int score = N["mileage"].AsInt;
            Debug.Log(N["mileage"]);
            //결과값을 콘솔뷰에 표시
            //  Debug.Log(ranking.ToString() + userName + killCount.ToString());
         //   Debug.Log(score);
           t_score.text = score.ToString();
             
        
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
