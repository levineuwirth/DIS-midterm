using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject ControlPanel;
    public static bool isGamePaused;
    private void Start() {
        if(PausePanel != null) {
            PausePanel.SetActive(false);
        }
        if(ControlPanel != null) {
            ControlPanel.SetActive(false);
        }
        isGamePaused = false;
    }
    private void Update() {
        if(PausePanel != null) {
            if(Input.GetKeyDown(PlayerController.Instance.pauseGame)) {
                if(!isGamePaused) {
                    Pause();
                }
                else {
                    Unpause();
                }
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
    public void EnterControlPanel() {
        if(PausePanel != null) {
            PausePanel.SetActive(false);
        }
        ControlPanel.SetActive(true);
    }
    public void LeaveControlPanel() {
        ControlPanel.SetActive(false);
        if(PausePanel != null) {
            PausePanel.SetActive(true);
        }
    }
}
