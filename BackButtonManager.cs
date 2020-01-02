using System.Collections.Generic;
using UnityEngine;

public class BackButtonManager : MonoBehaviour
{
    public static BackButtonManager instance;
    private Stack<BackButtonStateSaver> stateSavers = new Stack<BackButtonStateSaver>();

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

    public void CloseLastMenu()
    {
        if (GetClosableCount() == 0)
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
        // Check for empty links.
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
            CloseLastMenu();
        }
    }
}