using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    void OnEnable()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }

    void OnDisable()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
