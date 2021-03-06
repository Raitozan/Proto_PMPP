﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour {

    public LineRenderer lineRenderer;
    public Transform player1;
    public Transform deformP1;
    public Transform deformP2;
    public Transform player2;
    
    static private int numberPoints = 50;
    private Vector3[] positions = new Vector3[numberPoints+1];

	// Use this for initialization
	void Start () {
        //nombre de points du lien
        lineRenderer.positionCount = numberPoints;
	}
	
	// Update is called once per frame
	void Update () {
        DrawCubicCurve();
    }

    private void DrawCubicCurve()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            float t = i / (float)numberPoints;
            positions[i] = CalculateCubicBezierPoint(t, player1.position, deformP1.position, deformP2.position, player2.position);
        }
        lineRenderer.SetPositions(positions);
    }

    private Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return Mathf.Pow((1 - t), 3) * p0 + 3 * Mathf.Pow((1 - t), 2) * t * p1 + 3 * (1 - t) * t * t * p2 + Mathf.Pow(t,3) * p3;
    }

}
