using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Tack
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string inputFilePath = "E:\\28-09-2021.pu";
            ParseData(inputFilePath);
        }

        public bool ParseData(string inputFilePath)
        {
            string daten = inputFilePath.Substring(inputFilePath.Length - 13, 10);
            int count = 15;
            int file_extension = 1;
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;
            if (file_extension == 1)
            {
                for (int i = 1; i <= count; i++)
                {
                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = string.Format("f{0}", i);
                    dc.Unique = false;
                    dt.Columns.Add(dc);
                }
                if (File.Exists(inputFilePath))
                {
                    string OutputFilePath = (@"E:\" + daten + ".ur");
                    StreamReader sr = new StreamReader(inputFilePath, Encoding.Default);
                    string file;
                    int cout = 0;
                    while ((file = sr.ReadLine()) != null)
                    {
                        string[] arr = file.Split(new char[] { ',' });
                        dr = dt.NewRow();
                        for (int i = 1; i <= count; i++)
                        {
                            string col = string.Format("f{0}", i);
                            try
                            {
                                dr[col] = arr[i - 1];
                            }
                            catch (Exception e) { }
                        }
                        dt.Rows.Add(dr);

                        string lin1 = dt.Rows[0][0].ToString();
                        if (lin1 != "date:" + daten)
                        {
                            MessageBox.Show("invalid file !: file name don’t match with date in first line");
                            return false;
                        }
                        cout++;
                    }
                    if (Convert.ToInt32(dt.Rows[0][1].ToString().Substring(6)) != cout - 1)
                    {
                        MessageBox.Show("invalid file !:count on first line don’t match with lines count ");
                        return false;
                    }
                    using (StreamWriter writer = new StreamWriter(OutputFilePath))
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                writer.WriteLine(dt.Rows[i][0].ToString() + "," + dt.Rows[i][1].ToString() + "    " + "\n");
                            }
                            else
                            {
                                writer.WriteLine("[");
                                writer.WriteLine(" age:" + dt.Rows[i][0].ToString() + "\n" + "country:" + dt.Rows[i][1].ToString() + "\n" + "Name:" + dt.Rows[i][2].ToString());

                                writer.WriteLine("Data:" + dt.Rows[i][3].ToString());
                                writer.WriteLine(dt.Rows[i][4].ToString());
                                writer.WriteLine(dt.Rows[i][5].ToString());

                                writer.WriteLine("]");
                                writer.WriteLine("\n");
                            }
                        }
                    }
                    MessageBox.Show("Done");
                    sr.Close();
                }
                else
                {
                    MessageBox.Show("file not fined");
                }
            }
            return true;
        }
    }
}