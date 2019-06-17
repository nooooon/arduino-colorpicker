using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents : MonoBehaviour
{
    public SerialHandler serialHandler;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PickerOn()
    {
        serialHandler.Write("o");
    }

    public void PickerOff()
    {
        serialHandler.Write("f");
    }
}
