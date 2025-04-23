/* 
 * Connor Caruthers
 * 2365827
 * ccaruthers@chapman.edu
 * CPSC-340-01
 * Starcube Showdown
 * 
 * Play a sound on clicking a button
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip sound;

    public void OnButtonClick()
    {
        AudioManager.Instance.PlaySound(sound);
    }
}
