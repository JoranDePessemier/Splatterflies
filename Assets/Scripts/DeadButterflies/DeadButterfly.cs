using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DeadButterfly : MonoBehaviour, IPointerDownHandler , IPointerUpHandler
{
    private bool _isHolding;

    private Rigidbody2D _body;
    private Camera _mainCam;
    private Controls _controls;
    private Collider2D _collider;

    [SerializeField]
    private ButterflyType _type;

    [SerializeField]
    private UnityEvent _spawn;

    [SerializeField]
    private GameObject _particle;

    public ButterflyType Type => _type;

    public bool IsHolding => _isHolding;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();

        _mainCam = Camera.main;

        _controls = new Controls();
        _controls.Enable();

        _collider = this.GetComponent<Collider2D>();
        GameObject.Instantiate(_particle.transform, transform.position, transform.rotation).GetComponent<ParticleSystem>().Play();
        
    }

    private void Start()
    {
        _spawn.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isHolding = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isHolding = false;
    }
    private void FixedUpdate()
    {

        if (_isHolding)
        {
            _body.MovePosition(Utilities.GetMousePositionWorldSpace(_controls, _mainCam));
        }
        
    }

    public void PinOnPosition(Transform _pinTransform)
    {
        _collider.enabled = false;
    }
}
