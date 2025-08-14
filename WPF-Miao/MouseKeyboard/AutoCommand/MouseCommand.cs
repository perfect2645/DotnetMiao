using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace MouseKeyboard.AutoCommand
{
    public class MouseCommand
    {
        [DllImport("user32.dll", EntryPoint = "mouse_event")]
        public static extern void MouseEvent(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        public void MouseLeftButton()
        {

            // 设置鼠标位置为指定坐标（示例）
            //Point point = new Point(400, 400);
            //Cursor.Position = point;

            // 模拟鼠标左键按下并松开
            MouseEvent(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
    }
}
