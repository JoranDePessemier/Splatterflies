using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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

    [SerializeField]
    private LineRenderer _lineRenderer;

    [SerializeField]
    private SpriteRenderer _sprite;

    [SerializeField]
    private GameObject _aimingReticle;

    [SerializeField]
    private float _reticleIncreaseAmount;

    [SerializeField]
    private float _reticleAppearDissapearTime;


    [SerializeField]
    private UnityEvent _grappled;

    [SerializeField]
    private UnityEvent _StartGrappling;

    [SerializeField]
    private UnityEvent _returnToWell;


    private Transform _aimingReticleTransform;


    private GrappleState _state = GrappleState.OnBase;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _controls = new Controls();
        _controls.PlayerInput.ActionPressed.performed += StartHooking;
        _mainCam = Camera.main;
        _aimingReticleTransform = _aimingReticle.transform;

        LeanTween.alpha(_aimingReticle, 0, 0);
        LeanTween.scale(_aimingReticle, _reticleIncreaseAmount * Vector3.one, 0);
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
        if(GlobalVariables.Instance.ScreenState == ScreenType.Top && Time.timeScale != 0)
        {
            _controls.Enable();
        }
        else
        {
            _controls.Disable();
        }

        if(_state == GrappleState.OnBase)
        {
            _sprite.enabled = false;
        }
        else
        {
            _sprite.enabled = true;
        }


        _mousePosition = Utilities.GetMousePositionWorldSpace(_controls, _mainCam);

        if (_state == GrappleState.OnBase)
        {
            _rotatorTransform.up = _mousePosition - (Vector2)_rotatorTransform.position;
        }


        _lineRenderer.SetPosition(0, _rotatorTransform.position);
        _lineRenderer.SetPosition(1, _body.position);
    }

    private void StartHooking(InputAction.CallbackContext obj)
    {
        if (_state == GrappleState.OnBase && _mousePosition.y > _rotatorTransform.position.y)
        {
            StartCoroutine(Utilities.MoveToPoint(_mousePosition,StartReturning, _body, _hookSpeed));
            _state = GrappleState.Grappling;
            _StartGrappling.Invoke();

            _reticleAppear();
        }
    }

    private void _reticleAppear()
    {
        _aimingReticleTransform.position = _mousePosition;
        LeanTween.alpha(_aimingReticle,1,_reticleAppearDissapearTime).setEase(LeanTweenType.easeInCubic);
        LeanTween.scale(_aimingReticle, Vector3.one, _reticleAppearDissapearTime).setEase(LeanTweenType.easeInCubic);
    }

    private void StartReturning()
    {
        _state = GrappleState.Returning;
        ReticleDissapear();
        StartCoroutine(Utilities.MoveToPoint(_rotatorTransform.position, () => { _state = GrappleState.OnBase; _returnToWell.Invoke(); },_body,_hookSpeed));

    }

    private void ReticleDissapear()
    {
        LeanTween.cancel(_aimingReticle);
        LeanTween.alpha(_aimingReticle, 0, _reticleAppearDissapearTime).setEase(LeanTweenType.easeOutCubic);
        LeanTween.scale(_aimingReticle, Vector3.one * _reticleIncreaseAmount, _reticleAppearDissapearTime).setEase(LeanTweenType.easeOutCubic);
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
            _grappled.Invoke();
            StartReturning();
            
        }
    }


}
