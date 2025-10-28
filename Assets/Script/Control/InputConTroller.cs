using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class InputConTroller : MonoBehaviour
{
    public Player player;
    public float interactDistance;
    public float floatSpeed;

    private bool[] isHandBusy = new bool[2];

    /// <summary>
    /// Mouse Setting
    /// </summary>

    public float mouseSensitivity
    {
        get { return mouseSen; }
        set
        {
            if (value <= 0)
            { return; }
            else
            { mouseSen = value; }
        }
    }

    ///
    private float mouseSen = 100f;
    private float xRotation = 0f;
    private float yRotation = 0f;

    private bool isCouching = false;

    void Start()
    {
        Setup();
        
    }
    void Update()
    {
        if (GameManager.intance.isGamePause) { return; }
        InputControl();
    }

    private void InputControl()
    {
        
        InteractControl();

        //prevent player move while translating
        if (player.IsPlayerTranslating()) { return; }

        CamControl();
    
        float iZ = Input.GetAxis("MoveZ");
        float iX = Input.GetAxis("MoveX");
        if (Input.GetButton("MoveZ") || Input.GetButton("MoveX"))
        {
            player.Move(iZ, iX);
        }

        isCouching = Input.GetKey(KeyCode.LeftControl);
        player.Couch(isCouching);        
    }

    private void InteractControl()
    {
        ///
        /// Control mouse or item system
        /// 
        if (Input.GetMouseButton(0))
        {
            CheckFront(player.holdPos1, 0);
        }
        else if (Input.GetMouseButtonUp(0)) { ResetHold(0); }
        if (Input.GetMouseButton(1))
        {
            CheckFront(player.holdPos2, 1);
        }
        else if (Input.GetMouseButtonUp(1)) { ResetHold(1); }

        ///
        /// Hide Control
        /// 

        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hideSpotCheck = CheckFront();
            try
            {
                if (hideSpotCheck.collider.GetComponent<HideSpot>())
                {
                    HideSpot hideSpot = hideSpotCheck.collider.GetComponent<HideSpot>();
                    player.Interact(hideSpot);
                }
            }
            catch (NullReferenceException) { return; }
            
        }
    }
    //use this if player interact with Object
    private RaycastHit CheckFront()
    {
        RaycastHit hitCheck;
        Transform originPoint = player.cam.transform;
        Physics.Raycast(originPoint.position, originPoint.forward, out hitCheck, interactDistance);

        return hitCheck;
    }
    //Use this method if player going to pick Item (mouse click)
    private void CheckFront(Transform hand, int whichHand)
    {
        RaycastHit hit = CheckFront();
        try
        {
            if (hit.collider.GetComponent<Item>())
            {
                ItemHold(hit, hand, whichHand);
            }
        }
        catch (NullReferenceException) { return; }
    }

    private void ItemHold(RaycastHit _hit,Transform _hand, int _whichHand)
    {
        if (player.HoldedItem[_whichHand] != null)
        {
            return;
        }
    
        Item item = _hit.collider.GetComponent<Item>();
        //if a note
        if (item.GetComponent<Note>())
        {
            Debug.Log("this a note");
            Note note = item.GetComponent<Note>();
            for (int i = 0; i < player.HoldedItem.Length; i++)
            {
                ResetHold(i);
                isHandBusy[i] = true;
                player.HoldedItem[i] = note;
            }
            note.Holding(player.middlePos, floatSpeed);
                    
            return;
        }
        //if a regular Item
        player.HoldedItem[_whichHand] = item;
        isHandBusy[_whichHand] = true;
        item.Holding(_hand, floatSpeed);
        Debug.Log("this is Item");
            

    }

    private void ResetHold(int hand)
    {
        try
        {
            player.HoldedItem[hand].Drop();
            player.HoldedItem[hand] = null;
        }
        catch (NullReferenceException) { return; }

    }
    

    private void CamControl()
    {
        player.cam.transform.position = player.transform.position + player.camOffset;

        float mouseX = Input.GetAxis("Mouse X") * mouseSen * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSen * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        yRotation -= mouseX;

        player.cam.transform.localEulerAngles = new Vector3(xRotation,-yRotation,0);
        player.transform.localEulerAngles = new Vector3(0,-yRotation,0);
    }

    private void LockMouse()
    {
        if (Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    private void Setup()
    {
        player = GameObject.FindAnyObjectByType<Player>();
        if (player == null)
        {
            Debug.LogWarning("Player is null");
        }
        LockMouse();
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Transform camPos = Camera.main.transform;

        Ray checkray = new Ray();
        checkray.origin = camPos.position;
        checkray.direction = camPos.forward * 5;

        //Player Check Line
        Gizmos.DrawRay(checkray);
    }


}
