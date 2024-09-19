using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [field: SerializeField] public float speed {get ; private set;}
    [field: SerializeField] public float jumpHeight {get ; private set;}
    private Rigidbody2D _rb;

    private RaycastHit2D onGround;
    private bool isGrounded = false;

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        onGround = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        checkGround();
    }

    private void movePlayer() {
        if(Input.GetKey(PlayerController.Instance.left)) {
            _rb.linearVelocityX = -speed;
        }
        else if(Input.GetKey(PlayerController.Instance.right)) {
            _rb.linearVelocityX = speed;
        }

        if(Input.GetKeyDown(PlayerController.Instance.jump) && isGrounded) {
            jump();
        }
    }

    private void jump() {
        _rb.AddForceY(jumpHeight);
    }

    private void checkGround() {
        // bug?
        isGrounded = onGround.collider != null && onGround.collider.gameObject.tag == "Ground";
        PlayerAnimation.Instance.playerAnimator.SetBool("isGrounded", isGrounded);
    }
}
