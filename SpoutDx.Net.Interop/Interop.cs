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
    public static partial void SpoutDx_SetReceiverName(IntPtr spoutDx, string senderName);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SpoutDx_ReceiveTexture(IntPtr spoutDx);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SpoutDx_IsUpdated(IntPtr spoutDx);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SpoutDx_IsFrameNew(IntPtr spoutDx);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SpoutDx_IsConnected(IntPtr spoutDx);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial IntPtr SpoutDx_GetSenderTexture(IntPtr spoutDx);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial uint SpoutDx_GetSenderFormat(IntPtr spoutDx);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial uint SpoutDx_GetSenderWidth(IntPtr spoutDx);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial uint SpoutDx_GetSenderHeight(IntPtr spoutDx);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial double SpoutDx_GetSenderFps(IntPtr spoutDx);

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial IntPtr SpoutDx_GetSenderList(
        IntPtr spoutDx,
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
        IntPtr spoutDx,
        string senderName
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SpoutDx_SetSenderFormat(
        IntPtr spoutDx,
        uint format
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SpoutDx_SendTexture(
        IntPtr spoutDx,
        IntPtr pTexture // pointer to ID3D11Texture2D
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SpoutDx_IsInitialized(
        IntPtr spoutDx
    );

    [LibraryImport(DllName)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SpoutDx_ReleaseSender(
        IntPtr spoutDx
    );

    #endregion
}