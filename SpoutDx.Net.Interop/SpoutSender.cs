namespace SpoutDx.Net.Interop;

/// <summary>
/// A wrapper around a Spout2 sender.
/// </summary>
public sealed class SpoutSender : IDisposable
{
    /// <summary>
    /// Check if this sender is initialized.
    /// </summary>
    public bool IsInitialized
    {
        get
        {
            EnsureHandleIsValid();

            return Interop.SpoutDx_IsInitialized(_spoutSafeHandle);
        }
    }

    private string? _name = null;
    /// <summary>
    /// Name of this Spout2 sender.
    /// </summary>
    /// <exception cref="ArgumentNullException">value is null</exception>
    public string? Name
    {
        get => _name;
        set
        {
            // Do nothing if name did not change
            if (value == _name)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value), "Sender name cannot be null");
            }

            EnsureHandleIsValid();

            Interop.SpoutDx_SetSenderName(_spoutSafeHandle, value);
            _name = value;
        }
    }

    private uint _textureFormat = 87;
    /// <summary>
    /// Format of the texture sent.
    /// </summary>
    public uint TextureFormat
    {
        get => _textureFormat;
        set
        {
            if (value == _textureFormat)
            {
                return;
            }

            EnsureHandleIsValid();

            Interop.SpoutDx_SetSenderFormat(_spoutSafeHandle, value);
            _textureFormat = value;
        }
    }

    private readonly SpoutSafeHandle _spoutSafeHandle;

    /// <summary>
    /// Create a new instance of the wrapper with an already-initialized D3D11 device.
    /// </summary>
    /// <param name="d3d11DevicePointer"></param>
    public SpoutSender(IntPtr d3d11DevicePointer)
    {
        _spoutSafeHandle = SpoutSafeHandle.Allocate(SpoutSafeHandle.HandleType.Sender, d3d11DevicePointer);
    }

    /// <summary>
    /// Send a D3D11 texture using Spout2. Refer to the official SpoutDx SDK for supported formats.
    /// <para/>
    /// https://spoutdx-site.netlify.app/#File:SpoutDX/SpoutDX.cpp:spoutDX.SendTexture
    /// </summary>
    /// <param name="d3d11TexturePointer"></param>
    /// <returns></returns>
    public bool SendTexture(IntPtr d3d11TexturePointer)
    {
        EnsureHandleIsValid();

        return Interop.SpoutDx_SendTexture(_spoutSafeHandle, d3d11TexturePointer);
    }

    private void EnsureHandleIsValid()
    {
        if (_spoutSafeHandle.IsClosed || _spoutSafeHandle.IsInvalid)
        {
            throw new InvalidOperationException("Cannot use SpoutReceiver after disposing");
        }
    }

    /// <summary>
    /// Release the unmanaged sender from SpoutDx and its attached resources. Failing to call this method will cause memory leaks.
    /// </summary>
    public void Dispose()
    {
        _spoutSafeHandle.Dispose();
    }
}