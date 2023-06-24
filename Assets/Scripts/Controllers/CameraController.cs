using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
    private GameObject _camObject;
    private Controls _controls;

    [SerializeField]
    private Vector3 _camPositionUp;

    [SerializeField]
    private Vector3 _camPositionDown;

    [SerializeField]
    private Vector3 _camPositionMiddle;

    [SerializeField]
    private float _movementTime;

    [SerializeField]
    private UnityEvent _moveDown;

    [SerializeField]
    private UnityEvent _moveUp;

    public bool EnableDownMovement { get; set; } = true;

    public event EventHandler<EventArgs> WentToMiddle;

    public event EventHandler<EventArgs> WentUpAfterStart;

    private void Awake()
    {
        _camObject = this.gameObject;
        _controls = new Controls();
        transform.position = _camPositionMiddle;
        _controls.PlayerInput.ChangeScenePressed.performed += ChangePosition;

        LeanTween.moveLocal(_camObject, _camPositionUp, _movementTime / 2).setEase(LeanTweenType.easeOutCubic).setOnComplete(() => { GlobalVariables.Instance.ScreenState = ScreenType.Top; OnWentUpAfterStart(EventArgs.Empty); });
    }

    public void GoToMiddle()
    {
        GlobalVariables.Instance.ScreenState = ScreenType.Transition;
        LeanTween.cancel(_camObject);   
        LeanTween.moveLocal(_camObject, _camPositionMiddle, _movementTime).setEase(LeanTweenType.easeInCubic).setOnComplete(() => OnWentToMiddle(EventArgs.Empty));
    }

    private void ChangePosition(InputAction.CallbackContext obj)
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        if(GlobalVariables.Instance.ScreenState == ScreenType.Top && EnableDownMovement)
        {
            GlobalVariables.Instance.ScreenState = ScreenType.Transition;
            _moveDown.Invoke();
            LeanTween.moveLocal(_camObject,_camPositionDown, _movementTime).setEase(LeanTweenType.easeInOutCubic).setOnComplete(() => GlobalVariables.Instance.ScreenState = ScreenType.Bottom); 
        }
        else if(GlobalVariables.Instance.ScreenState == ScreenType.Bottom)
        {
            _moveUp.Invoke();
            GlobalVariables.Instance.ScreenState = ScreenType.Transition;
            LeanTween.moveLocal(_camObject, _camPositionUp, _movementTime).setEase(LeanTweenType.easeInOutCubic).setOnComplete(() => GlobalVariables.Instance.ScreenState = ScreenType.Top); 
        }
        
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable(); 
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(_camPositionDown,_camPositionUp);
        Gizmos.DrawIcon(_camPositionMiddle,"stupd");
    }

    private void OnWentToMiddle(EventArgs eventArgs)
    {
        var handler = WentToMiddle;
        handler?.Invoke(this, eventArgs);
    }

    private void OnWentUpAfterStart(EventArgs eventArgs)
    {
        var handler = WentUpAfterStart;
        handler?.Invoke(this, eventArgs);
    }
}
