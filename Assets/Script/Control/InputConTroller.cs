using System;
using Unity.Mathematics;
using UnityEngine;

public class InputConTroller : MonoBehaviour
{
    public Player player;

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
    
        float iZ = Input.GetAxis("MoveZ");
        float iX = Input.GetAxis("MoveX");
        if (Input.GetButton("MoveZ") || Input.GetButton("MoveX"))
        {
            player.Move(iZ, iX);
        }

        isCouching = Input.GetKey(KeyCode.LeftControl);
        player.Couch(isCouching);
        Debug.Log(isCouching);
        
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
