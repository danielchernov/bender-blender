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

        for (int i = 0; i < 3; i++)
        {
            int randomNumber = Random.Range(0, _keyHolders.Length);

            if (!_keyHolders[randomNumber].activeSelf)
            {
                _keyHolders[randomNumber].SetActive(true);
            }
            else
            {
                i--;
            }
        }
    }
}
