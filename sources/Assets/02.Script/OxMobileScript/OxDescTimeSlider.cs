using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class OxDescTimeSlider : MonoBehaviour
{

    //filled타입의 항목 연결
   // public Slider timeSlider;




    public float ztot;  // 경과 시 간 변 수 

    private PhotonView pv;




    private bool noinf = true; // 무한 루프 방 지 변 수 


    void Start()
    {

        pv = GetComponent<PhotonView>();
    }




    void Update()
    {
        remianTime();
    }

    void remianTime()
    {
        float ztot = Time.timeSinceLevelLoad;
        //timeSlider.value = (ztot);

        if (ztot >= 16)
        {

            if (SceneManager.GetActiveScene().name == "OxDesc")
            {
                
                pv.RPC("LoadOxMobile", PhotonTargets.Others);
                SceneManager.LoadScene("OxMain");
                
            }

        }

    }


    [PunRPC]
    void LoadOxMobile()
    {
        Debug.Log("작동!!!!!!!!!!!!!!!!!!");
        SceneManager.LoadScene("OxMobile");
    }


}