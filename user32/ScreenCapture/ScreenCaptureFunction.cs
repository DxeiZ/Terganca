using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class ScreenCaptureFunction
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetDC(IntPtr hwnd);

    [DllImport("user32.dll")]
    private static extern IntPtr ReleaseDC(IntPtr hwnd, IntPtr hdc);

    [DllImport("gdi32.dll")]
    private static extern IntPtr DeleteObject(IntPtr hObject);

    [DllImport("gdi32.dll")]
    private static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight,
        IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

    private const int SRCCOPY = 0x00CC0020;

    public static Bitmap CaptureScreen()
    {
        Rectangle bounds = Screen.PrimaryScreen.Bounds;
        Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);

        using (Graphics graphics = Graphics.FromImage(bitmap))
        {
            IntPtr hdcDest = graphics.GetHdc();
            IntPtr hdcSrc = GetDC(IntPtr.Zero);
            BitBlt(hdcDest, 0, 0, bounds.Width, bounds.Height, hdcSrc, 0, 0, SRCCOPY);
            graphics.ReleaseHdc(hdcDest);
            ReleaseDC(IntPtr.Zero, hdcSrc);
        }

        return bitmap;
    }
}
