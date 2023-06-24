using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum ScreenType
{
    Top,
    Transition,
    Bottom
}

public class GlobalVariables : Singleton<GlobalVariables>
{
    public ScreenType ScreenState { get; set; } = ScreenType.Transition;

    public int CompletedOrders { get; set; }

    public float CurrentTime { get; set; }

    public bool GameOver { get; set; }

    public bool  GameComplete { get; set; } 
}
