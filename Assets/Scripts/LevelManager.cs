using UnityEngine;

public class LevelManager : MonoBehaviour {

	/**
	* Attributes
	*/
	[HideInInspector] public static LevelManager instance = null;

	[Header("Attributes")]
	[HideInInspector] private int currency = 0;
	[SerializeField] private int startCurrency = 0;


	/**
	* Accessors
	*/
	public int Currency
	{
		get
		{
			return currency;
		}

		set
		{
			Debug.Log(currency);
			if (currency > 0)
				currency = value;
			else
				currency = 0;
		}
	}

	/**
	* Monobehavior methods
	*/
	void Awake()
	{

		// Check if there is another gamemanager if it is, I self-destruct
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;
		}

		instance = this;
	}

	private void Start()
	{
		currency = startCurrency;
	}
}
