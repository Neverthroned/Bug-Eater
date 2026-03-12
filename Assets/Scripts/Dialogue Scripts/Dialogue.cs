using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string characterName;   // renamed from 'name' to avoid System.Object conflict
    [TextArea(3, 10)]
    public string[] sentences;
}