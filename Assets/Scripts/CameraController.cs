using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private GameObject cameraTarget;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float zoomSpeed = 1f;
    [SerializeField] private float minFieldOfView = 1f;
    [SerializeField] private float maxFieldOfView = 100f;
    private Vector2 _mousePosition;
    
    private void LateUpdate()
    {
        GetPlayerInput();
    }

    private void Update()
    {
        HandleZoom();
    }


    private void GetPlayerInput()
    {   
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cameraTarget.transform.position = new Vector3(_mousePosition.x, _mousePosition.y, -10);
    }

    private void HandleZoom()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            var scroll = Input.GetAxis("Mouse ScrollWheel");
            
            if (scroll != 0f)
            {
                var newFOV = Mathf.Clamp(virtualCamera.m_Lens.OrthographicSize - scroll * zoomSpeed * 10, minFieldOfView, maxFieldOfView);
                Debug.Log("New FOV: " + newFOV);
                virtualCamera.m_Lens.OrthographicSize = newFOV;
            }
        }
    }
}