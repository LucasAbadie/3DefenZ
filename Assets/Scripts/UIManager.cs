using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	/**
	* Attributes
	*/
	private LevelManager levelManager;
	private PlayerStats playerStats;

	[SerializeField] private Text textLives;
	[SerializeField] private Text textCurrency;
	[SerializeField] private GameObject panelInfo;

	/**
	* Monobehavior methods
	*/
	private void Start()
	{
		panelInfo.SetActive(false);

		playerStats = PlayerStats.instance;
		levelManager = LevelManager.instance;
	}

	void Update () {
		textLives.text = playerStats.Lives + " LIVES";
		textCurrency.text = playerStats.Currency + " $";
	}

	/**
	* Personal methods
	*/
	public void DisplayPanelInfo()
	{
		Time.timeScale = (Time.timeScale != 0) ? 0 : 1;
		panelInfo.SetActive(!panelInfo.activeSelf);
	}
}
