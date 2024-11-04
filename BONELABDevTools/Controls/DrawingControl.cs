using System.Runtime.InteropServices;

namespace BonelabDevMode.Controls
{
    internal static class DrawingControl
    {
#pragma warning disable SYSLIB1054 // Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

#pragma warning restore SYSLIB1054 // Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time

        private const int WM_SETREDRAW = 11;

        public static void SuspendDrawing(Control parent)
        {
            int action() => SendMessage(parent.Handle, WM_SETREDRAW, false, 0);
            if (parent.InvokeRequired) parent.Invoke(action);
            else action();
        }

        public static void ResumeDrawing(Control parent)
        {
            int action() => SendMessage(parent.Handle, WM_SETREDRAW, true, 0);
            if (parent.InvokeRequired) parent.Invoke(action);
            else action();
            void action2() => parent.Refresh();
            if (parent.InvokeRequired) parent.Invoke(action2);
            else action2();
        }
    }
}