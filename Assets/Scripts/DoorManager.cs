using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField]
    DoorCheckForPlayer[] _doorColliders;

    [SerializeField]
    Animator _doorAnimator;

    [SerializeField]
    Transform _door;

    [SerializeField]
    AudioSource _doorAudio;

    [SerializeField]
    AudioClip[] _doorSFX;

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (_doorColliders[0].IsTriggered())
            {
                if (_doorAnimator.GetInteger("DoorOpened") == 0)
                {
                    _doorAnimator.SetInteger("DoorOpened", 1);
                    _doorAudio.PlayOneShot(_doorSFX[0], 0.5f);
                }
                else if (_doorAnimator.GetInteger("DoorOpened") == 1)
                {
                    _doorAnimator.SetInteger("DoorOpened", 0);
                    _doorAudio.PlayOneShot(_doorSFX[1], 0.5f);
                }
                else if (_doorAnimator.GetInteger("DoorOpened") == -1)
                {
                    _doorAnimator.SetInteger("DoorOpened", 0);
                    _doorAudio.PlayOneShot(_doorSFX[1], 0.5f);
                }
            }
            else if (_doorColliders[1].IsTriggered())
            {
                if (_doorAnimator.GetInteger("DoorOpened") == 0)
                {
                    _doorAnimator.SetInteger("DoorOpened", -1);
                    _doorAudio.PlayOneShot(_doorSFX[0], 0.5f);
                }
                else if (_doorAnimator.GetInteger("DoorOpened") == 1)
                {
                    _doorAnimator.SetInteger("DoorOpened", 0);
                    _doorAudio.PlayOneShot(_doorSFX[1], 0.5f);
                }
                else if (_doorAnimator.GetInteger("DoorOpened") == -1)
                {
                    _doorAnimator.SetInteger("DoorOpened", 0);
                    _doorAudio.PlayOneShot(_doorSFX[1], 0.5f);
                }
            }
        }
    }
}
