using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject menu;
    public GameObject setting;
    public GameObject guide;
    public GameObject credit;
    public GameObject jumpScene;
    public GameObject pasue;

    //
    public Button startButt;
    public Button settingButt;
    public Button creditButt;
    public Button exitButt;
    public Button backwordButt;

    private GameObject openedUI;



    //



    public GameObject deadScene;
    public Animator animator;

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

        //Time.timeScale = 0;
        SetUpButton();
        animator = jumpScene.GetComponentInChildren<Animator>();

        jumpScene.SetActive(false);
        deadScene.SetActive(false);
    }
    private void SetUpButton()
    {
        startButt.onClick.AddListener(delegate
        {
            menu.SetActive(false);
            Time.timeScale = 1;
        });
        settingButt.onClick.AddListener(delegate
        {
            menu.SetActive(false);
            setting.SetActive(true);
            openedUI = setting;
        });
        creditButt.onClick.AddListener(delegate
        {
            menu.SetActive(false);
            credit.SetActive(true);
            openedUI = credit;
        });
        backwordButt.onClick.AddListener(delegate
        {
            menu.SetActive(true);
            openedUI.SetActive(false);
            openedUI = null;
        });
        exitButt.onClick.AddListener(delegate
        {
            Application.Quit();
        });
        


    }


    //Has to include in Animator
    public void TriggerJumpScare(string _jumpSceneName)
    {
        jumpScene.SetActive(true);
        animator.SetTrigger(_jumpSceneName);

        // Wait one frame to allow the Animator to switch to the new clip
        StartCoroutine(PlayJumpScareCoroutine(_jumpSceneName));
    }

    private IEnumerator PlayJumpScareCoroutine(string jcN)
    {
        yield return null;

        AnimatorStateInfo clipInfo = animator.GetCurrentAnimatorStateInfo(0);


        {
            float clipLength = clipInfo.length / animator.speed; // adjusted for animator speed
            Debug.Log($"Current jump scare clip length: {clipLength}");

            yield return new WaitForSeconds(8.267f);

            if (jcN == "NemesisJC")
            {
                deadScene.SetActive(true);
                Time.timeScale = 0;
                Debug.Log("Stop Time");
            }
            else
            {
                jumpScene.SetActive(false);
            }
        }

    }

    public void SetPause(bool _isPause)
    {
        pasue.SetActive(_isPause);
        if (_isPause)
        {
            Time.timeScale = 0;
        }
        else { Time.timeScale = 1; }
    }

}
