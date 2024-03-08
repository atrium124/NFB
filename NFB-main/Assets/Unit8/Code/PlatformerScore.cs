using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerScore : MonoBehaviour
{
    public int Coins { get; private set; }
    public int Score { get; private set; }
    public int DeathCount { get; private set; }

    void Awake ()
    {
        if (FindObjectsOfType<PlatformerScore>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScore()
    {
        Coins = 0;
        Score = 0;
        DeathCount = 0;
    }

    public void AddDeath()
    {
        DeathCount++;
        Debug.Log("Death count is now" + DeathCount);
    }

    public void AddCoin()
    {
        Coins++;
        Debug.Log("Coin count is now" + Coins);
    }

    public void AddPoints(int points)
    {
        Score += points;
        Debug.Log("Score is now" + Score);
    }
}
