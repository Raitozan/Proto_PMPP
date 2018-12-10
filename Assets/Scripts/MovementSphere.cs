using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSphere : MonoBehaviour {

    public LineRenderer link;
    public GameObject player1, player2;

	int currentOld = 0;
	int current = 1;

    public float speed;
	public float damage;

	float startTime;

    float waypointRadius = 0.5f;
    bool toP1 = false;

    // Use this for initialization
    void Start () {
        transform.position = player1.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3[] positions = new Vector3[link.positionCount];
        link.GetPositions(positions);

        if (Vector3.Distance(positions[current], transform.position) < waypointRadius)
        {
            if (toP1 == false)
            {
				currentOld = current;
                current++;
				startTime = Time.time;
                if (current >= positions.Length-1)
                {
                    toP1 = true;
                }
            }
            if (toP1 == true)
			{
				currentOld = current;
				current--;
				startTime = Time.time;
				if (current <= 0)
                {
                    toP1 = false;
                }
            }

        }

		float lerpTime = 1/speed * Vector3.Distance(positions[currentOld], positions[current]);
		float progress = (Time.time - startTime) / lerpTime;
        transform.position = Vector3.Lerp(positions[currentOld], positions[current], progress);

    }


	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Wall"))
			toP1 = !toP1;
		else if (other.CompareTag("Player"))
			GameManager.instance.playersHealth -= damage/4;
		else if (other.CompareTag("Enemy"))
			other.GetComponent<EnemyBehavior>().health -= damage;
	}
}
