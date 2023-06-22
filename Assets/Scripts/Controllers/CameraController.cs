using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    private float _movementTime;

    private void Awake()
    {
        _camObject = this.gameObject;
        _controls = new Controls();
        _controls.PlayerInput.ChangeScenePressed.performed += ChangePosition;
    }

    private void ChangePosition(InputAction.CallbackContext obj)
    {
        if(GlobalVariables.Instance.ScreenState == ScreenType.Top)
        {
            GlobalVariables.Instance.ScreenState = ScreenType.Transition;
            LeanTween.moveLocal(_camObject,_camPositionDown, _movementTime).setEase(LeanTweenType.easeInOutCubic).setOnComplete(() => GlobalVariables.Instance.ScreenState = ScreenType.Bottom); 
        }
        else if(GlobalVariables.Instance.ScreenState == ScreenType.Bottom)
        {
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
    }
}
