using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSphere : MonoBehaviour {

	public LineRenderer link;
	public GameObject player1, player2;

	[HideInInspector]
	public int currentOld = 0;
	[HideInInspector]
	public int current = 1;

	public float speed;
	public float damage;

	float startTime;

	float waypointRadius = 0.5f;
	[HideInInspector]
	public bool toP1 = false;

	public Material cannotBeHit;
	public Material canBeHit;

	[HideInInspector]
	public bool swap;

    // Use this for initialization
    void Start () {
        transform.position = player1.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3[] positions = new Vector3[link.positionCount];
        link.GetPositions(positions);

		if (swap)
		{
			int tmp = current;
			current = currentOld;
			currentOld = tmp;
			toP1 = !toP1;
			swap = false;
		}

        if (Vector3.Distance(positions[current], transform.position) < waypointRadius)
        {
            if (toP1 == false)
            {
				currentOld = current;
                current++;
				startTime = Time.time;
                if (current >= positions.Length-1)
                    swap = true;
            }
            if (toP1 == true)
			{
				currentOld = current;
				current--;
				startTime = Time.time;
				if (current <= 0)
					swap = true;
            }

        }

		float lerpTime = 1/speed * Vector3.Distance(positions[currentOld], positions[current]);
		float progress = (Time.time - startTime) / lerpTime;
        transform.position = Vector3.Lerp(positions[currentOld], positions[current], progress);

    }


	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Wall"))
			swap = true;
		else if (other.CompareTag("Player"))
		{
			swap = true;
			other.GetComponentInChildren<BallHitter>().canHitBall = false;
			GetComponent<MeshRenderer>().material = cannotBeHit;
			GameManager.instance.playersHealth -= damage / 4;
		}
		else if (other.CompareTag("Enemy"))
			other.GetComponent<EnemyBehavior>().health -= damage;
	}
}
