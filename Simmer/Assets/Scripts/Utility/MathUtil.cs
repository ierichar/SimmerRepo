public static class MathUtil
{
    /// <summary>
    /// Scales a value bound within a range to a new range.
    /// Mins cannot be greater than maxes and maxes cannot
    /// be less than mins.
    /// Ex: Float x = 0.25 in range [0, 1].
    /// Rescale(0, 1, 4, 12, x) = 6 because in the
    /// old range 0.25 is 25% from the lower to the upper bound
    /// so for the new range [4, 12] the 25% from lower value is 6.
    /// https://forum.unity.com/threads/mapping-or-scaling-values-to-a-new-range.180090/
    /// </summary>
    /// <param name="OldMin">
    /// Old lower bound.
    /// </param>
    /// <param name="OldMax">
    /// Old upper bound.
    /// </param>
    /// <param name="NewMin">
    /// New lower bound.
    /// </param>
    /// <param name="NewMax">
    /// New upper bound.
    /// </param>
    /// <param name="OldValue">
    /// Value to scale. Must be within range of [OldMin, OldMax].
    /// </param>
    public static float Rescale(
        float OldMin, float OldMax
        , float NewMin, float NewMax
        , float OldValue)
    {
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }
}
