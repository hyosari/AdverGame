using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class NumberingDescMgr : MonoBehaviour {

    public float ztot;  // 경과 시 간 변 수 

    private PhotonView pv;

    private bool noinf = true; // 무한 루프 방 지 변 수 

    void Start()
    {

        pv = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "NumberingDesc")
            remianTime();
    }

    void remianTime()
    {
        float ztot = Time.timeSinceLevelLoad;
        //timeSlider.value = (ztot);

        if (ztot >= 17)
        {

            if (SceneManager.GetActiveScene().name == "NumberingDesc")
            {

                pv.RPC("LoadNumMobile", PhotonTargets.Others);
                SceneManager.LoadScene("NumberingScreen");

            }

        }

    }


    [PunRPC]
    void LoadNumMobile()
    {
        Debug.Log("작동!!!!!!!!!!!!!!!!!!");
        SceneManager.LoadScene("NumberingMobile");
    }

}
