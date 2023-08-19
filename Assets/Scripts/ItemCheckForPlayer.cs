using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class ItemCheckForPlayer : MonoBehaviour
{
    [SerializeField]
    Outline _itemOutline;

    [SerializeField]
    TextMeshProUGUI _tutorialText;

    RecordPlayer _recordPlayer;
    AudioSource _recordPlayerAudio;

    MeshRenderer _tvRenderer;
    bool _tvOn = true;
    bool _isTriggered = false;

    enum Furniture
    {
        Piano,
        TV,
        RecordPlayer
    }

    [SerializeField]
    Furniture furni;

    RaycastHit rayHit;
    bool _watchingItem = false;

    private void Start()
    {
        if (furni == Furniture.Piano) { }
        else if (furni == Furniture.TV)
        {
            _tvRenderer = _itemOutline.GetComponent<MeshRenderer>();
        }
        else if (furni == Furniture.RecordPlayer)
        {
            _recordPlayer = _itemOutline.GetComponent<RecordPlayer>();
            _recordPlayerAudio = _itemOutline.GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && _isTriggered)
        {
            if (furni == Furniture.Piano) { }
            else if (furni == Furniture.TV)
            {
                if (_tvOn)
                {
                    _tvRenderer.materials[1].color = Color.black;
                    _tvOn = false;
                }
                else
                {
                    _tvRenderer.materials[1].color = Color.white;
                    _tvOn = true;
                }
            }
            else if (furni == Furniture.RecordPlayer)
            {
                _recordPlayer.recordPlayerActive = !_recordPlayer.recordPlayerActive;

                if (_recordPlayerAudio.isPlaying)
                {
                    _recordPlayerAudio.Pause();
                }
                else
                {
                    _recordPlayerAudio.Play();
                }
            }
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(rayOrigin, out rayHit);
            _watchingItem = _itemOutline.transform == rayHit.transform;

            if (_watchingItem)
            {
                _isTriggered = true;
                _itemOutline.enabled = true;

                _tutorialText.transform.parent.gameObject.SetActive(true);

                if (furni == Furniture.Piano)
                {
                    _tutorialText.text = "Play Piano";
                }
                else if (furni == Furniture.TV)
                {
                    if (_tvOn)
                    {
                        _tutorialText.text = "Switch OFF";
                    }
                    else
                    {
                        _tutorialText.text = "Switch ON";
                    }
                }
                else if (furni == Furniture.RecordPlayer)
                {
                    if (_recordPlayerAudio.isPlaying)
                    {
                        _tutorialText.text = "Switch OFF";
                    }
                    else
                    {
                        _tutorialText.text = "Switch ON";
                    }
                }
            }
            else
            {
                _isTriggered = false;
                _itemOutline.enabled = false;

                if (furni == Furniture.TV)
                {
                    var materials = _tvRenderer.sharedMaterials.ToList();

                    if (materials.Count > 2)
                    {
                        materials.Remove(_tvRenderer.materials[2]);
                        materials.Remove(_tvRenderer.materials[3]);
                    }

                    _tvRenderer.materials = materials.ToArray();
                }
                _tutorialText.transform.parent.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            _isTriggered = false;
            _itemOutline.enabled = false;

            if (furni == Furniture.TV)
            {
                var materials = _tvRenderer.sharedMaterials.ToList();

                if (materials.Count > 2)
                {
                    materials.Remove(_tvRenderer.materials[2]);
                    materials.Remove(_tvRenderer.materials[3]);
                }

                _tvRenderer.materials = materials.ToArray();
            }
            _tutorialText.transform.parent.gameObject.SetActive(false);
        }
    }
}
