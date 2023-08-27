using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    [SerializeField]
    Transform _playerCamera;

    [SerializeField]
    Transform _portal;

    [SerializeField]
    Transform _otherPortal;

    private void LateUpdate()
    {
        Vector3 playerOffsetFromPortal = _playerCamera.position - _otherPortal.position;
        transform.position = _portal.position + playerOffsetFromPortal;

        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(
            _portal.rotation,
            _otherPortal.rotation
        );

        Quaternion portalRotationalDifference = Quaternion.AngleAxis(
            angularDifferenceBetweenPortalRotations,
            Vector3.up
        );

        Vector3 newCameraDirection =
            portalRotationalDifference
            * new Vector3(
                -_playerCamera.forward.x,
                _playerCamera.forward.y,
                -_playerCamera.forward.z
            );
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }
}
