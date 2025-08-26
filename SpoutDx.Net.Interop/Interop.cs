using System.Runtime.InteropServices;

namespace SpoutDx.Net.Interop;

internal partial class Interop
{
    private const string DllName = "SpoutDx.Net.dll";

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial IntPtr SpoutDx_Create(IntPtr pDevice);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SpoutDx_SetReceiverName(IntPtr spoutDxPtr, string senderName);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SpoutDx_ReceiveTexture(IntPtr spoutDxPtr);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SpoutDx_IsUpdated(IntPtr spoutDxPtr);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SpoutDx_IsFrameNew(IntPtr spoutDxPtr);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SpoutDx_IsConnected(IntPtr spoutDxPtr);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial IntPtr SpoutDx_GetSenderTexture(IntPtr spoutDxPtr);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial uint SpoutDx_GetSenderFormat(IntPtr spoutDxPtr);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial uint SpoutDx_GetSenderWidth(IntPtr spoutDxPtr);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial uint SpoutDx_GetSenderHeight(IntPtr spoutDxPtr);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial double SpoutDx_GetSenderFps(IntPtr spoutDxPtr);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial long SpoutDx_GetSenderFrame(IntPtr spoutDxPtr);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial IntPtr SpoutDx_GetSenderList(
        IntPtr spoutDxPtr,
        out int count
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SpoutDx_FreeSenderList(
        IntPtr senderList,
        int count
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SpoutDx_ReleaseReceiver(IntPtr spoutDxPtr);
}