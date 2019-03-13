using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
	[Serializable]
	public class RoundsLayout
	{
		[Serializable]
		public class rowData
		{
			[SerializeField] private float timeBetweensWaves = 10f;
			[SerializeField] private WavesLayout[] waves = new WavesLayout[0];

			public WavesLayout[] Waves
			{
				get
				{
					return waves;
				}
			}

			public float TimeBetweensWaves
			{
				get
				{
					return timeBetweensWaves;
				}
			}
		}

		[SerializeField] private rowData[] rounds = new rowData[0];

		public rowData[] Rounds
		{
			get
			{
				return rounds;
			}
		}
	}

	[Serializable]
	public class WavesLayout
	{
		[SerializeField] private WavesSpawner[] spawner;

		public WavesSpawner[] Spawner
		{
			get
			{
				return spawner;
			}
		}

		[Serializable]
		public class WavesSpawner
		{
			[SerializeField] private Transform spawnPoint;
			[SerializeField] private Transform[] enemiesPrefab;

			public Transform SpawnPoint
			{
				get
				{
					return spawnPoint;
				}
			}

			public Transform[] EnemiesPrefab
			{
				get
				{
					return enemiesPrefab;
				}
			}
		}
	}
}