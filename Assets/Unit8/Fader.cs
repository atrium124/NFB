using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public enum FaderState
    {
        FadingIn,
        AllIn,
        FadingOut,
        AllOut
    }

    [SerializeField] protected float fadeTime;

    public FaderState State { get; private set; }

    protected Color fadeColor;
    protected Image Image;

    protected float fadeTimer;

    void Awake()
    {
        Image = GetComponent<Image>();

        fadeColor = Image.color;
        fadeColor.a = 1f;
        Image.color = fadeColor;

        State = FaderState.AllOut;
    }

    // Update is called once per frame
    void Update()
    {
        if (State == FaderState.AllIn || State == FaderState.AllOut) return;

        fadeTimer += Time.deltaTime; 
        if (State == FaderState.FadingIn)
        {
            if (fadeTimer >= fadeTime)
            {
                fadeColor.a = 0f;
                State = FaderState.AllIn;
            }
            else
            {
                fadeColor.a = 1f - fadeTimer / fadeTime;
            }
        }
        else if (State == FaderState.FadingOut)
        {
            if (fadeTimer >= fadeTime)
            {
                fadeColor.a = 1f;
                State = FaderState.AllOut;
            }
            else
            {
                fadeColor.a = fadeTimer / fadeTime;
            }
        }

        Image.color = fadeColor;
    }

    public void FadeIn()
    {
        if (State != FaderState.AllOut) return;

        fadeTimer = 0f;
        State = FaderState.FadingIn;
    }

    public void FadeOut()
    {
        if (State != FaderState.AllIn) return;

        fadeTimer = 0f;
        State = FaderState.FadingOut;
    }
}
