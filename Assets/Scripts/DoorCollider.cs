using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DoorCollider : MonoBehaviour
{
    BoxCollider2D boxCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame 
    void Update()
    {
        
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /**private void OnTriggerEnter2D(Collider2D collision)
    {
    }
    **/
}
