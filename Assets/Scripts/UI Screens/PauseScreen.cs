/* 
 * Connor Caruthers
 * 2365827
 * ccaruthers@chapman.edu
 * CPSC-340-01
 * Starcube Showdown
 * 
 * Handles pausing the game and displaying options
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public KeyCode pauseButton;
    public Canvas tutorialScreen;
    public string quitScene = "TitleScene";

    public FirstPersonLook cameraController;

    private bool gamePaused;

    public static PauseScreen Instance { get; private set; }

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
        tutorialScreen.GetComponent<Canvas>().enabled = false;
        gamePaused = false;
    }

    private void Update() // check for pause button 
    {
        if (Input.GetKeyDown(pauseButton) && !DeathScreen.Instance.IsGameOver())
        {
            if (!gamePaused) { PauseGame(); }
            else { ResumeButton(); } // if press pause button while on pause screen, resume
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0; // pause everything in the scene
        this.GetComponent<Canvas>().enabled = true;
        tutorialScreen.GetComponent<Canvas>().enabled = false;
        gamePaused = true;

        cameraController.UnlockCursor(); // enable cursor to select buttons
    }

    public void ResumeButton() // resume everything and disable cursor
    {
        Time.timeScale = 1;
        this.GetComponent<Canvas>().enabled = false;
        tutorialScreen.GetComponent<Canvas>().enabled = false;
        gamePaused = false;

        cameraController.LockCursor();
    }

    public void TutorialButton()
    {
        this.GetComponent<Canvas>().enabled = false;
        tutorialScreen.GetComponent<Canvas>().enabled = true;
    }

    public void QuitButton()
    {
        StartCoroutine(FadeOutAndLoadScene());
    }

    public bool IsGamePaused()
    {
        return gamePaused;
    }

    private IEnumerator FadeOutAndLoadScene() // fades out music and returns to title scene
    {
        yield return StartCoroutine(AudioManager.Instance.FadeOutMusic());

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(quitScene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
