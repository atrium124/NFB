using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreKeeper : MonoBehaviour
{
    protected float playTime = 0f;
    protected int deathCount = 0;
    protected bool timeRunning = false;

    void Awake()
    {
        if (FindObjectsOfType<ScoreKeeper>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRunning)
        {
            playTime += Time.deltaTime;
        }
    }

    public string GetPlayTimeString()
    {
        float timerMinutes = Mathf.Floor(playTime / 60f);
        float timerSeconds = playTime - timerMinutes * 60f;

        return string.Format("{0:00}:{1:00.000}", timerMinutes, timerSeconds);
    }

    public string GetDeathCountString()
    {
        return deathCount.ToString();
    }

    public void ResetScore()
    {
        playTime = 0f;
        deathCount = 0;
    }

    public void StartTimer()
    {
        timeRunning = true;
    }

    public void StopTimer()
    {
        timeRunning = false; 
    }

    public void AddDeath()
    {
        deathCount++;
    }
}
