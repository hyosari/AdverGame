using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

    public Transform targetTr;  //추적할 타깃 게임오브젝트의 transform
    public float dist = 2.0f;  //카메라와의 거리
    public float height = 2.0f;  //카메라와의 높이
    public float dampTrace = 20.0f; //부드러운 추적을 위한 변수

    private Transform tr;

	// Use this for initialization
	void Start () {
        tr = GetComponent<Transform>(); //카메라 자신의 Transform컴포넌트를 tr에 연결
	
	}

    void Update()
    {
        if (Input.GetMouseButtonDown(1))    //마우스 오른쪽 버튼 1인칭
        {
            dist = 0.5f;
            height = 0f;
        }else if (Input.GetMouseButtonDown(0))  //마우스 왼쪽 버튼 3인칭
        {
            dist = 2f;
            height = 1.0f;
        }
    }

   
	
	//Update함수 다 호출된후에 호출되는 부분
    //추적할 타깃이 update로 모두 이동후 카메라가 움직여야하니까 
	void LateUpdate() {
            
         //카메라의 위치를 추적대상의 dist변수만큼뒤쪽으로 배치 and height만큼 위로 올림
         // vcector3.Lerp(vec3 시작위치, vec3 종료위치, float 시간)
        tr.position = Vector3.Lerp(tr.position                                          //시작위치
            , targetTr.position - (targetTr.forward * dist) + (Vector3.up * height)   //종료위치
            , (Time.deltaTime * dampTrace));                                         //시간 dampTrace값으로 추적감도 조절 가능

        //카메라가 타깃 게임오브젝트를 바라보게설정
        tr.LookAt(targetTr.position);
	
	}
}
