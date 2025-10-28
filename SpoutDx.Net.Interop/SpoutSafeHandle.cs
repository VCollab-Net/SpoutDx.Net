using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace SpoutDx.Net.Interop;

internal class SpoutSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    private readonly HandleType _handleType;

    private SpoutSafeHandle(HandleType handleType) : base(ownsHandle: true)
    {
        _handleType = handleType;
    }

    protected override bool ReleaseHandle()
    {
        if (_handleType is HandleType.Sender)
        {
            Interop.SpoutDx_ReleaseSender(handle);
        }
        else
        {
            Interop.SpoutDx_ReleaseReceiver(handle);
        }

        return true;
    }

    public static SpoutSafeHandle Allocate(HandleType handleType, IntPtr d3d11DevicePointer)
    {
        var handle = Interop.SpoutDx_Create(d3d11DevicePointer);

        var safeHandle = new SpoutSafeHandle(handleType);
        safeHandle.SetHandle(handle);

        return safeHandle;
    }

    internal enum HandleType
    {
        Sender,
        Receiver
    }
}