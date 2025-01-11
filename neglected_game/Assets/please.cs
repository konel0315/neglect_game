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

        // 1. â ��Ÿ�� ����: ������ �׵θ� ���� â���� ����
        SetWindowLong(hWnd, GWL_STYLE, WS_POPUP | WS_VISIBLE);

        // 2. Ȯ�� ��Ÿ�� ����: WS_EX_LAYERED �߰�
        int exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
        SetWindowLong(hWnd, GWL_EXSTYLE, (uint)(exStyle | WS_EX_LAYERED));

        // 3. Ư�� ����(������ ARGB = 0x000000)�� ���� ó��
        SetLayeredWindowAttributes(hWnd, 0x000000, 0, LWA_COLORKEY);
    }
}
