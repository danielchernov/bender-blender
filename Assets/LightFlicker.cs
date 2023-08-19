using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [SerializeField]
    Animator _flashlightAnimator;

    void Start()
    {
        StartCoroutine(LightFlickerRoutine());
    }

    IEnumerator LightFlickerRoutine()
    {
        yield return new WaitForSeconds(Random.Range(0f, 1.5f));
        _flashlightAnimator.SetBool("FlashlightOn", true);
        yield return new WaitForSeconds(Random.Range(0.03f, 0.3f));
        _flashlightAnimator.SetBool("FlashlightOn", false);

        StartCoroutine(LightFlickerRoutine());
    }
}
