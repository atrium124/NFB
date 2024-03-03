using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScene : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text deathCountText;

    void Start()
    {
        // Find the ScoreKeeper object in the scene
        ScoreKeeper scoreKeeper = FindObjectOfType<ScoreKeeper>();

        if (scoreKeeper != null)
        {
            // Set the text values of the TextMeshProUGUI elements
            timerText.text = scoreKeeper.GetPlayTimeString();
            deathCountText.text = scoreKeeper.GetDeathCountString();
        }
        else
        {
            Debug.LogError("ScoreKeeper not found in the scene.");
        }
    }
}