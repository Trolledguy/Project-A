using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class Monster : Entity , ISprite2D
{
    protected SpriteLoader spLoader;
    public NavMeshAgent ai;
    public SphereCollider Checkcolli;
    public Player seenPlayer;

    private Vector3 goingTo;
    public bool sawPlayer = false;
    protected float timer = 0;
    
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

    public virtual void CheckHide()
    {
        LayerMask hidespot = LayerMask.GetMask("HideSpot");
        RaycastHit[] hideinfo = Physics.SphereCastAll(transform.TransformPoint(Checkcolli.center), Checkcolli.radius,
        transform.forward, 4.29f, hidespot);

        if (hideinfo.Length > 0)
        {
            Debug.Log("Detect HideSpot");
            foreach (RaycastHit hideSpotHit in hideinfo)
            {
                int isCheck = UnityEngine.Random.Range(0, 100000);
                if (isCheck < 50)
                {
                    Debug.Log("Going Check");
                    HideSpot spot = hideSpotHit.collider.GetComponent<HideSpot>();
                    MonsterMove(spot.transform);
                    if (Vector3.Distance(transform.position, spot.transform.position) < 2f)
                    {
                        Interact(spot);
                        return;
                    }

                }
                else { return; }
            }
        }
    }
    public virtual void FollowPlayer()
    {
        RaycastHit lineOfSight;

        MonsterMove(seenPlayer.transform);
        if (Physics.Raycast(transform.position, seenPlayer.transform.position, out lineOfSight))
        {
            if (!lineOfSight.collider.GetComponent<Player>())
            {
                timer += Time.deltaTime;
                if (timer > 5)
                {
                    MonsterMove(transform);
                    sawPlayer = false;
                    seenPlayer = null;
                    timer = 0;
                }
            }
            else { timer = 0; }
        }
    }


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
