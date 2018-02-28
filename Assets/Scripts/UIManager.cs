using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	/**
	* Attributes
	*/
	[SerializeField] private Text scoreText;
	[SerializeField] private GameObject panelInfo;

	/**
	* Monobehavior methods
	*/
	private void Start()
	{
		panelInfo.SetActive(false);
	}

	void Update () {
		scoreText.text = "Waves " + WaveSpawner.WaveIndex.ToString();
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
