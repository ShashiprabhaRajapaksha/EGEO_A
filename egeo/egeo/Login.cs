using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
    public partial class Login : Form, IMessageFilter
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        public const int WM_LBUTTONDOWN = 0x0201;


        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private HashSet<Control> controlsToMove = new HashSet<Control>();

        public Login()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Application.AddMessageFilter(this);

            controlsToMove.Add(this);
            panel1.Show();
            panel2.Hide();

            textBox1.ForeColor = SystemColors.GrayText;
            textBox2.ForeColor = SystemColors.GrayText;
            textBox3.ForeColor = SystemColors.GrayText;
            textBox4.ForeColor = SystemColors.GrayText;
            textBox5.ForeColor = SystemColors.GrayText;
            textBox1.Text = "First Name";
            textBox2.Text = "Last Name";
            textBox3.Text = "Password";
            textBox4.Text = "First Name";
            textBox5.Text = "Password";
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            this.textBox2.Leave += new System.EventHandler(this.textBox2_Leave);
            this.textBox2.Enter += new System.EventHandler(this.textBox2_Enter);
            this.textBox3.Leave += new System.EventHandler(this.textBox3_Leave);
            this.textBox3.Enter += new System.EventHandler(this.textBox3_Enter);
            this.textBox4.Leave += new System.EventHandler(this.textBox4_Leave);
            this.textBox4.Enter += new System.EventHandler(this.textBox4_Enter);
            this.textBox5.Leave += new System.EventHandler(this.textBox5_Leave);
            this.textBox5.Enter += new System.EventHandler(this.textBox5_Enter);


            // The control will allow no more than 14 characters.
            textBox3.MaxLength = 14;
            textBox5.MaxLength = 14;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                textBox1.Text = "First Name";
                textBox1.ForeColor = SystemColors.GrayText;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "First Name")
            {
                textBox1.Text = "";
                textBox1.ForeColor = SystemColors.WindowText;
            }
        }
        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 0)
            {
                textBox2.Text = "Last Name";
                textBox2.ForeColor = SystemColors.GrayText;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Last Name")
            {
                // The password character is an asterisk.

                textBox2.Text = "";
                textBox2.ForeColor = SystemColors.WindowText;
                // textBox3.PasswordChar = '*';
            }
        }
        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 0)
            {
                textBox3.Text = "Password";
                textBox3.ForeColor = SystemColors.GrayText;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Password")
            {
                textBox3.Text = "";
                textBox3.ForeColor = SystemColors.WindowText;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text.Length == 0)
            {
                textBox4.Text = "First Name";
                textBox4.ForeColor = SystemColors.GrayText;
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "First Name")
            {
                textBox4.Text = "";
                textBox4.ForeColor = SystemColors.WindowText;
            }
        }
        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text.Length == 0)
            {
                textBox5.Text = "Password";
                textBox5.ForeColor = SystemColors.GrayText;
            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "First Name")
            {
                textBox5.Text = "";
                textBox5.ForeColor = SystemColors.WindowText;
            }
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

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            panel2.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel2.Hide();
            panel1.Show();
        }
    }
}

