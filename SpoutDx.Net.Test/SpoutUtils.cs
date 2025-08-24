using Veldrid;

namespace SpoutDx.Net.Test;

public class SpoutUtils
{
    public static PixelFormat DxgiToVeldridPixelFormat(uint dxgiFormat) => dxgiFormat switch
    {
        28 => PixelFormat.R8_G8_B8_A8_UNorm,
        87 => PixelFormat.B8_G8_R8_A8_UNorm,
        _ => throw new ArgumentException("Unknown DXGI pixel format, cannot convert it to Veldrid one", nameof(dxgiFormat))
    };
}