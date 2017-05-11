using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MajorGameManagerUser : MonoBehaviour {

	
   

    private bool aButtnOn = true;
    private bool bButtnOn = true;

   

    public GameObject aButton;   // button 할 당 
    public GameObject bButton;

    private Button aButtonScript;
    private Button bButtonScript;


    void Start()
    {

        aButtnOn = true;
        aButtonScript = aButton.GetComponent<Button>();
        bButtonScript = bButton.GetComponent<Button>();

    }


    

    public void OnAbuttonClicked()
    {
        if (aButtnOn)
        {
            // 포톤 변 수 또는 RPC 함 수 로 변 수 증 가 시키는 부분 추가 해야 함 
            aButtnOn = false;
            bButtnOn = false;
            aButtonScript.interactable = false; // button 기능 false 
                                                // Animator 를 추 가 시 켜 도 됨 
            bButtonScript.interactable = false;

            // pv.RPC("increaseCount", PhotonTargets.All, true);

       
            StartCoroutine(MajorDataMgr.instance.SaveScore("A"));
        }
    }

    public void OnBbuttonClicked()
    {
        if (bButtnOn)
        {
            // 포톤 변 수 또는 RPC 함 수 로 변 수 증 가 시키는 부분 추가 해야 함 
            aButtnOn = false;
            bButtnOn = false;
            aButtonScript.interactable = false; // button 기능 false 
                                                // Animator 를 추 가 시 켜 도 됨 
            bButtonScript.interactable = false;
            //  pv.RPC("increaseCount", PhotonTargets.AllBuffered, false);

            StartCoroutine(MajorDataMgr.instance.SaveScore("B"));
        }
    }


}
