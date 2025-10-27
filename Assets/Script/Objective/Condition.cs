using UnityEngine;

[CreateAssetMenu(fileName ="Condition",menuName = "Objective System/Condition")]
public class Condition : ScriptableObject
{
    public Objective linkObjective;
    public virtual void PassCondition()
    {

        //Call after execute condition Code
        
    }
}
