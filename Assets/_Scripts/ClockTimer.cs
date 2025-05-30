using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float timeRemaining = 60f; // Start at 1 minute
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject timerPanel;

    private bool isGameOver;

    private void Update()
    {
        if (!isGameOver)
        {
            timeRemaining -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);

            timerText.text = $"{minutes}:{seconds:D2}"; // Display time in MM:SS format

            if (timeRemaining <= 0)
            {
                EndGame();
            }
        }
    }

    private void EndGame()
    {
        isGameOver = true;
        Time.timeScale = 0; // Freeze the game
        timerPanel.SetActive(false);
        losePanel.SetActive(true); // Show "You Lose" panel
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Unfreeze the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload scene
    }
}