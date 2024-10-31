using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Exactly what the name implies, it's just storage for certain cooldown timings on actions
public static class PlayerCooldowns
{
    public static float DODGE_COOLDOWN = 1.0f, 
        BLOCK_WINDUP = 1.0f, 
        SWING_COOLDOWN = 1.0f, 
        SWING_DURATION = 1.0f, 
        SWING_CANCELWINDOW = 0.2f; //lower is faster for all stats except speed
}
