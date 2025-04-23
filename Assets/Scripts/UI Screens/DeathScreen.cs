using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    public string reloadScene = "GameScene";
    public string quitScene = "TitleScene";

    public FirstPersonLook cameraController;
    public TextMeshProUGUI scoreText;

    private bool isGameOver;

    public static DeathScreen Instance { get; private set; }

    private void Awake()
    {
        Time.timeScale = 1;

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        this.GetComponent<Canvas>().enabled = false;
        isGameOver = false;
    }

    private void Update() 
    {

    }

    public void EndGame()
    {
        Time.timeScale = 0;
        this.GetComponent<Canvas>().enabled = true;
        isGameOver = true;

        cameraController.UnlockCursor();
        DisplayScore();
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        this.GetComponent<Canvas>().enabled = false;
        isGameOver = false;

        cameraController.LockCursor();

        SceneManager.LoadScene(reloadScene);
    }

    public void QuitButton()
    {
        SceneManager.LoadScene(quitScene);
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void DisplayScore()
    {
        scoreText.text = "Score: " + Mathf.RoundToInt(ScoreController.Instance.score).ToString();
    }
}
