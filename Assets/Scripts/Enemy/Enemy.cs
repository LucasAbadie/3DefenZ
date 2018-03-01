using UnityEngine;

public class Enemy : MonoBehaviour {

	/**
	* Attributes
	*/
	[Header("Attributes")]
	public float speed = 10f;
	[SerializeField] private int dropCurrency = 0;

	[Header("Path")]
	private Transform target;
	private int wayPointIndex = 0;

	/**
	* Accessors
	*/

	/**
	* Monobehavior methods
	*/
	private void Start()
	{
		target = WayPoints.points[0];
	}

	private void Update()
	{
		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

		if(Vector3.Distance(transform.position, target.position) <= 0.2)
		{
			GetNextWayPoint();
		}
	}

	/**
	* Personals methods
	*/
	void GetNextWayPoint()
	{
		if (wayPointIndex >= WayPoints.points.Length - 1)
		{
			Destroy(this.gameObject);
		}
		else
		{
			wayPointIndex++;
			target = WayPoints.points[wayPointIndex];
		}
	}

	public void Kill()
	{
		LevelManager.instance.Currency += dropCurrency;
		//Debug.Log("(Increase) New currency : " + LevelManager.instance.Currency);
		Destroy(this.gameObject);
	}
}
