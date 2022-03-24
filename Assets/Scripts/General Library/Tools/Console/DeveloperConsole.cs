using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * A class containing the functionality of a console
 * Intended to be separate from the mono-behaviour that controls it
 * Written by Brandon Schulz '22, William Bostick '20
 */

public class DeveloperConsole
{
    private readonly IEnumerable<ConsoleInterface> commands;

    public DeveloperConsole(IEnumerable<ConsoleInterface> commands)
    {
        this.commands = commands;
    }

    public void ProcessCommand(string inputValue)
    {
        string[] inputSplit = inputValue.Trim().Split(' ');

        string commandInput = inputSplit[0];
        string[] args = inputSplit.Skip(1).ToArray();
        ProcessCommand(commandInput, args);
    }

    public void ProcessCommand(string commandInput, string[] args)
    {
        foreach (var command in commands)
        {
            if (!commandInput.Equals(command.CommandWord, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (command.Process(args))
            {
                return;
            }
        }
    }

    public List<string> AutoComplete(string inputValue)
    {
        string[] inputSplit = inputValue.Split(' ');
        string commandInput = inputSplit[0];

        List<string> matchingCommands = new List<string>();

        if (inputSplit.Length == 1)
        {

            foreach (var command in commands)
            {
                if (command.CommandWord.StartsWith(commandInput))
                {
                    matchingCommands.Add(command.CommandWord);
                }
            }

            return matchingCommands;
        }
        else
        {
            string[] args = inputSplit.Skip(1).ToArray();

            foreach (var command in commands)
            {
                if (command.CommandWord == commandInput)
                {
                    List<string> matchingArgs = command.AutoComplete(args);
                    foreach (string commandArg in matchingArgs)
                    {
                        matchingCommands.Add(command.CommandWord + " " + commandArg);
                    }
                }
            }

            return matchingCommands;
        }
    }
}