using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField]
    GameObject _flashlight;

    [SerializeField]
    AudioSource _sfxAudio;

    [SerializeField]
    AudioClip _sfxClip;

    Animator _flashlightAnimator;

    private void Start()
    {
        _flashlightAnimator = _flashlight.GetComponent<Animator>();
        _flashlight.SetActive(false);

        StartCoroutine(FlashlightMalfunctionRoutine());
    }

    void Update()
    {
        if (Input.GetButtonDown("Flashlight"))
        {
            _flashlight.SetActive(!_flashlight.activeSelf);
            _sfxAudio.PlayOneShot(_sfxClip, 1f);
        }
    }

    IEnumerator FlashlightMalfunctionRoutine()
    {
        yield return new WaitForSeconds(Random.Range(0f, 7f));
        _flashlightAnimator.SetBool("FlashlightOn", true);
        yield return new WaitForSeconds(Random.Range(0f, 0.25f));
        _flashlightAnimator.SetBool("FlashlightOn", false);

        StartCoroutine(FlashlightMalfunctionRoutine());
    }
}
