using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class NarrativeData
{
    public enum TransitionType
    {
        None,
        Fade,
        Slide
    }

    [Header("Text")]
    public string characterName;
    [TextArea] public string text;

    [Header("Sprites")]
    public Sprite backgroundSprite;
    public Sprite characterSprite;

    [Header("Settings")]
    public TransitionType transitionType;
    public float transitionTime;

    [Header("Audio")]
    public AK.Wwise.Event AssociatedSFX;

    public IEnumerator DoFadeTransition(NarrativeDisplay display)
    {
        float currentTime = 0;
        Image image = display.characterImage;
        image.color = new Color(1, 1, 1, 0);

        while(currentTime < transitionTime)
        {
            image.color = new Color(1, 1, 1, currentTime / transitionTime);
            currentTime += Time.deltaTime;
            yield return null;
        }

        image.color = Color.white;
    }

    public IEnumerator DoSlideTransition(NarrativeDisplay display)
    {
        float currentTime = 0;
        RectTransform rectTransform = display.characterImage.GetComponent<RectTransform>();
        Vector2 onscreenPosition = rectTransform.anchoredPosition;
        Vector2 offscreenPosition = display.offScreenPosition;

        rectTransform.anchoredPosition = offscreenPosition;
        while(currentTime < transitionTime)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(offscreenPosition, onscreenPosition, 
                currentTime / transitionTime);
            currentTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = onscreenPosition;
    }
}
