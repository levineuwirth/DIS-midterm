using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Rigidbody2D _rb;

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
    }

    private void movePlayer() {
        _rb.linearVelocityX = Input.GetAxisRaw("Horizontal");
    }
}
