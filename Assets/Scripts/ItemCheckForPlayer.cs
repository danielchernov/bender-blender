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
    AudioSource _itemAudio;

    [SerializeField]
    AudioSource _SFXAudio;

    [SerializeField]
    AudioClip _switchSFX;

    [SerializeField]
    AudioClip[] _toiletSFX;

    [SerializeField]
    LayerMask _layerMask;

    MeshRenderer _tvRenderer;
    bool _tvOn = true;
    bool _isTriggered = false;

    enum Furniture
    {
        Instrument,
        TV,
        RecordPlayer,
        Toilet
    }

    [SerializeField]
    Furniture furni;

    RaycastHit rayHit;
    bool _watchingItem = false;

    private void Start()
    {
        if (furni == Furniture.TV)
        {
            _tvRenderer = _itemOutline.GetComponent<MeshRenderer>();
        }
        else if (furni == Furniture.RecordPlayer)
        {
            _recordPlayer = _itemOutline.GetComponent<RecordPlayer>();
            _itemAudio = _itemOutline.GetComponent<AudioSource>();
        }
        else if (furni == Furniture.Instrument || furni == Furniture.Toilet)
        {
            _itemAudio = _itemOutline.GetComponent<AudioSource>();

            if (_itemAudio == null)
            {
                _itemAudio = _itemOutline.GetComponentInChildren<AudioSource>();
            }
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && _isTriggered)
        {
            if (furni == Furniture.Instrument)
            {
                if (_itemAudio.isPlaying)
                {
                    _itemAudio.Pause();
                }
                else
                {
                    _itemAudio.Play();
                }
            }
            else if (furni == Furniture.TV)
            {
                if (_tvOn)
                {
                    _tvRenderer.materials[1].color = Color.black;
                    _tvOn = false;
                    _SFXAudio.PlayOneShot(_switchSFX, 1f);
                }
                else
                {
                    _tvRenderer.materials[1].color = Color.white;
                    _tvOn = true;
                    _SFXAudio.PlayOneShot(_switchSFX, 1f);
                }
            }
            else if (furni == Furniture.RecordPlayer)
            {
                _recordPlayer.recordPlayerActive = !_recordPlayer.recordPlayerActive;

                if (_itemAudio.isPlaying)
                {
                    _itemAudio.Pause();
                }
                else
                {
                    _itemAudio.Play();
                }
            }
            else if (furni == Furniture.Toilet)
            {
                _itemAudio.Stop();

                _itemAudio.PlayOneShot(_toiletSFX[Random.Range(0, _toiletSFX.Length)], 1f);
            }
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(rayOrigin, out rayHit, Mathf.Infinity, ~(_layerMask));
            _watchingItem = _itemOutline.transform == rayHit.transform;

            if (_watchingItem)
            {
                _isTriggered = true;
                _itemOutline.enabled = true;

                _tutorialText.transform.parent.gameObject.SetActive(true);

                if (furni == Furniture.Instrument)
                {
                    if (_itemAudio.isPlaying)
                    {
                        _tutorialText.text = "Stop Playing";
                    }
                    else
                    {
                        _tutorialText.text = "Play " + _itemOutline.gameObject.name;
                    }
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
                    if (_itemAudio.isPlaying)
                    {
                        _tutorialText.text = "Switch OFF";
                    }
                    else
                    {
                        _tutorialText.text = "Switch ON";
                    }
                }
                else if (furni == Furniture.Toilet)
                {
                    _tutorialText.text = "Flush Toilet";
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
