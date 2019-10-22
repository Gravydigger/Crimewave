using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{

    public string nameIdentifier;

    [TextArea(1, 3)]
    public string[] sentences;

    public float[] sentenceDisplayTime;
}
