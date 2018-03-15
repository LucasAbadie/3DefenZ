using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

	/**
	* Attributes
	*/
	[SerializeField] private Color hoverColor;
	[SerializeField] private Vector3 turretPosOffset;

	private GameObject turret;
	private Renderer rend;
	private Color defaultColor;


	/**
	* Monobehavior methods
	*/
	void Start () {
		rend = GetComponent<Renderer>();
		defaultColor = rend.material.color;
	}

	private void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if(BuildingManager.instance.TurretToBuild == null)
		{
			GameManager.instance.logsManager.Log_warn(gameObject.name + " Node class", "OnMouseDown", "turretToBuild into the BuildingManager is null");
			return;
		}

		if(turret != null)
		{
			GameManager.instance.logsManager.Log_warn(gameObject.name + " Node class", "OnMouseDown", "turret in this node is not null");
			return;
		}

		GameObject turretToBuild = BuildingManager.instance.TurretToBuild;
		turret = Instantiate(turretToBuild, transform.position + turretPosOffset, transform.rotation);

		BuildingManager.instance.TurretToBuild = null;
	}

	private void OnMouseEnter()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if (!EventSystem.current.IsPointerOverGameObject())
			rend.material.color = hoverColor;
	}

	private void OnMouseExit()
	{
		rend.material.color = defaultColor;
	}
}
