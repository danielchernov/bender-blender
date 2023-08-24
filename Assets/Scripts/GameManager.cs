using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager is Null");
            }

            return _instance;
        }
    }

    [SerializeField]
    GameObject _gameOverScreen;

    [SerializeField]
    GameObject _bender;

    [SerializeField]
    AudioSource _sfxAudio;

    [SerializeField]
    AudioClip[] _deathSFX;

    [SerializeField]
    AudioClip[] _benderLaughSFX;

    private void Awake()
    {
        _instance = this;
    }

    public void GameOver()
    {
        _bender.SetActive(false);
        _gameOverScreen.SetActive(true);
        _sfxAudio.PlayOneShot(_deathSFX[Random.Range(0, _deathSFX.Length)], 1);
        _sfxAudio.PlayOneShot(_benderLaughSFX[Random.Range(0, _benderLaughSFX.Length)], 1);

        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public bool IsGameOver()
    {
        return _gameOverScreen.activeSelf;
    }
}
