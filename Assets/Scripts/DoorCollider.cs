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
        boxCollider.enabled = false;
	boxCollider.isTrigger = false;
    }

    void OnEnable()
    {
	ItemManager.EOnRecipeComplete += EnableCollider;
    }

    void OnDisable()
    {
        ItemManager.EOnRecipeComplete -= EnableCollider;
    }

    private void EnableCollider()
    {
        Debug.Log("Recipe complete! Enabling door collider.");
        boxCollider.enabled = true;
	boxCollider.isTrigger = true;
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
