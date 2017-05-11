using UnityEngine;
using System.Collections;

public class HintManagement : MonoBehaviour //********************사용하지 않음*********************************/
{
	public string message = "";
	
	private GameObject player;
	private bool used = false;

	private ControlsMessage manager;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		manager = this.transform.parent.GetComponent<ControlsMessage> ();
	}

	void OnTriggerEnter(Collider other)     //플레이어에 다른 GameObj의 collider가 충돌하고 사용되지 않은 힌트라면 힌트 보이기
	{
		if((other.gameObject == player) && !used)
		{
			manager.setShowMsg(true);
			manager.setMessage(message);
			used = true;
		}
	}

	void OnTriggerExit(Collider other)    ////플레이어에 다른 Gobj의 collider가 탈출시 힌트 보이기
    {
		if(other.gameObject == player)
		{
			manager.setShowMsg(false);
			Destroy(gameObject);
		}
	}
}
