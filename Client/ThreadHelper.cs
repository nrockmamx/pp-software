using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public static class ThreadHelperClass
    {
        delegate void SetTextCallback(Form f, Control ctrl, string text);
        delegate void SetEnableCallback(Form f, Control ctrl, bool enable);
        delegate void SetDataGridCallBack(Form f, DataGridView ctrl, params object[] value);
        /// <summary>
        /// Set text property of various controls
        /// </summary>
        /// <param name="form">The calling form</param>
        /// <param name="ctrl"></param>
        /// <param name="text"></param>
        public static void SetText(Form form, Control ctrl, string text)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (ctrl.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                form.Invoke(d, new object[] { form, ctrl, text });
            }
            else
            {
                ctrl.Text = text;
            }
        }
        public static void AddDataGrid(Form form, DataGridView ctrl, params object[] value)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (ctrl.InvokeRequired)
            {
                SetDataGridCallBack d = new SetDataGridCallBack(AddDataGrid);
                form.Invoke(d, new object[] { form, ctrl, value });
            }
            else
            {
                ctrl.Rows.Add(value);
            }
        }
        public static void Enable(Form form, Control ctrl, bool enable)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (ctrl.InvokeRequired)
            {
                SetEnableCallback d = new SetEnableCallback(Enable);
                form.Invoke(d, new object[] { form, ctrl,enable });
            }
            else
            {
                ctrl.Enabled = enable;
            }
        }
    }
}
