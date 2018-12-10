﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyAttack : MonoBehaviour {
    //pour récuperer la reference du tableau contenant les ennemis dans le champ de vision
    EnnemyFieldOfView ennemyVision;
    //les degats d'attaque de l'ennemi
    public float EnnemyDamage;
    //pour l'attaque
    public float attackCooldown = 2.0f;
    private float attackTimer = 0f;

    // Use this for initialization
    void Start () {
        //récupération de la référence
        ennemyVision = gameObject.GetComponent<EnnemyFieldOfView>();
    }
	
	// Update is called once per frame
	void Update () {
        attackTimer -= Time.deltaTime;
        //si on a des ennemis visibles
        if (ennemyVision.visibleTargets.Count > 0)
        {
            //on attaque toujours le premier ennemi du tableau
            if (Vector3.Distance(transform.position, ennemyVision.visibleTargets[0].position) < 2.0f)
            {
                if (attackTimer <= 0)
                {
                    Attack();
                }                
            }

        }
	}

    //fonction d'attaque
    void Attack()
    {
        attackTimer = attackCooldown;
        //GameManager.instance.health -= EnnemyDamage;
    }

}
