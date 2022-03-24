using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * Changes the scene to a given scene
 * Current just returns to the MainMenu, but will change in the future
 * Written by Nikhil Ghosh '24, Brandon Schulz '22, William Bostick '20
 */

// TO-DO: Make Command System more modular

public class ChangeSceneCommand : ConsoleCommand
{
    public ChangeSceneCommand()
    {
        commandWord = "changescene";
    }

    public override bool Process(string[] args)
    {
        if (args[0].Equals("MainMenu", StringComparison.OrdinalIgnoreCase))
        {
            WSoft.Core.GameManager.ReturnToMainMenu();
            return true;
        }

        return false;
    }

    public override List<string> GetValidArgs() { return new List<string>() { "MainMenu" }; }
}