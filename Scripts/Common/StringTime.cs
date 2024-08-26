using System;

public static class StringTime
{
    public static string SecondToTimeString(float second)
    {
        return TimeSpan.FromSeconds(second).ToString(@"mm\:ss\:ff");
    }
}