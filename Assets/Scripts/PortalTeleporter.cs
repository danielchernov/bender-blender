using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    [SerializeField]
    Transform _player;

    [SerializeField]
    Transform _receiver;

    [SerializeField]
    GameObject _house;

    [SerializeField]
    GameObject _endingManager;

    bool _isTriggered = false;

    void Update()
    {
        if (_isTriggered)
        {
            Vector3 portalToPlayer = _player.position - transform.position;

            float rotationDiff = -Quaternion.Angle(transform.rotation, _receiver.rotation);
            rotationDiff += 180;

            Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
            _player.position = _receiver.position + positionOffset;
            _player.Rotate(Vector3.up, rotationDiff);

            _house.SetActive(false);
            _receiver.gameObject.SetActive(false);
            _endingManager.SetActive(true);

            _isTriggered = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _isTriggered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _isTriggered = false;
        }
    }
}
