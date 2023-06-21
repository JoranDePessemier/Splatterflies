using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScreenType
{
    Top,
    Transition,
    Bottom
}

public class GlobalVariables : Singleton<GlobalVariables>
{
    public ScreenType ScreenState { get; set; }
}
