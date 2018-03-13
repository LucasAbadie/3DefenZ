using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	/**
	* Attributes
	*/
	[HideInInspector] public static PlayerStats instance = null;
	private LevelManager levelManager;

	private int currency = 0;
	private float lives = 0;

	/**
	* Accessors
	*/
	public int Currency
	{
		get
		{
			return currency;
		}

		set
		{
			if (currency <= 0)
				currency = 0;
			else
				currency = value;
		}
	}
	public float Lives
	{
		get
		{
			return lives;
		}

		set
		{
			if(value <= 0 && levelManager.stateGame != (int)LevelManager.StateGame.Lose)
			{
				levelManager.stateGame = (int)LevelManager.StateGame.Lose;
				Debug.Log("Game Over !");
				return;
			}

			lives = value;
		}
	}

	/**
	* Monobehavior methods
	*/
	void Awake()
	{

		// Check if there is another gamemanager if it is, I self-destruct
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;
		}

		instance = this;

		DontDestroyOnLoad(this.gameObject);
	}

	private void Start()
	{
		levelManager = LevelManager.instance;

		currency = levelManager.StartCurrency;
		Lives = levelManager.StartLives;
	}
}
