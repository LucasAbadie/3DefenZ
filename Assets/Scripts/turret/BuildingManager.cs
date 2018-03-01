using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

	/**
	* Attributes
	*/
	[HideInInspector] public static BuildingManager instance = null;

	public TurretBlueprint standarTurret;
	public TurretBlueprint pannelTurret;
	public TurretBlueprint missileTurret;

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
