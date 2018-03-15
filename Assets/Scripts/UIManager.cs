using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class UIManager : MonoBehaviour {

	/**
	* Attributes
	*/
	[HideInInspector] public static UIManager instance = null;

	private LevelManager levelManager;
	private PlayerStats playerStats;

	[Header("Panel")]
	[SerializeField] private GameObject panelEndMenu;
	[SerializeField] private GameObject panelGameOver;
	[SerializeField] private GameObject panelWinMenu;

	[Header("Main")]
	[SerializeField] private Text textLives;
	[SerializeField] private Text textCurrency;

	[Header("Info")]
	[SerializeField] private GameObject panelInfo;

	[Header("GameOver")]
	[SerializeField] private Text textGameOverRounds;
	[SerializeField] private Button buttonGameOverRetry;
	[SerializeField] private Button buttonGameOverBackToMenu;

	[Header("Win")]
	[SerializeField] private Text textWinRounds;
	[SerializeField] private Button buttonWinContinue;
	[SerializeField] private Button buttonWinBackToMenu;
	private Animator animEndMenu;

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
		panelInfo.SetActive(false);
		panelEndMenu.SetActive(false);
		panelGameOver.SetActive(false);
		panelWinMenu.SetActive(false);

		// Get all UI Animations
		animEndMenu = panelEndMenu.GetComponent<Animator>();

		//Set Listener on all buttons
		buttonGameOverRetry.onClick.AddListener(delegate { LevelManager.RestartCurrentScene(); });
		buttonGameOverBackToMenu.onClick.AddListener(delegate { LevelManager.BackToMenu(); });
		buttonWinContinue.onClick.AddListener(delegate { LevelManager.LoadNextLevel(); });
		buttonWinBackToMenu.onClick.AddListener(delegate { LevelManager.BackToMenu(); });

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

	public void DisplayPanelEndMenu()
	{
		Time.timeScale = 0;
		panelEndMenu.SetActive(true);

		switch(levelManager.stateGame)
		{
			case (int)LevelManager.StateGame.Lose:
				panelGameOver.SetActive(true);
				animEndMenu.SetBool("isLose", true);

				textGameOverRounds.text = levelManager.Rounds.ToString();
				break;
			case (int)LevelManager.StateGame.Win:
				panelWinMenu.SetActive(true);
				animEndMenu.SetBool("isWin", true);

				StartCoroutine(AnimateTextWinRounds());
				break;
		}
	}

	IEnumerator AnimateTextWinRounds()
	{
		textWinRounds.text = "0";
		int rounds = 0;

		yield return new WaitForSecondsRealtime(animEndMenu.GetCurrentAnimatorClipInfo(0).Length + 0.7f);

		while (rounds < levelManager.Rounds)
		{
			rounds++;
			textWinRounds.text = rounds.ToString();

			yield return new WaitForSecondsRealtime(.03f);
		}
	}
}
