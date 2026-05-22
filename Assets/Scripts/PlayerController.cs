using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioClip _music;
    [SerializeField] private AudioClip _footstep;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private SpriteRenderer _characterBody;
    [SerializeField] private Animator _animator;
    
    private Rigidbody2D _rb;
    private float _nextFootstepAudio = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayerMovement();
    }

    private void HandlePlayerMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        movement = Vector2.ClampMagnitude(movement, 1f);
        _rb.linearVelocity = movement * _movementSpeed;

        bool characterIsWalking = movement.magnitude > 0f;
        _animator.SetBool("isWalking", characterIsWalking);

        if (characterIsWalking)
        {
            HandleWalkingSounds();
        }

        bool flipSprite = movement.x < 0f;
        _characterBody.flipX = flipSprite;
    }

    private void HandleWalkingSounds()
    {
        if (Time.time >= _nextFootstepAudio)
        {
            AudioManager.Instance.PlayAudio(_footstep, AudioManager.SoundType.SFX, 1f, false);
            float audioFrequency = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length / 2f; 
            _nextFootstepAudio = Time.time + audioFrequency;
        }
    }
}
