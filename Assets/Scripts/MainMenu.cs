using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] AudioClip startSFX;

    void Start()
    {
        if (playButton != null)
        {
            AudioSource.PlayClipAtPoint(startSFX, Camera.main.transform.position);
            playButton.onClick.AddListener(LoadLevel);

        }
    }

    private void LoadLevel()
    {

        SceneManager.LoadScene(1); // Assuming Level 1 is Scene index 1
    }
}
