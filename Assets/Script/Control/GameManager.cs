using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager intance;
    public Player mPlayer;


    public Objective[] gameMission;
    private int currectMission = 0;

    void Start()
    {
        SetUp();
    }

    public void NextMission() 
    { currectMission++; }


    private void SetUp()
    {
        if (intance != this)
        {
            intance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }

        mPlayer = GameObject.FindAnyObjectByType<Player>();
    }

}
