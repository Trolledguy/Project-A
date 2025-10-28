using System;
using UnityEngine;


[CreateAssetMenu(fileName = "Objective", menuName = "Objective System/Objective")]
public class Objective : ScriptableObject
{

    public string missionName;
    public string description;
    private bool isDone = false;

    public Condition condition;

    void Awake()
    {
        SetUpMission();
    }

    private void SetUpMission()
    {
        try
        {
            condition.linkObjective = this;
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Warning : Objective is MISSION CPNDITION");    
            return; 
        }
    }
    public void PassMission()
    {
        isDone = true;
        GameManager.intance.NextMission();
    }

}
