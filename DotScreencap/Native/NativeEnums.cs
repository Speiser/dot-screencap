#pragma warning disable 1591
using System;

namespace DotScreencap.Native
{
    // Warnings disabled due to the nature of pinvokes.
    // Review msdn and other existing documentation for more information on contents contained here.
    // File access modes
    [Flags]
    public enum OpenFileMode
    {
        Read = 0x00000000,
        Write = 0x00000001,
        ReadWrite = 0x00000002,
        ShareCompat = 0x00000000,
        ShareExclusive = 0x00000010,
        ShareDenyWrite = 0x00000020,
        ShareDenyRead = 0x00000030,
        ShareDenyNone = 0x00000040,
        Parse = 0x00000100,
        Delete = 0x00000200,
        Verify = 0x00000400,
        Cancel = 0x00000800,
        Create = 0x00001000,
        Prompt = 0x00002000,
        Exist = 0x00004000,
        Reopen = 0x00008000
    }
}

#pragma warning restore 1591