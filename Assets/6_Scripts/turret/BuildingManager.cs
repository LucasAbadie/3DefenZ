using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

	/**
	* Attributes
	*/
	[HideInInspector] public static BuildingManager instance = null;

	[Header("Unity Setup fields")]
	[SerializeField] private NodeUI nodeUI;

	private GameObject turretToBuild;
	private Node selectedNode;

	/**
	* Accessors
	*/
	public GameObject TurretToBuild
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

	/**
	* Personal methods
	*/
	public void BuildTurretOnNode(Node node)
	{
		if (node.Turret != null)
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

		GameObject turretToBuild = instance.TurretToBuild;
		node.Turret = Instantiate(turretToBuild, node.BuildPos, node.transform.rotation);

		instance.TurretToBuild = null;
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
