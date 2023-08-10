using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    float xRotation = 0;
    float yRotation = 0;

    [SerializeField]
    Transform toFollow;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80, 80);

        yRotation += mouseX;
        //yRotation = Mathf.Clamp(yRotation, -90, 140);

        toFollow.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);
    }
}
