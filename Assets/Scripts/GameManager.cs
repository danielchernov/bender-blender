using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
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
    float _timeOn = 0.2f;

    [SerializeField]
    float _timeOff = 1f;

    private void Awake()
    {
        _instance = this;
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
        RectTransform scaryBenderTransform = _scaryBenderAnimator.GetComponent<RectTransform>();
        float initialScaryBenderX = scaryBenderTransform.position.x;

        for (int i = 0; i < Random.Range(4, 8); i++)
        {
            yield return new WaitForSecondsRealtime(Random.Range(0.05f, _timeOn));
            _scaryBenderAnimator.SetBool("BenderOn", true);

            scaryBenderTransform.position = new Vector3(
                initialScaryBenderX + Random.Range(-250f, 250f),
                scaryBenderTransform.position.y,
                scaryBenderTransform.position.z
            );

            scaryBenderTransform.localScale = Vector3.one;
            scaryBenderTransform.localScale *= 1 + Random.Range(0f, 1f);

            yield return new WaitForSecondsRealtime(Random.Range(0.1f, _timeOff));
            _scaryBenderAnimator.SetBool("BenderOn", false);
        }

        //StartCoroutine(AnimateScaryBender());
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
