using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [SerializeField]
    Animator _flashlightAnimator;

    [SerializeField]
    float _timeOn = 2f;

    [SerializeField]
    float _timeOff = 0.3f;

    void Start()
    {
        StartCoroutine(LightFlickerRoutine());
    }

    IEnumerator LightFlickerRoutine()
    {
        yield return new WaitForSeconds(Random.Range(0f, _timeOn));
        _flashlightAnimator.SetBool("FlashlightOn", true);

        yield return new WaitForSeconds(Random.Range(0f, _timeOff));
        _flashlightAnimator.SetBool("FlashlightOn", false);

        StartCoroutine(LightFlickerRoutine());
    }
}
