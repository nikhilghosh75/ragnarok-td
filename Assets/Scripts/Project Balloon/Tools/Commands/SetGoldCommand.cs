using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGoldCommand : ConsoleCommand
{
    public SetGoldCommand()
    {
        commandWord = "setgold";
    }

    public override bool Process(string[] args)
    {
        if (args.Length != 1)
        {
            Debug.LogError("SetGold requires a gold amount as an argument");
            return false;
        }

        int goldToSet = int.Parse(args[0]);
        PlayerResources.Get().SetCurrency(goldToSet);

        return true;
    }

    public override List<string> GetValidArgs()
    {
        return new List<string>();
    }

    public override List<string> AutoComplete(string[] args)
    {
        List<string> newArgs = new List<string>();
        newArgs.Add("100");
        newArgs.Add("500");
        newArgs.Add("1000");
        newArgs.Add("5000");

        return newArgs;
    }
}
