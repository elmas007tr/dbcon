using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Web;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace dbcon
{
    public class Logwrite
    {

       
        private static string LogTxt = "log.txt";

        public string BaseDirectory
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }
             
        public void logwrite(string message, string logfilename)
        {
            lock (this)
            {
                try
                {
                    if (string.IsNullOrEmpty(logfilename.Trim()))
                        logfilename = LogTxt;
                    else
                        logfilename = ".txt";


                    if (!Directory.Exists(BaseDirectory + "Log"))
                        Directory.CreateDirectory(BaseDirectory + "Log");

                    logfilename = BaseDirectory + "Log\\" + logfilename;

                    if (File.Exists(logfilename))
                    {
                        FileStream fs = new FileStream(logfilename, FileMode.Open);
                        long fsSize = fs.Length;
                        fs.Flush();
                        fs.Close();
                        fs = null;
                        if (fsSize >= 1048576)
                            File.Delete(logfilename);
                    }

                    StreamWriter w = File.AppendText(logfilename);
                    w.Write("\r\n" + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + "\r\n" + message);
                    w.Flush();
                    w.Close();

                }
                catch
                {

                }
            }
        }
    }

    public class Log
    {
       
        public static void Logwrite(string message)
        {
            Logwrite log = new Logwrite(); 
            log.logwrite(message, "");
        }

    }
}
