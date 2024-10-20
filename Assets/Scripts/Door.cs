using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{

    public static Animator doorAnimator;
    [field: SerializeField] public AudioSource doorOpenSFX {get ; private set;}
    [field: SerializeField] public Animator loadingScreen {get ; private set;}
    private BoxCollider2D doorCollider;

    private void Start() {
        doorAnimator = gameObject.GetComponent<Animator>();
        doorCollider = gameObject.GetComponent<BoxCollider2D>();

        doorCollider.enabled = false;
        doorCollider.isTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            StartCoroutine(LoadLoadingScreen());
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
        doorOpenSFX.Play();
    }

    IEnumerator LoadLoadingScreen() {
        loadingScreen.SetTrigger("Start");

        yield return new WaitForSeconds(3f);

        SceneController.instance.NextLevel();
    }
}
