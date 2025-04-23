/* 
 * Connor Caruthers
 * 2365827
 * ccaruthers@chapman.edu
 * CPSC-340-01
 * Starcube Showdown
 * 
 * Keeps track of score and plasma, and updates UI accordingly
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public float score;
    public float plasma;

    public float scoreRate;
    public float plasmaRate;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI plasmaText;

    public KeyCode testPlasmaButton;

    public float totalScoreRateRightNow;

    public static ScoreController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Update() // constantly updating score and plasma as they are always increasing/decreasing
    {
        UpdateScore();
        UpdatePlasma();

        if (Input.GetKeyDown(testPlasmaButton)) // dev tool for testing plasma adding
        {
            plasma += 10;
        }
    }

    public void AddPlasma(float bonusPlasma)
    {
        plasma += bonusPlasma;
    }

    private void UpdateScore() // increase score constantly depending on plasma multiplier
    {
        totalScoreRateRightNow = scoreRate * PlasmaMultiplier() * Time.deltaTime;
        score += totalScoreRateRightNow;
        scoreText.text = Mathf.RoundToInt(score).ToString();
    }

    private void UpdatePlasma() // decraese plasma constantly at a flat rate
    {
        plasma -= plasmaRate * Time.deltaTime;
        PlasmaMultiplier();
    }

    private float PlasmaMultiplier() // sets plasma text and color, and returns plasma multiplier
    {
        if (plasma > 0) 
        {
            plasmaText.text = "+" + (Mathf.Round(plasma * 100f)/100f).ToString();
            plasmaText.color = Color.green;
            return plasma / 1f; 
        }

        // if you have 0 or less plasma, the score stops going up, but doesn't go down
        else if (plasma == 0)
        {
            plasmaText.text = "+0.00";
            plasmaText.color = Color.green;
            return 0;
        }
        else
        {
            plasmaText.text = (Mathf.Round(plasma * 100f) / 100f).ToString();
            plasmaText.color = Color.red;
            return 0;
        }
    }
}
