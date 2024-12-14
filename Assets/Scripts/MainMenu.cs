using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton; // Added Exit Button
    [SerializeField] private AudioClip startSFX;
    [SerializeField] private int levelSceneIndex = 1; // Scene index for the first level

    void Start()
    {
        if (playButton != null)
        {
            AudioSource.PlayClipAtPoint(startSFX, Camera.main.transform.position);
            playButton.onClick.AddListener(LoadLevel);
        }

        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame); // Listen for exit button click
        }
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene(levelSceneIndex);
    }

    private void ExitGame()
    {
#if UNITY_WEBGL
        Application.OpenURL("https://www.linkedin.com/in/mirza-ali-63921b191/");  
#else
        // On other platforms, Application.Quit works as expected
        Application.Quit();
#endif
    }
}
