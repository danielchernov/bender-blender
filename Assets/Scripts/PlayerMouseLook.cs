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

    bool mouseLocked = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(WaitAndChange());
    }

    void Update()
    {
        if (!mouseLocked)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);

            yRotation += mouseX;

            toFollow.localRotation = Quaternion.Euler(xRotation, 0, 0);
            transform.localRotation = Quaternion.Euler(0, yRotation + 90, 0);
        }
    }

    IEnumerator WaitAndChange()
    {
        yield return new WaitForSeconds(0.2f);
        mouseLocked = !mouseLocked;
    }
}
