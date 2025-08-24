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
        void* spoutDxPtr,
        const char* senderName
    )
    {
        auto* spoutDx = reinterpret_cast<spoutDX*>(spoutDxPtr);

        spoutDx->SetReceiverName(senderName);
    }

    __declspec(dllexport) bool __cdecl SpoutDx_ReceiveTexture(
        void* spoutDxPtr)
    {
        auto* spoutDx = reinterpret_cast<spoutDX*>(spoutDxPtr);

        return spoutDx->ReceiveTexture();
    }

    __declspec(dllexport) bool __cdecl SpoutDx_IsUpdated(
        void* spoutDxPtr
    )
    {
        auto* spoutDx = reinterpret_cast<spoutDX*>(spoutDxPtr);

        return spoutDx->IsUpdated();
    }

    __declspec(dllexport) bool __cdecl SpoutDx_IsFrameNew(
        void* spoutDxPtr
    )
    {
        auto* spoutDx = reinterpret_cast<spoutDX*>(spoutDxPtr);

        return spoutDx->IsFrameNew();
    }

    __declspec(dllexport) ID3D11Texture2D* __cdecl SpoutDx_GetSenderTexture(
        void* spoutDxPtr
    )
    {
        auto* spoutDx = reinterpret_cast<spoutDX*>(spoutDxPtr);

        return spoutDx->GetSenderTexture();
    }

    __declspec(dllexport) UINT32 __cdecl SpoutDx_GetSenderFormat(
        void* spoutDxPtr
    )
    {
        auto* spoutDx = reinterpret_cast<spoutDX*>(spoutDxPtr);

        return (UINT32)spoutDx->GetSenderFormat();
    }

    __declspec(dllexport) UINT32 __cdecl SpoutDx_GetSenderWidth(
        void* spoutDxPtr
    )
    {
        auto* spoutDx = reinterpret_cast<spoutDX*>(spoutDxPtr);

        return spoutDx->GetSenderWidth();
    }

    __declspec(dllexport) UINT32 __cdecl SpoutDx_GetSenderHeight(
        void* spoutDxPtr
    )
    {
        auto* spoutDx = reinterpret_cast<spoutDX*>(spoutDxPtr);

        return spoutDx->GetSenderHeight();
    }

    __declspec(dllexport) double __cdecl SpoutDx_GetSenderFps(
        void* spoutDxPtr
    )
    {
        auto* spoutDx = reinterpret_cast<spoutDX*>(spoutDxPtr);

        return spoutDx->GetSenderFps();
    }

    __declspec(dllexport) INT64 __cdecl SpoutDx_GetSenderFrame(
        void* spoutDxPtr
    )
    {
        auto* spoutDx = reinterpret_cast<spoutDX*>(spoutDxPtr);

        return spoutDx->GetSenderFrame();
    }

    __declspec(dllexport) char** __cdecl SpoutDx_GetSenderList(
            void* spoutDxPtr,
            int* count
        )
    {
        // Map the C++ vector to C-style array
        auto* spoutDx = reinterpret_cast<spoutDX*>(spoutDxPtr);

        auto namesSize = spoutDx->GetSenderCount();
        *count = namesSize;

        auto names = static_cast<char**>(malloc(namesSize * sizeof(char*)));

        for (int i = 0; i < namesSize; i++)
        {
            names[i] = static_cast<char*>(malloc(100 * sizeof(char)));

            spoutDx->GetSender(i, names[i], 100);
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

    __declspec(dllexport) void __cdecl SpoutDx_ReleaseReceiver(void* spoutDxPtr)
    {
        if (!spoutDxPtr)
        {
            return;
        }

        auto* spoutDx = reinterpret_cast<spoutDX*>(spoutDxPtr);

        spoutDx->ReleaseReceiver();

        delete spoutDx;
    }

    #pragma endregion Receiver

    #pragma region Utils

    #pragma endregion
}
