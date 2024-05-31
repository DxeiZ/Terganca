using System;
using System.Drawing;
using System.Runtime.InteropServices;

public class ColorPicker
{
    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out POINT lpPoint);

    [DllImport("gdi32.dll")]
    private static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

    [DllImport("user32.dll")]
    private static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

    public struct POINT
    {
        public int X;
        public int Y;
    }

    public static Color GetColorAtCursor()
    {
        POINT cursorPos;
        GetCursorPos(out cursorPos);

        IntPtr hdc = GetDC(IntPtr.Zero);
        uint pixel = GetPixel(hdc, cursorPos.X, cursorPos.Y);
        ReleaseDC(IntPtr.Zero, hdc);

        Color color = Color.FromArgb((int)(pixel & 0x000000FF),
                                     (int)(pixel & 0x0000FF00) >> 8,
                                     (int)(pixel & 0x00FF0000) >> 16);
        return color;
    }
}
