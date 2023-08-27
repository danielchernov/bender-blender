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

    public AudioSource[] AllAudio;

    [SerializeField]
    AudioClip[] _deathSFX;

    [SerializeField]
    AudioClip[] _benderLaughSFX;

    [SerializeField]
    Camera _cameraB;

    [SerializeField]
    Material _cameraMatB;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        if (_cameraB.targetTexture != null)
        {
            _cameraB.targetTexture.Release();
        }

        _cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        _cameraMatB.mainTexture = _cameraB.targetTexture;
    }

    public IEnumerator GameOver()
    {
        _bender.SetActive(false);
        _gameOverScreen.SetActive(true);

        for (int i = 0; i < AllAudio.Length; i++)
            if (AllAudio[i] != null)
                AllAudio[i].Pause();

        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _sfxAudio.PlayOneShot(_deathSFX[Random.Range(0, _deathSFX.Length)], 1);

        yield return new WaitForSecondsRealtime(1);

        _sfxAudio.PlayOneShot(_benderLaughSFX[Random.Range(0, _benderLaughSFX.Length)], 1);
    }

    public bool IsGameOver()
    {
        return _gameOverScreen.activeSelf;
    }
}
