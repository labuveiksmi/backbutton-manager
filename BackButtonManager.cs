using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonManager : MonoBehaviour
{
    public static BackButtonManager instance;
    private bool stopManaging = false;
    Stack<BackButtonStateSaver> stateSavers = new Stack<BackButtonStateSaver>();
    HR_MainMenuHandler menuHandler;
    private HR_MainMenuHandler GetHandler()
    {
        if (!menuHandler)
        {
            menuHandler = FindObjectOfType<HR_MainMenuHandler>();
        }
        return menuHandler;
    }
    #region properties
    public bool StopManaging
    {
        get
        {
            return stopManaging;
        }

        set
        {
            stopManaging = value;
        }
    } 
    #endregion

    #region singleton

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    } 
    #endregion

    public void AddState(BackButtonStateSaver saver)
    {
        stateSavers.Push(saver);
    }

    public void Close()
    {
        GetClosableCount();
        if (stateSavers.Count == 0)
        {
            PauseOrQuit();
        }
        else
        {
            stateSavers.Pop().gameObject.SetActive(false);
        }
    }

    private void PauseOrQuit()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            GetHandler().QuitGame();
        }
        else
        {
            HR_GamePlayHandler.Instance.Paused();
        } 
    }

    public int GetClosableCount()
    {
        int count = stateSavers.Count;
        if (count > 0 && !stateSavers.Peek())
        {
            for(int i = 0; i < count; i++)
            {
                stateSavers.Pop();
            }
            count = 0;
        }
        return count;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Close();
        }
    }
}
