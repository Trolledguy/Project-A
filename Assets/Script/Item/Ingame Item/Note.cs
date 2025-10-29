using UnityEngine;

public class Note : Item
{
    public override void ObjectInteract(Player player)
    {
        return;
    }
    public override void Holding(Transform holdPos, float _time)
    {
        base.Holding(holdPos, _time);
        
    }
}
