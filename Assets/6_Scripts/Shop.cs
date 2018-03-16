using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

	BuildingManager buildManager;
	LevelManager levelManager;
	PlayerStats playerStats;

	public TurretBlueprint standarTurret;
	public TurretBlueprint panelTurret;
	public TurretBlueprint missileTurret;
	public TurretBlueprint laserBeamer;

	private void Start()
	{
		buildManager = BuildingManager.instance;

		levelManager = LevelManager.instance;
		playerStats = PlayerStats.instance;
	}


	public void PurchaseStandarTurret()
	{
		Purchase(standarTurret);
	}

	public void PurchasePanelTurret()
	{
		Purchase(panelTurret);
	}

	public void PurchaseMissileTurret()
	{
		Purchase(missileTurret);
	}

	public void PurchaseLaserBeamer()
	{
		Purchase(laserBeamer);
	}

	private void Purchase(TurretBlueprint turret)
	{
		if (playerStats.Currency >= turret.Cost && buildManager.TurretToBuild == null)
		{
			playerStats.Currency -= turret.Cost;
			buildManager.TurretToBuild = turret;
		}
		else
		{
			GameManager.instance.logsManager.Log_info("Shop", "Purchase", turret.prefab.gameObject.name + " cost is upper than currency");
		}
	}
}
