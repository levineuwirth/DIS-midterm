using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [field: SerializeField] public Animator playerAnimator {get ; private set;}
    public static PlayerAnimation Instance;
    private bool _flip;

    void Awake() {
        Instance = this;
        _flip = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAnimator = gameObject.GetComponent<Animator>();
        playerAnimator.SetBool("Dead", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!MenuController.isGamePaused && !playerAnimator.GetBool("Dead")) {
            updateRun();
            checkFlip();
        }
    }

    private void updateRun() {
        playerAnimator.SetBool("Run", Input.GetKey(PlayerController.Instance.left) || Input.GetKey(PlayerController.Instance.right));
    }

    private void checkFlip() {
       if (playerAnimator.GetBool("Run")) {
            _flip = Input.GetKey(PlayerController.Instance.left);
       }
    }
    public bool getFlip() {
        return _flip;
    }

    public void setThrow() {
        playerAnimator.SetTrigger("Throw");
        playerAnimator.ResetTrigger("Throw");
    }

    // TODO: make transition to jump faster
}
