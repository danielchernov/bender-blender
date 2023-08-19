using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] _keyHolders;

    void Start()
    {
        foreach (GameObject keyholder in _keyHolders)
        {
            keyholder.SetActive(false);
        }

        _keyHolders[Random.Range(0, _keyHolders.Length)].SetActive(true);
    }
}
