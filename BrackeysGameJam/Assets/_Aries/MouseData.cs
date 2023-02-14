using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MouseData
{
    public int health;
    public int max_health;
    public int speed;
    public int lifespan;
    public System.Action OnStatsChanged;
}
