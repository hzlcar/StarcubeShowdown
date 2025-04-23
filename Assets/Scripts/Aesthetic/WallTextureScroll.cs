/* 
 * Connor Caruthers
 * 2365827
 * ccaruthers@chapman.edu
 * CPSC-340-01
 * Starcube Showdown
 * 
 * This script controls the scrolling textures on the walls
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTextureScroll : MonoBehaviour
{
    public Material wallMaterial;
    public float scrollSpeed;

    private Vector2 currOffset;

    // Update is called once per frame
    void Update()
    {
        currOffset.x += (scrollSpeed * Time.deltaTime / 100);
        currOffset.y = -currOffset.x;

        wallMaterial.mainTextureOffset = currOffset;
    }

    private void FixedUpdate()
    {
        
    }
}
