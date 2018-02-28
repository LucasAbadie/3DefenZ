using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private Transform target;
	[SerializeField] private float speed = 70f;
	[SerializeField] private GameObject impactEffect;

	public void Seek(Transform _target)
	{
		this.target = _target;
	}

	// Update is called once per frame
	void Update () {
		if(target == null)
		{
			Destroy(this.gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame)
		{
			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
	}

	void HitTarget()
	{
		GameObject effectInst = Instantiate(impactEffect, transform.position, transform.rotation).gameObject;
		Destroy(effectInst, 2f);

		Destroy(this.gameObject);
	}
}
