#pragma once
#include "SpoutDX.h"

extern "C" {

    __declspec(dllexport) void* __cdecl SpoutDx_Create(
        ID3D11Device* pDevice
    );

    #pragma region Receiver

    __declspec(dllexport) void __cdecl SpoutDx_SetReceiverName(
        void* spoutDxPtr,
        const char* senderName
    );

    __declspec(dllexport) bool __cdecl SpoutDx_ReceiveTexture(
        void* spoutDxPtr
    );

    __declspec(dllexport) bool __cdecl SpoutDx_IsUpdated(
        void* spoutDxPtr
    );

    __declspec(dllexport) bool __cdecl SpoutDx_IsFrameNew(
        void* spoutDxPtr
    );

    __declspec(dllexport) void* __cdecl SpoutDx_GetSenderTexture(
        void* spoutDxPtr
    );

    __declspec(dllexport) UINT32 __cdecl SpoutDx_GetSenderFormat(
        void* spoutDxPtr
    );

    __declspec(dllexport) UINT32 __cdecl SpoutDx_GetSenderWidth(
        void* spoutDxPtr
    );

    __declspec(dllexport) UINT32 __cdecl SpoutDx_GetSenderHeight(
        void* spoutDxPtr
    );

    __declspec(dllexport) double __cdecl SpoutDx_GetSenderFps(
        void* spoutDxPtr
    );

    __declspec(dllexport) INT64 __cdecl SpoutDx_GetSenderFrame(
        void* spoutDxPtr
    );

    __declspec(dllexport) void __cdecl SpoutDx_ReleaseReceiver(void* spoutDxPtr);

    #pragma endregion Receiver

}