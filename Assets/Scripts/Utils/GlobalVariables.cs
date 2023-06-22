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
    public ScreenType ScreenState { get; set; }

    public int CompletedOrders { get; set; }
}
