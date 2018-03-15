using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private Enemy target;
	[SerializeField] private float speed = 70f;
	[SerializeField, Range(0, 50)] private float explosionRadius = 0f;
	[SerializeField] private GameObject impactEffect;

	public void Seek(GameObject _target)
	{
		this.target = _target.GetComponent<Enemy>();
	}

	// Update is called once per frame
	void Update () {
		if(target == null)
		{
			Destroy(this.gameObject);
			return;
		}

		Vector3 dir = target.transform.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame)
		{
			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
		transform.LookAt(target.transform);
	}

	void HitTarget()
	{
		GameObject effectInst = Instantiate(impactEffect, transform.position, transform.rotation).gameObject;
		Destroy(effectInst, 2f);

		if(explosionRadius > 0f)
		{
			Explode();
		}
		else
		{
			Damage(target.transform);
		}

		Destroy(this.gameObject);
	}

	private void Explode()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach(Collider coll in colliders)
		{
			if(coll.tag == GameManager.instance.EnemyTag)
			{
				Damage(coll.transform);
			}
		}
	}

	private void Damage(Transform targetEnemy)
	{
		targetEnemy.GetComponent<Enemy>().Health -= transform.parent.GetComponent<TurretBehavior>().FireDamage;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}
}
