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

    [SerializeField]
    private LayerMask _butterflyLayerMask;

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
            StartCoroutine(Utilities.MoveToPoint(_mousePosition,StartReturning, _body, _hookSpeed));
            _state = GrappleState.Grappling;
        }
    }

    private void StartReturning()
    {
        _state = GrappleState.Returning;
        StartCoroutine(Utilities.MoveToPoint(_rotatorTransform.position, () => { _state = GrappleState.OnBase; },_body,_hookSpeed));

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (Utilities.IsInMask(collisionObject, _butterflyLayerMask))
        {
            if(!collisionObject.TryGetComponent<BaseButterflyFlying>(out BaseButterflyFlying butterfly))
            {
                Debug.LogError("A butterfly that is in the flying layer does not have the butterfly flying script, you stupid");
            }

            butterfly.Caught(_rotatorTransform.position,_hookSpeed);
            StopAllCoroutines();
            StartReturning();
            
        }
    }
}
