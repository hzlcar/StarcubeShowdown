/* 
 * Connor Caruthers
 * 2365827
 * ccaruthers@chapman.edu
 * CPSC-340-01
 * Starcube Showdown
 * 
 * Displays the intro lore text on the title screen
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroTextScreen : MonoBehaviour
{
    public Canvas titleScreen;
    public string startScene = "GameScene";

    public void StartButton()
    {
        StartCoroutine(FadeOutAndLoadScene());
    }

    private IEnumerator FadeOutAndLoadScene() // fades the audio out and loads the game scene
    {
        yield return StartCoroutine(AudioManager.Instance.FadeOutMusic());

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(startScene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void BackButton() // go back to title
    {
        titleScreen.GetComponent<Canvas>().enabled = true;
        this.GetComponent<Canvas>().enabled = false;
    }


}
