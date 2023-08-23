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
    GameObject _keySprite;

    [SerializeField]
    PlayerHasKey _playerKey;

    bool _isTriggered = false;

    private void Start() { }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && _isTriggered)
        {
            _keySprite.SetActive(true);

            _playerKey.HasKey = true;

            _keyOutline.enabled = false;
            _tutorialText.transform.parent.gameObject.SetActive(false);
            _isTriggered = false;

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
