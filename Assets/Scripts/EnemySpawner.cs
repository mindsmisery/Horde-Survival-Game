using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _spawnCooldown;
    [SerializeField] private float _spawnCooldownReductionMultiplier;

    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private Tilemap _groundTiles;
    [SerializeField] private Transform player;
    private List<Vector3> _spawnPositions = new();
    private float _nextSpawnTime;

    void Start()
    {
        SetEnemySpawnPositions();
        InvokeRepeating(nameof(HandleGameDifficultyIncrease), 1f, 1f);
    }

    void Update()
    {
        HandleEnemySpawning();
    }

    void HandleGameDifficultyIncrease()
    {
        _spawnCooldown *= _spawnCooldownReductionMultiplier;
    }

    Vector3 GetRandomPosition()
    {
        Vector3 position = Vector3.zero;
        do
        {
            position = _spawnPositions[Random.Range(0, _spawnPositions.Count)];
        } while (Vector3.Distance(position, player.position) < 10f);
        return position;
    }

    private void HandleEnemySpawning()
    {
        if (Time.time < _nextSpawnTime)
            return;
        Enemy enemy = _enemyPool.GetEnemy();
        if (enemy == null)
            return;
        enemy.transform.position = GetRandomPosition();
        enemy.gameObject.SetActive(true);
        _nextSpawnTime = Time.time + _spawnCooldown;
    }

    void SetEnemySpawnPositions()
    {
        foreach (Vector3Int position in _groundTiles.cellBounds.allPositionsWithin)
        {
            if (_groundTiles.HasTile(position))
            {
                _spawnPositions.Add(_groundTiles.GetCellCenterWorld(position));
            }
        }
    }
}