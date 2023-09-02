using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    AudioSource _pauseMenuAudio;

    [SerializeField]
    AudioClip[] _openMenuSFX;

    void OnEnable()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        for (int i = 0; i < GameManager.Instance.AllAudio.Length; i++)
            if (GameManager.Instance.AllAudio[i] != null)
                GameManager.Instance.AllAudio[i].Pause();

        _pauseMenuAudio.PlayOneShot(_openMenuSFX[0], 0.3f);
    }

    void OnDisable()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;

        for (int i = 0; i < GameManager.Instance.AllAudio.Length; i++)
            if (GameManager.Instance.AllAudio[i] != null)
                GameManager.Instance.AllAudio[i].UnPause();

        _pauseMenuAudio.PlayOneShot(_openMenuSFX[1], 0.3f);
    }
}
