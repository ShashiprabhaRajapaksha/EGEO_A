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
        string filename;
        string prov;

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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Marking_Area.BackgroundImage = Image.FromFile(@"C:\Users\Shashi\Desktop\CDAP\mapmarkingValidation\myfunction\NC_NEW.png");
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
            var c = DB.GetCollection("fs.files");
            List<getters_setters> des = new List<getters_setters>();


            foreach (getters_setters f in cards.FindAll())
            {
                des.Add(f);
                description = "Here is the description" + "   " + f.description;
                label1.Text = description;
                filename = f.filename;
                using (var fs = new FileStream(filename, FileMode.Open))
                {
                    var gridFsInfo = DB.GridFS.Upload(fs, filename);
                    var fileId = gridFsInfo.Id;
                    ObjectId oid = new ObjectId(fileId.ToString());
                    var file = DB.GridFS.FindOne(Query.EQ("_id", oid));
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
            }

            /*var fileName = @"C:\Users\Shashi\Desktop\CDAP\mapmarkingValidation\myfunction\map_new\gif\ruwanwelimahaseya.jpg";
            var newFileName = @"C:\Users\Shashi\Desktop\CDAP\mapmarkingValidation\myfunction\map_new\gif\o.jpg";
            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                var gridFsInfo = DB.GridFS.Upload(fs, fileName);
                var fileId = gridFsInfo.Id;

                ObjectId oid = new ObjectId(fileId.ToString());
                var file = DB.GridFS.FindOne(Query.EQ("_id", oid));


                using (var stream = file.OpenRead())
                {
                    var bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, (int)stream.Length);
                    using (var newFs = new FileStream(newFileName, FileMode.Create))
                    {
                        newFs.Write(bytes, 0, bytes.Length);
                        Image image = Image.FromStream(newFs);

                        f2.SizeMode = PictureBoxSizeMode.StretchImage;

                        f2.Image = image;

                    }
                }
            }*/
        }



        private void MapMarking_Load(object sender, EventArgs e)
        {

            if (prov == "North Central Province")
            {
                string filename = @"C:\Users\Shashi\Desktop\CDAP\mapmarkingValidation\myfunction\NC_NEW.png";
                Marking_Area.BackgroundImage = Image.FromFile(filename);
            }

        }




    }



}

