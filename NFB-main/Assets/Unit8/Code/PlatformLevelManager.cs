using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLevelManager : MonoBehaviour
{
    public enum PlatformLevelState
    {
        GetReady,
        Playing,
        PlayerDied,
        FadeOut
    }

    [SerializeField] protected GameObject playerPrefab;
    [SerializeField] protected Transform startPosition;
    [SerializeField] protected float getReadyTime;
    [SerializeField] protected float diedMessageTime;

    public PlatformLevelState State { get; private set; }

    protected float timer;
    protected bool restartLevel;

    protected Fader fader;
    protected SceneLoader sceneLoader;
    protected PlatformCamera cam;

    // Start is called before the first frame update
    void Start()
    {
        fader = FindObjectOfType<Fader>();
        sceneLoader = FindObjectOfType<SceneLoader>();

        cam = FindObjectOfType<PlatformCamera>();

        timer = 0f;
        State = PlatformLevelState.GetReady;
        fader.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        if (State == PlatformLevelState.Playing) return;

        timer += Time.deltaTime;
        switch (State)
        {
            case PlatformLevelState.GetReady:
                if (timer > getReadyTime)
                {
                    cam.followTarget = Instantiate(
                        playerPrefab,
                        startPosition.position,
                        Quaternion.identity
                    );
                    cam.isFollowing = true;
                    State = PlatformLevelState.Playing;
                }
                break;

            case PlatformLevelState.PlayerDied:
                if (timer >= diedMessageTime)
                {
                    State = PlatformLevelState.FadeOut;
                }
                break;

            case PlatformLevelState.FadeOut:
                if (fader.State == Fader.FaderState.AllOut)
                {
                    if (restartLevel)
                    {
                        sceneLoader.ReloadCurrentScene();
                    }
                    else
                    {
                        sceneLoader.GoToNextScene();
                    }
                }
                else if (fader.State != Fader.FaderState.FadingOut)
                {
                    fader.FadeOut();
                }
                break;
        }
    }

    public void HandleDeath()
    {
        timer = 0f;
        State = PlatformLevelState.PlayerDied;

        // Respawn the player at the spawn point
        cam.followTarget = Instantiate(
            playerPrefab,
            startPosition.position,
            Quaternion.identity
        );
        cam.isFollowing = true;
    }

    public void LevelFinished()
    {
        restartLevel = false;
        State = PlatformLevelState.FadeOut;
    }
}