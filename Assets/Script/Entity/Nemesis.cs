using UnityEngine;


public class Nemesis : Monster
{
    void Start()
    {
        base.Initialized();
    }
    void Update()
    {
        
    }
    public override void Interact()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnPlayerHit()
    {
        throw new System.NotImplementedException();
    }
}
