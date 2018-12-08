using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeformMovement : MonoBehaviour {

	public Transform player1, player2;
	public Transform minDeform, maxDeform;
	public Transform deformPoint1, deformPoint2;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 p1 = player1.localPosition;
		p1.y = 1;
		Vector3 p2 = player2.localPosition;
		p2.y = 1;
		transform.position = p1 + (p2 - p1) / 2;
		transform.LookAt(p1);

		deformPoint1.position = Vector3.Lerp(minDeform.position, maxDeform.position, (Input.GetAxis("DeformPlayer1") + 1) / 2);
		deformPoint2.position = Vector3.Lerp(minDeform.position, maxDeform.position, (Input.GetAxis("DeformPlayer2") + 1) / 2);
	}
}
