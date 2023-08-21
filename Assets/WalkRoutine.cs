using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkRoutine : MonoBehaviour
{
    [SerializeField]
    BenderController _benderController;

    public void WalkAgain()
    {
        StartCoroutine(_benderController.WalkRoutine());
    }
}
