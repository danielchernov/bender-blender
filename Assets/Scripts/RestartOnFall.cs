using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartOnFall : MonoBehaviour
{
    [SerializeField]
    Vector3 _positionToSpawn;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = _positionToSpawn;
            print("Fell off!");
        }
    }
}
