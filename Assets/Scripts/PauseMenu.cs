using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    AudioSource[] _backgroundAudio;

    void OnEnable()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;

        for (int i = 0; i < _backgroundAudio.Length; i++)
            _backgroundAudio[i].Pause();
    }

    void OnDisable()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;

        for (int i = 0; i < _backgroundAudio.Length; i++)
            _backgroundAudio[i].UnPause();
    }
}
