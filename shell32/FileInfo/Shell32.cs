using System;
using System.Runtime.InteropServices;

public static class Shell32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SHFILEINFO
    {
        public IntPtr hIcon;
        public IntPtr iIcon;
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string szTypeName;
    }

    [DllImport("shell32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SHGetFileInfo(
        string pszPath,
        uint dwFileAttributes,
        ref SHFILEINFO psfi,
        uint cbFileInfo,
        uint uFlags);

    public const uint SHGFI_ICON = 0x000000100;
    public const uint SHGFI_DISPLAYNAME = 0x000000200;
    public const uint SHGFI_TYPENAME = 0x000000400;
    public const uint SHGFI_ATTRIBUTES = 0x000000800;
    public const uint SHGFI_PIDL = 0x000000008;
    public const uint SHGFI_USEFILEATTRIBUTES = 0x000000010;
}
