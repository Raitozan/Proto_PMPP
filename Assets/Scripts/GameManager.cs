using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public float maxplayersHealth;
	[HideInInspector]
	public float playersHealth;

	public Image barL, barR;

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		playersHealth = maxplayersHealth;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(playersHealth <= 0.0f)
			SceneManager.LoadScene(0);

		barL.fillAmount = playersHealth / maxplayersHealth;
		barR.fillAmount = playersHealth / maxplayersHealth;
	}
}
