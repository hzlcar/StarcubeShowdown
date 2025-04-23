/* 
 * Connor Caruthers
 * 2365827
 * ccaruthers@chapman.edu
 * CPSC-340-01
 * Starcube Showdown
 * 
 * Displays the initial title screen and buttons
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public Canvas tutorialScreen;
    public Canvas IntroTextScreen;

    void Start()
    {
        // disable other screens on launch
        tutorialScreen.GetComponent<Canvas>().enabled = false;
        IntroTextScreen.GetComponent<Canvas>().enabled = false;
    }

    public void StartButton() // transition to intro text screen
    {
        IntroTextScreen.GetComponent<Canvas>().enabled = true;
        this.GetComponent<Canvas>().enabled = false;
    }

    public void TutorialButton() // transition to tutorial screen 
    {
        tutorialScreen.GetComponent<Canvas>().enabled = true;
        this.GetComponent<Canvas>().enabled = false;
    }

    public void QuitButton() // quit game and editor
    {
        Debug.Log("Quit");

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
