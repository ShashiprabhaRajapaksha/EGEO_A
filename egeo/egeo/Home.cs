using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Home : Form, IMessageFilter
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        public const int WM_LBUTTONDOWN = 0x0201;


        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private HashSet<Control> controlsToMove = new HashSet<Control>();


        public Home()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Application.AddMessageFilter(this);

            controlsToMove.Add(this);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MapMarking_First f = new MapMarking_First();
            f.Show();
            this.Hide();
        }


        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_LBUTTONDOWN &&
                 controlsToMove.Contains(Control.FromHandle(m.HWnd)))
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                return true;
            }
            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            time_lb.Text = DateTime.Now.ToLongTimeString();
            date_lb.Text = DateTime.Now.ToLongDateString();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time_lb.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }





        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            MapMarking_First m = new MapMarking_First();
            m.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }



        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to exit?",
                                          "E-GEO E-Learning Application",
                                           MessageBoxButtons.OKCancel,
                                           MessageBoxIcon.Information) == DialogResult.OK)
            {
                Environment.Exit(1);
            }

        }
    }
}

