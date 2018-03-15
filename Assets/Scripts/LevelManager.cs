using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	/**
	* Attributes
	*/
	[HideInInspector] public static LevelManager instance = null;

	[Header("Attributes")]
	[SerializeField] private int startCurrency = 0;
	[SerializeField] private int startLives = 0;
	[SerializeField] private int maxLevelForWin = 0;
	private int rounds = 0;

	[HideInInspector] public int stateGame;
	public enum StateGame { Win, Lose, Pause, InGame };

	/**
	* Accessors
	*/
	public int StartCurrency
	{
		get
		{
			return startCurrency;
		}
	}
	public int StartLives
	{
		get
		{
			return startLives;
		}
	}

	public int Rounds
	{
		get
		{
			return rounds;
		}

		set
		{
			rounds = value;

			if (rounds >= maxLevelForWin && stateGame != (int)StateGame.Win)
			{
				stateGame = (int)StateGame.Win;
				UIManager.instance.DisplayPanelEndMenu();
				return;
			}
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
		Time.timeScale = 1;

		stateGame = (int)StateGame.InGame;
		Rounds = 0;
	}

	/**
	* Personal methods
	*/
	public static void RestartCurrentScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public static void LoadNextLevel()
	{
		int index = (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)? 0 : SceneManager.GetActiveScene().buildIndex + 1;

		SceneManager.LoadScene(index);
	}

	public static void BackToMenu()
	{
		SceneManager.LoadScene(0);
	}
}
