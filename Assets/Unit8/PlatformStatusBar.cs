using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlatformStatusBar : MonoBehaviour
{
    [SerializeField] protected TMP_Text coinsText;
    [SerializeField] protected TMP_Text scoreText;
    [SerializeField] protected TMP_Text deathCountText;

    protected PlatformerScore platformerScore;

    // Start is called before the first frame update
    void Start()
    {
        platformerScore = FindObjectOfType<PlatformerScore>();
    }

    // Update is called once per frame
    void Update()
    {
        if (platformerScore != null)
        {
            coinsText.text = platformerScore.Coins.ToString();
            scoreText.text =  platformerScore.Score.ToString();
            deathCountText.text = platformerScore.DeathCount.ToString();
        }
        else
        {
            Debug.LogWarning("PlatformerScore not found!");
        }
    }
}