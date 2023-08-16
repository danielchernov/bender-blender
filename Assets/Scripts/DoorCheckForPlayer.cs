using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCheckForPlayer : MonoBehaviour
{
    [SerializeField]
    bool _isTriggered = false;

    [SerializeField]
    Outline _doorOutline;

    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
            _isTriggered = true;
            _doorOutline.enabled = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            _isTriggered = false;
            _doorOutline.enabled = false;
        }
    }

    public bool IsTriggered()
    {
        return _isTriggered;
    }
}
