using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyFieldOfView))]
public class FieldOfViewEditor : Editor
{
    //pour la visualisation de la vision de l'ennemi
    void OnSceneGUI()
    {
        //reference au Composant qui gere la vision de l'ennemi
        EnemyFieldOfView fieldOfView = (EnemyFieldOfView)target;
        //couleur du tracage
        Handles.color = Color.white;
        //trace le cercle qui représente la distance max du champ de vision de l'ennemi
        Handles.DrawWireArc(fieldOfView.transform.position, Vector3.up, Vector3.forward, 360, fieldOfView.viewRadius);
        //extrémité gauche de l'angle de vue
        Vector3 viewAngleA = fieldOfView.DirectionFromAngle(-fieldOfView.viewAngle / 2, false);
        //éxtrémité droite de l'angle de vue
        Vector3 viewAngleB = fieldOfView.DirectionFromAngle(fieldOfView.viewAngle / 2, false);
        Handles.DrawLine(fieldOfView.transform.position, fieldOfView.transform.position + viewAngleA * fieldOfView.viewRadius);
        Handles.DrawLine(fieldOfView.transform.position, fieldOfView.transform.position + viewAngleB * fieldOfView.viewRadius);

        //trace le raycast vers les joueurs visible par l'ennemi en rouge
        Handles.color = Color.red;
        foreach (Transform visibleTarget in fieldOfView.visibleTargets)
        {
            Handles.DrawLine(fieldOfView.transform.position, visibleTarget.position);
        }
    }
}
