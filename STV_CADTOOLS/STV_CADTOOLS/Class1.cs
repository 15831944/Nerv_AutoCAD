//Read a Text File
using System;
using System.IO;

namespace readwriteapp
{
    using System.Windows;

    using System.Collections.Generic;
    class ReadCSV
    {
        [STAThread]
        public static IDictionary<string, string> Read(string file)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            String line;

            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(file);

                //Read the first line of text
                line = sr.ReadLine();

                //Continue to read until you reach end of file
                while (line != null)
                {
                    string[] ss = line.Split(',');
                    //append line to list
                    dict.Add(ss[0], ss[1]);
                    //Read the next line
                    line = sr.ReadLine();
                }

                //close the file
                sr.Close();
                return dict;
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }



}
