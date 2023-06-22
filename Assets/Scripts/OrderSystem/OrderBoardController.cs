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

    private GameObject _boardObject;
    private int _unCompletedOrders;

    private void Awake()
    {
        _boardObject = this.gameObject;
        transform.position = _completePosition;
        GenerateOrder(4, 20, 4);
    }

    public void GenerateOrder(int orderAmount, float orderTime, int availableOrderTypes)
    {
        _unCompletedOrders = orderAmount;

        for (int i = 0; i < orderAmount; i++)
        {
            _currentOrdertypes.Add(_orderTypes[UnityEngine.Random.Range(0, availableOrderTypes)]);
            _orderSpots[i].sprite = _currentOrdertypes[i].PreviewSprite;
        }

        LeanTween.moveLocal(_boardObject, _openPosition, _movementTime).setEase(LeanTweenType.easeOutBounce);
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
        throw new NotImplementedException();
    }
}
