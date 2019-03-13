using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
	public class WaveSpawner : MonoBehaviour
	{

		private LevelManager levelManager;

		[Header("Unity Setup fields")]
		public RoundsLayout dataRW;

		[Header("Attributes")]
		[SerializeField]
		private float timeBetweensRounds = 20f;
		private float countdown = 2f;

		private bool bCanStartNewRound = false;
		private bool bIsRoundPlaying = false;
		private RoundsLayout.rowData currentRound;
		private int waveIndex = 0;
		private List<GameObject> enemies;

		public float Countdown
		{
			get
			{
				return countdown;
			}
		}

		public int WaveIndex
		{
			get
			{
				return waveIndex;
			}
		}

		public bool isRoundPlaying
		{
			get
			{
				return bIsRoundPlaying;
			}
		}

		public List<GameObject> Enemies
		{
			get
			{
				return enemies;
			}
		}

		private void Start()
		{
			this.levelManager = LevelManager.instance;

			this.currentRound = new RoundsLayout.rowData();
			this.enemies = new List<GameObject>();
		}

		private void Update()
		{
			if (bCanStartNewRound)
			{
				if (countdown <= 0f)
				{
					bCanStartNewRound = false;
					levelManager.CurrentRoundsIndex++;

					currentRound = dataRW.Rounds[levelManager.CurrentRoundsIndex - 1];

					StartCoroutine(SpawnWave());
					countdown = timeBetweensRounds;
				}

				countdown -= Time.deltaTime;
			}
		}

		private void FixedUpdate()
		{
			if (!bCanStartNewRound
				&& !bIsRoundPlaying
				&& levelManager.CurrentRoundsIndex < dataRW.Rounds.Length)
			{
				bCanStartNewRound = true;
				bIsRoundPlaying = true;
			}
		}

		IEnumerator SpawnWave()
		{
			waveIndex = 0;
			float countdownWaves = 0;

			do
			{
				if (enemies.Count <= 0)
				{


					if (countdownWaves <= 0f)
					{
						print("Waves : " + waveIndex);

						for (int spawnerIndex = 0; spawnerIndex < currentRound.Waves[waveIndex].Spawner.Length; spawnerIndex++)
						{
							StartCoroutine(SpawnEnemies(spawnerIndex));
							yield return new WaitForSeconds(0.1f);
						}

						waveIndex++;
						countdownWaves = currentRound.TimeBetweensWaves;
					}

					countdownWaves -= Time.deltaTime;
				}

				yield return null;

			} while (waveIndex < currentRound.Waves.Length || enemies.Count > 0);

			bIsRoundPlaying = false;

			yield return 0;
		}

		IEnumerator SpawnEnemies(int spawnerIndex)
		{
			Quaternion rot = new Quaternion();
			Transform spawner = currentRound.Waves[waveIndex].Spawner[spawnerIndex].SpawnPoint;

			foreach (Transform enemy in currentRound.Waves[waveIndex].Spawner[spawnerIndex].EnemiesPrefab)
			{
				rot = Quaternion.Euler(spawner.rotation.x, 0f, spawner.rotation.z);

				Transform transformNewEnemy = Instantiate(enemy, spawner.position, rot);
				enemies.Add(transformNewEnemy.gameObject);

				yield return new WaitForSeconds(0.5f);
			}
		}
	}
}
