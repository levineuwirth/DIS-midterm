using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu3 : MonoBehaviour
{    
    
    public GameObject levelSelectionPanel;
    // Method to start the game
    public void StartGame()
    {
        // Replace "GameScene" with the name of your actual game scene
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




{
    // Reference to the level selection panel


    // Show the level selection panel when Start is clicked
    public void ShowLevelSelection()
    {
        // Disable the main menu elements (optional, if you want to hide the main menu when level selection is shown)
        gameObject.SetActive(false);

        // Activate the level selection panel
        levelSelectionPanel.SetActive(true);
    }

    // Method to load the tutorial level
    public void LoadTutorial()
    {
        SceneManager.LoadScene("TutorialScene"); // Replace with the exact name of your tutorial scene
    }

    // Method to load Level One
    public void LoadLevelOne()
    {
        SceneManager.LoadScene("LevelOneScene"); // Replace with the exact name of your level one scene
    }

    // Method to load Level Two
    public void LoadLevelTwo()
    {
        SceneManager.LoadScene("LevelTwoScene"); // Replace with the exact name of your level two scene
    }

    // Method to quit the game
    public void QuitGame()
    {
        Debug.Log("Quit game.");
        Application.Quit();

        // For quitting in the Unity Editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
}
