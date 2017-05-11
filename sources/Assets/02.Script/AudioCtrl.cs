using UnityEngine;
using System.Collections;

public class AudioCtrl : MonoBehaviour {

    private AudioSource AudioS;
	// Use this for initialization
	void Start () {
        if (!PhotonNetwork.isMasterClient)
        {
            AudioS = this.gameObject.GetComponent<AudioSource>();
            AudioS.Stop();
        }
            
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
