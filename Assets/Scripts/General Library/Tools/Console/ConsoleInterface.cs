using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * I'm not entirely sure why this exists, oh well
 * Written by Brandon Schulz '22, William Bostick '20
 */

public interface ConsoleInterface
{
    string CommandWord { get; }
    bool Process(string[] args);
    List<string> AutoComplete(string[] args);
    List<string> GetValidArgs();
}