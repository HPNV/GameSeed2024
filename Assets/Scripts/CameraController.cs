using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private GameObject cameraTarget;
    private Vector2 _mousePosition;
    void Start()
    {
        // _camera = GetComponent<CinemachineVirtualCamera>();
    }

    
    private void LateUpdate()
    {

        GetPlayerInput();
    }
    

    private void GetPlayerInput()
    {   
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log($"Position {_mousePosition}");
        cameraTarget.transform.position = new Vector3(_mousePosition.x, _mousePosition.y, -10);
    }
}