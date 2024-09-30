using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [field: SerializeField] public float moveSpeed {get ; private set;}
    [field: SerializeField] public float acceleration {get ; private set;}
    [field: SerializeField] public float decceleration {get ; private set;}
    [field: SerializeField] public float velPower {get ; private set;}
    [field: SerializeField] public float frictionAmount {get ; private set;}

    [Header("Jumping")]
    public float jumpForce;
    [field: SerializeField] public float downwardForce {get ; private set;}

    [Header("Ground Check Visualizer")]
    [field: SerializeField] public Vector2 boxSize {get ; private set;}
    [field: SerializeField] public float castDistance {get ; private set;}
    [field: SerializeField] public LayerMask groundLayer {get ; private set;}

    [Header("Item Inventory")]
    private GameObject heldItem;
    private bool hasItem;
    public delegate void OnItemPickup();
    public static event OnItemPickup EOnItemPickup;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private ParticleSystem _dust;
    private AudioSource _jumpSound;
    private bool _isJumpBuffered = false;
    private bool _isJumpRelease = false;
    private bool _jumpCutDone = false;
    private float _moveInput;

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _sr = gameObject.GetComponent<SpriteRenderer>();
        _dust = gameObject.GetComponent<ParticleSystem>();
        _jumpSound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAnimation.Instance.playerAnimator.SetBool("isGrounded", isGrounded());

        _moveInput = Input.GetAxisRaw("Horizontal");

        flipSprite();

        if(Input.GetKeyDown(PlayerController.Instance.jump) && isGrounded()) {
            _isJumpBuffered = true;
            _jumpCutDone = false;
        }
        
        if(Input.GetKeyUp(PlayerController.Instance.jump) && _rb.linearVelocityY > 0 && !_jumpCutDone) {
            _isJumpRelease = true;
        }
    }

    private void FixedUpdate() {
        movePlayer();
    }

    private void movePlayer() {
        Run();
        Jump();
        FastFall();
    }

    private void Run() {
        float targetSpeed = _moveInput * moveSpeed;

        float speedDif = targetSpeed - _rb.linearVelocityX;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        _rb.AddForce(movement * Vector2.right);

        ApplyFriction(targetSpeed);
    }

    private void Jump() {
        if(_isJumpBuffered) {
            _isJumpBuffered = false;
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            // vfx and sfx
            CreateDust();
            PlayJumpSFX();
        }

        // implement jump cut
        else if(_isJumpRelease) {
            _isJumpRelease = false;
            _jumpCutDone = true;
            _rb.AddForce(Vector2.down * _rb.linearVelocityY, ForceMode2D.Impulse);
        }
    }
    
    private void FastFall() {
        if(_rb.linearVelocityY < 0) {
            _rb.AddForce(Vector2.down * downwardForce * _rb.mass, ForceMode2D.Force);
        }
        //TODO: Clamp?
    }

    private void ApplyFriction(float targetSpeed) {
        if(isGrounded() && Mathf.Abs(targetSpeed) < 0.01f) {
            float amount = Mathf.Min(Mathf.Abs(_rb.linearVelocityX), Math.Abs(frictionAmount)) * Mathf.Sign(_rb.linearVelocityX);

            _rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
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
        // add particles when changing direction
        if(_sr.flipX != PlayerAnimation.Instance.getFlip() && isGrounded()) {
            CreateDust();
        }

        _sr.flipX = PlayerAnimation.Instance.getFlip();
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Item" && Input.GetKeyDown(PlayerController.Instance.pickOrDropItem)) {
            if(!hasItem) {
                addItem(other.gameObject);
                //trigger event
                EOnItemPickup?.Invoke();
            }
        }
    }

    private void addItem(GameObject newItem) {
        heldItem = newItem;
    }
}
