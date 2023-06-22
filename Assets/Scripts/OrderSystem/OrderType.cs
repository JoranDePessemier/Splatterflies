using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OrderType
{
    [SerializeField]
    private ButterflyType _type;

    public ButterflyType Type => _type;

    [SerializeField]
    private Sprite _previewSprite;

    public Sprite PreviewSprite => _previewSprite;

    [SerializeField]
    private Sprite _filledInSprite;

    public Sprite FilledInSprite => _filledInSprite;
}
