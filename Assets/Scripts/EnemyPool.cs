using UnityEngine;
using System.Collections.Generic;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemyPrefabs;
    [SerializeField] private int _initialSize;
    private List<Enemy> _pool = new();

    void Start()
    {
        for (int i = 0; i < _initialSize; i++)
        {
            Enemy prefab = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)];
            Enemy enemy = Instantiate(prefab, transform);
            enemy.Initialize(this);
            enemy.gameObject.SetActive(false);
            _pool.Add(enemy);
        }
    }

    public Enemy GetEnemy()
    {
        foreach (Enemy enemy in _pool)
        {
            if (!enemy.gameObject.activeSelf)
                return enemy;
        }
        Enemy prefab = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)];
        Enemy newEnemy = Instantiate(prefab, transform);
        newEnemy.Initialize(this);
        newEnemy.gameObject.SetActive(false);
        _pool.Add(newEnemy);
        return newEnemy;
    }

    public void ReturnEnemy(Enemy enemy)
    {
        enemy.Reset();
        enemy.gameObject.SetActive(false);
    }
}
