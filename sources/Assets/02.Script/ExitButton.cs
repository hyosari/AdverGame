using UnityEngine;
using System.Collections;

public class ExitButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        EndKey();
    }

    //프로그램을 버튼으로 종료하게 하는 함수
    //PC에서 esc, 안드로이드에서 back
    void EndKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
