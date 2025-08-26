using System.Runtime.InteropServices;

namespace SpoutDx.Net.Interop;

/// <summary>
/// A wrapper around a Spout2 receiver with texture managed on the SpoutDx side.
/// </summary>
public sealed class SpoutReceiver : IDisposable
{
    /// <summary>
    /// Get the sender texture format. This is the numerical value of the DXGI_FORMAT enum.
    /// <para />
    /// https://learn.microsoft.com/fr-fr/windows/win32/api/dxgiformat/ne-dxgiformat-dxgi_format
    /// </summary>
    public uint SenderTextureFormat => Interop.SpoutDx_GetSenderFormat(_spoutDxPointer);

    /// <summary>
    /// Get Sender texture width (in pixels).
    /// </summary>
    public uint SenderTextureWidth => Interop.SpoutDx_GetSenderWidth(_spoutDxPointer);

    /// <summary>
    /// Get Sender height (in pixels).
    /// </summary>
    public uint SenderTextureHeight => Interop.SpoutDx_GetSenderHeight(_spoutDxPointer);

    /// <summary>
    /// Check if this receiver is connected to any sender.
    /// </summary>
    public bool IsConnected => Interop.SpoutDx_IsConnected(_spoutDxPointer);

    private readonly IntPtr _spoutDxPointer;

    private string? _senderName = null;
    /// <summary>
    /// Name of the Spout2 sender to receive a texture from.
    /// </summary>
    /// <exception cref="ArgumentNullException">value is null</exception>
    public string? SenderName
    {
        get => _senderName;
        set
        {
            // Do nothing if name did not change
            if (value == _senderName)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value), "Sender name cannot be null");
            }

            Interop.SpoutDx_SetReceiverName(_spoutDxPointer, value);

            _senderName = value;
        }
    }

    /// <summary>
    /// Create a new instance of the wrapper with an already-initialized D3D11 device.
    /// </summary>
    /// <param name="d3d11DevicePointer"></param>
    public SpoutReceiver(IntPtr d3d11DevicePointer)
    {
        _spoutDxPointer = Interop.SpoutDx_Create(d3d11DevicePointer);
    }

    /// <summary>
    /// Copy the sender texture to the SpoutDx internal class texture. Always call <see cref="IsUpdated"/> before this.
    /// </summary>
    /// <returns>True if the texture could be read. False in case of an internal error</returns>
    public bool ReceiveTexture()
    {
        return Interop.SpoutDx_ReceiveTexture(_spoutDxPointer);
    }

    /// <summary>
    /// Check if the sender texture has changed pointer, format or size. You need to recreate your texture wrapper if this returns true.
    /// </summary>
    /// <returns>True if the texture has changed, false otherwise.</returns>
    public bool IsUpdated()
    {
        return Interop.SpoutDx_IsUpdated(_spoutDxPointer);
    }

    /// <summary>
    /// Indicates if the sender sent a new frame. You may want to redraw the texture in this case.
    /// </summary>
    /// <returns>TTrue if a new frame is available, false otherwise</returns>
    public bool IsFrameNew()
    {
        return Interop.SpoutDx_IsFrameNew(_spoutDxPointer);
    }

    /// <summary>
    /// Get the internal class texture pointer to the D3D11Texture2D.
    /// </summary>
    /// <returns>A pointer to a D3D11Texture2D</returns>
    public IntPtr GetSenderTexture()
    {
        return Interop.SpoutDx_GetSenderTexture(_spoutDxPointer);
    }

    /// <summary>
    /// Get a list of all the available sender names.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Release the unmanaged receiver from SpoutDx and its attached resources. Failing to call this method will cause memory leaks.
    /// </summary>
    public void Dispose()
    {
        Interop.SpoutDx_ReleaseReceiver(_spoutDxPointer);
    }
}