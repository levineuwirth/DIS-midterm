using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    private void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("set to not destroy on load");
        }
        else {
            Destroy(gameObject);
        }
    }
    public void NextLevel() {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(int buildIndex) {
        SceneManager.LoadSceneAsync(buildIndex);
    }
    public void QuitApp() {
        Application.Quit();
        Debug.Log("App Quit");
    }
}
