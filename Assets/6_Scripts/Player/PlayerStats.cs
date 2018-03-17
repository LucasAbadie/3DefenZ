﻿using UnityEngine;

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
			currency = value;

			if (currency <= 0)
				currency = 0;
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
			lives = value;

			if (lives <= 0 && levelManager.stateGame != (int)LevelManager.StateGame.Lose)
			{
				levelManager.stateGame = (int)LevelManager.StateGame.Lose;
				UIManager.instance.DisplayPanelEndMenu();
				return;
			}
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
	}

	private void Start()
	{
		levelManager = LevelManager.instance;

		currency = levelManager.StartCurrency;
		Lives = levelManager.StartLives;

		print(Currency);
	}
}
