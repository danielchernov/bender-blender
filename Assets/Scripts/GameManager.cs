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
    Animator _scaryBenderAnimator;

    [SerializeField]
    GameObject _bender;

    [SerializeField]
    AudioSource _sfxAudio;

    [SerializeField]
    AudioSource _bgmAudio;

    public AudioSource[] AllAudio;

    [SerializeField]
    AudioClip[] _deathSFX;

    [SerializeField]
    AudioClip[] _benderLaughSFX;

    [SerializeField]
    AudioClip _whiteNoiseSFX;

    [SerializeField]
    Camera _cameraB;

    [SerializeField]
    Material _cameraMatB;

    [SerializeField]
    float _timeOn = 0.2f;

    [SerializeField]
    float _timeOff = 1f;

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
        _gameOverScreen.SetActive(true);
        _bender.SetActive(false);

        StartCoroutine(AnimateScaryBender());

        for (int i = 0; i < AllAudio.Length; i++)
            if (AllAudio[i] != null)
                AllAudio[i].Pause();

        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _sfxAudio.PlayOneShot(_deathSFX[Random.Range(0, _deathSFX.Length)], 0.8f);
        _bgmAudio.clip = (_whiteNoiseSFX);
        _bgmAudio.volume = 0.01f;
        _bgmAudio.Play();

        yield return new WaitForSecondsRealtime(1f);

        _sfxAudio.PlayOneShot(_benderLaughSFX[Random.Range(0, _benderLaughSFX.Length)], 1f);
    }

    private IEnumerator AnimateScaryBender()
    {
        yield return new WaitForSecondsRealtime(Random.Range(0.05f, _timeOn));
        _scaryBenderAnimator.SetBool("BenderOn", true);

        _scaryBenderAnimator.transform.localScale = Vector3.one;
        _scaryBenderAnimator.transform.localScale *= 1 + Random.Range(0f, 1f);

        _scaryBenderAnimator.transform.position = new Vector3(
            Random.Range(-200f, 200f),
            _scaryBenderAnimator.transform.position.y,
            _scaryBenderAnimator.transform.position.z
        );

        yield return new WaitForSecondsRealtime(Random.Range(0.1f, _timeOff));
        _scaryBenderAnimator.SetBool("BenderOn", false);

        StartCoroutine(AnimateScaryBender());
    }

    public bool IsGameOver()
    {
        return _gameOverScreen.activeSelf;
    }

    public void PlayGameOver()
    {
        StartCoroutine(GameOver());
    }
}
