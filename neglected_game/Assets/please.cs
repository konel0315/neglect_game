using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class TransparentWindow : MonoBehaviour
{
    [DllImport("user32.dll", SetLastError = true)]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
    private static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    private const int GWL_STYLE = -16;
    private const int GWL_EXSTYLE = -20;
    private const uint WS_POPUP = 0x80000000;
    private const uint WS_VISIBLE = 0x10000000;
    private const uint WS_EX_LAYERED = 0x00080000;
    private const uint LWA_COLORKEY = 0x00000001;

    void Start()
    {
        IntPtr hWnd = GetActiveWindow();

        // 1. 창 스타일 설정: 완전한 테두리 없는 창으로 설정
        SetWindowLong(hWnd, GWL_STYLE, WS_POPUP | WS_VISIBLE);

        // 2. 확장 스타일 설정: WS_EX_LAYERED 추가
        int exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
        SetWindowLong(hWnd, GWL_EXSTYLE, (uint)(exStyle | WS_EX_LAYERED));

        // 3. 특정 색상(검정색 ARGB = 0x000000)을 투명 처리
        SetLayeredWindowAttributes(hWnd, 0x000000, 0, LWA_COLORKEY);
    }
}
