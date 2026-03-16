using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private static bool _shouldLock;

    private void Start()    
    {
        LockCursor();
    }

    private void LateUpdate()
    {
        if (_shouldLock && Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public static void LockCursor()
    {
        _shouldLock = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public static void UnlockCursor()
    {
        _shouldLock = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}