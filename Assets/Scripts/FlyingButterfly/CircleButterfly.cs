using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CircleButterflyMovementState
{
    GoingToPoint,
    ExpandingRadius,
    Stable
}

public class CircleButterfly: BaseButterflyFlying
{

    [SerializeField]
    private Vector2 _movementSpeedMinMax;

    [SerializeField]
    private float _radiusExpandingSpeed;

    [SerializeField]
    private Vector2 _radiusMinMax;

    [SerializeField]
    private Vector2 _startPointSquare;

    [SerializeField]
    private Vector2 _endPointSquare;

    [Header("Sine Radius Properties")]


    private float _movementSpeed;
    private float _radius;
    private float _currentRadius;
    private float _angle;
    private Vector2 _centerPoint;

    private bool _isMoving;

    private CircleButterflyMovementState _state = CircleButterflyMovementState.GoingToPoint;

    private void Start()
    {
        _movementSpeed = Random.Range(_movementSpeedMinMax.x, _movementSpeedMinMax.y);
        _radius = Random.Range(_radiusMinMax.x,_radiusMinMax.y);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(_endPointSquare.x, _startPointSquare.y, 0), new Vector3(_endPointSquare.x, _endPointSquare.y, 0));
        Gizmos.DrawLine(new Vector3(_startPointSquare.x, _startPointSquare.y, 0), new Vector3(_startPointSquare.x, _endPointSquare.y, 0));
        Gizmos.DrawLine(new Vector3(_startPointSquare.x, _startPointSquare.y, 0), new Vector3(_endPointSquare.x, _startPointSquare.y, 0));
        Gizmos.DrawLine(new Vector3(_startPointSquare.x, _endPointSquare.y, 0), new Vector3(_endPointSquare.x, _endPointSquare.y, 0));
    }

    protected override void UpdateMovement()
    {

        switch (_state)
        {
            case CircleButterflyMovementState.GoingToPoint:

                if(!_isMoving)
                {
                    _centerPoint = new Vector2(Random.Range(_startPointSquare.x, _endPointSquare.x), Random.Range(_startPointSquare.y, _endPointSquare.y));
                    _isMoving = true;
                    StartCoroutine(Utilities.MoveToPoint(_centerPoint, () => { _isMoving = false; _state = CircleButterflyMovementState.ExpandingRadius; }, _body, _movementSpeed));
                }
                

                break;

            case CircleButterflyMovementState.ExpandingRadius:

                _currentRadius = Mathf.MoveTowards(_currentRadius, _radius, _radiusExpandingSpeed * Time.deltaTime);    

                if(_currentRadius >= _radius)
                {
                    _state = CircleButterflyMovementState.Stable;
                }

                break;

        }

        if(_state != CircleButterflyMovementState.GoingToPoint)
        {
            float x = _centerPoint.x + Mathf.Cos(_angle) * _currentRadius;
            float y = _centerPoint.y + Mathf.Sin(_angle) * _currentRadius;

            _body.MovePosition(new Vector2(x, y));


            _angle += Time.deltaTime * _movementSpeed;
        }
    }
}
