using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyCheckForPlayer : MonoBehaviour
{
    [SerializeField]
    Outline _keyOutline;

    [SerializeField]
    TextMeshProUGUI _tutorialText;

    [SerializeField]
    GameObject[] _keySprites;

    [SerializeField]
    PlayerHasKey _playerKey;

    [SerializeField]
    AudioSource _sfxAudio;

    [SerializeField]
    AudioClip[] _keySFX;

    bool _isTriggered = false;

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && _isTriggered)
        {
            _playerKey.HasKeys++;

            if (_playerKey.HasKeys == 1)
            {
                _keySprites[0].SetActive(true);
            }
            else if (_playerKey.HasKeys == 2)
            {
                _keySprites[1].SetActive(true);
            }
            else if (_playerKey.HasKeys == 3)
            {
                _keySprites[2].SetActive(true);
            }

            _keyOutline.enabled = false;
            _tutorialText.transform.parent.gameObject.SetActive(false);
            _isTriggered = false;

            _sfxAudio.PlayOneShot(_keySFX[Random.Range(0, _keySFX.Length)], 1f);

            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
            _keyOutline.enabled = true;

            _tutorialText.transform.parent.gameObject.SetActive(true);
            _tutorialText.text = "Grab Key";

            _isTriggered = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            _keyOutline.enabled = false;
            _tutorialText.transform.parent.gameObject.SetActive(false);
            _isTriggered = false;
        }
    }
}
