using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorFinalCheckForPlayer : MonoBehaviour
{
    [SerializeField]
    bool _isTriggered = false;

    [SerializeField]
    Outline _doorOutline;

    [SerializeField]
    TextMeshProUGUI _tutorialText;

    [SerializeField]
    GameObject _specialText;

    RaycastHit rayHit;
    bool _watchingDoor;
    bool _watchingColliders;

    Animator _doorAnimator;

    [SerializeField]
    AudioSource _doorAudio;

    [SerializeField]
    AudioClip[] _doorSFX;

    private void Start()
    {
        _doorAnimator = _doorOutline.GetComponentInParent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (_isTriggered)
            {
                if (_doorAnimator.GetInteger("DoorOpened") == 0)
                {
                    _doorAnimator.SetInteger("DoorOpened", -1);
                    _doorAudio.PlayOneShot(_doorSFX[0], 0.5f);
                }
            }
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
            bool hasKey = collider.GetComponent<PlayerHasKey>().HasKey;

            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(rayOrigin, out rayHit);
            _watchingDoor = _doorOutline.transform == rayHit.transform;

            _watchingColliders = transform == rayHit.transform;

            if ((_watchingDoor || _watchingColliders) && hasKey)
            {
                _isTriggered = true;
                _doorOutline.enabled = true;

                _tutorialText.transform.parent.gameObject.SetActive(true);

                if (_doorAnimator.GetInteger("DoorOpened") == 0)
                {
                    _tutorialText.text = "Escape the house!";
                }
                else
                {
                    _tutorialText.transform.parent.gameObject.SetActive(false);
                }
            }
            else
            {
                _isTriggered = false;
                _doorOutline.enabled = false;

                if (!hasKey)
                {
                    _specialText.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            _isTriggered = false;
            _doorOutline.enabled = false;

            _tutorialText.transform.parent.gameObject.SetActive(false);
            _specialText.SetActive(false);
        }
    }

    public bool IsTriggered()
    {
        return _isTriggered;
    }
}
