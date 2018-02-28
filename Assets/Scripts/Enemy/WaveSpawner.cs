using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

	[Header("Unity Setup fields")]
	public Transform enemyPrefab;
	public Transform spawnPoint;

	[Header("Attributes")]
	public float timeBetweensWaves = 5f;
	public float countdown = 2f;

	[SerializeField] private static int waveIndex = 0;

	public static int WaveIndex
	{
		get
		{
			return waveIndex;
		}
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
		waveIndex++;

		for (int i = 0; i < WaveIndex; i++)
		{
			SpawnEnemy();
			yield return new WaitForSeconds(0.5f);
		}
	}

	void SpawnEnemy()
	{
		Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
	}
}
