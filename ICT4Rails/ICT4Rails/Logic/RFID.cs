using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phidgets.Events;
using Phidgets;
using System.Diagnostics;
using ICT4Rails.Models;
using ICT4Rails.Forms;
using ICT4Rails.Data;
using ICT4Rails.Models.Enums;

namespace ICT4Rails.Logic
{
    public class Rfid
    {
        private static RFID rfid;
        private static bool started;
        private string tramid;
        private CacheData cache;
        private Tram tram;


        public Rfid()
        {

        }
        public Rfid(string tramid, CacheData cache)
        {
            this.tramid = tramid;
            this.cache = cache;
        }

        public static void Start()
        {
            try
            {
                rfid = new RFID();
                //rfid.Error
                rfid.Tag += rfid_Tag;
                rfid.open();
                rfid.waitForAttachment();
                rfid.Antenna = true;
                rfid.LED = true;
                started = true;
            }
            catch(PhidgetException e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public string ConvertToLineNumber(Tramtype tramtype)
        {
            string line = "";
            if (tramtype == Tramtype.Combinos)
            {
                line = "undefined";
            }
            else if (tramtype == Tramtype.DubbelekopCombinos)
            {
                line = "undefined";
            }
            else if (tramtype == Tramtype.elevenG)
            {
                line = "5";
            }
            else if (tramtype == Tramtype.twelfG)
            {
                line = "16/24";
            }
            else if (tramtype == Tramtype.Opleidingstram)
            {
                line = "undefined";
            }

            return line;
        }


        public static void rfid_Tag(object sender, TagEventArgs e)
        {
            string tag = e.Tag;
            

            if (tag == "2800b3b724")
            {
               
              
             
               
            }
            else if (tag == "1100ad7362")
            {

             
            }
            

        }
    }
}
