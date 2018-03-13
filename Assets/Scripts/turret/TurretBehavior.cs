using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour {

	/**
	* Attributes
	*/
	[Header("Unity Setup fields")]
	[SerializeField] private Transform target;
	private Transform pivot;
	[SerializeField] private Transform bulletPrefab;
	private Transform firePoint;

	private bool isUseLaser;
	private LineRenderer lineRenderer;
	private ParticleSystem impactEffect;
	private Light impactLight;

	[Header("Attributes")]
	[SerializeField, Range(5, 40)] private float range = 15f;
	[SerializeField, Range(0, 20)] private float rotSpeed = 10f;
	[Space(15)]
	[SerializeField, Range(0, 5)] private float fireRate = 1f;
	[SerializeField] private float fireDamage = 1f;
	private float fireCD = 0f;

	/**
	* Accessors
	*/
	public float FireDamage
	{
		get
		{
			return fireDamage;
		}
	}


	/**
	* Monobehavior methods
	*/
	// Use this for initialization
	void Start () {
		InvokeRepeating("UpdateTarget", 0, 0.5f);

		pivot = transform.GetChild(0);
		firePoint = transform.GetChild(0).GetChild(0);

		if (bulletPrefab == null)
		{
			isUseLaser = true;

			lineRenderer = transform.GetComponent<LineRenderer>();
			impactEffect = transform.GetChild(transform.childCount - 1).GetComponent<ParticleSystem>();
			impactLight = impactEffect.transform.GetChild(0).GetChild(0).GetComponent<Light>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null)
		{
			if (isUseLaser)
			{
				if (lineRenderer.enabled)
				{
					lineRenderer.enabled = false;
					impactEffect.Stop();
					impactLight.enabled = false;
				}
					
			}

			return;
		}

		// Rotation turret
		LockOnTarget();

		// Shoot
		if (isUseLaser)
		{
			Laser();
		}
		else
		{
			if (fireCD <= 0f)
			{
				Shoot();
				fireCD = 1 / fireRate;
			}

			fireCD -= Time.deltaTime;
		}		
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}


	/**
	* Personal methods
	*/
	void LockOnTarget()
	{
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(pivot.rotation, lookRotation, Time.deltaTime * rotSpeed).eulerAngles;

		pivot.rotation = Quaternion.Euler(0f, rotation.y, 0f);
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
		GameObject bulletDir = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation, transform).gameObject;
		Bullet bullet = bulletDir.GetComponent<Bullet>();

		if (bullet != null)
		{
			bullet.Seek(target.gameObject);
		}
	}

	void Laser()
	{
		if (!lineRenderer.enabled)
		{
			lineRenderer.enabled = true;
			impactEffect.Play();
			impactLight.enabled = true;
		}

		lineRenderer.SetPosition(0, firePoint.position);
		lineRenderer.SetPosition(1, target.position);

		// Set pos & rot impacte effect particle
		Vector3 dir = firePoint.position - target.position;
		impactEffect.transform.position = target.position + dir.normalized;
		impactEffect.transform.rotation = Quaternion.LookRotation(dir);
	}
}
