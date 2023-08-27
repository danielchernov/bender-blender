using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRoutine : MonoBehaviour
{
    // [SerializeField]
    // GameObject[] _controls;

    // [SerializeField]
    // GameObject _escapeText;

    // void Start()
    // {
    //     StartCoroutine(Tutorial());
    // }

    // IEnumerator Tutorial()
    // {
    //     yield return new WaitForSeconds(2);
    //     _escapeText.SetActive(true);
    //     yield return new WaitForSeconds(4);
    //     _controls[0].SetActive(true);
    //     yield return new WaitForSeconds(1.5f);
    //     _controls[1].SetActive(true);
    //     yield return new WaitForSeconds(1.5f);
    //     _controls[2].SetActive(true);
    //     yield return new WaitForSeconds(1.5f);
    //     _controls[3].SetActive(true);
    //     yield return new WaitForSeconds(3f);
    //     gameObject.SetActive(false);
    // }

    public void DisableTutorial()
    {
        gameObject.SetActive(false);
    }
}
