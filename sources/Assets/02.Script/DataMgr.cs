using UnityEngine;
using System.Collections;
using SimpleJSON;
using System;

public class DataMgr : MonoBehaviour
{
    //싱글턴 인스턴스 선언
    public static DataMgr instance = null;

    //MySQL데이터 베이스 사용을 위해 부여된 고유번호
    private const string seqNo = "7777";
    //룸넘버 저장주소
    private string urlSaveR="http://ec2-52-78-85-116.ap-northeast-2.compute.amazonaws.com/Room_create.php";

    //다수결 테이블 저장주소
    //private string urlSaveD = "http://ec2-52-78-85-116.ap-northeast-2.compute.amazonaws.com/Major_table_create.php";

    //학번 저장주소
    private string urlSaveS= "http://ec2-52-78-85-116.ap-northeast-2.compute.amazonaws.com/Save_id.php";

    //랭킹 정보를 요청하기 위한 php주소
    private string urlScoreList = "http://localhost/TankAttack/get_score_list.php";


    void Awake()
    {
        //싱글턴 인스턴스 할당
        instance = this;

    }

    //room_num저장을 위한 코루틴 함수
    /*public IEnumerator SaveRoomNumD(string room_num)
    {
        Debug.Log("room_num 12312412412 :" + room_num);
        //StartCoroutine(GameManager.instance.roomNo(room_num));
        //int  intRoom_num =Convert.ToInt32(room_num);
        //POST방식으로 인자를 전달하기 위한 FORM선언
        WWWForm form = new WWWForm();
        //전달할 파라미터 설정
        form.AddField("room_num", room_num);

        //url호출
        var www = new WWW(urlSaveD, form);

        //완료시점까지 대기
        yield return www;

        Debug.Log("room_num :" + room_num);

        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.text);
        }
        else
        {
            Debug.Log("Error : " + www.error);
        }

        //점수저장후 랭킹정보 요청을 위한 코루틴 함수 호출****************************************해제필요
        StartCoroutine(this.GetScoreList());
    }*/

    //room_num저장을 위한 코루틴 함수
    public IEnumerator SaveRoomNum(string room_num, int opt)
    {
        if(opt==1)
        {
            Debug.Log("room_num 12312412412 :" + room_num);
            //StartCoroutine(GameManager.instance.roomNo(room_num));
            //int  intRoom_num =Convert.ToInt32(room_num);
            //POST방식으로 인자를 전달하기 위한 FORM선언
            WWWForm form = new WWWForm();
            //전달할 파라미터 설정
            form.AddField("room_num", room_num);

            //url호출
            var www = new WWW(urlSaveR, form);

            //완료시점까지 대기
            yield return www;

            Debug.Log("room_num :" + room_num);

            if (string.IsNullOrEmpty(www.error))
            {
                Debug.Log(www.text);
            }
            else
            {
                Debug.Log("Error : " + www.error);
            }
        }

        if (opt == 0)
        {
            //POST방식으로 인자를 전달하기 위한 FORM선언
            WWWForm form = new WWWForm();
            //전달할 파라미터 설정
            form.AddField("stu_num", room_num);

            //url호출
            var www = new WWW(urlSaveR, form);

            //완료시점까지 대기
            yield return www;

            Debug.Log("stu_num : 확인" + room_num);

            if (string.IsNullOrEmpty(www.error))
            {
                Debug.Log(www.text);
            }
            else
            {
                Debug.Log("Error : " + www.error);
            }
        }


        //점수저장후 랭킹정보 요청을 위한 코루틴 함수 호출****************************************해제필요
        StartCoroutine(this.GetScoreList());
    }
    ///*************SaveRoomNum 오버라이딩******************************************이거 어떻게 넣을까???
   /* public IEnumerator SaveRoomNum(string student_id, int opt)
    {
        if(opt==0)
        {
            //POST방식으로 인자를 전달하기 위한 FORM선언
            WWWForm form = new WWWForm();
            //전달할 파라미터 설정
            form.AddField("stu_num", student_id);

            //url호출
            var www = new WWW(urlSaveR, form);

            //완료시점까지 대기
            yield return www;

            Debug.Log("stu_num : 확인" + student_id);

            if (string.IsNullOrEmpty(www.error))
            {
                Debug.Log(www.text);
            }
            else
            {
                Debug.Log("Error : " + www.error);
            }
        }
        

        //점수저장후 랭킹정보 요청을 위한 코루틴 함수 호출****************************************해제필요
        StartCoroutine(this.GetScoreList());
    }*/

    //student_id 저장을 위한 코루틴 함수
    public IEnumerator SaveStudent_id(string student_id)
    {
        //POST방식으로 인자를 전달하기 위한 FORM선언
        WWWForm form = new WWWForm();
        //전달할 파라미터 설정
        form.AddField("student_id", student_id);

        //url호출
        var www = new WWW(urlSaveS, form);

        //완료시점까지 대기
        yield return www;
        Debug.Log("student_id :" + student_id);


        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.text);
        }
        else
        {
            Debug.Log("Error : " + www.error);
        }

        //점수저장후 랭킹정보 요청을 위한 코루틴 함수 호출***************************************************************************해제요망
        StartCoroutine(this.GetScoreList());
    }

    //점수저장을 위한 코루틴 함수-------------------------------------기존코드
    /* public IEnumerator SaveScore(string user_name, int killCount)
     {
         //POST방식으로 인자를 전달하기 위한 FORM선언
         WWWForm form = new WWWForm();
         //전달할 파라미터 설정
         form.AddField("user_name", user_name);
         form.AddField("kill_count", killCount);
         form.AddField("seq_no", seqNo);

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
         StartCoroutine(this.GetScoreList());

     }*/



    //랭킹정보 요청하는 코루틴 함수-----------------------기존코드
    public IEnumerator GetScoreList()
    {
        //post방식으로 인자를 전달하기 위한 form선언
        WWWForm form = new WWWForm();
        form.AddField("seq_no", seqNo);

        //url 호출
        var www = new WWW(urlScoreList, form);
        //완료시점까지 대기
        yield return www;

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
        for (int i = 0; i < N.Count; i++)
        {
            int ranking = N[i]["ranking"].AsInt;
            string userName = N[i]["user_name"].ToString();
            int killCount = N[i]["kill_count"].AsInt;
            //결과값을 콘솔뷰에 표시
            Debug.Log(ranking.ToString() + userName + killCount.ToString());
        }
    }
}
