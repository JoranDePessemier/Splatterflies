using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightLineButterfly : BaseButterflyFlying
{
    private bool _moveLeft;

    [SerializeField]
    private Vector2 _movementSpeedMinMax;


    private float _movementSpeed;

    private void Start()
    {
        _movementSpeed = Random.Range(_movementSpeedMinMax.x, _movementSpeedMinMax.y);

        Type = ButterflyType.StraightLine;

        if (_body.position.x > 0)
        {
            _moveLeft = true;
        }
    }

    protected override void UpdateMovement()
    {
        base.UpdateMovement();

        Vector2 target = _body.position;

        if (_moveLeft)
        {
            target.x -= _movementSpeed;
        }
        else
        {
            target.x += _movementSpeed;
        }

        Vector2 newPosition = Vector2.MoveTowards(_body.position, target, _movementSpeed * Time.deltaTime);
        _body.MovePosition(newPosition);
    }
}
