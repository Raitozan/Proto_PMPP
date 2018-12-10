using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldOfView : MonoBehaviour
{

    /*VARIABLES POUR CHAMP DE VISION ET ATTAQUE*/
    //distance de vue
    public float viewRadius;
    //angle de vue
    [Range(0, 360)]
    public float viewAngle;

    //les layers qui permettent de savoir si on voit un obstacle ou un joueur
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    //liste des cibles que l'ennemi voit
    public List<Transform> visibleTargets = new List<Transform>();


    void Start()
    {
        //debut de la co routine toute les 0.2 seconde
        StartCoroutine("FindTargetWithDelay", .2f);
    }


    //la fonction FindTargetWithDelay est appelée avec une coroutine
    IEnumerator FindTargetWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTarget();
        }
    }

    /*FONCTIONS VISION ENNEMIE*/
    //Unity utilise une reference speciale pour les angles
    //cette fonction prend un angle en entrée et sort les directions de cet angle
    public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    void FindVisibleTarget()
    {
        visibleTargets.Clear();
        //tableau des targets. on récupere les collider des GO dans la sphereOverlap
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            //vecteur normalisé de la directio vers le joueur
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            //si l'angle entre le vecteur forward de l'ennemi et le vecteur en direction du joueur est compris dans l'angle de vue alors l'ennemi voit le joueur
            //V3.forward est le vecteur droit devant
            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                //distance entre l'ennemi et le joueur
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                //s'il n'y a pas d'obstacle entre le joueur et l'ennemi et s'il le joueur est a distance de vue de l'ennemi
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }


}
