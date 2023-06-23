using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CursorController : MonoBehaviour
{
    private Transform _transform;

    private Controls _controls;

    [SerializeField]
    private UnityEvent _clicked;

    private void Awake()
    {
        _transform = this.transform;
        _controls = new Controls();
        _controls.Enable();
        Cursor.visible = false;
        _controls.PlayerInput.ActionPressed.performed += CursorClicked;
    }

    private void CursorClicked(InputAction.CallbackContext obj)
    {
        _clicked.Invoke();
    }

    private void Update()
    {
        _transform.position = _controls.PlayerInput.MousePosition.ReadValue<Vector2>();
    }
}
