using System;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cube;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Instantiate(_cube);
        }
    }
}
