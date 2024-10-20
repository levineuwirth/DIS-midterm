using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [field: SerializeField] public float moveSpeed {get ; private set;}
    [field: SerializeField] public float acceleration {get ; private set;}
    [field: SerializeField] public float decceleration {get ; private set;}
    [field: SerializeField] public float velPower {get ; private set;}
    [field: SerializeField] public float frictionAmount {get ; private set;}

    [Header("Jumping")]
    [field: SerializeField] public float jumpForce {get ; private set;}
    [field: SerializeField] public float downwardForce {get ; private set;}

    [field: SerializeField] public float coyoteTime {get ; private set;}
    private float _coyoteTimeCounter;

    [Header("Ground Check Visualizer")]
    [field: SerializeField] public Vector2 boxSize {get ; private set;}
    [field: SerializeField] public float castDistance {get ; private set;}
    [field: SerializeField] public LayerMask groundLayer {get ; private set;}
    [field: SerializeField] public float postDeathTimer {get ; private set;}

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private ParticleSystem _dust;
    private AudioSource _jumpSound;
    private bool _isJumpBuffered = false;
    private bool _isJumpRelease = false;
    private bool _jumpCutDone = false;
    private float _moveInput;

    void Start()
    {
        Health.EOnDamageTaken += () => InvulnerabilityBlink();
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _dust = gameObject.GetComponent<ParticleSystem>();
        _jumpSound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!MenuController.isGamePaused && !PlayerAnimation.Instance.playerAnimator.GetBool("Dead")) {
            PlayerAnimation.Instance.playerAnimator.SetBool("isGrounded", isGrounded());

            _moveInput = Input.GetAxisRaw("HorizontalWASD");

            flipSprite();

            if(isGrounded()) {
                _coyoteTimeCounter = coyoteTime;
            }
            else {
                _coyoteTimeCounter -= Time.deltaTime;
            }

            if(Input.GetKeyDown(PlayerController.Instance.jump) && _coyoteTimeCounter > 0f) {
                _isJumpBuffered = true;
                _jumpCutDone = false;
            }
            
            if(Input.GetKeyUp(PlayerController.Instance.jump) && _rigidbody.linearVelocityY > 0 && !_jumpCutDone) {
                _isJumpRelease = true;
                _coyoteTimeCounter = 0f;
            }
        }
        else if(PlayerAnimation.Instance.playerAnimator.GetBool("Dead")) {
            postDeathTimer -= Time.deltaTime;
            _rigidbody.linearVelocity = new Vector2(0, 0);
            _rigidbody.gravityScale = 0;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            if(postDeathTimer < 0) {
                KillPlayer();
            }
        }
    }

    private void FixedUpdate() {
        if(!PlayerAnimation.Instance.playerAnimator.GetBool("Dead")) {
            movePlayer();
        }
    }

    private void movePlayer() {
        Run();
        Jump();
        FastFall();
    }

    private void Run() {
        float targetSpeed = _moveInput * moveSpeed;

        float speedDif = targetSpeed - _rigidbody.linearVelocityX;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        _rigidbody.AddForce(movement * Vector2.right);

        ApplyFriction(targetSpeed);
    }

    private void Jump() {
        if(_isJumpBuffered) {
            _isJumpBuffered = false;
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            // vfx and sfx
            CreateDust();
            PlayJumpSFX();
        }

        // implement jump cut
        else if(_isJumpRelease) {
            _isJumpRelease = false;
            _jumpCutDone = true;
            _rigidbody.AddForce(Vector2.down * _rigidbody.linearVelocityY, ForceMode2D.Impulse);
        }
    }
    
    private void FastFall() {
        if(_rigidbody.linearVelocityY < 0) {
            _rigidbody.AddForce(Vector2.down * downwardForce * _rigidbody.mass, ForceMode2D.Force);
        }
    }

    private void ApplyFriction(float targetSpeed) {
        if(isGrounded() && Mathf.Abs(targetSpeed) < 0.01f) {
            float amount = Mathf.Min(Mathf.Abs(_rigidbody.linearVelocityX), Math.Abs(frictionAmount)) * Mathf.Sign(_rigidbody.linearVelocityX);

            _rigidbody.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }

    // shoots a box raycast below the player to check if player is grounded
    private bool isGrounded() {
        return Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer);
    }

    // visualizes the raycast hitbox and allows editing in the unity editor
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    private void CreateDust() {
        _dust.Play();
    }

    private void PlayJumpSFX() {
        _jumpSound.Play();
    }

    private void flipSprite() {
        if(_spriteRenderer.flipX != PlayerAnimation.Instance.getFlip() && isGrounded()) {
            CreateDust();
        }

        _spriteRenderer.flipX = PlayerAnimation.Instance.getFlip();
    }

    private void KillPlayer() {
        SceneController.instance.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void InvulnerabilityBlink() {
        Debug.Log("Invulnerability Event Detected");
        StartCoroutine(BlinkSprite());
    }

    IEnumerator BlinkSprite() {
        
        _spriteRenderer.color = Color.gray;

        yield return new WaitForSeconds(1.5f);

        _spriteRenderer.color = Color.white;
    }

    private void OnDestroy() {
        Health.EOnDamageTaken -= () => InvulnerabilityBlink();
    }

}