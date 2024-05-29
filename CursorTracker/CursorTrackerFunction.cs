using System.Runtime.InteropServices;

public class CursorTrackerFunction
{
    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out POINT lpPoint);

    public struct POINT
    {
        public int X;
        public int Y;
    }

    public static POINT GetCursorPosition()
    {
        GetCursorPos(out POINT lpPoint);
        return lpPoint;
    }
}
