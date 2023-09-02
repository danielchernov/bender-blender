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

    // [SerializeField]
    // AudioClip[] _jumpscareSFX;

    [SerializeField]
    AudioSource _benderAudio;

    [SerializeField]
    AudioClip[] _benderAssSFX;

    bool _alreadyActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_alreadyActivated)
        {
            _doorAnimator.SetInteger("DoorOpened", -1);
            _bender.SetActive(true);

            _sfxAudio.PlayOneShot(_doorOpenSFX, 1f);
            // _sfxAudio.PlayOneShot(_jumpscareSFX[Random.Range(0, _jumpscareSFX.Length)], 1f);
            _benderAudio.PlayOneShot(_benderAssSFX[Random.Range(0, _benderAssSFX.Length)], 2f);

            _alreadyActivated = true;
        }
    }
}
