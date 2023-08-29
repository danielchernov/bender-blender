using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePaintingCollider : MonoBehaviour
{
    [SerializeField]
    ChangePainting _changePainting;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _changePainting.ChangePicture();
        }
    }
}
