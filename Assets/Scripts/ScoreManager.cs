using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Reference to the TextMeshProUGUI component for displaying the score
    public TextMeshProUGUI scoreText;

    // Current score
    private int currentScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the score display
        UpdateScoreText();
    }

    /// <param name="amount">The amount to increase the score.</param>
    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreText();
    }

   
    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreText();
    }

   
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + currentScore;
    }
}
