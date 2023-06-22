using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAround : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    Rigidbody2D _body;

    private Vector2 _previousPosition;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if((_body.position - _previousPosition).x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }

        _previousPosition = _body.position; 
    }
}
