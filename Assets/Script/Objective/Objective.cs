using UnityEngine;

[System.Serializable]
public class Objective
{
    public string objectiveID;   // เช่น "PickUpApple01"
    public string description;   // เช่น "เก็บแอปเปิ้ลจากโต๊ะ"
    public bool isCompleted = false;
    
}