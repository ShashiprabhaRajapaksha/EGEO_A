using EGEO;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class MapMarking : Form, IMessageFilter
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        public const int WM_LBUTTONDOWN = 0x0201;
        getters_setters gs = new getters_setters();
        string description;
        string place;
        string filename;
        string prov;
        int fid;
        private int score = 0;
        int a = 1;
        string tag;
        int tag1 = 0;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private HashSet<Control> controlsToMove = new HashSet<Control>();

        public MapMarking(string p)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Application.AddMessageFilter(this);
            controlsToMove.Add(this);
            prov = p;
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


        private void button2_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }



        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //f2.Visible = f2.Visible == true ? false : true;
            String conString = "mongodb://localhost:27017";
            MongoClient Client = new MongoClient(conString);
            MongoServer server = Client.GetServer();
            MongoDatabase DB = server.GetDatabase("egeodb");
            MongoCollection<getters_setters> cards = DB.GetCollection<getters_setters>("fs.files");
            var c = DB.GetCollection("fs.files");
            List<getters_setters> des = new List<getters_setters>();


            foreach (getters_setters f in cards.FindAll())
            {
                des.Add(f);
                description = "Here is the description" + "   " + f.description;
                label1.Text = description;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {


            String conString = "mongodb://localhost:27017";
            MongoClient Client = new MongoClient(conString);
            MongoServer server = Client.GetServer();
            MongoDatabase DB = server.GetDatabase("egeodb");

            MongoCollection<getters_setters> cards = DB.GetCollection<getters_setters>("fs.files");
            List<getters_setters> des = new List<getters_setters>();

            var collection = DB.GetCollection<getters_setters>("fs.files");
            if (a < 11)
            {
                a = a + 1;
                var query = Query<getters_setters>.EQ(u => u.f_id, a);

                gs = collection.FindOne(query);
                place = gs.name;
                description = gs.description;
                textBox1.Text = description;
                placename.Text = place;

                filename = gs.filename;


                ObjectId oid = new ObjectId(gs._id.ToString());
                var file = DB.GridFS.FindOne(Query.EQ("f_id", a));

                using (var stream = file.OpenRead())
                {
                    var bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, (int)stream.Length);
                    using (var newFs = new FileStream(filename, FileMode.Open))
                    {


                        newFs.Write(bytes, 0, bytes.Length);
                        Image image = Image.FromStream(newFs);

                        f2.SizeMode = PictureBoxSizeMode.StretchImage;

                        f2.Image = image;

                    }


                }
            }
            else
            {
                var result = MessageBox.Show("Do you want to try next flash card set ?", "Important Message", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    Scoreboard s = new Scoreboard();
                    s.Show();
                    this.Close();

                }
            }

        }





        private void MapMarking_Load(object sender, EventArgs e)
        {
            Marking_Area.Enabled = false;

            if (prov == "North Central Province")
            {
                string filename = @"C:\Users\Shashi\Desktop\CDAP\mapmarkingValidation\myfunction\NC_NEW.png";
                Marking_Area.BackgroundImage = Image.FromFile(filename);
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Marking_Area.Enabled = true;
            String conString = "mongodb://localhost:27017";
            MongoClient Client = new MongoClient(conString);
            MongoServer server = Client.GetServer();
            MongoDatabase DB = server.GetDatabase("egeodb");

            MongoCollection<getters_setters> cards = DB.GetCollection<getters_setters>("fs.files");
            List<getters_setters> des = new List<getters_setters>();

            var collection = DB.GetCollection<getters_setters>("fs.files");
            if (a < 12)
            {
                a = a + 1;
                var query = Query<getters_setters>.EQ(u => u.f_id, a);

                gs = collection.FindOne(query);
                place = gs.name;
                description = gs.description;
                textBox1.Text = description;
                placename.Text = place;

                filename = gs.filename;


                ObjectId oid = new ObjectId(gs._id.ToString());
                var file = DB.GridFS.FindOne(Query.EQ("f_id", a));

                using (var stream = file.OpenRead())
                {
                    var bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, (int)stream.Length);
                    using (var newFs = new FileStream(filename, FileMode.Open))
                    {


                        newFs.Write(bytes, 0, bytes.Length);
                        Image image = Image.FromStream(newFs);

                        f2.SizeMode = PictureBoxSizeMode.StretchImage;

                        f2.Image = image;

                    }


                }
            }
            else
            {
                var result = MessageBox.Show("Do you want to try next flash card set ?", "Important Message", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    Scoreboard s = new Scoreboard();
                    s.Show();
                    this.Close();

                }
                else
                {
                    this.Show();
                }
            }
            foreach (Control p in Marking_Area.Controls)
                if (p.GetType() == typeof(PictureBox))
                {

                    p.BackColor = Color.Transparent;
                    p.Enabled = false;


                }
        }

        private void NC_selection_MouseClick(object sender, MouseEventArgs e)
        {

        }
        public void validation_Exact(string namep)
        {

            place = gs.name;
            if (place == null)
            {
                score = score + 0;
            }
            else if (place == namep)
            {
                score = score + 10;
                MessageBox.Show("Correct Answer!!!!!!!!!!!!You Earned 10 points");
            }
            else
            {
                MessageBox.Show("Incorrect answer");
            }

        }

        public void validation_Nearby(int tagv)
        {
            fid = gs.f_id;
            if (tagv == fid)
            {
                score = score + 5;
                MessageBox.Show("You Earned 5 points");
            }


        }
        private void NC_p55_Click(object sender, EventArgs e)
        {

            NC_p55.BackColor = Color.GreenYellow;
            place = NC_p55.Tag.ToString();
            validation(place, NC_p55);

        }


        private void check_Click(object sender, EventArgs e)
        {
            MessageBox.Show(score.ToString());
        }

        private void k7_Click(object sender, EventArgs e)
        {
            k7.BackColor = Color.RoyalBlue;
            place = k7.Tag.ToString();
            validation(place, NC_p55);

        }

        public void validation(string t, PictureBox p)
        {
            if (t == gs.name)//exact position
            {
                score = score + 10;
                p.BackColor = Color.Yellow;
                MessageBox.Show("Correct Answer!!!!!!You Eearned 10 points!!!!!!!");

            }
            else if (t == gs.tag)//NearBy position
            {
                score = score + 5;
                p.BackColor = Color.Red;
                MessageBox.Show("Close to the answer!!!!!!!You Eearned 5 points!!!!!!!");
            }
            else if (t == null)
            {
                MessageBox.Show("Mark a place");
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void k1_Click(object sender, EventArgs e)
        {
            k1.BackColor = Color.RoyalBlue;
            place = k1.Tag.ToString();
            validation(place, NC_p55);

        }

        private void k2_Click(object sender, EventArgs e)
        {
            k2.BackColor = Color.RoyalBlue;
            place = k2.Tag.ToString();
            validation(place, NC_p55);

        }

        private void k3_Click(object sender, EventArgs e)
        {
            k3.BackColor = Color.RoyalBlue;
            place = k3.Tag.ToString();
            validation(place, NC_p55);

        }

        private void k4_Click(object sender, EventArgs e)
        {
            k4.BackColor = Color.RoyalBlue;
            place = k4.Tag.ToString();
            validation(place, NC_p55);

        }

        private void k5_Click(object sender, EventArgs e)
        {
            k5.BackColor = Color.RoyalBlue;
            place = k5.Tag.ToString();
            validation(place, NC_p55);

        }

        private void k6_Click(object sender, EventArgs e)
        {
            k6.BackColor = Color.RoyalBlue;
            place = k6.Tag.ToString();
            validation(place, NC_p55);

        }

        private void k8_Click(object sender, EventArgs e)
        {
            k8.BackColor = Color.RoyalBlue;
            place = k8.Tag.ToString();
            validation(place, NC_p55);

        }





    }



}

