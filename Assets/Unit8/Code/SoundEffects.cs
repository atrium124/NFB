using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{

    public AudioSource deathSound = null;

    public bool DeathSong = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeathSound()
    {
        if (!deathSound.isPlaying && DeathSong == false)
        {
            DeathSong = true;
        }
    }
}
