using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	/**
	* Attributes
	*/
	[Header("Unity Setup fields")]
	[SerializeField] private GameObject deathEffect;

	[Header("Attributes")]
	[SerializeField] private float healthMax = 0;
	private float health = 0;
	private Image healthBar;

	[SerializeField] [Range(0, 30)] private float initSpeed = 10f;
	private float currentSpeed = 0;

	[SerializeField] private int dropCurrency = 0;

	[Header("Path")]
	private Transform target;
	private int wayPointIndex = 0;

	private PlayerStats playerStats;

	/**
	* Accessors
	*/
	public float Health
	{
		get
		{
			return health;
		}

		set
		{
			if(value <= 0)
			{
				Kill();
				return;
			}

			health = value;
			healthBar.fillAmount = ToolMath.GetPercent(health, 0, healthMax, 0, 1);
		}
	}

	public float InitSpeed
	{
		get
		{
			return initSpeed;
		}
	}

	public float CurrentSpeed
	{
		get
		{
			return currentSpeed;
		}

		set
		{
			currentSpeed = value;
		}
	}

	/**
	* Monobehavior methods
	*/
	private void Start()
	{
		playerStats = PlayerStats.instance;

		target = WayPoints.points[0];
		healthBar = transform.parent.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
		Health = healthMax;
		currentSpeed = initSpeed;
	}

	private void Update()
	{
		Vector3 dir = target.position - transform.position;
		transform.parent.Translate(dir.normalized * currentSpeed * Time.deltaTime, Space.World);
		transform.LookAt(target);

		if (Vector3.Distance(transform.position, target.position) <= 0.2)
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
			EndPath();
		}
		else
		{
			wayPointIndex++;
			target = WayPoints.points[wayPointIndex];
		}
	}

	private void EndPath()
	{
		playerStats.Lives--;
		Destroy(this.gameObject);
	}

	public void Kill()
	{
		PlayerStats.instance.Currency += dropCurrency;

		GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(effect, 2.5f);
		Destroy(transform.parent.gameObject);
	}
}
