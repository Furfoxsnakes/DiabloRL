using Godot;

namespace DiabloRL.Scripts.Common;

public static partial class Utils {
    public static int Clamp(this int value, int min, int max) {
        return Mathf.Clamp(value, min, max);
    }
}