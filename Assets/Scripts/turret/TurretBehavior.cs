using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour {

	[Header("Unity Setup fields")]
	[SerializeField] private Transform target;
	private Transform pivot;
	[SerializeField] private Transform bulletPrefab;
	private Transform firePoint;

	[Header("Attributes")]
	[SerializeField, Range(5, 40)] private float range = 15f;
	[SerializeField, Range(0, 20)] private float rotSpeed = 10f;
	[Space(15)]
	[SerializeField] private float fireRate = 1f;
	[SerializeField, Range(0, 5)] private float fireCD = 0f;

	// Use this for initialization
	void Start () {
		InvokeRepeating("UpdateTarget", 0, 0.5f);

		pivot = transform.GetChild(0);
		firePoint = transform.GetChild(0).GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null)
			return;

		// Rotation turret
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(pivot.rotation, lookRotation, Time.deltaTime * rotSpeed).eulerAngles;
		pivot.rotation = Quaternion.Euler(0f, rotation.y, 0f);

		// Shoot
		if(fireCD <= 0f)
		{
			Shoot();
			fireCD = 1 / fireRate;
		}

		fireCD -= Time.deltaTime;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}

	void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(GameManager.instance.EnemyTag);

		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;

		foreach(GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

			if(distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if(nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
		}
		else
		{
			target = null;
		}
	}

	void Shoot()
	{
		GameObject bulletDir = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation).gameObject;
		Bullet bullet = bulletDir.GetComponent<Bullet>();

		if (bullet != null)
		{
			bullet.Seek(target.gameObject);
		}
	}
}
