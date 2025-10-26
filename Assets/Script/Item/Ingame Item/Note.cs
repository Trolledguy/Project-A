using UnityEngine;

public class Note : Item
{
    public override void ObjectInteract()
    {
        throw new System.NotImplementedException();
    }
    public override void Holding(Transform holdPos, float _time)
    {
        base.Holding(holdPos, _time);
        transform.position = Vector3.Lerp(transform.position, holdPos.position, _time);
        icollider.isTrigger = true;
        transform.SetParent(holdPos);
    }
}
