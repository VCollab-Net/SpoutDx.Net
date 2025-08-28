#include "spout_dx_bridge.h"

extern "C" {

    __declspec(dllexport) spoutDX* __cdecl SpoutDx_Create(
        ID3D11Device* pDevice
    )
    {
        auto* spoutDx = new spoutDX();

        if (spoutDx->OpenDirectX11(pDevice))
        {
            return spoutDx;
        }

        delete spoutDx;
        return nullptr;
    }

    #pragma region Receiver

    __declspec(dllexport) void __cdecl SpoutDx_SetReceiverName(
        spoutDX* spoutDx,
        const char* senderName
    )
    {
        spoutDx->SetReceiverName(senderName);
    }

    __declspec(dllexport) bool __cdecl SpoutDx_ReceiveTexture(
        spoutDX* spoutDx
    )
    {
        return spoutDx->ReceiveTexture();
    }

    __declspec(dllexport) bool __cdecl SpoutDx_IsUpdated(
        spoutDX* spoutDx
    )
    {
        return spoutDx->IsUpdated();
    }

    __declspec(dllexport) bool __cdecl SpoutDx_IsFrameNew(
        spoutDX* spoutDx
    )
    {
        return spoutDx->IsFrameNew();
    }

    __declspec(dllexport) bool __cdecl SpoutDx_IsConnected(
        spoutDX* spoutDx
    )
    {
        return spoutDx->IsConnected();
    }

    __declspec(dllexport) ID3D11Texture2D* __cdecl SpoutDx_GetSenderTexture(
        spoutDX* spoutDx
    )
    {
        return spoutDx->GetSenderTexture();
    }

    __declspec(dllexport) UINT32 __cdecl SpoutDx_GetSenderFormat(
        spoutDX* spoutDx
    )
    {
        return (UINT32)spoutDx->GetSenderFormat();
    }

    __declspec(dllexport) UINT32 __cdecl SpoutDx_GetSenderWidth(
        spoutDX* spoutDx
    )
    {
        return spoutDx->GetSenderWidth();
    }

    __declspec(dllexport) UINT32 __cdecl SpoutDx_GetSenderHeight(
        spoutDX* spoutDx
    )
    {
        return spoutDx->GetSenderHeight();
    }

    __declspec(dllexport) double __cdecl SpoutDx_GetSenderFps(
        spoutDX* spoutDx
    )
    {
        return spoutDx->GetSenderFps();
    }

    __declspec(dllexport) INT64 __cdecl SpoutDx_GetSenderFrame(
        spoutDX* spoutDx
    )
    {
        return spoutDx->GetSenderFrame();
    }

    __declspec(dllexport) char** __cdecl SpoutDx_GetSenderList(
        spoutDX* spoutDx,
        int* count
    )
    {
        // Map the C++ vector to C-style array
        auto namesSize = spoutDx->GetSenderCount();
        *count = namesSize;

        auto names = static_cast<char**>(malloc(namesSize * sizeof(char*)));

        for (int i = 0; i < namesSize; i++)
        {
            names[i] = static_cast<char*>(malloc(256 * sizeof(char)));

            spoutDx->GetSender(i, names[i], 256);
        }

        return names;
    }

    __declspec(dllexport) void __cdecl SpoutDx_FreeSenderList(
        char** senderList,
        int count
    )
    {
        if (!senderList)
        {
            return;
        }

        for (int i = 0; i < count; i++)
        {
            free(senderList[i]);
        }

        free((char*)senderList);
    }

    __declspec(dllexport) void __cdecl SpoutDx_ReleaseReceiver(spoutDX* spoutDx)
    {
        if (!spoutDx)
        {
            return;
        }

        spoutDx->ReleaseReceiver();

        delete spoutDx;
    }

    #pragma endregion Receiver

    #pragma region Sender

    __declspec(dllexport) bool __cdecl SpoutDx_SetSenderName(
        spoutDX* spoutDx,
        const char* senderName
    )
    {
        return spoutDx->SetSenderName(senderName);
    }

    __declspec(dllexport) void __cdecl SpoutDx_SetSenderFormat(
        spoutDX* spoutDx,
        UINT32 format
    )
    {
        spoutDx->SetSenderFormat(static_cast<DXGI_FORMAT>(format));
    }

    __declspec(dllexport) bool __cdecl SpoutDx_SendTexture(
        spoutDX* spoutDx,
        ID3D11Texture2D* pTexture
    )
    {
        return spoutDx->SendTexture(pTexture);
    }

    __declspec(dllexport) bool __cdecl SpoutDx_IsInitialized(
        spoutDX* spoutDx
    )
    {
        return spoutDx->IsInitialized();
    }

    __declspec(dllexport) void __cdecl SpoutDx_ReleaseSender(spoutDX* spoutDx)
    {
        if (!spoutDx)
        {
            return;
        }

        spoutDx->ReleaseSender();

        delete spoutDx;
    }

    #pragma endregion Sender

    #pragma region Utils

    #pragma endregion
}
