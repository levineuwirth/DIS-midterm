using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator playerAnimator {get ; private set;}
    private Rigidbody2D _rb;
    public static PlayerAnimation Instance;


    void Awake() {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        playerAnimator = gameObject.GetComponent<Animator>();
        playerAnimator.SetBool("Dead", false);
    }

    // Update is called once per frame
    void Update()
    {
        updateRun();
    }

    private void updateRun() {
        playerAnimator.SetBool("Run", Input.GetKey(PlayerController.Instance.left) || Input.GetKey(PlayerController.Instance.right));
    }
}
