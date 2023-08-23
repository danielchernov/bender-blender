using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkRoutine : MonoBehaviour
{
    [SerializeField]
    BenderController _benderController;

    public void WalkAgain()
    {
        _benderController.WalkingRoutine = StartCoroutine(_benderController.WalkRoutine());
    }
}
