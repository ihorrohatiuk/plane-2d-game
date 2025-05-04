using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private GameObject _enemyTorpedo;
    [SerializeField] private float _maxX;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxY;
    [SerializeField] private float _minY;
    [SerializeField] private float _timeBetweenSpawn;
    private float _spawnTime;

    [SerializeField] private GameObject _piratesShipPrefab;
    private GameObject _currentPiratesShip;
    private PiratesShipBehaviour _piratesShipBehaviour;
    [SerializeField] private Transform _gameManagerParent;

    private ScoreManager _scoreManager;

    [SerializeField] private GameObject _piratesTorpedo;

    private void Start()
    {
        _scoreManager = GameObject.Find("Score").GetComponent<ScoreManager>();
        if (_scoreManager is null) Debug.Log("SCORE MANAGER IS NULL!");
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("PiratesShip") is null)
        {
            if (Time.time > _spawnTime)
            {
                SpawnTorpedo();
                _spawnTime = Time.time + _timeBetweenSpawn;
            }

            if ((int)_scoreManager.Score % 30 == 0 && (int)_scoreManager.Score != 0)
            {
                Debug.Log("PIRATES SHIP IS COMING!");
                SpawnPiratesShip();
            }
        }

        else if (_piratesShipBehaviour is not null && !_piratesShipBehaviour.IsMovingFromEndScreen)
        {
            if (Time.time > _spawnTime)
            {
                SpawnPiratesTorpedo();
                _spawnTime = Time.time + _timeBetweenSpawn;
            }
        }
    }

    private void SpawnTorpedo()
    {
        float randomX = Random.Range(_minX, _maxX);
        float randomY = Random.Range(_minY, _maxY);

        Instantiate(_enemyTorpedo, transform.position + new Vector3(randomX, randomY, 0), transform.rotation);
    }

    private void SpawnPiratesShip()
    {
        _currentPiratesShip = Instantiate(_piratesShipPrefab, transform.position + new Vector3(-10, 0, 0), Quaternion.identity, _gameManagerParent);
        _piratesShipBehaviour = _currentPiratesShip.GetComponent<PiratesShipBehaviour>();
    }

    private void SpawnPiratesTorpedo()
    {
        Vector3 spawnPosition = _currentPiratesShip.transform.position + new Vector3(-5, 0, 0);
        Instantiate(_piratesTorpedo, spawnPosition, Quaternion.identity);
    }
}
