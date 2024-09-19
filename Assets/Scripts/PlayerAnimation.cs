using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator playerAnimator;
    private Rigidbody2D _rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        playerAnimator = gameObject.GetComponent<Animator>();
        playerAnimator.SetBool("Dead", false);
        playerAnimator.SetBool("isGrounded", true);
    }

    // Update is called once per frame
    void Update()
    {
        playerAnimator.SetBool("Run", _rb.linearVelocityX != 0);
    }
}
