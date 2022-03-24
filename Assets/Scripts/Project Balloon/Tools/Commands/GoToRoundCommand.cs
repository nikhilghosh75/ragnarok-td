using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToRoundCommand : ConsoleCommand
{
    public GoToRoundCommand()
    {
        commandWord = "gotoround";
    }

    public override bool Process(string[] args)
    {
        if(args.Length != 1)
        {
            Debug.LogError("GoToRound requires a round number as an arguments");
            return false;
        }

        int roundToGoTo = int.Parse(args[0]) - 1;
        int maxRounds = EnemySpawner.Get().GetMaxRounds();
        if(roundToGoTo > maxRounds)
        {
            Debug.LogError("Arg 0 is greater than the max round");
            return false;
        }

        EnemySpawner.Get().GoToRound(roundToGoTo);

        return true;
    }

    public override List<string> GetValidArgs()
    {
        return new List<string>();
    }

    public override List<string> AutoComplete(string[] args)
    {
        List<string> newArgs = new List<string>();
        int maxRounds = EnemySpawner.Get().GetMaxRounds();
        for(int i = 5; i < maxRounds; i += 5)
        {
            newArgs.Add(i.ToString());
        }

        return newArgs;
    }
}
