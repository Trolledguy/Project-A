using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public abstract class Identity : MonoBehaviour
{
    protected Collider icollider;
    protected Rigidbody eRigi;

    protected float positionX
    {
        get { return transform.position.x; }
        set { transform.position = new Vector3(value, positionY, positionZ); }
    }
    protected float positionY
    {
        get { return transform.position.y; }
        set { transform.position = new Vector3(positionX, value, positionZ); }
    }
    protected float positionZ
    {
        get { return transform.position.z; }
        set { transform.position = new Vector3(positionX, positionY, value); }
    }
    public Player player;

    protected virtual void Initialized()
    {
        player = GameObject.FindAnyObjectByType<Player>();
        icollider = GetComponent<Collider>();
        eRigi = GetComponent<Rigidbody>();

        eRigi.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
    }

    public Vector2 GetPlayerPosition()
    {
        // Step 1: หาทิศทาง A → B
        Vector3 dir = player.transform.position - transform.position;
        dir.y = 0;

         // Step 2: แปลงให้เป็นทิศทางใน local ของ objA
        Vector3 localDir = transform.InverseTransformDirection(dir.normalized);

        // Step 3: แปลงเป็น Vector2 (Z = Y)
        Vector2 relativePos = new Vector2(localDir.x, localDir.z);

        // Step 4: จำกัดค่าในช่วง -1 ถึง 1
        relativePos = Vector2.ClampMagnitude(relativePos, 1f);

        return relativePos;
    }

}
