using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _travelSpeed;
    [SerializeField] private float _damage;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private ParticleSystem _hitParticles;
    [SerializeField] private AudioClip _enemyHitSound;

    public void InitializeProjectile(Vector2 direction)
    {
        Launch(direction);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain")) // If the projectile hits the terrain, this destroys it
        {
            DestroyProjectile();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            DealDamage(collision.gameObject);
            DestroyProjectile();
        }
    }

    void DealDamage(GameObject target)
    {
        if (target.TryGetComponent(out EntityHealth entityHealth))
        {
            entityHealth.LoseHealth(_damage);
            AudioManager.Instance.PlayAudio(_enemyHitSound, AudioManager.SoundType.SFX, 1.0f, false);
        }
    }

    void Launch(Vector2 direction)
    {
        Vector2 movement = direction.normalized * _travelSpeed;
        _rb.linearVelocity = movement;
    }

    void DestroyProjectile()
    {
        ParticleSystem hitParticles = Instantiate (_hitParticles, transform.position, Quaternion.identity);
        Destroy(hitParticles.gameObject, 1f);
        Destroy(gameObject);
    }
}
