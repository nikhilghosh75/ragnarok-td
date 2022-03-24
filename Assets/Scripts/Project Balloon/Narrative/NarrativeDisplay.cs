using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using WSoft.UI;

public class NarrativeDisplay : MonoBehaviour
{
    public Text characterNameText;
    public Text dialogueText;
    public Image backgroundImage;
    public Image characterImage;
    public GameObject characterNameWindow;
    
    public float charactersPerSecond;

    [Tooltip("Place the Wwise event that stops all dialogue here.")]
    public AK.Wwise.Event StopDialogue;
    [Tooltip("Place the WwiseAudio GameObject here.")] 
    public GameObject WwiseAudioObject;
    /*The WwiseAudio GameObject is needed here for two reasons:
     * 1. If called on the narrative object, stop events either do not work all the time or don't work across scenes; and
     * 2. Although we could just change the event in Wwise to be "global", this way preserves it in Unity for future audio implementers, and
     * 3. Is better practice than using the global setting for an event. -Crystal Lee, I Didn't Go to UMich '20 */

    [Space]

    public List<NarrativeData> narrativeDatas;

    public UnityEvent OnDialogueEnd;

    [Space]
    [Header("Settings")]
    public Vector2 offScreenPosition;
    public bool showNarrativeOnStart = true;

    

    // Start is called before the first frame update
    void Start()
    {
        if (showNarrativeOnStart)
            StartNarrative();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNarrative()
    {
        StartCoroutine(ShowEntireNarrative());
    }

    public IEnumerator ShowEntireNarrative()
    {
        for(int i = 0; i < narrativeDatas.Count; i++)
        {
            yield return StartCoroutine(ShowNarrativeData(narrativeDatas[i]));
            StopDialogue.Post(WwiseAudioObject);

        }
        OnDialogueEnd.Invoke();
    }

    IEnumerator ShowNarrativeData(NarrativeData data)
    {
        // Setup Text and Images
        SetupNarrativeData(data);

        // Do Transition
        switch(data.transitionType)
        {
            case NarrativeData.TransitionType.None: 
                break;
            case NarrativeData.TransitionType.Fade:
                yield return StartCoroutine(data.DoFadeTransition(this));
                break;
            case NarrativeData.TransitionType.Slide:
                yield return StartCoroutine(data.DoSlideTransition(this));
                break;
        }

        yield return null;

        // Do Dialogue
        IEnumerator textEnumerator = StringFunctions.FillText(data.text, ChangeDialogueText, 
            charactersPerSecond);
        Coroutine textCoroutine = StartCoroutine(textEnumerator);
        data.AssociatedSFX.Post(WwiseAudioObject);

        // Wait until either end of narrative or a mouse click
        float narrativeTime = data.text.Length / charactersPerSecond;
        float currentTime = 0;

        while(currentTime <= narrativeTime)
        {
            currentTime += Time.unscaledDeltaTime;

            if(Mouse.current.leftButton.wasReleasedThisFrame)
            {
                StopCoroutine(textEnumerator);
                ChangeDialogueText(data.text);
                break;
            }

            yield return null;
        }
        // Wait for click to advance scene
        yield return new WaitForSecondsRealtime(0.01f); // make sure to not be affected by a pause
        IEnumerator clickDetect = CoroutineUtilities.WaitForClick();
        yield return StartCoroutine(clickDetect);
        
    }

    void SetupNarrativeData(NarrativeData data)
    {
        if(characterNameWindow != null)
        {
            characterNameWindow.SetActive(data.characterName.Length != 0);
        }
        if(characterNameText != null)
        {
            characterNameText.text = data.characterName;
        }
        if(backgroundImage != null)
        {
            backgroundImage.sprite = data.backgroundSprite;
        }
        if(characterImage != null)
        {
            characterImage.gameObject.SetActive(data.characterSprite != null);
            characterImage.sprite = data.characterSprite;
        }
        dialogueText.text = "";
    }

    // Callback function
    void ChangeDialogueText(string text)
    {
        dialogueText.text = text;
    }
}
