using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

	/**
	* Attributes
	*/
	private Node target;
	private GameObject ui;

	/**
	* Accessors
	*/

	/**
	* Monobehavior methods
	*/
	private void Start()
	{
		ui = transform.GetChild(0).gameObject;
		ui.SetActive(false);
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
	}



}
