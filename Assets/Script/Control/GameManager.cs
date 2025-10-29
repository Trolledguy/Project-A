using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager intance;
    public Player mPlayer;

    public GameObject[] exploreNode;
    public bool isGamePause = false;

    public Monster[] allmons;
    
    

    void Awake()
    {
        SetUp();
    }

    

    public void TriggerGamePause()
    {
        isGamePause = !isGamePause;
        if (isGamePause)
        {
            Time.timeScale = 0;
            mPlayer.TriggerFreeze();
        }
        else
        {
            Time.timeScale = 1f;
            mPlayer.TriggerFreeze(false);
        }

    }

    public Transform GetDestination()
    {
        int randomDes = Random.Range(0, exploreNode.Length);
        return exploreNode[randomDes].transform;
    }

    private void SetUp()
    {
        if (intance != this)
        {
            intance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log($"{GameManager.intance}");
        }
        else { Destroy(gameObject); }

        mPlayer = GameObject.FindAnyObjectByType<Player>();
        exploreNode = GameObject.FindGameObjectsWithTag("Explore Node");
    }

}
