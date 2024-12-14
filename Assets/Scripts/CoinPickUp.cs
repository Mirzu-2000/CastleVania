using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    /*     [SerializeField]private LevelUI levelUI;
    */
    [SerializeField] private ScoreManager scoreManager; 
    /*private void Start()
    {
        levelUI = GetComponent<LevelUI>();
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        scoreManager.AddScore(100);
        //levelUI.UpdateScoreText();
        Destroy(gameObject);
    }

 


}
