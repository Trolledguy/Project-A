using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity
{
    public Camera cam;
    public Vector3 camOffset;
    public float speed;

    const float defaultHieght = 2;


    void Start()
    {
        SetupPlayer();
    }

    public override void Move(float MoveZ, float MoveX)
    {
        Vector3 forword = transform.forward * MoveZ * speed * Time.deltaTime;
        Vector3 right = transform.right * MoveX * speed * Time.deltaTime;

        Vector3 moveForce = right + forword;
        eRigi.AddForce(moveForce, ForceMode.VelocityChange);
        /*
        transform.position += transform.forward * MoveZ * speed * Time.deltaTime;
        transform.position += transform.right * MoveX * speed * Time.deltaTime;
        */
    }
    public void Couch(bool isCouching)
    {
        CapsuleCollider colli = icollider.GetComponent<CapsuleCollider>();
        if (isCouching)
        {
            colli.height = defaultHieght / 2;
        }
        else { colli.height = defaultHieght; }
        
        

        
    }

    public override void Interact()
    {
        throw new System.NotImplementedException();
    }

    private void SetupPlayer()
    {
        base.Initialized();
        cam = Camera.main;
    }

}
