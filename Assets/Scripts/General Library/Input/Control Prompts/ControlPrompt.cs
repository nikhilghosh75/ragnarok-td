/* Stores the display name and control sprite of an input action.
 * @Zhenyuan Zhang
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Control Prompt", menuName = "WSoft/Control Prompt/Control Prompt")]
public class ControlPrompt : ScriptableObject
{
    public string actionName;
    public string displayName;
    public Sprite sprite;
}
