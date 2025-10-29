using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Checker : MonoBehaviour
{
    Monster monster;
    SphereCollider ccollider;
    LayerMask playerMask;
    float checkerTimer = 0;
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
            monster.FollowPlayer();
        }
        else
        {
            monster.CheckHide();
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
                if (player.IsplayerHiding() && monster.seenPlayer == null)
                {
                    return;
                }

                if (hit.collider.GetComponent<Player>())
                {
                    Debug.Log("See player");
                    checkerTimer += Time.deltaTime;

                    if (checkerTimer > 3f)
                    {
                        monster.MonsterMove(hit.transform);
                        monster.sawPlayer = true;
                        monster.seenPlayer = hit.collider.GetComponent<Player>();
                        checkerTimer = 0f;
                    }
                }
                else
                {
                    // ถ้าเจออะไรบังระหว่างทาง — reset timer
                    checkerTimer = 0f;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            checkerTimer = 0f;
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
