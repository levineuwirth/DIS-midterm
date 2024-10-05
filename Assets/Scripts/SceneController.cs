using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    private int lastScene;

    private void Awake() {
        if(instance == null) {
            instance = this;
            lastScene = 0;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    //dont use lol
    public void NextLevel() {
        int nextSceneBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadSceneAsync(nextSceneBuildIndex);
        lastScene = SceneManager.GetSceneByBuildIndex(nextSceneBuildIndex).buildIndex;
    }

    public void LoadScene(string sceneName) {
        lastScene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(lastScene);
        SceneManager.LoadSceneAsync(sceneName);
    }
    public void GoBackToLastScene() {
        int temp = lastScene;
        lastScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(temp);
    }
    public void QuitApp() {
        Application.Quit();
    }
}
