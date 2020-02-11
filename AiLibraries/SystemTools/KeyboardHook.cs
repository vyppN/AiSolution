using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AiLibraries.SystemTools
{
    /// <summary>
    ///  ใช้เรียกโปรแกรมให้ทำงานผ่าน Keyboard Shortcut จากทุกๆที่
    /// ใช้กับโปรแกรมที่เป็น service จะแจ่มแมวมาก
    /// </summary>
    public sealed class KeyboardHook : IDisposable
    {
        private readonly Window _window = new Window();
        private int _currentId;

        public KeyboardHook()
        {
            _window.KeyPressed += delegate(object sender, KeyPressedEventArgs args)
                                      {
                                          if (KeyPressed != null)
                                              KeyPressed(this, args);
                                      };
        }

        #region IDisposable Members

        public void Dispose()
        {
            // unregister all the registered hot keys.
            for (int i = _currentId; i > 0; i--)
            {
                UnregisterHotKey(_window.Handle, i);
            }

            // dispose the inner native window.
            _window.Dispose();
        }

        #endregion

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public void RegisterHotKey(ModifierKeys modifier, Keys key)
        {
            // increment the counter.
            _currentId = _currentId + 1;

            // register the hot key.
            if (!RegisterHotKey(_window.Handle, _currentId, (uint) modifier, (uint) key))
                throw new InvalidOperationException("Couldn’t register the hot key.");
        }

        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        #region Nested type: Window

        private sealed class Window : NativeWindow, IDisposable
        {
            private const int WmHotkey = 0x0312;

            public Window()
            {
                CreateHandle(new CreateParams());
            }

            #region IDisposable Members

            public void Dispose()
            {
                DestroyHandle();
            }

            #endregion

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);
                if (m.Msg == WmHotkey)
                {
                    var key = (Keys) (((int) m.LParam >> 16) & 0xFFFF);
                    var modifier = (ModifierKeys) ((int) m.LParam & 0xFFFF);

                    if (KeyPressed != null)
                        KeyPressed(this, new KeyPressedEventArgs(modifier, key));
                }
            }

            public event EventHandler<KeyPressedEventArgs> KeyPressed;
        }

        #endregion
    }

    public class KeyPressedEventArgs : EventArgs
    {
        private readonly Keys _key;
        private readonly ModifierKeys _modifier;

        internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
        {
            _modifier = modifier;
            _key = key;
        }

        public ModifierKeys Modifier
        {
            get { return _modifier; }
        }

        public Keys Key
        {
            get { return _key; }
        }
    }

    [Flags]
    public enum ModifierKeys : uint
    {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
        Win = 8
    }
}