using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenderCheckForPlayer : MonoBehaviour
{
    bool _isWatching = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _isWatching = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _isWatching = false;
        }
    }

    public bool IsWatching()
    {
        return _isWatching;
    }
}
