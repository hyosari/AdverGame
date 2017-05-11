using UnityEngine;
using UnityEngine.UI;       //UI접근
using System.Collections;
using UnityEngine.SceneManagement;


/*[System.Serializable]   //이렇게 명시해야 class가 inspector뷰에 나타난다
public  class Anim {
    public AnimationClip idle;
    public AnimationClip runForward;
    public AnimationClip runbackward;
    public AnimationClip runRight;
    public AnimationClip runLeft;

}*/

public class PlayerCtrl : MonoBehaviour {
    
    //목적지 설정과 네비게이션 에이전트 변수 설정
    public Transform destTr;
    public NavMeshAgent nvAgent;
    private Animator animator;

    private float h = 0.0f;
    private float v = 0.0f;

    private Transform tr;

    public float moveSpeed = 10.0f;
    public float rotSpeed = 100.0f;

    //public Anim anim;   //inspetor뷰에 노출될 에니메이션 클래스 변수 선언
    //public Animation _animation;    //아래에 있는 3D모델의 Animation컴포넌트에 접근하기 위한 변수

    //목적지 파괴 변수
    public bool destIsBrake = false;

    //상자를 위한 게임오브젝트 변수
    public GameObject target;           //v3.5.1 상자 열리는 애니메이션 작동
    //상자 파티클을 위한 게임오브젝트 변수
    public GameObject particle;

    //카메라 피벗을 받는 변수
    public GameObject CamPivot;

    public GameObject trDoor;
    public GameObject trFinal;



    //tr변수는 update함수에서 계속 이용하는 것이기 떄문에 start나 awake함수에서 미리 설정해서 매번 실행되는 과부하를 막는다
    void Start () {
        
        //목적지 설정과 네비게이션 에이전트 컴포넌트 받기
        destTr = GameObject.FindWithTag("DEST").GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        animator = this.gameObject.GetComponent<Animator>();


        //doorAnimator = GameObject.FindWithTag("Final").GetComponent<Animator>();
        //CamPivot = GameObject.FindWithTag("CamPivot").GetComponent<GameObject>();



        animator.SetBool("IsTrace", false);
        //doorAnimator.SetBool("openTheDoor", false);

        tr = GetComponent<Transform>();     //이 스크립트가 포함된 게임오브젝트가 갖고있는 여러 컴포넌트 중에서 Transform 컴포넌트를 추출해서 tr변수에 넣어라

        //테스트 용도로 씬을 시작할 때 네비게이션 활성화 코드
        /*if (SceneManager.GetActiveScene().name == "scGameA_4")    
            LastGameStart();*/
    }
	
	// Update is called once per frame
	void Update () {


        //이동 입력받기
        h = Input.GetAxis("Horizontal");    
        v = Input.GetAxis("Vertical");

        //////////////////////////////////////////////////////////////////////////////////////////////////
        //GameStart();

        /*if(!destTr) //목적지가 없을때 새로운 목적지를 찾아 가기 마지막에 오류가 생긴다
        {
           destTr = GameObject.FindWithTag("DEST").GetComponent<Transform>();
            Debug.Log(destTr);

            nvAgent.destination = destTr.position;
            animator.SetBool("IsTrace", true);
        }*/
        


            Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);  //전후좌우 이동 백터계산 
        //->최대 1인값 두개를 더해서 변수에 넣은후 백터 연산을 하면1.414이므로 방향 성분만 이용하려고 .nomalized속성을 이용한다

        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self); //이동방형 * 속도*dleta,기준좌표계

        //tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));//vec3.up축을 기준으로 rotSpeed만큼의 속도로 회전
                                                                                     //Rorate(회전할 기준좌표축 * deltatime*회전속도*변위 입력값)

      /*  //키보드 입력값 기준으로 동작할 애니메이션 결정
         if(v >= 0.1f)
         {
             _animation.CrossFade(anim.walk.name, 0.3f);
         }
         else if (v <= -0.1f)
         {
             _animation.CrossFade(anim.walk.name, 0.3f);
         }
         else if (h >= 0.1f)
         {
             _animation.CrossFade(anim.walk.name, 0.3f);
         }
         else if (h <= -0.1f)
         {
             _animation.CrossFade(anim.walk.name, 0.3f);
         }
         else
         {
             _animation.CrossFade(anim.idle.name, 0.3f);
         }*/

    }
    [PunRPC]
    public void GameStart()
    {
            //마우스를 누를 경우 NAV를 따라서 출발한다
            nvAgent.destination = destTr.position;
            animator.SetBool("IsTrace", true);        
    }

    void LastGameStart()
    {
        //마우스를 누를 경우 NAV를 따라서 출발한다
        nvAgent.destination = destTr.position;
        

    }

    //Trigger가 있는 물체에 부딪힐떄 작동하는 함수
    IEnumerator OnTriggerEnter(Collider coll)
    {
        //충돌 물체가 DEST일때
        if(coll.gameObject.tag == "DEST" && SceneManager.GetActiveScene().name != "scGameA_4")
        {
            
            //v3.5.1 상자 열리는 애니메이션 작동
            target.GetComponent<Animation>().Play("ChestAnim");

            particle.SetActive(true);
            //추적을 멈추기
            animator.SetBool("IsTrace", false);
            //Destroy(coll.gameObject);
            nvAgent.Stop();

            yield return new WaitForSeconds(2.5f);
            //물체과 파괴되었음을 알리는 변수
            destIsBrake = true;

        }

        if(coll.gameObject.tag=="DownCam")
        {
            Debug.Log("DownCam");
            CamPivot.transform.Translate(new Vector3(0f, -2.7f, 0.29f));
        }

        if (coll.gameObject.tag == "UPCam")
        {
            Debug.Log("DownCam");
            CamPivot.transform.Translate(new Vector3(0f, 2.7f, -0.29f));
        }

        if (coll.gameObject.tag == "FinalDest" && SceneManager.GetActiveScene().name == "scGameA_4")
        {
            Debug.Log("FinalDest");
            //Destroy(coll.gameObject);
            //네비게이션이 문 열릴때까지만 정지
            nvAgent.Stop();
            //this.transform.Rotate(0f, -49f, 0f);
            
            yield return new WaitForSeconds(1f);
            trDoor.GetComponent<Animation>().Play("doorOpen");
            
            //문열리고 네비게이션 다시 시작
            nvAgent.destination = destTr.position;

            yield return new WaitForSeconds(2f);
            trFinal.SetActive(true);

            yield return new WaitForSeconds(1f);
            destIsBrake = true;

            //doorAnimator.SetTrigger("openTrigger");


        }

    }
    


}
