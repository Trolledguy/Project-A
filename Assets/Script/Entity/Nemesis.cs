using System.Collections;
using UnityEngine;


public class Nemesis : Monster
{
    float timer = 0;
    void Start()
    {
        StartCoroutine(MonsterBeavior());
        base.Initialized();
    }

    protected override IEnumerator MonsterBeavior()
    {
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > 30)
            {
                MonsterMove(GameManager.intance.GetDestination());
                timer = 0;
                yield return null;
            }
            
            yield return null;
        }
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        Player hitPlayer = collision.collider.GetComponent<Player>();
        if (collision.collider.GetComponent<Player>())
        {
            OnPlayerHit(hitPlayer);
        }
        
    }

    public override void Interact(HideSpot _hidespot)
    {
        if (_hidespot.hiddingPlayer != null)
        {
            _hidespot.hiddingPlayer.Dead();
        }
    }
    public override void Interact(Item _item)
    {
        throw new System.NotImplementedException();
    }

    protected override void OnPlayerHit(Player _player)
    {
        _player.Dead();
    }
}
