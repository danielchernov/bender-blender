using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyCheckForPlayer : MonoBehaviour
{
    [SerializeField]
    Outline _keyOutline;

    [SerializeField]
    TextMeshProUGUI _tutorialText;

    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
            _keyOutline.enabled = true;

            _tutorialText.transform.parent.gameObject.SetActive(true);
            _tutorialText.text = "Grab Key";
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            _keyOutline.enabled = false;
            _tutorialText.transform.parent.gameObject.SetActive(false);
        }
    }
}
