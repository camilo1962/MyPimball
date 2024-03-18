using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Text ballText, loopsText, lightsText,countdownText;
    public TMP_Text scoreText, finalScoreText;
    public GameObject gameOverPanel, pauseMenuPanel, inputNameField, submitButton, highscoresButton, menuButton;

    public static bool GameIsPaused = false;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || SimpleInput.GetButtonDown("Pausa"))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuPanel.SetActive(false);
        StartCoroutine(Countdown());
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowGameOverPanel(bool on)
    {
        gameOverPanel.SetActive(on);
    }

    public void UpdateLivesText(int amount)
    {
        ballText.text = "" + amount;
    }

    public void UpdateScoreText(int amount)
    {
        scoreText.text = amount.ToString("D7");
        ScoreManager.instance.CheckAddLife();
    }

    public void UpdateLoopsText(int amount)
    {
        loopsText.text = (amount + 1).ToString();
    }

    public void UpdateLightsText(int amount)
    {
        lightsText.text = "" + amount.ToString() + "/" + ScoreManager.instance.objectsList.Count.ToString();
    }

    public void UpdateFinalScoreText(int amount)
    {
        finalScoreText.text = amount.ToString("D7");
    }

    public void ShowHighscoreComponents()
    {
        inputNameField.SetActive(true);
        submitButton.SetActive(true);
    }

    public void ShowOtherButtons()
    {
        highscoresButton.SetActive(true);
        menuButton.SetActive(true);
    }

    IEnumerator Countdown()
    {
        countdownText.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        countdownText.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        countdownText.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        countdownText.text = "";
        Time.timeScale = 1f;
    }
}
