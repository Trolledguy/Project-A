using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest System/Quest")]
public class Quest : ScriptableObject
{
    public string questID;
    public string questName;
    public string description;
    public Objective[] objectives = new Objective[2];

    public bool IsCompleted()
    {
        foreach (Objective obj in objectives)
        {
            if (!obj.isCompleted)
                return false;
        }
        return true;
    }
}
