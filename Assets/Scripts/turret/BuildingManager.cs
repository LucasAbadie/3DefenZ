using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

	/**
	* Attributes
	*/
	[HideInInspector] public static BuildingManager instance = null;

	public GameObject standarTurretPrefab;
	public GameObject pannelTurretPrefab;
	public GameObject missilTurretPrefab;

	private GameObject turretToBuild;

	public GameObject TurretToBuild
	{
		get
		{
			return turretToBuild;
		}

		set
		{
			turretToBuild = value;
		}
	}

	/**
	* Monobehavior methods
	*/
	// Use this for initialization
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
}
