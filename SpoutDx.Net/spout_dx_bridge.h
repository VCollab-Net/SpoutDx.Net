#pragma once
#include "SpoutDX.h"

extern "C" {

    __declspec(dllexport) spoutDX* __cdecl SpoutDx_Create(
        ID3D11Device* pDevice
    );

    #pragma region Receiver

    __declspec(dllexport) void __cdecl SpoutDx_SetReceiverName(
        spoutDX* spoutDx,
        const char* senderName
    );

    __declspec(dllexport) bool __cdecl SpoutDx_ReceiveTexture(
        spoutDX* spoutDx
    );

    __declspec(dllexport) bool __cdecl SpoutDx_IsUpdated(
        spoutDX* spoutDx
    );

    __declspec(dllexport) bool __cdecl SpoutDx_IsFrameNew(
        spoutDX* spoutDx
    );

    __declspec(dllexport) bool __cdecl SpoutDx_IsConnected(
        spoutDX* spoutDx
    );

    __declspec(dllexport) ID3D11Texture2D* __cdecl SpoutDx_GetSenderTexture(
        spoutDX* spoutDx
    );

    __declspec(dllexport) UINT32 __cdecl SpoutDx_GetSenderFormat(
        spoutDX* spoutDx
    );

    __declspec(dllexport) UINT32 __cdecl SpoutDx_GetSenderWidth(
        spoutDX* spoutDx
    );

    __declspec(dllexport) UINT32 __cdecl SpoutDx_GetSenderHeight(
        spoutDX* spoutDx
    );

    __declspec(dllexport) double __cdecl SpoutDx_GetSenderFps(
        spoutDX* spoutDx
    );

    __declspec(dllexport) char** __cdecl SpoutDx_GetSenderList(
        spoutDX* spoutDx,
        int* count
    );

    __declspec(dllexport) void __cdecl SpoutDx_FreeSenderList(
        char** senderList,
        int count
    );

    __declspec(dllexport) void __cdecl SpoutDx_ReleaseReceiver(spoutDX* spoutDx);

    #pragma endregion Receiver

    #pragma region Sender

    __declspec(dllexport) bool __cdecl SpoutDx_SetSenderName(
        spoutDX* spoutDx,
        const char* senderName
    );

    __declspec(dllexport) void __cdecl SpoutDx_SetSenderFormat(
        spoutDX* spoutDx,
        UINT32 format
    );

    __declspec(dllexport) bool __cdecl SpoutDx_SendTexture(
        spoutDX* spoutDx,
        ID3D11Texture2D* pTexture
    );

    __declspec(dllexport) bool __cdecl SpoutDx_IsInitialized(
        spoutDX* spoutDx
    );

    __declspec(dllexport) void __cdecl SpoutDx_ReleaseSender(spoutDX* spoutDx);

    #pragma endregion Sender
}