using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseButterflyDungeon : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    protected Rigidbody2D _body;
    private Controls _controls;

    private Camera _mainCam;

    [SerializeField]
    private ButterflyType _type;

    [SerializeField]
    private Vector2 _movementSpeedMinMax;

    [SerializeField]
    private Vector2 _startPointSquare;

    [SerializeField]
    private Vector2 _endPointSquare;


    private float _movementSpeed;
    private bool _isMoving = false;
    private bool _isHolding = false;

    public ButterflyType Type { get { return _type; } private set { _type = value; } }

    private void Awake()
    {
        _body = this.GetComponent<Rigidbody2D>();
        _movementSpeed = Random.Range(_movementSpeedMinMax.x, _movementSpeedMinMax.y);

        _mainCam = Camera.main;

        _controls = new Controls();
        _controls.Enable();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(_endPointSquare.x, _startPointSquare.y, 0), new Vector3(_endPointSquare.x, _endPointSquare.y, 0));
        Gizmos.DrawLine(new Vector3(_startPointSquare.x, _startPointSquare.y, 0), new Vector3(_startPointSquare.x, _endPointSquare.y, 0));
        Gizmos.DrawLine(new Vector3(_startPointSquare.x, _startPointSquare.y, 0), new Vector3(_endPointSquare.x, _startPointSquare.y, 0));
        Gizmos.DrawLine(new Vector3(_startPointSquare.x, _endPointSquare.y, 0), new Vector3(_endPointSquare.x, _endPointSquare.y, 0));
    }

    private void FixedUpdate()
    {
        if (!_isMoving)
        {
            Vector2 randomPoint = new Vector2(Random.Range(_startPointSquare.x, _endPointSquare.x), Random.Range(_startPointSquare.y, _endPointSquare.y));
            _isMoving = true;
            StartCoroutine(Utilities.MoveToPoint(randomPoint, () => _isMoving = false, _body, _movementSpeed));
        }

        if (_isHolding)
        {
            _body.MovePosition(Utilities.GetMousePositionWorldSpace(_controls, _mainCam));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isHolding = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isHolding = false;
    }
}
