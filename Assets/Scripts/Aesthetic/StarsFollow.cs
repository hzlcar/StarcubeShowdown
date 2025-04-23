/* 
 * Connor Caruthers
 * 2365827
 * ccaruthers@chapman.edu
 * CPSC-340-01
 * Starcube Showdown
 * 
 * This script controls the stars in the background and keeps them in the same position relative to the camera
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsFollow : MonoBehaviour
{
    private Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main;
        transform.position = playerCamera.transform.position;
        transform.parent = playerCamera.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
}
