using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AiLibraries.Image
{
    public class WebCam : IDisposable
    {
        private const int WM_CAP = 0x400;
        private const int WM_CAP_DRIVER_CONNECT = 0x40a;
        private const int WM_CAP_DRIVER_DISCONNECT = 0x40b;
        private const int WM_CAP_EDIT_COPPY = 0x41e;
        private const int WM_CAP_GET_FRAME = 1084;
        private const int WM_CAP_COPY = 1054;
        private const int WM_CAP_SET_PREVIEW = 0x432;
        private const int WM_CAP_SET_OVERLAY = 0x433;
        private const int WM_CAP_SET_PREVIEWRATE = 0x434;
        private const int WM_CAP_SET_SCALE = 0x435;
        private const int WS_CHILD = 0x40000000;
        private const int WS_VISIBLE = 0x10000000;
        private const int SWP_NOMOVE = 0x2;
        private const int SWP_NOZORDER = 0x4;
        private const int HWND_BOTTOM = 1;

        [DllImport("avicap32.dll")]
        protected static extern bool capGetDriverDescriptionA(short wDriverIndex,
            [MarshalAs(UnmanagedType.VBByRefStr)] ref String lpszName, int cbName,
            [MarshalAs(UnmanagedType.VBByRefStr)] ref String lpszVer, int cbVer);

        [DllImport("avicap32.dll")]
        protected static extern int capCreateCaptureWindowA(
            [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszWindowName,
            int dwStyle, int x, int y, int nWidth, int nHeight, int hWndParent, int nID);

        [DllImport("user32")]
        protected static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy,
            int wFlags);

        [DllImport("user32", EntryPoint = "SendMessageA")]
        protected static extern int SendMessage(int hwnd, int wMsg, int wParam,
            [MarshalAs(UnmanagedType.AsAny)] object lParam);

        [DllImport("user32")]
        protected static extern bool DestroyWindow(int hwnd);

        int DeviceID = 0;
        int hHwnd = 0;
        readonly ArrayList _listOfDevices = new ArrayList();

        public PictureBox Container = new PictureBox();

        private int _width;
        private int _height;
        public WebCam(int width, int heigth)
        {
            _width = width;
            _height = heigth;
        }

        public void Load()
        {
            string name = String.Empty.PadRight(100);
            string version = String.Empty.PadRight(100);
            bool moreDevices;
            short index = 0;
            do
            {
                moreDevices = capGetDriverDescriptionA(index, ref name, 100, ref version, 100);
                if (moreDevices)
                {
                    _listOfDevices.Add(name.Trim());
                }
                index += 1;

            } while (moreDevices);
        }

        public void OpenConnection()
        {
            string DeviceIndex = Convert.ToString(DeviceID);
            IntPtr oHandle = Container.Handle;

            hHwnd = capCreateCaptureWindowA(ref DeviceIndex, WS_VISIBLE | WS_CHILD, 0, 0, _width, _height, oHandle.ToInt32(),
                0);
            if (SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, DeviceID, 0) != 0)
            {
                SendMessage(hHwnd, WM_CAP_SET_SCALE, -1, 0);
                SendMessage(hHwnd, WM_CAP_SET_PREVIEWRATE, 100, 0);
                SendMessage(hHwnd, WM_CAP_SET_PREVIEW, -1, 0);
                //SetWindowPos(hHwnd, HWND_BOTTOM, 0, 0, Container.Width, Container.Height, SWP_NOMOVE | SWP_NOZORDER);
            }
            else
            {
                DestroyWindow(hHwnd);
            }
        }

        void CloseConnection()
        {
            SendMessage(hHwnd, WM_CAP_DRIVER_DISCONNECT, DeviceID, 0);
            DestroyWindow(hHwnd);
        }

        public Bitmap GetCurrentImage()
        {
            SendMessage(hHwnd, WM_CAP_GET_FRAME, 0, 0);
            SendMessage(hHwnd, WM_CAP_COPY, 0, 0);
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent(typeof(Bitmap)))
            {
                var oImage = (Bitmap)data.GetData(typeof(Bitmap));
                Container.Image = oImage;
                return oImage;
            }

            return null;
        }

        ~WebCam()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            CloseConnection();
        }
    }
}
