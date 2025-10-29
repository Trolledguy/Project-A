using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager intance;
    public int prayProgress = 0;
    public Quest activeQuest;
    public Quest[] allQuest;


    void Awake()
    {
        SetUp();
    }
    public void NextMission()
    {
        Debug.Log("Next Mission Trigger");
        prayProgress++;

        try
        {
            AddQuest(allQuest[prayProgress]);
            Debug.Log("Add Mission");
        }
        catch (IndexOutOfRangeException) 
        {
            Debug.Log("Final mission Reach");
            return; 
        }
    }

    public void AddQuest(Quest quest)
    {
        activeQuest = null;
        activeQuest = quest;   
    }
    public void CompleteObjective(string objectiveID)
    {
        foreach (Objective obj in activeQuest.objectives)
        {
            if (obj.objectiveID == objectiveID && !obj.isCompleted)
            {
                obj.isCompleted = true;
                Debug.Log($" Objective Complete: {obj.description}");
            }
        }
        if (activeQuest.IsCompleted())
        {
            Debug.Log($" Quest Complete: {activeQuest.questName}");
            NextMission();
        }

    }

    void SetUp()
    {
        if (ObjectiveManager.intance != this)
        {
            ObjectiveManager.intance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }


        SetObjectiveFalse();
        AddQuest(allQuest[prayProgress]);
    }

    void SetObjectiveFalse()
    {
        foreach (Quest quest in allQuest)
        {
            foreach (Objective obj in quest.objectives)
            {
                obj.isCompleted = false;
            }
        }
    }
}
