using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHitter : MonoBehaviour {

	public bool canHitBall;
	public MovementSphere ms;
	
	// Update is called once per frame
	void Update () {
		if (canHitBall)
			HitBall();
	}

	public void HitBall()
	{
		if (GetComponentInParent<Player>().player1 && Input.GetKeyDown(KeyCode.Space) || !GetComponentInParent<Player>().player1 && Input.GetKeyDown(KeyCode.Keypad0))
		{
			if (!ms.swap)
			{
				ms.swap = true;
				ms.GetComponent<MeshRenderer>().material = ms.cannotBeHit;
				canHitBall = false;
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Ball"))
		{
			ms.GetComponent<MeshRenderer>().material = ms.canBeHit;
			canHitBall = true;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Ball"))
		{
			ms.GetComponent<MeshRenderer>().material = ms.cannotBeHit;
			canHitBall = false;
		}
	}
}
