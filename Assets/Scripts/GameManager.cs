using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int startBallNumber = 3;
    int currentBallNumber;
    int activeBallsOnPlayfield; // para evitar que se generen nuevas bolas antes de que las bolas de multibola estén en el campo

    

    //SPAWNBALL
    public GameObject ballPrefab;
    public Transform spawnPoint;
    public Transform multiSpawnPoint;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        currentBallNumber = startBallNumber;
        UIManager.instance.UpdateLivesText(currentBallNumber);
        UIManager.instance.ShowGameOverPanel(false);
        ScoreManager.instance.ResetScore();
        ScoreManager.instance.ResetAllLights();
        ScoreManager.instance.ResetLoops();
        MissionManager.instance.ResetAllMissions();
        Collectible.instance.gameObject.SetActive(false);

        CreateNewBall();
    }

    public void CreateNewBall()
    {
        if (activeBallsOnPlayfield == 0 && currentBallNumber > 0)
        {
            Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
            ScoreManager.instance.ResetLights();
            UpdateBallsOnPlayfield(+1);
            currentBallNumber--;
            UIManager.instance.UpdateLivesText(currentBallNumber);
        }
        else if (activeBallsOnPlayfield == 0 && currentBallNumber == 0)
        {
            UIManager.instance.UpdateFinalScoreText(ScoreManager.instance.ReadFinalScore());
            // ZAGRAJ DŹWIĘK - game over
            FindObjectOfType<AudioManager>().Play("GameOverSound");
            if (ScoreSavingManager.CanScoreBeSaved(ScoreManager.instance.ReadFinalScore()))
            {
                UIManager.instance.ShowHighscoreComponents();
            }
            else
            {
                UIManager.instance.ShowOtherButtons();
            }
            UIManager.instance.ShowGameOverPanel(true);
        }
    }

    public void StartMultiball()
    {
        // ZAGRAJ DŹWIĘK - multiball
        FindObjectOfType<AudioManager>().Play("MultiballSound");
        StartCoroutine(MultiBall());
    }

    public void UpdateBallsOnPlayfield(int number)
    {
        activeBallsOnPlayfield += number;
    }

    // el jugador es recompensado con una vida extra cuando obtiene 100000 puntos o completa la misión de reinversión
    public void AddLife()
    {
        currentBallNumber++;
        UIManager.instance.UpdateLivesText(currentBallNumber);
        // ZAGRAJ DŹWIĘK - dodano życie
        FindObjectOfType<AudioManager>().Play("AddLifeSound");
    }

    IEnumerator MultiBall()
    {
        int number = 3;
        while (number > 0)
        {
            Instantiate(ballPrefab, multiSpawnPoint.position, Quaternion.identity);
            UpdateBallsOnPlayfield(+1);
            number--;
            yield return new WaitForSeconds(1f);
        }
    }

   

   
}

