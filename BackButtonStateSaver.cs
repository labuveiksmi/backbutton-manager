using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BackButtonStateSaver : MonoBehaviour
{
    private bool dontBacked = true;

    public void OnEnable()
    {
        if (BackButtonManager.instance)
        {
            BackButtonManager.instance.AddState(this);
        }
    }

    public virtual void Back()
    {
        dontBacked = false;
    }

    private void OnDisable()
    {
        if (dontBacked)
        {
            Back();
        }
    }
}