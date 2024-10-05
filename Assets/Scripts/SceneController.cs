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
            lastScene = SceneManager.GetActiveScene().buildIndex;
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
        Debug.Log("prev scene is" + lastScene);
        lastScene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("this scene is" + lastScene);
        SceneManager.LoadScene(temp);
    }
    public void QuitApp() {
        Application.Quit();
        Debug.Log("App has quit");
    }
}
