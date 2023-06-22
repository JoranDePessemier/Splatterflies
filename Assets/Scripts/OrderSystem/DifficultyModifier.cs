using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DifficultyModifier
{
    [SerializeField]
    private int _modifyMinScore;

    [SerializeField]
    private float _maxTimePerOrder;

    [SerializeField]
    private int _maxButterflyAmount;

    [SerializeField]
    private int _butterflyTypeAmount;

    [SerializeField]
    private int _orderAmount;

    public int ModifyMinScore => _modifyMinScore;
    public float MaxTimePerOrder => _maxTimePerOrder;
    public int MaxButterflyAmount => _maxButterflyAmount;
    public int ButterflyTypeAmount => _butterflyTypeAmount;
    public int OrderAmount => _orderAmount;

}
