using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

	private LevelManager levelManager;

	[Header("Unity Setup fields")]
	public Transform enemyPrefab;
	public Transform spawnPoint;

	[Header("Attributes")]
	public float timeBetweensWaves = 5f;
	public float countdown = 2f;

	[SerializeField] private int waveIndex = 0;

	public int WaveIndex
	{
		get
		{
			return waveIndex;
		}
	}

	private void Start()
	{
		levelManager = LevelManager.instance;
		waveIndex = 0;
	}

	private void Update()
	{
		if(countdown <= 0f)
		{
			StartCoroutine(SpawnWave());
			countdown = timeBetweensWaves;
		}

		countdown -= Time.deltaTime;
	}

	IEnumerator SpawnWave()
	{
		levelManager.Rounds = waveIndex++;

		for (int i = 0; i < WaveIndex; i++)
		{
			SpawnEnemy();
			yield return new WaitForSeconds(0.5f);
		}
	}

	void SpawnEnemy()
	{
		Quaternion rot = Quaternion.Euler(spawnPoint.rotation.x, 0f, spawnPoint.rotation.z);
		Instantiate(enemyPrefab, spawnPoint.position, rot);
	}
}
