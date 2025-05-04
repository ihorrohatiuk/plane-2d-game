using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    [SerializeField] private float _backgroundSpeed;
    [SerializeField] private Renderer _backgroundRenderer;
    void Update()
    {
        _backgroundRenderer.material.mainTextureOffset += new Vector2(_backgroundSpeed * Time.deltaTime, 0f);
    }
}
