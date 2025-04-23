/* 
 * Connor Caruthers
 * 2365827
 * ccaruthers@chapman.edu
 * CPSC-340-01
 * Starcube Showdown
 * 
 * Displays the tutorial screen
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreen : MonoBehaviour
{

    public Canvas previousScreen;

    public void BackButton() // return to title screen
    {
        previousScreen.GetComponent<Canvas>().enabled = true;
        this.GetComponent<Canvas>().enabled = false;
    }
}
