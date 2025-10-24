using UnityEngine;


public class Nemesis : Monster, IMovable
{
    void Start()
    {
        base.Initialized();
    }
    void Update()
    {
        ControlSprite();
    }

    protected override void OnPlayerHit()
    {
        throw new System.NotImplementedException();
    }
}
