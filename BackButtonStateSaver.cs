using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonStateSaver : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        BackButtonManager.instance.AddState(this);
    }

    public void Close()
    {
        BackButtonManager.instance.Close();
    }
}
