using UnityEngine;
using System.Collections;

public class Master : Photon.MonoBehaviour {    /*****************사용하지 않음*******************/

/*


    //목적지 설정과 네비게이션 에이전트 변수 설정
    private Transform destTr;
    private NavMeshAgent nvAgent;
    private Animator animator;



    private Anim anim;   //inspetor뷰에 노출될 에니메이션 클래스 변수 선언
    private Animation _animation;    //아래에 있는 3D모델의 Animation컴포넌트에 접근하기 위한 변수

    //목적지 파괴 변수
    private bool destIsBrake = false;


    //tr변수는 update함수에서 계속 이용하는 것이기 떄문에 start나 awake함수에서 미리 설정해서 매번 실행되는 과부하를 막는다
    void Start()
    {

        //목적지 설정과 네비게이션 에이전트 컴포넌트 받기
        destTr = GameObject.FindWithTag("DEST").GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        animator = this.gameObject.GetComponent<Animator>();

        animator.SetBool("IsTrace", false);

    }

    // Update is called once per frame
    void Update()
    {


    }
    [PunRPC]
    public void GameStart()
    {
        //자동이동
        //if (Input.GetMouseButtonDown(1))
        {

            //마우스를 누를 경우 NAV를 따라서 출발한다
            nvAgent.destination = destTr.position;
            animator.SetBool("IsTrace", true);

        }
    }

    */
}


