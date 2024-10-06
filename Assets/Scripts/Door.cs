using UnityEngine;

public class Door : MonoBehaviour
{

    public static Animator doorAnimator;
    private BoxCollider2D doorCollider;

    private void Start() {
        doorAnimator = gameObject.GetComponent<Animator>();
        doorCollider = gameObject.GetComponent<BoxCollider2D>();

        doorCollider.enabled = false;
        doorCollider.isTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            SceneController.instance.NextLevel();
        }
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
        Debug.Log("Recipe Complete! Opening Door");
        doorAnimator.SetBool("OpenDoor", true);
        doorCollider.enabled = true;
	    doorCollider.isTrigger = true;
    }
}
