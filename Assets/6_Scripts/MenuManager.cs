using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	/**
	* Attributes
	*/
	[HideInInspector] public static MenuManager instance = null;

	[Header("Attributes")]
	[SerializeField] private GameObject panelMainMenu;
	[SerializeField] private GameObject panelSelectLevel;

	/**
	* Accessors
	*/

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

		panelMainMenu.SetActive(true);
		panelSelectLevel.SetActive(false);
	}

	/**
	* Personal methods
	*/
	public void DisplayMainMenu()
	{
		panelMainMenu.SetActive(true);
		panelSelectLevel.SetActive(false);
	}

	public void DisplaySelectLevel()
	{
		panelMainMenu.SetActive(false);
		panelSelectLevel.SetActive(true);
	}

	public void LoadLevel(int indexLevel)
	{
		SceneManager.LoadScene(indexLevel);
	}

	public void QuitApplication()
	{
		#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
		#else
						Application.Quit ();
		#endif
	}
}
