using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class MajorABMgr : MonoBehaviour
{
    //각 사용자가 어떤 것을 선택했는지 저장하는 변수
    private string myButton;

    //A,B의 카운트 변수
    public int countA;
    public int countB;

    //라운드 변수
    private static int round = 0;

    //다수결에 의한 항목 변수
    public static int win1;   //1라운드
    public static int win2;   //2라운드
    public static int win3;   //3라운드

    //filled타입의 항목 연결
    public Slider timeSlider;
    public Animator animator;

    //button항목
    public Button aButton;
    public Button bButton;

    //A,B의 카운트 변수
    private GameMgr gm;
    //private int countA;
    //private int countB;

    //photonview위한 변수
    private PhotonView pv;

    //승리한 이미지 출력을 위한 변수
    public GameObject[] ImageView1;
    public GameObject[] ImageView2;

    //다수결 %출력을 위한 변수 선언
    public Text percentTextA;
    public Text percentTextB;
    private int percentA;
    private int percentB;


    public float ztot;  // 경과 시 간 변 수 

	private bool noinf ; // 무한 루프 방 지 변 수 

    //remainTime()함수내에서 13~13.1중 한번만 실행되도록 하는 변수
    private bool isActive = false;

    public Animator aniumator;
    //public Animation anim;

    //popup창을 위한 변수
    public GameObject popUp;

    void Start()
    {
        //씬이 새로 부를때마다 초기화
        myButton = "C";

        pv = GetComponent<PhotonView>();
        //gm = GetComponent<GameMgr>();
		noinf = true; // 10 초 후 image 를 array에 ADD 하 기 위 한 무 한 루 프 방 지 변 수 

        //라운드 증가
        round++;

        //라운드 별로 이미지 선택하기
        if (SceneManager.GetActiveScene().name == "MajorScreen")
            SelectImg();

        animator.SetInteger("AnsewrA1B2", 0);
    }



    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MajorScreen")
            ScrennRemianTime();

        if (SceneManager.GetActiveScene().name == "MajorMobile")
            remianTime();
    }

    void remianTime()
    {
        float ztot = Time.timeSinceLevelLoad;
        timeSlider.value = (ztot);
    }

    void ScrennRemianTime()
    {
        
        float ztot = Time.timeSinceLevelLoad;
        timeSlider.value = (ztot);
		//Debug.Log ("ztot Value : " + ztot);
		if(timeSlider.value == 10 && noinf)  // 막대바가 다 찼을 때 
        {
            //animator.SetTrigger("Play");
        }
		if (ztot >= 13 &&ztot<13.1f && isActive==false) 
		{
            isActive = true;

            Debug.Log("Acount : " + countA);
            Debug.Log("Bcount : " + countB);

            Debug.Log("@@라운드 : " + round);
            //1라운드
            if (round==1)
            {
               
                if (countA > countB)
                {
                    win1 = 0;
                    ImageView2[1].SetActive(false);
                    percentTextA.text = countA + " 명";
                    percentTextB.text = countB + " 명";
                    animator.SetInteger("AnsewrA1B2", 1);
                }

                else if (countB > countA)
                {
                    win1 = 1;
                    ImageView1[0].SetActive(false);
                    percentTextA.text = countA + " 명";
                    percentTextB.text = countB + " 명";
                    animator.SetInteger("AnsewrA1B2", 2);
                }
                else if (countA == countB)
                {
                    win1 = 0;
                    ImageView2[1].SetActive(false);
                    percentTextA.text = countA + " 명";
                    percentTextB.text = countB + " 명";
                    animator.SetInteger("AnsewrA1B2", 1);
                }

                

                //승리 변수 출력
                Debug.Log("****이긴거 : " + win1);
                //Screen에서 모바일에 이긴 이미지와 라운드 정보를 보내줘서, 자신이 누른 버튼이 이겼는지 확인하고 점수 부여 및 DB에 전송
                pv.RPC("SetScore", PhotonTargets.Others, win1, round);
            }
            //2라운드
            else if(round==2)
            {
                
                if (countA > countB)
                {
                    win2 = 2;
                    ImageView2[3].SetActive(false);
                    percentTextA.text = countA + " 명";
                    percentTextB.text = countB + " 명";
                    animator.SetInteger("AnsewrA1B2", 1);
                }
                else if (countB > countA)
                {
                    win2 = 3;
                    ImageView1[2].SetActive(false);
                    percentTextA.text = countA + " 명";
                    percentTextB.text = countB + " 명";
                    animator.SetInteger("AnsewrA1B2", 2);
               }
                else if (countA == countB)
                {
                    win2 = 2;
                    ImageView2[3].SetActive(false);
                    percentTextA.text = countA + " 명";
                    percentTextB.text = countB + " 명";
                    animator.SetInteger("AnsewrA1B2", 1);
                }


                //승리 변수 출력
                Debug.Log("****이긴거 : " + win2);
                pv.RPC("SetScore", PhotonTargets.Others, win2, round);
            }
            //3라운드
            else if(round==3)
            {
                if (countA > countB)
                {
                    win3 = win1;
                    ImageView2[win2].SetActive(false);
                    percentTextA.text = countA + " 명";
                    percentTextB.text = countB + " 명";
                    animator.SetInteger("AnsewrA1B2", 1);
                }
                else if (countB > countA)
                {
                    win3 = win2;
                    ImageView1[win1].SetActive(false);
                    percentTextA.text = countA + " 명";
                    percentTextB.text = countB + " 명";
                    animator.SetInteger("AnsewrA1B2", 2);
                }

                else if (countA == countB)
                {
                    win3 = win1;
                    ImageView2[win2].SetActive(false);
                    percentTextA.text = countA + " 명";
                    percentTextB.text = countB + " 명";
                    animator.SetInteger("AnsewrA1B2", 1);
                }


                //승리 변수 출력
                Debug.Log("****이긴거 : " + win3);
                pv.RPC("SetScore", PhotonTargets.Others, win3, round);
                                
            }
        }

        if (ztot >= 15)
        {
            if (round<3)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); //씬 불러오기
                pv.RPC("LoadMobileSc", PhotonTargets.Others);
            }
                

            if (round == 3)
            {
                pv.RPC("LoadIntro",PhotonTargets.Others);
                SceneManager.LoadScene("scGameA_4"); //씬 불러오기
            }
                
        }  
    }

    //A버튼 클릭 함수
    public void OnAbuttonClicked()
    {
        if (aButton)
        {
            Debug.Log("Abutton");
            myButton = "A";
            pv.RPC("CountA", PhotonTargets.MasterClient);
            //버튼 멈추기
            aButton.interactable = false;
            bButton.interactable = false;

            //popup창 호출
            popUp.SetActive(true);

        }
    }
    //B버튼 클릭 함수
    public void OnBbuttonClicked()
    {
        if (bButton)
        {
            Debug.Log("Bbutton");
            myButton = "B";
            pv.RPC("CountB", PhotonTargets.MasterClient);
            //버튼 멈추기
            aButton.interactable = false;
            bButton.interactable = false;

            //popup창 호출
            popUp.SetActive(true);
        }
    }

    //모바일 부르는 RPC함수
    [PunRPC]
    void LoadMobileSc()
    {
        SceneManager.LoadScene("MajorMobile");
    }

    [PunRPC]
    void LoadIntro()
    {
        SceneManager.LoadScene("scIntro");
    }

    //A의 카운트 증가 함수
    [PunRPC]
    void CountA()
    {
        countA++;
        Debug.Log("A Counted!!");
    }

    //B의 카운트 증가 함수
    [PunRPC]
    void CountB()
    {
        countB++;
        Debug.Log("B Counted!!");
    }
    
    //이긴 이미지와 사용자의 버튼을 판단해서 점수 부여하는 RPC함수 - 스크린에서 파라미터로  이건 이미지의 인덱스와 , 라운드를 알려줘서 모바일에서 각자의 점수 부여
    [PunRPC]
    void SetScore(int winImg, int Sround)
    {
        //int score;
        if(Sround==1)
        {
            //게임넘버 설정
            int GNum = 7;
            //이긴쪽 이미지가 0이고 누른 버튼이 A일때 점수 저장
            if(winImg == 0 && myButton == "A")
            {
                /*score = 10;
                Debug.Log("1라운드 score : " + score);*/                          //userId : 방번호          //게임넘버             //name:학번     //answer저장
                StartCoroutine(NumberingScoreSaveMgr.instance.SaveScore(PhotonNetwork.player.userId, GNum, PhotonNetwork.player.name, 1));
             }
            //이긴쪽 이미지가 1이고 누른 버튼이 B일때 점수 저장
            else if (winImg == 1 && myButton == "B")
            {
                /*score = 10;
                Debug.Log("1라운드  score : " + score);*/
                StartCoroutine(NumberingScoreSaveMgr.instance.SaveScore(PhotonNetwork.player.userId, GNum, PhotonNetwork.player.name, 1));
            }
            else
            {//아무것도 누르지 않거나, 탈락한 것을 선택할 경우 점수 0저장
               /* score = 0;
                Debug.Log("1라운드  score : " + score);*/
                StartCoroutine(NumberingScoreSaveMgr.instance.SaveScore(PhotonNetwork.player.userId, GNum, PhotonNetwork.player.name, 0));
            }
        }

        if (Sround == 2)
        {
            int GNum = 8;
            if (winImg == 2 && myButton == "A")
            {
                /*score = 10;
                Debug.Log("2라운드 score : " + score);  */                        //userId : 방번호          //게임넘버             //name:학번     //answer저장
                StartCoroutine(NumberingScoreSaveMgr.instance.SaveScore(PhotonNetwork.player.userId, GNum, PhotonNetwork.player.name, 1));
            }
            else if (winImg == 3 && myButton == "B")
            {
               /* score = 10;
                Debug.Log("2라운드  score : " + score);*/
                StartCoroutine(NumberingScoreSaveMgr.instance.SaveScore(PhotonNetwork.player.userId, GNum, PhotonNetwork.player.name, 1));
            }
            else
            {
               /* score = 0;
                Debug.Log("2라운드  score : " + score);*/
                StartCoroutine(NumberingScoreSaveMgr.instance.SaveScore(PhotonNetwork.player.userId, GNum, PhotonNetwork.player.name, 0));
            }
        }

        if (Sround == 3)
        {
            int GNum = 9;
            if ((winImg == 0 || winImg==1) && myButton == "A")
            {
                /*score = 10;
                Debug.Log("3라운드 score : " + score);    */                      //userId : 방번호          //게임넘버             //name:학번      //answer저장
                StartCoroutine(NumberingScoreSaveMgr.instance.SaveScore(PhotonNetwork.player.userId, GNum, PhotonNetwork.player.name, 1));
            }
            else if ((winImg == 2 || winImg == 3) && myButton == "B")
            {
               /* score = 10;
                Debug.Log("3라운드  score : " + score);*/
                StartCoroutine(NumberingScoreSaveMgr.instance.SaveScore(PhotonNetwork.player.userId, GNum, PhotonNetwork.player.name, 1));
            }
            else
            {
               /* score = 0;
                Debug.Log("3라운드  score : " + score);*/
                StartCoroutine(NumberingScoreSaveMgr.instance.SaveScore(PhotonNetwork.player.userId, GNum, PhotonNetwork.player.name, 0));
            }
        }

    }

    //이미지 동적 활성화 함수
    void SelectImg()
    {
        if(round==1)
        {
            
            ImageView1[0].SetActive(true);
            ImageView2[1].SetActive(true);
        }

        if (round == 2)
        {
            
            ImageView1[2].SetActive(true);
            ImageView2[3].SetActive(true);
        }
        if (round == 3)
        {
           
            ImageView1[win1].SetActive(true);
            ImageView2[win2].SetActive(true);
            
        }

    }

}