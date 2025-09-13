using UnityEngine;
using UnityEngine.UI;
using TMPro;  // for TextMeshProUGUI

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;

    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiScoreText;
    public TextMeshProUGUI gameOverText;
    public Button retryButton;
    public Button exitButton;

    public float gameSpeed { get; private set; }
    private float score;

    private bool isGameOver;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (isGameOver) return;

        // Increase speed over time
        gameSpeed += gameSpeedIncrease * Time.deltaTime;

        // Update score
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
    }

    public void NewGame()
    {
        score = 0f;
        gameSpeed = initialGameSpeed;
        isGameOver = false;

        // Enable gameplay
        Time.timeScale = 1f;

        // Hide UI
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);

        UpdateHiscore();
    }

    public void GameOver()
    {
        isGameOver = true;
        gameSpeed = 0f;

        // Stop game logic
        Time.timeScale = 0f;

        // Show UI
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);

        UpdateHiscore();
    }

    private void UpdateHiscore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }

        hiScoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    }

    public void Retry()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    public void Exit()
    {
        Application.Quit();
    }
}
