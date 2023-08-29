using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePainting : MonoBehaviour
{
    [SerializeField]
    Texture[] _simpsonsSprites;

    [SerializeField]
    MeshRenderer _simpsonsRenderer;

    void Start()
    {
        _simpsonsRenderer = GetComponent<MeshRenderer>();
        ChangePicture();
    }

    public void ChangePicture()
    {
        _simpsonsRenderer.material.SetTexture(
            "_BaseMap",
            _simpsonsSprites[Random.Range(0, _simpsonsSprites.Length)]
        );
    }
}
