using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class StatusBar : MonoBehaviour
{
    [SerializeField] protected TMP_Text timerText;
    [SerializeField] protected TMP_Text deathCountText; 

    protected ScoreKeeper scoreKeeper;

    // Start is called before the first frame update
    void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();  
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = scoreKeeper.GetPlayTimeString();
        deathCountText.text = scoreKeeper.GetDeathCountString();
    }
}
