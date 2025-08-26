using System.Runtime.InteropServices;

namespace SpoutDx.Net.Interop;

public sealed class SpoutReceiver : IDisposable
{
    private readonly IntPtr _spoutDxPointer;

    public SpoutReceiver(IntPtr d3d11DevicePointer)
    {
        _spoutDxPointer = Interop.SpoutDx_Create(d3d11DevicePointer);
    }

    public void SetReceiverName(string senderName)
    {
        Interop.SpoutDx_SetReceiverName(_spoutDxPointer, senderName);
    }

    public bool ReceiveTexture()
    {
        return Interop.SpoutDx_ReceiveTexture(_spoutDxPointer);
    }

    public bool IsUpdated()
    {
        return Interop.SpoutDx_IsUpdated(_spoutDxPointer);
    }

    public bool IsFrameNew()
    {
        return Interop.SpoutDx_IsFrameNew(_spoutDxPointer);
    }

    public IntPtr GetSenderTexture()
    {
        return Interop.SpoutDx_GetSenderTexture(_spoutDxPointer);
    }

    public uint GetSenderFormat()
    {
        return Interop.SpoutDx_GetSenderFormat(_spoutDxPointer);
    }

    public uint GetSenderWidth()
    {
        return Interop.SpoutDx_GetSenderWidth(_spoutDxPointer);
    }

    public uint GetSenderHeight()
    {
        return Interop.SpoutDx_GetSenderHeight(_spoutDxPointer);
    }

    public long GetSenderFrame()
    {
        return Interop.SpoutDx_GetSenderFrame(_spoutDxPointer);
    }

    public string[] GetSenderNames()
    {
        var senderList = Interop.SpoutDx_GetSenderList(_spoutDxPointer, out var count);

        var names = new string[count];
        for (var i = 0; i < count; i++)
        {
            IntPtr strPtr = Marshal.ReadIntPtr(senderList, i * IntPtr.Size);

            names[i] = Marshal.PtrToStringAnsi(strPtr)!;
        }

        Interop.SpoutDx_FreeSenderList(senderList, count);

        return names;
    }

    public void Dispose()
    {
        Interop.SpoutDx_ReleaseReceiver(_spoutDxPointer);
    }
}