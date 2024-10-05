using UnityEngine;
using UnityEditor.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    public static bool isGamePaused;

    private void Start() {
        PausePanel.SetActive(false);
        isGamePaused = false;
    }
    private void Update() {
        if(Input.GetKeyDown(PlayerController.Instance.pauseGame)) {
            if(!isGamePaused) {
                Pause();
            }
            else {
                Unpause();
            }
        }
    }
    public void Pause() {
        isGamePaused = true;
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Unpause() {
        isGamePaused = false;
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
