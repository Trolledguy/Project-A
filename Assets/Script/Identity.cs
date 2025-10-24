using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public abstract class Identity : MonoBehaviour
{

    private Collider icollider;
    protected Rigidbody eRigi;
    protected Vector3 position;


    public virtual void Interacted() { }

    protected virtual void Initialized()
    {
        icollider = GetComponent<Collider>();
        eRigi = GetComponent<Rigidbody>();
        Debug.Log($"{eRigi} \n {icollider}");
    }


}
