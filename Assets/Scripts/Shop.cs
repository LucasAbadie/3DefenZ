using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

	BuildingManager buildManager;

	private void Start()
	{
		buildManager = BuildingManager.instance;
	}


	public void PurchaseStandarTurret()
	{
		buildManager.TurretToBuild = buildManager.standarTurretPrefab;
	}

	public void PurchasePannelTurret()
	{
		buildManager.TurretToBuild = buildManager.pannelTurretPrefab;
	}

	public void PurchaseMissilTurret()
	{
		buildManager.TurretToBuild = buildManager.missilTurretPrefab;
	}
}
