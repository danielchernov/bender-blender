using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField]
    DoorCheckForPlayer[] _doorColliders;

    [SerializeField]
    Animator _doorAnimator;

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (_doorColliders[0].IsTriggered())
            {
                if (_doorAnimator.GetInteger("DoorOpened") == 0)
                {
                    _doorAnimator.SetInteger("DoorOpened", 1);
                }
                else if (_doorAnimator.GetInteger("DoorOpened") == 1)
                {
                    _doorAnimator.SetInteger("DoorOpened", 0);
                }
                else if (_doorAnimator.GetInteger("DoorOpened") == -1)
                {
                    _doorAnimator.SetInteger("DoorOpened", 0);
                }
            }
            else if (_doorColliders[1].IsTriggered())
            {
                if (_doorAnimator.GetInteger("DoorOpened") == 0)
                {
                    _doorAnimator.SetInteger("DoorOpened", -1);
                }
                else if (_doorAnimator.GetInteger("DoorOpened") == 1)
                {
                    _doorAnimator.SetInteger("DoorOpened", 0);
                }
                else if (_doorAnimator.GetInteger("DoorOpened") == -1)
                {
                    _doorAnimator.SetInteger("DoorOpened", 0);
                }
            }
        }
    }
}
