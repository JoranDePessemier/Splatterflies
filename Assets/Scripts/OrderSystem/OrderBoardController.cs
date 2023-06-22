using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderBoardController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer[] _orderSpots;

    [SerializeField]
    private OrderType[] _orderTypes;

    [SerializeField]
    private LayerMask _deadButterflyMask;

    [SerializeField]
    private Vector2 _completePosition;

    [SerializeField]
    private Vector2 _openPosition;

    [SerializeField]
    private float _movementTime;

    private List<OrderType> _currentOrdertypes = new List<OrderType>();

    private int _unCompletedOrders;

    private bool _hasActiveOrder;

    public bool HasActiveOrder => _hasActiveOrder;

    public event EventHandler<EventArgs> OrderCompleted;

    private void Awake()
    {
        transform.position = _completePosition;
    }

    public void GenerateOrder(int orderAmount, int availableOrderTypes)
    {
        foreach(SpriteRenderer renderer in _orderSpots)
        {
            renderer.sprite = null;
        }
        _currentOrdertypes.Clear();
        _unCompletedOrders = orderAmount;

        _hasActiveOrder = true;

        for (int i = 0; i < orderAmount; i++)
        {
            _currentOrdertypes.Add(_orderTypes[UnityEngine.Random.Range(0, availableOrderTypes)]);
            _orderSpots[i].sprite = _currentOrdertypes[i].PreviewSprite;
        }

        LeanTween.moveLocal(gameObject, _openPosition, _movementTime).setEase(LeanTweenType.easeOutBounce);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(_completePosition, _openPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        if(!collisionObject.TryGetComponent<DeadButterfly>(out DeadButterfly butterfly))
        {
            return;
        }

        if(Utilities.IsInMask(collisionObject,_deadButterflyMask) && butterfly.IsHolding)
        {
            for (int i = 0; i <_currentOrdertypes.Count; i++)
            {
                OrderType type = _currentOrdertypes[i];

                if(type.Type == butterfly.Type && _orderSpots[i].sprite == type.PreviewSprite)
                {
                    Destroy(butterfly.gameObject);
                    _orderSpots[i].sprite = type.FilledInSprite;
                    _unCompletedOrders--;
                    if(_unCompletedOrders <= 0 )
                    {
                        CompleteOrder();
                    }
                    return; 
                }
            }
        }
    }

    private void CompleteOrder()
    {
        LeanTween.moveLocal(gameObject, _completePosition, _movementTime).setEase(LeanTweenType.easeInBack).setOnComplete(() => { _hasActiveOrder = false; OnOrderCompleted(EventArgs.Empty);  });

    }

    private void OnOrderCompleted(EventArgs eventArgs)
    {
        var handler = OrderCompleted;
        handler?.Invoke(this, eventArgs);
    }
}
