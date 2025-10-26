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
    private bool isCouching = false;

    void Start()
    {
        Setup();
        
    }
    void Update()
    {
        InputControl();
    }

    private void InputControl()
    {
        CamControl();
        InteractControl();
    
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
        if (Input.GetMouseButton(0))
        {
            CheckFront(player.holdPos1,0);
        }
        else if (Input.GetMouseButtonUp(0)) { ResetHold(0); }
        if (Input.GetMouseButton(1))
        {
            CheckFront(player.holdPos2, 1);
        }
        else if (Input.GetMouseButtonUp(1)) { ResetHold(1); }
    }

    private void CheckFront(Transform hand, int whichHand)
    {
        RaycastHit _hit;
        Transform originPoint = player.cam.transform;
        Physics.Raycast(originPoint.position, originPoint.forward, out _hit, interactDistance);

        if (player.HoldedItem[whichHand] != null)
        {
            return;
        }
        try
        {
            if (_hit.collider.gameObject.GetComponent<Item>())
            {
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
                player.HoldedItem[whichHand] = item;
                isHandBusy[whichHand] = true;
                item.Holding(hand, floatSpeed);
                Debug.Log("this is Item");
            }
        }
        catch (NullReferenceException) { return; } //Prevent interact Check null Error
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

        float mouseX = Input.GetAxis("Mouse X") * mouseSen * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSen * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        player.cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        player.transform.Rotate(Vector3.up * mouseX);
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

}
