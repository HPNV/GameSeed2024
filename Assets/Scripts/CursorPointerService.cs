using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorPointerService : PointerService
{
    private Vector3 lookInput;
    
    void OnPointer(InputValue inputValue)
    {
        lookInput = inputValue.Get<Vector2>();
        lookInput.z = Camera.main.nearClipPlane;
    }

    public override Vector3 GetPointerPosition()
    {
        return Camera.main.ScreenToWorldPoint(lookInput);
    }
}
