using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtil
{
    //https://forum.unity.com/threads/mapping-or-scaling-values-to-a-new-range.180090/
    public static float Rescale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }
}
