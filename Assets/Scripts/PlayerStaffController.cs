using UnityEngine;

public class PlayerStaffController : MonoBehaviour
{
    [SerializeField] private float _fireRate;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private Projectile _secondaryProjectile;
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private Transform _tip;
    private float _nextFireTime;
    private Vector2 _lookDirection;

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0f)
            return;

        SetLookDirection();
        RotateStaff();
        if (Input.GetButton("Fire1") && Time.time >= _nextFireTime)
        {
            _nextFireTime = Time.time + 1f / _fireRate;
            ShootPrimary();
        }
        if (Input.GetButton("Fire2") && Time.time >= _nextFireTime)
        {
            _nextFireTime = Time.time + 3f / _fireRate;
            ShootSecondary();
        }
    }

    void RotateStaff()
    {
        float angle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void SetLookDirection()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _lookDirection = (mousePosition - (Vector2)transform.position).normalized;
    }

    void ShootPrimary()
    {
        AudioManager.Instance.PlayAudio(_shootSound, AudioManager.SoundType.SFX, 0.4f, false);
        Projectile newProjectile = Instantiate(_projectile, _tip.position, Quaternion.identity);
        newProjectile.InitializeProjectile(_lookDirection);
    }

    void ShootSecondary()
    {
        AudioManager.Instance.PlayAudio(_shootSound, AudioManager.SoundType.SFX, 0.4f, false);
        Projectile newProjectile = Instantiate(_secondaryProjectile, _tip.position, Quaternion.identity);
        newProjectile.InitializeProjectile(_lookDirection);
    }
}
