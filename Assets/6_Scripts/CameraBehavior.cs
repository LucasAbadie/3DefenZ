using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

	/**
	* Attributes
	*/

	[SerializeField, Range(0,100)] private float scrollSpeed = 30f;
	[SerializeField] private float screenBorderThickness = 10f;

	[SerializeField, Range(0,100)] private float zoomSpeed = 30f;
	[SerializeField] private float zoomMinY = 10f;
	[SerializeField] private float zoomMaxY = 80f;

	private Vector3 startPos;

	private bool doMove = true;

	/**
	* Accessors
	*/

	/**
	* Monobehavior methods
	*/
	private void Start()
	{
		startPos = Camera.main.transform.position;
	}

	void Update () {

		if (Input.GetKeyDown(KeyCode.Space))
			doMove = !doMove;

		if (!doMove)
			return;

		if(Camera.main.transform.position.z <= startPos.z + (Camera.main.fieldOfView / 6 + (startPos.y - Camera.main.transform.position.y)) && (Input.GetKey(KeyCode.Z) || Input.mousePosition.y >= Screen.height - screenBorderThickness))
		{
			transform.Translate(Vector3.forward * scrollSpeed * Time.deltaTime, Space.World);
		}

		if (Camera.main.transform.position.z >= startPos.z - (Camera.main.fieldOfView / 6 + (startPos.y - Camera.main.transform.position.y)) && (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= screenBorderThickness))
		{
			transform.Translate(Vector3.back * scrollSpeed * Time.deltaTime, Space.World);
		}

		if (Camera.main.transform.position.x <= startPos.x + (Camera.main.fieldOfView / 6 + (startPos.y - Camera.main.transform.position.y)) && (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - screenBorderThickness))
		{
			transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime, Space.World);
		}

		if (Camera.main.transform.position.x >= startPos.x - (Camera.main.fieldOfView / 6 + (startPos.y - Camera.main.transform.position.y)) && (Input.GetKey(KeyCode.Q) || Input.mousePosition.x <= screenBorderThickness))
		{
			transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime, Space.World);
		}

		float zoom = Input.GetAxis("Mouse ScrollWheel");
		Vector3 pos = transform.position;

		pos.y -= zoom * 250 * zoomSpeed * Time.deltaTime;
		pos.y = Mathf.Clamp(pos.y, zoomMinY, zoomMaxY);

		transform.position = pos;
	}
}
