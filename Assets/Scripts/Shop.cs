using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

	BuildingManager buildManager;
	LevelManager levelManager;

	private void Start()
	{
		buildManager = BuildingManager.instance;
		levelManager = LevelManager.instance;
	}


	public void PurchaseStandarTurret()
	{
		buildManager.TurretToBuild = buildManager.standarTurret.prefab;
		Purchase(buildManager.standarTurret);
	}

	public void PurchasePanelTurret()
	{
		buildManager.TurretToBuild = buildManager.panelTurret.prefab;
		Purchase(buildManager.panelTurret);
	}

	public void PurchaseMissileTurret()
	{
		buildManager.TurretToBuild = buildManager.missileTurret.prefab;
		Purchase(buildManager.missileTurret);
	}

	private void Purchase(TurretBlueprint turret)
	{
		if (levelManager.Currency >= turret.cost)
		{
			levelManager.Currency -= turret.cost;
			//Debug.Log("(Descrease) New currency : " + LevelManager.instance.Currency);
		}
		else
		{
			GameManager.instance.logsManager.Log_info("Shop", "Purchase", turret.prefab.gameObject.name + " cost is upper than currency");
		}
	}
}
