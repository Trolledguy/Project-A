using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Checker : MonoBehaviour
{
    Monster monster;
    Player seenPlayer;

    SphereCollider ccollider;
    LayerMask playerMask;
    float timer = 0;
    void Awake()
    {
        monster = GetComponentInParent<Monster>();
        ccollider = GetComponent<SphereCollider>();
        ccollider.isTrigger = true;

        playerMask = LayerMask.GetMask("Player");
    }

    void Update()
    {
        if (monster.sawPlayer)
        {
            FollowPlayer();
        }
        else
        {
            CheckHide();
        }
    }


    void OnTriggerStay(Collider other)
    {
        if (monster.sawPlayer) { return; }

        if (other.gameObject.GetComponent<Player>())
        {
            // Raycast หาผู้เล่น (จากมอนสเตอร์ไปหาผู้เล่น)
            RaycastHit hit;
            Vector3 dir = (other.transform.position - monster.transform.position).normalized;

            if (Physics.Raycast(monster.transform.position, dir, out hit,100f,playerMask))
            {
                Player player = hit.collider.GetComponent<Player>();
                if (player.IsplayerHiding() && seenPlayer == null)
                {
                    return;
                }

                if (hit.collider.GetComponent<Player>())
                {
                    Debug.Log("See player");
                    timer += Time.deltaTime;

                    if (timer > 3f)
                    {
                        monster.MonsterMove(hit.transform);
                        monster.sawPlayer = true;
                        seenPlayer = hit.collider.GetComponent<Player>();
                        timer = 0f;
                    }
                }
                else
                {
                    // ถ้าเจออะไรบังระหว่างทาง — reset timer
                    timer = 0f;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            timer = 0f;
        }
    }

    private void CheckHide()
    {
        LayerMask hidespot = LayerMask.GetMask("HideSpot");
        RaycastHit[] hideinfo = Physics.SphereCastAll(monster.transform.TransformPoint(ccollider.center), ccollider.radius,
        monster.transform.forward, 4.29f, hidespot);
        
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
                    monster.MonsterMove(spot.transform);
                    if (Vector3.Distance(monster.transform.position, spot.transform.position) < 2f)
                    {
                        monster.Interact(spot);
                        return;
                    }
                    
                }
                else { return; }
            }
        }
    }

    void FollowPlayer()
    {
        RaycastHit lineOfSight;

        monster.MonsterMove(seenPlayer.transform);
        if (Physics.Raycast(monster.transform.position, seenPlayer.transform.position, out lineOfSight))
        {
            if (!lineOfSight.collider.GetComponent<Player>())
            {
                timer += Time.deltaTime;
                if (timer > 5)
                {
                    monster.MonsterMove(monster.transform);
                    monster.sawPlayer = false;
                    seenPlayer = null;
                    timer = 0;
                }
            }
            else { timer = 0; }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        
        try
        {
            Gizmos.DrawLine(monster.transform.position, GameManager.intance.mPlayer.transform.position);
            Gizmos.DrawWireSphere(monster.transform.TransformPoint(ccollider.center),ccollider.radius);
        }
        catch (NullReferenceException) { return; }
    }
}
