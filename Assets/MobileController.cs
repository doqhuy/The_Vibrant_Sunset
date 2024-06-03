using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileController : MonoBehaviour
{
    public bool HoldA = false;
    public bool HoldD = false;
    public bool HoldSpace = false;

    public bool PressJ = false;
    public bool HoldK = false;
    public bool PressL = false;

    public bool PressU = false;
    public bool PressI = false;


    public void SetHoldA(bool value)
    {
        if(value && HoldD)
        {
            return;
        }
        HoldA = value;
    }

    public void SetHoldD(bool value) 
    {
        if (value && HoldA)
        {
            return;
        }
        HoldD = value;
    }

    public void SetHoldSpace(bool value)
    {
        HoldSpace = value;
    }

    public void SetPressJ(bool value)
    {
        PressJ = value;
    }

    public void SetHoldK(bool value)
    {
        HoldK = value;
    }    

    public void SetPressL(bool value)
    {
        PressL = value;
    }    

    public void SetPressU(bool value)
    { 
        PressU = value;
    }

    public void SetPressI(bool value)
    {
        PressI = value;
    }    



 
        
}
