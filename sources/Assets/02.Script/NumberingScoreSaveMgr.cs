using UnityEngine;
using System.Collections;
using SimpleJSON;

public class NumberingScoreSaveMgr : MonoBehaviour
{
    //싱글턴 인스턴스 선언
    public static NumberingScoreSaveMgr instance = null;



    //MySQL데이터 베이스 사용을 위해 부여된 고유번호
    private const string seqNo = "7777";
    //점수 저장 PHP주소
    private string urlSave = "http://ec2-52-78-85-116.ap-northeast-2.compute.amazonaws.com/Score_save.php";
    //  private string urlSave = "http://localhost/save_score.php";
    //랭킹 정보를 요청하기 위한 php주소
    private string urlScoreList = "http://ec2-52-78-85-116.ap-northeast-2.compute.amazonaws.com/get_score_list.php";
    //private string urlScoreList = "http://localhost/get_score_list.php";


    void Awake()
    {
        //싱글턴 인스턴스 할당
        instance = this;

    }

    //점수저장을 위한 코루틴 함수
    public IEnumerator SaveScore(string room_num, int game_num, string stu_num, int answ)  //방번호, 게임번호(순서맞추기는 2), 학번, 답/(나는 맞추면 1,아니면0)
    {

        //POST방식으로 인자를 전달하기 위한 FORM선언
        WWWForm form = new WWWForm();
        //전달할 파라미터 설정
        form.AddField("room_num", room_num);
        form.AddField("game_num", game_num);
        form.AddField("stu_num", stu_num);
        form.AddField("answ", answ);


        //url호출
        var www = new WWW(urlSave, form);

        //완료시점까지 대기
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.text);
        }
        else
        {
            Debug.Log("Error : " + www.error);
        }

        //점수저장후 랭킹정보 요청을 위한 코루틴 함수 호출
        //StartCoroutine(RankingManager.instance.GetScoreList(room_num));

    }



}