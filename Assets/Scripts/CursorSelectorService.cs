using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorSelectorService : MonoBehaviour
{
    private Camera _mainCamera;

    [SerializeField] 
    private SelectorService selectorService;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    void OnFire(InputValue inputValue)
    {
        // var rayhit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        // if (!rayhit.collider) return;
        selectorService.Place();
    }
}
