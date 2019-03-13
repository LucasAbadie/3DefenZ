using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Enemy.WaveSpawner))]
public class LevelManager : MonoBehaviour {

	/**
	* Attributes
	*/
	[HideInInspector] public static LevelManager instance = null;
	private Enemy.WaveSpawner waveSpawner;

	[Header("Attributes")]
	[SerializeField] private int startCurrency = 0;
	[SerializeField] private int startLives = 0;
	private int currentRoundsIndex = 0;

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

	public int CurrentRoundsIndex
	{
		get
		{
			return currentRoundsIndex;
		}

		set
		{
			currentRoundsIndex = value;
		}
	}

	public Enemy.WaveSpawner WaveSpawner
	{
		get
		{
			return waveSpawner;
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
		waveSpawner = GetComponent<Enemy.WaveSpawner>();
	}

	private void Update()
	{
		if(currentRoundsIndex > 0)
		{
			print(currentRoundsIndex >= waveSpawner.dataRW.Rounds.Length);
			print(waveSpawner.WaveIndex >= waveSpawner.dataRW.Rounds[currentRoundsIndex - 1].Waves.Length);
			print(!waveSpawner.isRoundPlaying);
			print("---------------------------");

			if (currentRoundsIndex >= waveSpawner.dataRW.Rounds.Length
				&& waveSpawner.WaveIndex >= waveSpawner.dataRW.Rounds[currentRoundsIndex - 1].Waves.Length
				&& !waveSpawner.isRoundPlaying
				&& stateGame != (int)StateGame.Win)
			{
				stateGame = (int)StateGame.Win;
				UIManager.instance.DisplayPanelEndMenu();
				return;
			}
		}
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
