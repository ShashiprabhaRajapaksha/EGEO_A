using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class getters_setters
    {

        public getters_setters()
        {

        }

        public ObjectId _id { get; set; }
        public int f_id { get; set; }
        public string tag { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string filename { get; set; }
        public Int64 length { get; set; }
        public Int32 chunkSize { get; set; }
        public DateTime uploadDate { get; set; }
        public string md5 { get; set; }





    }
}
