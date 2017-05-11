using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class OxMobileTimeScroll : MonoBehaviour {
    //filled타입의 항목 연결
    public Slider timeSlider;
    public GameObject SliderPanel;

    private PhotonView pv;   //********************v3.5넘어가는거 동작완료


    void Start () {
        pv = GetComponent<PhotonView>();        //********************v3.5넘어가는거 동작완료
        StartCoroutine(nextSc());       //씬넘기기


        SliderPanel.SetActive(true);    //슬라이더 보이기

      /*  if (!PhotonNetwork.isMasterClient)
        {
            SliderPanel.SetActive(false);   //스마트폰에는 슬라이더 안보이기
        }*/  // 스 마 트 폰 에 서 도 보 이 게 하 기 
    }

	void Update () {

       // if (SceneManager.GetActiveScene().name == "DragAndDrop")
            remianTime();
       //if (timeSlider.value == 10) /*****************/
            //nextSc();
    }

    void remianTime()
    {
        float ztot = Time.timeSinceLevelLoad;
        timeSlider.value = (ztot);
    }

    IEnumerator nextSc()       //********************v3.5넘어가는거 동작완료
    {
        if (PhotonNetwork.isMasterClient && SceneManager.GetActiveScene().name == "MainLevel")
        {
            yield return new WaitForSeconds(10.0f);             //10초 기다리다가 넘기기
            //Debug.Log("퀴즈게임 동작1?");
               //10초기다리고 넘어감
            //Debug.Log("퀴즈게임 동작2?");
            pv.RPC("LoadscIntro", PhotonTargets.Others);    //모바일은 INtro  
           // Debug.Log("퀴즈게임 동작3?");
            LoadScG2();                                     //스크린은 G2
            //Debug.Log("퀴즈게임 동작4?");
        }
    }
    [PunRPC]
    void LoadscIntro()
    {
        SceneManager.LoadSceneAsync("scIntro");
    }

    
    void LoadScG2()
    {
        SceneManager.LoadSceneAsync("scGameA_2");
    }
}
