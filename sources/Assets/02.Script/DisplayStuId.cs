using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class DisplayStuId : MonoBehaviour {

    
    public Text userId; //학번을 표시하기 위한 txt
    private PhotonView pv = null;
    
    //ID오브젝트에서 자신의 stu를 txt에 표시하는 것
    void Start () {
        pv = GetComponent<PhotonView>();
        if(pv.isMine && ("admin" != pv.owner.name)) //어드민 아닌 사람의 이름을 받아서 표시
        {

            //userId.text = ("STU ID : " + pv.owner.name);    //USER_ID표시

            //DataMgr의 SaveRoomNum에 학번 저장하고 서버로 보냄
            StartCoroutine(DataMgr.instance.SaveRoomNum(PhotonNetwork.player.name,0));
            //DataMgr의 SaveStudent_id에 학번 저장하고 서버로 보냄      
            StartCoroutine(DataMgr.instance.SaveStudent_id(PhotonNetwork.player.name));    
        }
            

    }
	

}
