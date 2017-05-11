using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MajorScreenTimeScroll : MonoBehaviour
{
   
    //filled타입의 항목 연결
    public Slider timeSlider;
    public Animator animator;

	public MajorQuestion currentQuestion;

	public SourceImage[] imagePaths;
	private static List<SourceImage> unanseredimagePaths;

	public SourceImage winImage; // 다수결 해당 라 운 드 에서 이 긴 이 미 지 

	public float ztot;  // 경과 시 간 변 수 


	[SerializeField]
	private Text questionText;   // Question Text ( only one display)

	[SerializeField]
	private float timeBetweenQuestions = 10.0f;

	[SerializeField]
	private Image imageA;
	[SerializeField]
	private Image imageB;

	[SerializeField]
	private Text resultA;
	[SerializeField]
	private Text resultB;

	private bool noinf ; // 무한 루프 방 지 변 수 


    void Start()
    {
		if (unanseredimagePaths == null || unanseredimagePaths.Count == 0)   // 문제에 나타나지 않은 source image path  저장하는 array
		{
			unanseredimagePaths = imagePaths.ToList<SourceImage> ();
		}

		resultA.text = "0";
		resultB.text = "0";

		SetCurrentQuestion ();
		SetCurrentImages ();
		noinf = true; // 10 초 후 image 를 array에 ADD 하 기 위 한 무 한 루 프 방 지 변 수 
    }

	void SetCurrentQuestion()
	{
		questionText.text = currentQuestion.question;
	}

	void SetCurrentImages()
	{
		if (unanseredimagePaths.Count == 1) // 최후의 답 !! 
		{
			Debug.Log(" 마지막 남은 답은 " + unanseredimagePaths[0].sourcePath +" 입니다! ");
			return;

		} else 
		{
			int randomImageIndexA = Random.Range (0, unanseredimagePaths.Count);
			Debug.Log ("first Count: " + unanseredimagePaths.Count + " randomImageIndexA: " + randomImageIndexA);
			Sprite image1 = Resources.Load<Sprite> (unanseredimagePaths [randomImageIndexA].sourcePath);  // Random 하게 array에서 불러옴 
			Debug.Log ("image1 source Path : " + unanseredimagePaths [randomImageIndexA].sourcePath);
			unanseredimagePaths.Remove (unanseredimagePaths [randomImageIndexA]);// array 에서 삭제 
			int randomImageIndexB = Random.Range (0, unanseredimagePaths.Count);
			Debug.Log ("second Count: " + unanseredimagePaths.Count + " randomImageIndexB: " + randomImageIndexB);
			Sprite image2 = Resources.Load<Sprite> (unanseredimagePaths [randomImageIndexB].sourcePath);
			Debug.Log ("image2 source Path : " + unanseredimagePaths [randomImageIndexB].sourcePath);
			unanseredimagePaths.Remove (unanseredimagePaths [randomImageIndexB]);

			Debug.Log ("image1 : " + image1 + " image2 : " + image2);

			imageA.sprite = image1;
			imageB.sprite = image2;
		}
	}


    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MajorMain")
            remianTime();
    }

    void remianTime()
    {
        float ztot = Time.timeSinceLevelLoad;
        timeSlider.value = (ztot);
		Debug.Log ("ztot Value : " + ztot);
		if(timeSlider.value == 10 && noinf)  // 막대바가 다 찼을 때 
        {
			
			DisplayResult();
            animator.SetTrigger("Play");
            //StartCoroutine(MajorResult.instance.GetScoreList("A"));
           // StartCoroutine(MajorResult.instance.GetScoreList("B"));
        }
		if (ztot >= 20) 
		{
            
                SceneManager.LoadScene("Ranking");
            
            /*Debug.Log ("ztot 20 in ");
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex); // 씐 다 시 불 러 오 기 */
		}

    }
		

	void DisplayResult()
	{
		// 다수결 결과 percetage로 출력 하는 함 수 
		int a,b;
		Image winImage;
		string path;

		StartCoroutine (MajorResult.instance.GetMajorScore ());

		//다 수 결 결 과 값 받 아 오 기 
		a = MajorResult.instance.GetMajorOptScore("A");
		b = MajorResult.instance.GetMajorOptScore ("B");


		resultA.text = a.ToString()+" %";
		resultB.text = b.ToString()+" %";

		if (a > b) {
			winImage = imageA;
			animator.SetTrigger("PlayA");
		} else 
		{
			winImage = imageB;
			animator.SetTrigger("PlayB");
		}

        //Debug.Log (AssetDatabase.GetAssetPath (winImage.sprite));

#if UNITY_EDITOR
        path = AssetDatabase.GetAssetPath (winImage.sprite).Replace ("Assets/Resources1/", "");
        path = path.Replace (".jpg", "");

        ImageAddtoList(path);
#endif

    }

    void ImageAddtoList(string path)
    {
		winImage.sourcePath = path;
		Debug.Log ("winImage path: " + path);
		unanseredimagePaths.Add(winImage);

	}
}