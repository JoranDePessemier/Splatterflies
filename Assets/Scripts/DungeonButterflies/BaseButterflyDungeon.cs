using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BaseButterflyDungeon : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    protected Rigidbody2D _body;
    protected Controls _controls;

    private Camera _mainCam;

    [SerializeField]
    private ButterflyType _type;


    [SerializeField]
    private Vector2 _movementSpeedMinMax;

    [SerializeField]
    private Vector2 _startPointSquare;

    [SerializeField]
    private Vector2 _endPointSquare;

    [SerializeField]
    private DeadButterfly _deadButterFlyToSpawn;

    [SerializeField]
    private GameObject _spriteObject;

    [SerializeField]
    private UnityEvent _clicked;

    public event EventHandler<WasCaughtEventArgs> WasCompleted;


    private float _movementSpeed;
    private bool _isMoving = false;
    protected bool _isHolding = false;

    public bool PlayerIsHolding => _isHolding;

    private bool _isInactive = false;

    public ButterflyType Type { get { return _type; } private set { _type = value; } }

    private void Awake()
    {
        _body = this.GetComponent<Rigidbody2D>();
        _movementSpeed = UnityEngine.Random.Range(_movementSpeedMinMax.x, _movementSpeedMinMax.y);

        _mainCam = Camera.main;

        _controls = new Controls();

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
            Vector2 randomPoint = new Vector2(UnityEngine.Random.Range(_startPointSquare.x, _endPointSquare.x), UnityEngine.Random.Range(_startPointSquare.y, _endPointSquare.y));
            _isMoving = true;
            StartCoroutine(Utilities.MoveToPoint(randomPoint, () => _isMoving = false, _body, _movementSpeed));
        }

        if (_isHolding)
        {
            _body.MovePosition(Utilities.GetMousePositionWorldSpace(_controls, _mainCam));
        }

        if(_isInactive)
        {
            _body.position = new Vector2(200, 200);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isHolding = true;
        Clicked();
    }

    protected virtual void Clicked()
    {
        _clicked.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isHolding = false;
    }

    public virtual GameObject Completed()
    {
        Destroy(this.gameObject);
        OnWasCompleted(new WasCaughtEventArgs(Type));
        return GameObject.Instantiate(_deadButterFlyToSpawn.gameObject, _body.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));
    }

    private void OnWasCompleted(WasCaughtEventArgs eventArgs)
    {
        var handler = WasCompleted;
        handler?.Invoke(this,eventArgs);
    }

    internal void SetInactive()
    {
        _isInactive = true;
        _spriteObject.SetActive(false);
        this.GetComponent<Collider2D>().enabled = false;
    }
}
