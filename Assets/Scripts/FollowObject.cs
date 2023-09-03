using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField]
    Transform _objectToFollow;

    SphereCollider _collider;

    [SerializeField]
    bool _isSax = false;

    private void Start()
    {
        _collider = GetComponent<SphereCollider>();
    }

    void Update()
    {
        if (_isSax)
        {
            _collider.center = new Vector3(
                _objectToFollow.localPosition.x,
                _objectToFollow.localPosition.y + 4,
                _objectToFollow.localPosition.z + 5
            );
        }
        else
        {
            _collider.center = _objectToFollow.localPosition;
        }
    }
}
