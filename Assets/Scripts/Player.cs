using System;
using System.Data.Common;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEditor.Callbacks;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [field: SerializeField] public float moveSpeed {get ; private set;}
    [field: SerializeField] public float acceleration {get ; private set;}
    [field: SerializeField] public float decceleration {get ; private set;}
    [field: SerializeField] public float velPower {get ; private set;}
    [field: SerializeField] public float frictionAmount {get ; private set;}
    [field: SerializeField] public float jumpCutMultiplier {get ; private set;}

    //issue caused by serilaizefield?? 
    public float jumpForce;
    private Rigidbody2D _rb;

    [field: SerializeField] public Vector2 boxSize {get ; private set;}
    [field: SerializeField] public float castDistance {get ; private set;}
    [field: SerializeField] public LayerMask groundLayer {get ; private set;}

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAnimation.Instance.playerAnimator.SetBool("isGrounded", isGrounded());
    }

    private void FixedUpdate() {
        movePlayer();
    }

    private void movePlayer() {
        
        #region Run
        float moveInput = 0;
        
        if(Input.GetKey(PlayerController.Instance.left)) {
            moveInput = -1;
        }
        else if(Input.GetKey(PlayerController.Instance.right)) {
            moveInput = 1;
        }

        float targetSpeed = moveInput * moveSpeed;

        float speedDif = targetSpeed - _rb.linearVelocityX;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        _rb.AddForce(movement * Vector2.right);
        #endregion

        #region Friction
        if(isGrounded() && Mathf.Abs(targetSpeed) < 0.01f) {
            float amount = Mathf.Min(Mathf.Abs(_rb.linearVelocityX), Math.Abs(frictionAmount)) * Mathf.Sign(_rb.linearVelocityX);

            _rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
        #endregion

        #region Jump
        if(Input.GetKeyDown(PlayerController.Instance.jump) && isGrounded()) {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        //implement jump cut
        else if(Input.GetKeyUp(PlayerController.Instance.jump)) {
            _rb.AddForce(Vector2.down * _rb.linearVelocityY * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
        }
        #endregion
    }

    private void jump() {
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    // shoots a box raycast below the player to check if player is grounded
    private bool isGrounded() {
        return Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer);
    }

    // visualizes the raycast hitbox and allows editing in the unity editor
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
}
