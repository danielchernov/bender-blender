using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    [SerializeField]
    AudioSource[] _bgmAudios;

    [SerializeField]
    AudioClip _endingBGM;

    [SerializeField]
    GameObject[] _creditsLines;

    [SerializeField]
    GameObject _fadeOutScreen;

    [SerializeField]
    GameObject _thanksForPlaying;

    public bool NoMoPause = false;

    void Start()
    {
        StartCoroutine(PlayEnding());
    }

    IEnumerator PlayEnding()
    {
        _bgmAudios[0].Stop();
        _bgmAudios[1].Stop();

        _bgmAudios[0].PlayOneShot(_endingBGM, 1f);

        yield return new WaitForSeconds(8);

        _creditsLines[0].SetActive(true);
        yield return new WaitForSeconds(2);

        for (int i = 1; i < _creditsLines.Length; i++)
        {
            _creditsLines[i].SetActive(true);
            yield return new WaitForSeconds(1.15f);
        }

        yield return new WaitForSeconds(2);

        NoMoPause = true;

        _fadeOutScreen.SetActive(true);

        yield return new WaitForSeconds(6);

        _thanksForPlaying.SetActive(true);

        yield return new WaitForSeconds(7);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }
}
