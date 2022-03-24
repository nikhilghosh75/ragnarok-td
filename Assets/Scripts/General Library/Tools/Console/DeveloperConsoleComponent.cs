using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/*
 * A MonoBehaviour enabling a development console in the game
 * Written by Brandon Schulz '22, William Bostick '20, Nikhil Ghosh '24
 */

public class DeveloperConsoleComponent : MonoBehaviour
{
    private List<ConsoleCommand> commands = new List<ConsoleCommand>()
    {
        new ChangeSceneCommand(),
        new HelpCommand(),
        new LogCommand(),
    };

    #region Public Variables
    public UnityEvent OnConsoleClose;
    public UnityEvent OnConsoleOpen;
    #endregion

    #region Serialized Variables
    [Header("UI")]
    [SerializeField] private GameObject uiCanvas = null;
    [SerializeField] private TMP_InputField inputField = null;
    [SerializeField] private TMP_Text autocompleteText = null;

    [Header("Input")]
    public Key openKey;
    public Key closeKey;
    public Key autocompleteKey;
    #endregion

    #region Private Variables (and Constructor)
    private float pausedTimeScale;
    private static DeveloperConsoleComponent instance;
    private DeveloperConsole developerConsole;
    private DeveloperConsole DeveloperConsole
    {
        get
        {
            if (developerConsole != null) { return developerConsole; }
            return developerConsole = new DeveloperConsole(commands);
        }
    }
    #endregion

    private void Awake()
    {
        // playerInput = GetComponentInParent<PlayerInputController>();
        // aimAndShooting = GetComponentInParent<PlayerAimingAndShooting>();
        inputField.onEndEdit.AddListener(ProcessCommand);
        inputField.onValueChanged.AddListener(AutoComplete);
    }

    private void Start()
    {
        uiCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        // close
        if (Keyboard.current[closeKey].wasPressedThisFrame && uiCanvas.activeSelf)
        {
            CloseConsole();

        }
        // open
        else if (Keyboard.current[openKey].wasPressedThisFrame && uiCanvas.activeSelf == false)
        {
            OpenConsole();
            AutoComplete(inputField.text);
        }

        if (Keyboard.current[autocompleteKey].wasPressedThisFrame && uiCanvas.activeSelf)
        {
            List<string> autoCompletedMatches = DeveloperConsole.AutoComplete(inputField.text);
            if (autoCompletedMatches.Count != 0)
            {
                inputField.text = autoCompletedMatches[0] + " ";
                inputField.caretPosition = inputField.text.Length;
            }
        }
    }

    private void CloseConsole()
    {
        uiCanvas.SetActive(false);

        if (!WSoft.Core.GameManager.gamePaused)
        {
            Time.timeScale = pausedTimeScale;

            OnConsoleClose.Invoke();
        }
    }

    private void OpenConsole()
    {
        pausedTimeScale = Time.timeScale;
        Time.timeScale = 0;
        uiCanvas.SetActive(true);
        inputField.ActivateInputField();

        OnConsoleOpen.Invoke();
    }

    public void ProcessCommand(string inputValue)
    {
        DeveloperConsole.ProcessCommand(inputValue);

        inputField.text = string.Empty;
        CloseConsole();
    }

    public void AutoComplete(string value)
    {
        List<string> matchingCommands = DeveloperConsole.AutoComplete(value);
        string autocompleteString = "";
        foreach (string matchingCommand in matchingCommands)
        {
            autocompleteString += matchingCommand + "\n";
        }

        autocompleteText.text = autocompleteString;
    }

    public void AddCommand(ConsoleCommand newCommand)
    {
        commands.Add(newCommand);
    }
}