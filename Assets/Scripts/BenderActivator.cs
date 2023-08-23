using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenderActivator : MonoBehaviour
{
    [SerializeField]
    Animator _doorAnimator;

    [SerializeField]
    GameObject _bender;

    [SerializeField]
    AudioSource _sfxAudio;

    [SerializeField]
    AudioClip _doorOpenSFX;

    [SerializeField]
    AudioSource _benderAudio;

    [SerializeField]
    AudioClip _benderSFX;

    bool _alreadyActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_alreadyActivated)
        {
            _doorAnimator.SetInteger("DoorOpened", -1);
            _bender.SetActive(true);

            _sfxAudio.PlayOneShot(_doorOpenSFX, 0.5f);
            _benderAudio.PlayOneShot(_benderSFX, 0.5f);

            _alreadyActivated = true;
        }
    }
}
