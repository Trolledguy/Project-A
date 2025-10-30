using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity
{
    public Camera cam;
    public Vector3 camOffset;
    public float speed;

    [SerializeField]
    private bool ishided = false;
    [SerializeField]
    private bool isTranslating = false;

    public Transform holdPos1;
    public Transform holdPos2;
    public Transform middlePos;

    public Item[] HoldedItem = new Item[2];

    const float defaultHieght = 2;


    void Start()
    {
        SetupPlayer();
    }

    public void Move(float MoveZ, float MoveX)
    {
        Vector3 forword = transform.forward * MoveZ * speed * Time.deltaTime;
        Vector3 right = transform.right * MoveX * speed * Time.deltaTime;

        Vector3 moveForce = right + forword;
        transform.position += transform.forward * MoveZ * 0.5f * Time.deltaTime;
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

    }
    public override void Interact(HideSpot hideSpot)
    {
        if (ishided == false)
        {
            hideSpot.ObjectInteract(this);
            ishided = true;
        }
        else
        {
            hideSpot.UnHidePlayer();
            ishided = false;
        }
    }

    public void TriggerFreeze()
    {
        eRigi.isKinematic = !eRigi.isKinematic;
    }
    //set player Freeze
    public void TriggerFreeze(bool setKenetic)
    {
        eRigi.isKinematic = setKenetic;
    }

    public bool IsPlayerTranslating() { return isTranslating; }

    public override void Interact(Item _item)
    {
        throw new System.NotImplementedException();
    }
    public IEnumerator TranslatePlayerPos(Vector3 targetPos, float _time)
    {
        while (Vector3.Distance(transform.position, targetPos) > 0.15f)
        {
            isTranslating = true;
            transform.position = Vector3.Lerp(transform.position, targetPos, _time * Time.deltaTime);
            yield return null;
        }
        isTranslating = false;
        yield return null;
    }

    public IEnumerator TranslatePlayerPos(Vector3 targetPos, Vector3 targetCamRotate, float _time)
    {
        while (Vector3.Distance(transform.position, targetPos) > 0.15f)
        {
            isTranslating = true;
            transform.position = Vector3.Lerp(transform.position, targetPos, _time * Time.deltaTime);
            cam.transform.localEulerAngles = Vector3.Lerp(cam.transform.localEulerAngles, targetCamRotate, _time * Time.deltaTime);
            yield return null;
        }
        isTranslating = false;
        yield return null;
    }

    public bool IsplayerHiding() { return ishided; }

    public void Dead()
    {
        //Trigger JumpScare
        UIManager.instance.TriggerJumpScare("NemesisJC");
        Debug.Log("!!! PLAYER DEAD");
    }

    private void SetupPlayer()
    {
        base.Initialized();
        cam = Camera.main;
        cam.transform.SetParent(null);
    }

}
