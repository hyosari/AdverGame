using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class MajorResult : MonoBehaviour {

    public static MajorResult instance = null;
    public Animator animator;

  //  public static int aValue,bValue;

    //private string urlScoreList = "http://localhost/get_score_list.php";
   // private string urlScoreList = "http://ec2-52-78-85-116.ap-northeast-2.compute.amazonaws.com/Major_Result.php";

	private string urlGetScore = "http://ec2-52-78-85-116.ap-northeast-2.compute.amazonaws.com/Major_Result.php";

	public int valueA;
	public int valueB;


    
    


    void Awake()
    {
        //싱글턴 인스턴스 할당
        instance = this;

    }

	public IEnumerator GetMajorScore()
	{
		int value;
		WWWForm form = new WWWForm ();

		form.AddField ("btn", "A");  // 임의의 btn 변수  어느 것 이나 상 관 없 음 < 추 후 삭 제 요 망 >
        form.AddField("room_num", PhotonNetwork.player.userId);// room_num 전 달 

		var www = new WWW (urlGetScore, form);

		yield return www;

		if(!string.IsNullOrEmpty(www.error))    // json 데 이 터 보 내 는 데 에 러 발 생 여 부 
		{
			Debug.Log ("JSON SENDING ERROR");

		}

		//server 에 서 전 송 한 json 데 이 터 parsing 
		var N = JSON.Parse (www.text);

		yield return N;

		valueA = N [0] ["value"].AsInt;
		valueB = N [1] ["value"].AsInt;


	}

	public int GetMajorOptScore(string btn)
	{
		if (btn == "A") 
		{
			return valueA;
		} else if (btn == "B")
		{
			return valueB;
		} else 
		{
			Debug.Log (" you input the wrong btn option");
			return 0;
		}
	}

    

   /* public IEnumerator GetScoreList(string btn)
    {
      
        //post방식으로 인자를 전달하기 위한 form선언
        WWWForm form = new WWWForm();
        form.AddField("btn", btn);

        //url 호출
        var www = new WWW(urlScoreList, form);
        //완료시점까지 대기



      

        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            
            Debug.Log(www.text);
            //점수 표시 함수 호출
           StartCoroutine(this.DispScoreList(www.text,btn));
        }
        else
        {
            Debug.Log("Error : " + www.error);
        }

    }
    //JSON 파일을 파싱한 후 점수를 표시하는 함수
    public IEnumerator DispScoreList(string strJsonData,string btn)
    {
       
        //JSON파일 파싱
        var N = JSON.Parse(strJsonData);
        //JSON오브젝트의 배열만큼 순회
        for (int i = 0; i < N.Count; i++)
        {
           
            //  int ranking = N[i]["ranking"].AsInt;
            if(btn=="A")
              aValue = N[i]["value"].AsInt;

            if (btn == "B")
              bValue = N[i]["value"].AsInt;

            Debug.Log(aValue);
        }
        yield return N;
        
        if(btn=="B")
            StartCoroutine(this.Result());
       // animator.SetTrigger("Play");
    }

    public IEnumerator Result()
    {
        if (aValue > bValue)
            animator.SetTrigger("PlayA");
        else if(bValue > aValue)
            animator.SetTrigger("PlayB");

        yield return new WaitForSeconds(1);
    }*/



}
