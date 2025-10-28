using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class Monster : Entity , ISprite2D
{
    protected SpriteLoader spLoader;
    public NavMeshAgent ai;
    public Collider Checkcolli;

    private Vector3 goingTo;
    public bool sawPlayer = false;
    
    public SpriteRenderer sprite
    {
        get
        {
            if (spLoader != null && spLoader.GetComponent<SpriteLoader>())
            {
                return spLoader.sRenderer;
            }
            else
            {
                Debug.LogWarning("Sprite Loader Fail");
                return null;
            }
        }
        
        set
        {
            if (value != null)
            {
                spLoader.sRenderer = value;
            }
        }
    }

    protected abstract IEnumerator MonsterBeavior();


    public virtual void MonsterMove(Transform _destinate)
    {
        ai.ResetPath();
        ai.SetDestination(_destinate.position);
        goingTo = _destinate.position;
    }

    protected abstract void OnCollisionEnter(Collision collision);
    
    protected abstract void OnPlayerHit(Player _player);
    protected override void Initialized()
    {
        base.Initialized();
        spLoader = GetComponentInChildren<SpriteLoader>();
        Physics.IgnoreCollision(icollider, Checkcolli);
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.darkRed;
        Gizmos.DrawWireCube(this.transform.position - new Vector3(0, -0.03f, 0), new Vector3(1, 1.5f, 0.51f));
        try
        {
            Gizmos.DrawWireSphere(goingTo,0.25f);
            Gizmos.DrawLine(transform.position,goingTo);
        }
        catch (NullReferenceException) { return; }

    }
}
