using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private float _spawnPeriod;
    [SerializeField] private bool _maySpawn = true;

    private int _nextSpawnPointIndex = 0;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (_maySpawn)
        {
            _spawnPoints[_nextSpawnPointIndex].Spawn();
            _nextSpawnPointIndex++;
            if (_nextSpawnPointIndex >= _spawnPoints.Length)
            {
                _nextSpawnPointIndex = 0;
            }

            yield return new WaitForSeconds(_spawnPeriod);
        }
    }
}
