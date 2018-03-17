using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

	/**
	* Attributes
	*/
	[SerializeField] private Color hoverColor;
	[SerializeField] private Vector3 turretPosOffset;

	private TurretBlueprint turret;
	private Renderer rend;
	private Color defaultColor;

	/**
	* Accessors
	*/
	public TurretBlueprint Turret
	{
		get
		{
			return turret;
		}

		set
		{
			turret = value;
		}
	}

	public Vector3 BuildPos
	{
		get
		{
			return transform.position + turretPosOffset;
		}
	}

	/**
	* Monobehavior methods
	*/
	void Start () {
		Turret = new TurretBlueprint();

		rend = GetComponent<Renderer>();
		defaultColor = rend.material.color;
	}

	private void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		BuildingManager.instance.BuildTurretOnNode(this);
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
