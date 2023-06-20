using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPointButterfly : BaseButterflyFlying
{

    [SerializeField]
    private Vector2 _movementSpeedMinMax;

    [SerializeField]
    private Vector2 _startPointSquare;

    [SerializeField]
    private Vector2 _endPointSquare;

    private float _movementSpeed;

    private void Start()
    {
        _movementSpeed = Random.Range(_movementSpeedMinMax.x, _movementSpeedMinMax.y);
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
    }
}
