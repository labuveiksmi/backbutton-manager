using System.Collections.Generic;
using UnityEngine;

public class BackButtonManager : MonoBehaviour
{
    public static BackButtonManager instance;
    private Stack<ClosablePopup> popups = new Stack<ClosablePopup>();

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

    public void AddState(ClosablePopup popup)
    {
        popups.Push(popup);
    }

    public void CloseLastMenu()
    {
        if (GetClosableCount() == 0)
        {
            PauseOrQuit();
        }
        else
        {
            popups.Pop().Back();
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
        int count = popups.Count;
        // Check for empty links.
        if (count > 0 && !popups.Peek())
        {
            EmptyStack();
            count = 0;
        }
        Debug.Log("popups count =" + count);
        return count;
    }

    public void EmptyStack()
    {
        for (int i = 0; i < popups.Count; i++)
        {
            popups.Pop();
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