using UnityEngine;


[CreateAssetMenu(fileName = "Objective", menuName = "Objective System/Objective")]
public class Objective : ScriptableObject
{

    public string missionName;
    public string description;
    private bool isDone = false;

    public Condition condition;

    public Objective()
    {
        condition.linkObjective = this;
    }

    public void PassMission()
    {
        isDone = true;
        GameManager.intance.NextMission();
    }

}
