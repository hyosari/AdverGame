using UnityEngine;
using System.Collections;
using SimpleJSON;


public class MajorDataMgr : MonoBehaviour
{
    //싱글턴 인스턴스 선언
    public static MajorDataMgr instance = null;


    //MySQL데이터 베이스 사용을 위해 부여된 고유번호
    private const string seqNo = "7777";
    //점수 저장 PHP주소
    private string urlSave = "http://ec2-52-78-85-116.ap-northeast-2.compute.amazonaws.com/Major_Save.php";
    //private string urlSave = "http://localhost/Major_Save.php";



    void Awake()
    {
        //싱글턴 인스턴스 할당
        instance = this;

    }

    //점수저장을 위한 코루틴 함수
    public IEnumerator SaveScore(string btn)
    {
        Debug.Log("A가 나와야한다" + btn);
        Debug.Log("방번호가 나와야한다" + PhotonNetwork.player.userId);
        //POST방식으로 인자를 전달하기 위한 FORM선언
        WWWForm form = new WWWForm();
        //전달할 파라미터 설정
        form.AddField("btn", btn);
        form.AddField("room_num", PhotonNetwork.player.userId);// 포 톤 네 트 워 크 로 roomnumber 줘 야 함


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
        // StartCoroutine(this.GetScoreList());

    }

  
}