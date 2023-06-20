using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GrappleState
{
    OnBase,
    Grappling,
    Returning
}

public class HookController : MonoBehaviour
{
    private Rigidbody2D _body;
    private Camera _mainCam;

    private Controls _controls;

    private Vector2 _mousePosition;

    [SerializeField]
    private Transform _rotatorTransform;

    [SerializeField]
    private float _hookSpeed = 1f;

    private GrappleState _state = GrappleState.OnBase;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _controls = new Controls();
        _controls.PlayerInput.ActionPressed.performed += StartHooking;
        _mainCam = Camera.main;
    }

    private void OnEnable()
    {
        _controls.Enable();

    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void Update()
    {
        _mousePosition = Utilities.GetMousePositionWorldSpace(_controls, _mainCam);

        if (_state == GrappleState.OnBase)
        {
            _rotatorTransform.up = _mousePosition - (Vector2)_rotatorTransform.position;
        }
        
    }

    private void StartHooking(InputAction.CallbackContext obj)
    {
        if (_state == GrappleState.OnBase)
        {
            StartCoroutine(MoveToPoint(_mousePosition,StartReturning));
            _state = GrappleState.Grappling;
        }
    }

    private void StartReturning()
    {
        _state = GrappleState.Returning;
        StartCoroutine(MoveToPoint(_rotatorTransform.position, () => { _state = GrappleState.OnBase; }));

    }

    private IEnumerator MoveToPoint(Vector2 point,Action OnMoveCompleted)
    {
        while(_body.position != point)
        {
            Vector2 nextPosition = Vector2.MoveTowards(_body.position, point, _hookSpeed * Time.deltaTime);
            _body.MovePosition(nextPosition);
            yield return new WaitForFixedUpdate();
        }

        OnMoveCompleted();
    }
}
