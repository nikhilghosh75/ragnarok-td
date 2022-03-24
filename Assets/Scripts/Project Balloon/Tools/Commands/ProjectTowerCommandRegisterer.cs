using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTowerCommandRegisterer : MonoBehaviour
{
    private void Awake()
    {
        DeveloperConsoleComponent consoleComponent = GetComponent<DeveloperConsoleComponent>();
        consoleComponent.AddCommand(new GoToRoundCommand());
        consoleComponent.AddCommand(new SetGoldCommand());
    }
}
