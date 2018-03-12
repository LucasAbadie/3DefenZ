using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

	BuildingManager buildManager;
	LevelManager levelManager;

	public TurretBlueprint standarTurret;
	public TurretBlueprint panelTurret;
	public TurretBlueprint missileTurret;
	public TurretBlueprint laserBeamer;

	private void Start()
	{
		buildManager = BuildingManager.instance;
		levelManager = LevelManager.instance;
	}


	public void PurchaseStandarTurret()
	{
		buildManager.TurretToBuild = standarTurret.prefab;
		Purchase(standarTurret);
	}

	public void PurchasePanelTurret()
	{
		buildManager.TurretToBuild = panelTurret.prefab;
		Purchase(panelTurret);
	}

	public void PurchaseMissileTurret()
	{
		buildManager.TurretToBuild = missileTurret.prefab;
		Purchase(missileTurret);
	}

	public void PurchaseLaserBeamer()
	{
		buildManager.TurretToBuild = laserBeamer.prefab;
		Purchase(laserBeamer);
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
