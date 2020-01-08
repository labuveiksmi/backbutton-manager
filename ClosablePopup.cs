using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClosablePopup : MonoBehaviour
{
    private bool didBack = false;

    protected void OnEnable()
    {
        if (BackButtonManager.instance)
        {
            BackButtonManager.instance.AddState(this);
        }
    }

    public virtual void Back()
    {
        didBack = true;
        BackButtonManager.instance.CloseLastMenu();
    }

    protected void OnDisable()
    {
        if (!didBack)
        {
            Back();
        }
    }
}