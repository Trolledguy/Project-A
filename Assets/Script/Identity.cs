using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public abstract class Identity : MonoBehaviour
{
    private Collider icollider;
    protected Rigidbody eRigi;
    protected Vector3 position;
    public Player player;

    protected virtual void Initialized()
    {
        player = GameObject.FindAnyObjectByType<Player>();
        icollider = GetComponent<Collider>();
        eRigi = GetComponent<Rigidbody>();

        eRigi.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
    }


}
