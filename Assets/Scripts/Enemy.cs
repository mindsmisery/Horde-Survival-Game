using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] AudioClip _deathSound;
    [SerializeField] SpriteRenderer _enemyBody;
    [SerializeField] Animator _animator;
    EntityHealth _entityHealth;
    EnemyPool _pool;
    UnityEngine.AI.NavMeshAgent _agent;
    GameObject _target;

    public void Initialize(EnemyPool pool)
    {
        _pool = pool;
    }
    void Awake()
    {
        _entityHealth = GetComponent<EntityHealth>();
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _agent.updateRotation = false;
    }

    void OnEnable()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        _entityHealth.OnDeath += HandleEnemyDeath;
    }

    void Update()
    {
        _agent.SetDestination(_target.transform.position);
    }

    void OnDisable()
    {
        _entityHealth.OnDeath -= HandleEnemyDeath;
    }

    void HandleEnemyDeath()
    {
        AudioManager.Instance.PlayAudio(_deathSound, AudioManager.SoundType.SFX, 1.0f, false);
            if (_pool == null)
            {
            Debug.LogError("Enemy has no pool assigned!");
            Destroy(gameObject); // fallback
            return;
            }
        _pool.ReturnEnemy(this);
    }

    public void Reset()
    {
        _entityHealth.ResetHealth();
        _agent.ResetPath();
        _agent.velocity = Vector3.zero;
    }
}
