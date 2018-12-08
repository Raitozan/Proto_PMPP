using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSphere : MonoBehaviour {

    public LineRenderer Lien;
    public GameObject pOrigine, pFin;
    int current = 0;
    float rotationSpeed;
    public float speed;
    float WPradius = 1;
    bool versP1 = false;

    // Use this for initialization
    void Start () {
        transform.position = pOrigine.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3[] positions = new Vector3[Lien.positionCount];
        Lien.GetPositions(positions);

        if (Vector3.Distance(positions[current], transform.position) < WPradius)
        {
            if (versP1 == false)
            {
                current++;
                if (current >= positions.Length)
                {
                    versP1 = true;
                }
            }
            if (versP1 == true)
            {
                current--;
                if (current <= 0)
                {
                    versP1 = false;
                }
            }

        }
        transform.position = Vector3.MoveTowards(transform.position, positions[current], Time.deltaTime * speed);

    }
}
