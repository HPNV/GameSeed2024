using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Manager;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private GameObject cameraTarget;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float zoomSpeed = 1f;
    [SerializeField] private float minFieldOfView = 1f;
    [SerializeField] private float maxFieldOfView = 100f;
    [SerializeField] private float panSpeed = 50;
    [SerializeField] private int leftLimit = -100;
    [SerializeField] private int rightLimit = 100;
    [SerializeField] private int topLimit = 100;
    [SerializeField] private int bottomLimit = -100;
    private Vector3 _oldPosition;
    private Vector3 _panOrigin;
    
    private void LateUpdate()
    {
        HandleCameraMovement();
    }

    private void Update()
    {
        HandleZoom();
    }

    private void HandleCameraBounds()
    {
        var cameraPosition = cameraTarget.transform.position;
        
        if (cameraTarget.transform.position.x < leftLimit)
        {
            cameraTarget.transform.position = new Vector3(leftLimit, cameraTarget.transform.position.y, cameraPosition.z);
        }
            
        if (cameraTarget.transform.position.x > rightLimit)
        {
            cameraTarget.transform.position = new Vector3(rightLimit, cameraTarget.transform.position.y, cameraPosition.z);
        }
        
        if (cameraTarget.transform.position.y < bottomLimit)
        {
            cameraTarget.transform.position = new Vector3(cameraTarget.transform.position.x, bottomLimit, cameraPosition.z);
        }
        
        if (cameraTarget.transform.position.y > topLimit)
        {
            cameraTarget.transform.position = new Vector3(cameraTarget.transform.position.x, topLimit, cameraPosition.z);
        }
    }


    private void HandleCameraMovement()
    {
        if(Input.GetMouseButtonDown(1))
        {
            SingletonGame.Instance.CursorManager.ChangeCursor(CursorType.Hand);
            _oldPosition = cameraTarget.transform.position;
            _panOrigin = Camera.main.ScreenToViewportPoint(Input.mousePosition);				
        }

        if (Input.GetMouseButtonUp(1))
        {
            SingletonGame.Instance.CursorManager.ChangeCursor(CursorType.Arrow);
        }

        if(Input.GetMouseButton(1))
        {
            var pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) - _panOrigin;
            cameraTarget.transform.position = _oldPosition + -pos * panSpeed; 				
            HandleCameraBounds();
        }
        
    }

    private void HandleZoom()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            var scroll = Input.GetAxis("Mouse ScrollWheel");
            
            if (scroll != 0f)
            {
                var newFOV = Mathf.Clamp(virtualCamera.m_Lens.OrthographicSize - scroll * zoomSpeed * 10, minFieldOfView, maxFieldOfView);
                virtualCamera.m_Lens.OrthographicSize = newFOV;
            }
        } 
    }
}