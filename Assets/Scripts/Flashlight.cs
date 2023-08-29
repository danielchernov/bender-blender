using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;

using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{
    [SerializeField]
    GameObject _flashlight;

    [SerializeField]
    AudioSource _sfxAudio;

    [SerializeField]
    AudioClip _sfxClip;

    [SerializeField]
    Sprite[] _flashlightTextures;

    [SerializeField]
    Image _flashlightUI;

    [SerializeField]
    Image _flashlightBackground;

    [SerializeField]
    GameObject _fKeyUI;

    Animator _flashlightAnimator;

    [SerializeField]
    float _maxFlashlightCharge = 35;

    float _flashlightCharge;

    private void Start()
    {
        _flashlightAnimator = _flashlight.GetComponent<Animator>();
        _flashlightCharge = _maxFlashlightCharge;
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

        if (_flashlight.activeSelf)
        {
            _flashlightCharge = Mathf.Clamp(
                _flashlightCharge - Time.deltaTime,
                0,
                _maxFlashlightCharge
            );

            _fKeyUI.SetActive(false);
            _flashlightBackground.gameObject.SetActive(true);
        }
        else
        {
            _flashlightCharge = Mathf.Clamp(
                _flashlightCharge + (Time.deltaTime * 5),
                0,
                _maxFlashlightCharge
            );

            _fKeyUI.SetActive(true);
            _flashlightBackground.gameObject.SetActive(false);
        }

        if (_flashlightCharge > _maxFlashlightCharge * 0.8f)
        {
            _flashlightUI.sprite = _flashlightTextures[5];
        }
        else if (_flashlightCharge > _maxFlashlightCharge * 0.6f)
        {
            _flashlightUI.sprite = _flashlightTextures[4];
        }
        else if (_flashlightCharge > _maxFlashlightCharge * 0.4f)
        {
            _flashlightUI.sprite = _flashlightTextures[3];
            _flashlightUI.color = Color.green;
            _flashlightBackground.color = Color.green * 0.5f;
        }
        else if (_flashlightCharge > _maxFlashlightCharge * 0.2f)
        {
            _flashlightUI.sprite = _flashlightTextures[2];
            _flashlightUI.color = Color.yellow;
            _flashlightBackground.color = Color.yellow * 0.5f;
        }
        else if (_flashlightCharge > 0.5f)
        {
            _flashlightUI.sprite = _flashlightTextures[1];
            _flashlightUI.color = Color.red;
            _flashlightBackground.color = Color.red * 0.5f;
        }
        else if (_flashlightCharge > 0)
        {
            _flashlightUI.sprite = _flashlightTextures[0];
        }
        else if (_flashlightCharge <= 0)
        {
            _flashlight.SetActive(!_flashlight.activeSelf);
            _sfxAudio.PlayOneShot(_sfxClip, 0.5f);
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
