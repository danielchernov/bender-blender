using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorCheckForPlayer : MonoBehaviour
{
    [SerializeField]
    bool _isTriggered = false;

    [SerializeField]
    Outline _doorOutline;

    [SerializeField]
    TextMeshProUGUI _tutorialText;

    [SerializeField]
    DoorCheckForPlayer _otherCollider;

    RaycastHit rayHit;
    bool _watchingDoor;
    bool _watchingColliders;

    Animator _doorAnimator;

    private void Start()
    {
        _doorAnimator = _doorOutline.GetComponentInParent<Animator>();
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(rayOrigin, out rayHit);
            _watchingDoor = _doorOutline.transform == rayHit.transform;

            _watchingColliders =
                transform == rayHit.transform || _otherCollider.transform == rayHit.transform;

            if (_watchingDoor || _watchingColliders)
            {
                _isTriggered = true;
                _doorOutline.enabled = true;

                _tutorialText.transform.parent.gameObject.SetActive(true);

                if (_doorAnimator.GetInteger("DoorOpened") == 0)
                {
                    _tutorialText.text = "Open Door";
                }
                else
                {
                    _tutorialText.text = "Close Door";
                }
            }
            else
            {
                _isTriggered = false;
                _doorOutline.enabled = false;

                _tutorialText.transform.parent.gameObject.SetActive(false);
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
        }
    }

    public bool IsTriggered()
    {
        return _isTriggered;
    }
}
