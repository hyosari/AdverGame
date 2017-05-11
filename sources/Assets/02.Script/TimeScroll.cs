using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TimeScroll : MonoBehaviour {
    //filled타입의 항목 연결
    public Slider timeSlider;
    //방장이 아닌 손님일 경우, 슬라이더 안보이기 위한 변수
    public GameObject timeScroll;
       

void Start () {
        //방장이 아닌 손님일 경우, 슬라이더 안보이기
        if (!PhotonNetwork.isMasterClient)
        {
                timeScroll.SetActive(false);
        }
	
	}
	void Update () {
        //씬이 INTRO일떄 시간계산
        if (SceneManager.GetActiveScene().name == "scIntro")
            remianTime();

    }

    //씬이 시작한 시간계산해서 timeScroll로 값을 보내주기
    void remianTime()
    {
        float ztot = Time.timeSinceLevelLoad;
        timeSlider.value = (ztot);
    }
}
