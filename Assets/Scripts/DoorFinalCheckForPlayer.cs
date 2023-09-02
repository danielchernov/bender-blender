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
    TextMeshProUGUI _specialText;

    [SerializeField]
    GameObject _ending;

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
                    _ending.SetActive(true);
                    _doorAnimator.SetInteger("DoorOpened", 1);
                    _doorAudio.PlayOneShot(_doorSFX[0], 0.5f);
                }
            }
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
            int hasKey = collider.GetComponent<PlayerHasKey>().HasKeys;

            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(rayOrigin, out rayHit);
            _watchingDoor = _doorOutline.transform == rayHit.transform;

            _watchingColliders = transform == rayHit.transform;

            if ((_watchingDoor || _watchingColliders) && hasKey == 3 && !_ending.activeSelf)
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

                if (hasKey != 3)
                {
                    _specialText.transform.parent.gameObject.SetActive(true);
                    if (hasKey == 2)
                    {
                        _specialText.text = "One more key to open the door";
                    }
                    else if (hasKey == 1)
                    {
                        _specialText.text = "Need to find 2 keys to open the door";
                    }
                    else if (hasKey == 0)
                    {
                        _specialText.text = "Need to find 3 keys to open the door";
                    }
                }
                else if (_doorAnimator.GetInteger("DoorOpened") == 1)
                {
                    _tutorialText.transform.parent.gameObject.SetActive(false);
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
            _specialText.transform.parent.gameObject.SetActive(false);
        }
    }

    public bool IsTriggered()
    {
        return _isTriggered;
    }
}
