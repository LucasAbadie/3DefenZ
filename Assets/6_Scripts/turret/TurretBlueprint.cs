using UnityEngine;

[System.Serializable]
public class TurretBlueprint {

	public GameObject prefab;
	[HideInInspector] public GameObject gameObject;
	[HideInInspector] public TurretBehavior stats_Behavior;
	[SerializeField] private int cost = 1;
	[SerializeField, Range(2, 10)] private int maxUpgrade;
	private float coeffScaleUpgrade = 0.1f;
	private int cptUpgrade = 0;
	private bool canUpgrade = true;

	private int upgradeCost = 0;
	private int sellCost = 0;

	public int Cost
	{
		get
		{
			return cost;
		}
	}
	public int UpgradeCost
	{
		get
		{
			return upgradeCost;
		}
	}
	public int SellCost
	{
		get
		{
			return sellCost;
		}
	}
	public bool CanUpgrade
	{
		get
		{
			return canUpgrade;
		}
	}

	public int MaxUpgrade
	{
		get
		{
			return maxUpgrade;
		}
	}

	public TurretBlueprint()
	{
		prefab = null;
		gameObject = null;
		IncrementUpgradeSellCost();
	}

	public TurretBlueprint(TurretBlueprint turret)
	{
		prefab = turret.prefab;
		gameObject = null;
		cost = turret.Cost;
		maxUpgrade = turret.MaxUpgrade;
		cptUpgrade = 0;
		canUpgrade = true;

		IncrementUpgradeSellCost();
	}

	public void AllUpdate()
	{
		if (cptUpgrade > maxUpgrade)
		{
			canUpgrade = false;
			return;
		}

		gameObject.transform.localScale += new Vector3(coeffScaleUpgrade, coeffScaleUpgrade, coeffScaleUpgrade);
		IncrementUpgradeSellCost();

		cptUpgrade++;
	}

	private void IncrementUpgradeSellCost()
	{
		if(upgradeCost <= cost)
			upgradeCost = cost * 2;
		else
			upgradeCost *= 2;

		sellCost = upgradeCost / 3;
	}
}
