using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Game Over Panel")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button mainMenuButton;


    private int currentSceneIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        HideGameOverPanel();
        AddButtonListeners();
    }


    private void AddButtonListeners()
    {

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(MainMenuOnClick);

       
    }

    private void MainMenuOnClick()
    {
        Time.timeScale = 1; // Ensure the game resumes before loading the menu
        SceneManager.LoadScene(0); // Assuming Main Menu is scene 0
    }

    private void OnLevelWasLoaded(int level)
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        HideGameOverPanel(); // Ensure Game Over panel is hidden when a level is loaded
    }

    /// Loads the next level if there is one, or shows the Game Over panel if in the last level.
 
    public void LoadNextLevel()
    {
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            ShowGameOverPanel();
        }
    }

    /// Loads the main menu scene (assumed to be at index 0).
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }


    /// Reloads the current level.
    public void RestartLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    
    /// Displays the Game Over panel and pauses the game.
    
    public void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0; // Pause the game
    }

    /// Hides the Game Over panel and resumes the game.
    public void HideGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        Time.timeScale = 1; // Resume the game
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1 && other.CompareTag("GameOver"))
        {
            ShowGameOverPanel();
        }
    }
}
