using DotSpatial.Controls;
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
    public partial class MapMarking_First : Form, IMessageFilter
    {
        //public System.Windows.Forms.Label NC_lb;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        public const int WM_LBUTTONDOWN = 0x0201;


        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private HashSet<Control> controlsToMove = new HashSet<Control>();

        public MapMarking_First()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Application.AddMessageFilter(this);

            controlsToMove.Add(this);
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
        private void pictureBox2_Click(object sender, EventArgs e)
        {

            panel3.BackColor = Color.DarkSlateGray;
            panel5.BackColor = Color.DarkSlateGray;
            panel6.BackColor = Color.DarkSlateGray;
            panel8.BackColor = Color.DarkSlateGray;
            panel4.BackColor = Color.Purple;
            map1.ZoomIn();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panel9.Show();
            panel4.BackColor = Color.DarkSlateGray;
            panel5.BackColor = Color.DarkSlateGray;
            panel6.BackColor = Color.DarkSlateGray;
            panel8.BackColor = Color.DarkSlateGray;
            panel3.BackColor = Color.YellowGreen;

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

            panel3.BackColor = Color.DarkSlateGray;
            panel4.BackColor = Color.DarkSlateGray;
            panel6.BackColor = Color.DarkSlateGray;
            panel8.BackColor = Color.DarkSlateGray;
            panel5.BackColor = Color.Orange;
            map1.ZoomOut();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

            panel3.BackColor = Color.DarkSlateGray;
            panel5.BackColor = Color.DarkSlateGray;
            panel4.BackColor = Color.DarkSlateGray;
            panel8.BackColor = Color.DarkSlateGray;
            panel6.BackColor = Color.CornflowerBlue;
            if (panel6.BackColor == Color.CornflowerBlue)
            {
                map1.FunctionMode = FunctionMode.Info;
            }
            else
                map1.FunctionMode = FunctionMode.None;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

            panel3.BackColor = Color.DarkSlateGray;
            panel5.BackColor = Color.DarkSlateGray;
            panel6.BackColor = Color.DarkSlateGray;
            panel4.BackColor = Color.DarkSlateGray;
            panel8.BackColor = Color.Cyan;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Hide();
        }

        private void MapMarking_First_Load(object sender, EventArgs e)
        {
            // pictureBox8.Image = Image.FromFile(@"C:\Users\Shashi\Desktop\CDAP\interfaces\cube.gif");
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.pictureBox1, "You can select a province");

        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.pictureBox2, "You can Zoom In the map");
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.pictureBox5, "You can Zoom Out the map");
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.pictureBox3, "Learn basic information about places");
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.pictureBox4, "You can ask questions");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            panel9.Hide();
            string filename = @"C:\Users\Shashi\Desktop\CDAP\mapmarkingValidation\MAP\NC.shp";
            map1.AddLayer(filename);

        }

        private void label3_Click(object sender, EventArgs e)
        {
            panel9.Hide();
            string filename = @"C:\Users\Shashi\Desktop\CDAP\mapmarkingValidation\MAP\NC.shp";
            map1.AddLayer(filename);
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string province = "North Central Province";
            MapMarking mm = new MapMarking(province);
            this.Close();
            mm.Show();
        }
    }
}
