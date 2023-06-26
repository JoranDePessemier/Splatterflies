using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

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

    [SerializeField]
    private Animator _cameraAnimator;

    [SerializeField]
    private float _musicFadeInOutSpeed;

    public bool EnableDownMovement { get; set; } = true;

    public event EventHandler<EventArgs> WentUpAfterStart;



    private void Awake()
    {
        _camObject = this.gameObject;
        _controls = new Controls();
        transform.position = _camPositionMiddle;
        _controls.PlayerInput.ChangeScenePressed.performed += ChangePosition;


        LeanTween.moveLocal(_camObject, _camPositionUp, _movementTime / 2).setEase(LeanTweenType.easeOutCubic).setOnComplete(() => { GlobalVariables.Instance.ScreenState = ScreenType.Top; OnWentUpAfterStart(EventArgs.Empty); });
    }

    public void ScreenShake()
    {
        _cameraAnimator.SetTrigger("ScreenShake");
    }

    public void GoToMiddle(Action completedAction)
    {
        GlobalVariables.Instance.ScreenState = ScreenType.Transition;
        LeanTween.cancel(_camObject);   
        LeanTween.moveLocal(_camObject, _camPositionMiddle, _movementTime).setEase(LeanTweenType.easeInCubic).setOnComplete(completedAction);
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

            MusicManager.Instance.StartFadeOut("GardenBody",_musicFadeInOutSpeed);
            MusicManager.Instance.StartFadeOut("GardenMelody", _musicFadeInOutSpeed);
            MusicManager.Instance.StartFadeIn("DungeonBody", _musicFadeInOutSpeed);
            MusicManager.Instance.StartFadeIn("DungeonMelody", _musicFadeInOutSpeed);


            LeanTween.moveLocal(_camObject,_camPositionDown, _movementTime).setEase(LeanTweenType.easeInOutCubic).setOnComplete(() => GlobalVariables.Instance.ScreenState = ScreenType.Bottom); 
        }
        else if(GlobalVariables.Instance.ScreenState == ScreenType.Bottom)
        {
            _moveUp.Invoke();

            MusicManager.Instance.StartFadeIn("GardenBody", _musicFadeInOutSpeed);
            MusicManager.Instance.StartFadeIn("GardenMelody", _musicFadeInOutSpeed);
            MusicManager.Instance.StartFadeOut("DungeonBody", _musicFadeInOutSpeed);
            MusicManager.Instance.StartFadeOut("DungeonMelody", _musicFadeInOutSpeed);


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


    private void OnWentUpAfterStart(EventArgs eventArgs)
    {
        var handler = WentUpAfterStart;
        handler?.Invoke(this, eventArgs);
    }
}
