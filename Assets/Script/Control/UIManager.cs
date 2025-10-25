using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    void Start()
    {
        SetUp();
    }
    private void SetUp()
    {
        if (instance != this)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }
}
