using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

	/**
	* Attributes
	*/
	private Node target;

	[SerializeField] private GameObject ui;
	[SerializeField] private Button buttonUpgrade;
	[SerializeField] private Text buttonUpgradeText;
	[SerializeField] private Button buttonSell;
	[SerializeField] private Text buttonSellText;

	/**
	* Accessors
	*/

	/**
	* Monobehavior methods
	*/
	private void Start()
	{
		// Set Button Listeners
		buttonUpgrade.onClick.AddListener(
			delegate {
				BuildingManager.instance.UpgradeTurret(target);
				SetTarget(target);
			});
		buttonSell.onClick.AddListener(
			delegate {
				BuildingManager.instance.SellTurret(target);
				SetTarget();
			});
	}

	/**
	* Personal methods
	*/
	public void SetTarget(Node node = null)
	{
		if(node == null)
		{
			if (ui.activeSelf)
				ui.SetActive(false);

			return;
		}

		if (!ui.activeSelf)
			ui.SetActive(true);

		target = node;
		transform.position = target.BuildPos;

		string upgradeText = "";

		if (target.Turret.CanUpgrade)
			upgradeText = "$" + target.Turret.UpgradeCost;
		else
			upgradeText = "MAX";

		buttonUpgradeText.text = upgradeText;
		buttonSellText.text = "$" + target.Turret.SellCost; ;
	}



}
