using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeGameOverText : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _gameOverText;

    [SerializeField]
    string[] _killedSynonyms;

    [SerializeField]
    string[] _crazySynonyms;

    [SerializeField]
    string[] _robotSynonyms;

    void Start()
    {
        _gameOverText.text =
            "You were "
            + _killedSynonyms[Random.Range(0, _killedSynonyms.Length)]
            + " by<br>the "
            + _crazySynonyms[Random.Range(0, _crazySynonyms.Length)]
            + " "
            + _robotSynonyms[Random.Range(0, _robotSynonyms.Length)];
    }
}
