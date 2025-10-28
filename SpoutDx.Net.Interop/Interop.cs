using System.Runtime.InteropServices;

namespace SpoutDx.Net.Interop;

internal partial class Interop
{
    private const string DllName = "SpoutDx.Net.dll";

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial IntPtr SpoutDx_Create(IntPtr pDevice);

    #region Receiver

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SpoutDx_SetReceiverName(SpoutSafeHandle spoutDx, string senderName);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SpoutDx_ReceiveTexture(SpoutSafeHandle spoutDx);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SpoutDx_IsUpdated(SpoutSafeHandle spoutDx);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SpoutDx_IsFrameNew(SpoutSafeHandle spoutDx);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SpoutDx_IsConnected(SpoutSafeHandle spoutDx);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial IntPtr SpoutDx_GetSenderTexture(SpoutSafeHandle spoutDx);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial uint SpoutDx_GetSenderFormat(SpoutSafeHandle spoutDx);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial uint SpoutDx_GetSenderWidth(SpoutSafeHandle spoutDx);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial uint SpoutDx_GetSenderHeight(SpoutSafeHandle spoutDx);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial double SpoutDx_GetSenderFps(SpoutSafeHandle spoutDx);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial IntPtr SpoutDx_GetSenderList(
        SpoutSafeHandle spoutDx,
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
    public static partial void SpoutDx_ReleaseReceiver(IntPtr spoutDx);

    #endregion

    #region Sender

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SpoutDx_SetSenderName(
        SpoutSafeHandle spoutDx,
        string senderName
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SpoutDx_SetSenderFormat(
        SpoutSafeHandle spoutDx,
        uint format
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SpoutDx_SendTexture(
        SpoutSafeHandle spoutDx,
        IntPtr pTexture // pointer to ID3D11Texture2D
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SpoutDx_IsInitialized(
        SpoutSafeHandle spoutDx
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SpoutDx_ReleaseSender(
        IntPtr spoutDx
    );

    #endregion
}