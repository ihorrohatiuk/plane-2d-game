using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private float _cameraSpeed;

    void Update()
    {
        transform.position += new Vector3(_cameraSpeed * Time.deltaTime, 0, 0);
    }
}
