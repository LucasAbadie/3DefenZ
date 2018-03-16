using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

	/**
	* Attributes
	*/
	[HideInInspector] public static BuildingManager instance = null;

	private PlayerStats playerStats;

	[Header("Unity Setup fields")]
	[SerializeField] private GameObject buildEffect;
	[SerializeField] private GameObject nodeUIPrefab;

	private TurretBlueprint turretToBuild;
	private Node selectedNode;
	private NodeUI nodeUI;

	/**
	* Accessors
	*/
	public TurretBlueprint TurretToBuild
	{
		get
		{
			return turretToBuild;
		}

		set
		{
			turretToBuild = value;
			DeselectNode();
		}
	}

	public GameObject BuildEffect
	{
		get
		{
			return buildEffect;
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
		playerStats = PlayerStats.instance;

		// Create NodeUI into the scene and get it
		GameObject nodeUIClone = Instantiate(nodeUIPrefab);
		nodeUI = nodeUIClone.GetComponent<NodeUI>();
	}

	/**
	* Personal methods
	*/
	public void BuildTurretOnNode(Node node)
	{
		if (node.Turret.gameObject != null)
		{
			GameManager.instance.logsManager.Log_warn("BuildingManager", "BuildTurretOnNode", "turret in " + node.gameObject.name + " is not null");

			SelectedNode(node);

			return;
		}

		if (instance.TurretToBuild == null)
		{
			GameManager.instance.logsManager.Log_warn("BuildingManager", "BuildTurretOnNode", "turretToBuild into the BuildingManager is null");
			return;
		}

		node.Turret = new TurretBlueprint(instance.TurretToBuild);

		GameObject turretBuild = instance.TurretToBuild.prefab;
		turretBuild = Instantiate(turretBuild, node.BuildPos, node.transform.rotation);

		node.Turret.gameObject = turretBuild;
		node.Turret.stats_Behavior = node.Turret.gameObject.GetComponent<TurretBehavior>();

		GameObject effect = instance.BuildEffect;
		effect = Instantiate(effect, node.Turret.gameObject.transform);
		Destroy(effect, 1f);

		instance.TurretToBuild = null;
	}

	public void UpgradeTurret(Node node)
	{
		if (playerStats.Currency < node.Turret.UpgradeCost || !node.Turret.CanUpgrade)
		{
			GameManager.instance.logsManager.Log_info("BuildingManager", "UpgradeTurret", node.Turret.prefab.gameObject.name + "doesn't upgrade (cost is upper than currency or Max upgrade)");
			return;
		}

		GameObject effect = instance.BuildEffect;
		effect = Instantiate(effect, node.Turret.gameObject.transform);
		Destroy(effect, 1f);

		playerStats.Currency -= node.Turret.UpgradeCost;
		node.Turret.AllUpdate();
		node.Turret.stats_Behavior.UpdateSelf();
	}

	public void SellTurret(Node node)
	{

	}

	private void SelectedNode (Node node)
	{
		if(selectedNode == node)
		{
			DeselectNode();
			return;
		}

		selectedNode = node;
		nodeUI.SetTarget(selectedNode);
	}

	private void DeselectNode()
	{
		selectedNode = null;
		nodeUI.SetTarget();
	}
}
