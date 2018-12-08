using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementP1 : MonoBehaviour {

    public GameObject pOrigine;
    public GameObject pFin;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 middle = (pOrigine.transform.position + pFin.transform.position) / 2;
        float distance = Vector3.Distance(pOrigine.transform.position, pFin.transform.position);
	}
}
