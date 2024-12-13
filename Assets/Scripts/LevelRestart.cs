using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRestart : MonoBehaviour
{
    private int currentSceneIndex;


    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

    }


    public void  RestartLevel()
    {
        
        SceneManager.LoadScene(currentSceneIndex);  
    }


}
