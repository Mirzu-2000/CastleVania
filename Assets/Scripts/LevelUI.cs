using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class LevelUI : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button restartButton;


    [SerializeField] private int levelNumber = 1;
    [SerializeField] private TextMeshProUGUI levelText;


    private bool isPaused = false;

    private void Start()
    {
        // Ensure both panels are inactive at the start
        if (pausePanel != null) pausePanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        // Add button listeners
        AddButtonListeners();
        UpdateLevelText();
    }
     
    

    private void Update()
    {
        HandlePauseInput();

    }

    private void UpdateLevelText()
    {
        levelText.text = "Level: " + levelNumber;
    }

    /// Toggles the pause panel when the pause key is pressed.
    private void HandlePauseInput()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                ShowPausePanel();
            }
            else
            {
                HidePausePanel();
            }
        }
    }

    /// Adds listeners to the buttons for their respective functionality.
    private void AddButtonListeners()
    {
        if (resumeButton != null)
            resumeButton.onClick.AddListener(ResumeOnClick);

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(MainMenuOnClick);

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartOnClick);
    }

    /// Displays the pause panel and pauses the game.
    private void ShowPausePanel()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0; // Pause the game
        }
    }

    /// Hides the pause panel and resumes the game.
    private void HidePausePanel()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1; // Resume the game
        }
    }

    /// Handles the Resume button click to hide the pause panel.
    private void ResumeOnClick()
    {
        isPaused = false;
        HidePausePanel();
    }

    /// Handles the Main Menu button click to return to the main menu.
    private void MainMenuOnClick()
    {
        Time.timeScale = 1; // Ensure the game resumes before loading the menu
        SceneManager.LoadScene(0); // Assuming Main Menu is scene 0
    }

    /// Handles the Restart button click to restart the current level.
    private void RestartOnClick()
    {
        Time.timeScale = 1; // Ensure the game resumes before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// Displays the Game Over panel and pauses the game.
    /// This can be called from other scripts ( LevelManager).
    public void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0; // Pause the game
        }
    }
}
