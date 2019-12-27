using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonManager : MonoBehaviour
{
    public static BackButtonManager instance;
    private bool stopManaging = false;
    private Stack<BackButtonStateSaver> stateSavers = new Stack<BackButtonStateSaver>();

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

    #endregion properties

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

    #endregion singleton

    public void AddState(BackButtonStateSaver back)
    {
        stateSavers.Push(back);
    }

    public void Close()
    {
        if (GetClosableCount() == 0
            /*&& !stateSavers.Peek()*/)
        {
            PauseOrQuit();
        }
        else
        {
            stateSavers.Pop().Back();
        }
    }

    private void PauseOrQuit()
    {
        Menu menu = FindObjectOfType<Menu>();
        if (menu)
        {
            menu.OpenExitPopup();
        }
    }

    public int GetClosableCount()
    {
        int count = stateSavers.Count;
        if (count > 0 && !stateSavers.Peek())
        {
            EmptyStack();
            count = 0;
        }
        return count;
    }

    public void EmptyStack()
    {
        for (int i = 0; i < stateSavers.Count; i++)
        {
            stateSavers.Pop();
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Close();
        }
    }
}