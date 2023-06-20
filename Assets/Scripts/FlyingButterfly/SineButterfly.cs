using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineButterfly : BaseButterflyFlying
{
    private bool _moveLeft;

    [SerializeField]
    private Vector2 _movementSpeedMinMax;

    [Header("Sin wave attributes")]

    [SerializeField]
    private Vector2 _frequencyMinMax;

    [SerializeField]
    private Vector2 _amplitudeMinMax;

    [SerializeField]
    private Vector2 _sinSpeedMinMax;

    private float _time;
    private float _movementSpeed;
    private float _amplitude;
    private float _frequency;
    private float _sinSpeed;

    private void Start()
    {
        _movementSpeed = Random.Range(_movementSpeedMinMax.x, _movementSpeedMinMax.y);
        _amplitude = Random.Range(_amplitudeMinMax.x, _amplitudeMinMax.y);
        _frequency = Random.Range(_frequencyMinMax.x, _frequencyMinMax.y);
        _sinSpeed = Random.Range(_sinSpeedMinMax.x, _sinSpeedMinMax.y);



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

   
        _time += Time.deltaTime * _sinSpeed;
        target.y += Mathf.Sin(_time *  _frequency) * _amplitude;

        Vector2 newPosition = Vector2.MoveTowards(_body.position, target, _movementSpeed * Time.deltaTime);
        _body.MovePosition(newPosition);
    }
}
