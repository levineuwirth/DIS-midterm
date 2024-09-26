using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu3 : MonoBehaviour
{
    
    public void StartGame()
    {
        // Change this to level select
        SceneManager.LoadScene("GameScene");
    }

    // Method for Options - implement your options here
    public void OpenOptions()
    {
        // Add options logic here
        Debug.Log("Options menu opened.");
    }

    // Method for Credits - implement your credits here
    public void ShowCredits()
    {
        // Add credits logic here
        Debug.Log("Credits menu opened.");
    }

    // Method to quit the game
    public void QuitGame()
    {
        Debug.Log("Quit game.");
        Application.Quit();
        // The line below is to ensure it works in the Unity editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
