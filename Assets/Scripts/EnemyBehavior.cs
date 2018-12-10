using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{

	public float maxHealth;
	[HideInInspector]
	public float health;
	public Canvas healthBar;
	public Transform camera;
	public Image bar;

    /* VARIABLES POUR PATROUILLE*/
    //liste des points de patrouille
    [SerializeField]
    List<Transform> destinations;

    //si on veut que l'ennemi fasse des pauses dans sa patrouille
    [SerializeField]
    bool waitPatrol;

    NavMeshAgent _navMesh;
    //index du point de patrouille
    int currentPatrolPoint;
    //en train de bouger ou non
    bool traveling;
    //en train d'attendre ou non
    bool waiting;

    //temps d'attente d'une pause
    [SerializeField]
    float waitTime = 3f;
    //timer d'attente pour mesurer une pause
    float waitTimer;

    /* VARIABLES POUR L'ATTAQUE*/
    [SerializeField]
    float chaseTime = 5f;
    float chaseTimer;
    bool chasing;

    //pour récupere le fieldOfView de l'ennemi
    EnemyFieldOfView enemyVision;


    // Use this for initialization
    void Start()
    {
		health = maxHealth;
        //reference au champ de vision de l'ennemi pour acceder au tableau qui contient les joueurs visibles.
        enemyVision = gameObject.GetComponent<EnemyFieldOfView>();

        //reference au navMesh du GO.
        _navMesh = this.GetComponent<NavMeshAgent>();

        if (_navMesh == null)
        {
            Debug.Log("Aucun navMesh attaché à l'objet : " + gameObject.name);
        }
        else
        {
            if (destinations != null && destinations.Count >= 2)
            {
                SetDestination();
            }
            else
            {
                Debug.Log("Pas asser de points de patrouille");
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
		UpdateHealthBar();

        //si on a vu au moins un ennemi ou non
        if (enemyVision.visibleTargets.Count == 0)
        {
            Patroling();
        }
        else if (enemyVision.visibleTargets.Count > 0)
        {
            chasing = true;
            Chasing();
        }

		if (health <= 0.0f)
			Destroy(gameObject);
    }

    //fonction de chasse
    void Chasing()
    {
        if (chasing == true)
        {
            //chasse le premier ennemi en vue
			if(enemyVision.visibleTargets.Count > 1)
			{
				if (Vector3.Distance(transform.position, enemyVision.visibleTargets[0].position) <= Vector3.Distance(transform.position, enemyVision.visibleTargets[1].position))
					_navMesh.SetDestination(enemyVision.visibleTargets[0].position);
				else
					_navMesh.SetDestination(enemyVision.visibleTargets[1].position);
			}
			else
				_navMesh.SetDestination(enemyVision.visibleTargets[0].position);
			chaseTimer += Time.deltaTime;
            if (chaseTimer >= chaseTime)
            {
                chaseTimer = 0f;
                chasing = false;
            }
        }
    }

    //fonction de patrouille
    void Patroling()
    {
        if (traveling && _navMesh.remainingDistance <= 1.0)
        {
            traveling = false;
            if (waitPatrol)
            {
                waiting = true;
                waitTimer = 0f;
            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }
        }

        if (waiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                waiting = false;
                ChangePatrolPoint();
                SetDestination();
            }
        }
    }


    /*FONCTIONS PATROUILLE*/
    //defini la destination à prendre pour l'ennemi
    void SetDestination()
    {
        if (destinations != null)
        {
            Vector3 target = destinations[currentPatrolPoint].position;
            _navMesh.SetDestination(target);
            traveling = true;
        }
    }
    //defini le prochain point de patrouille
    void ChangePatrolPoint()
    {
        currentPatrolPoint++;
        if (currentPatrolPoint >= destinations.Count)
        {
            currentPatrolPoint = 0;
        }
    }

	void UpdateHealthBar()
	{
		healthBar.transform.LookAt(camera.position);
		bar.fillAmount = health / maxHealth;
	}
}
