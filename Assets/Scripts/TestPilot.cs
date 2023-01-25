using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LukensUtils;

public class TestPilot : MonoBehaviour
{
    public bool startFalse;

    private void Start()
    {
        LukensUtilities.DelayedFire(CoolBool, 2);
    }

    private void CoolBool()
    {
        startFalse = true;
    }
}
