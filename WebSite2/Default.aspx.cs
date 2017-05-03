using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Diagnostics;
using System.IO;

namespace tutor
{
    public partial class Default : Page
    {
        public byte Checker(string inp, string outp)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "fOut.exe",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true
                }
            };
            process.Start();
            process.StandardInput.WriteLine(inp);
            bool graceful = process.WaitForExit(1000);
            if (!graceful)
            {
                process.Kill();
                return 2;
            }
            if (process.StandardOutput.ReadLine() == outp)
                return 1;
            return 3;
        }

        public void Compile()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd",
                    Arguments = "/c g++ fIn.cpp -o fOut.exe 2>logs.txt",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            process.WaitForExit();
        }

        public bool InFile(string text)
        {
            try
            {
                FileStream fIn = new FileStream("fIn.cpp", FileMode.Create);
                StreamWriter writer = new StreamWriter(fIn);
                writer.Write(text);
                writer.Close();
                return true;
            }
            catch { return false; }
        }

        public string OutLogs()
        {
            try
            {
                FileStream fOut = new FileStream("logs.txt", FileMode.Open);
                StreamReader reader = new StreamReader(fOut);
                string data = reader.ReadToEnd();
                reader.Close();
                return data;
            }
            catch { return "Error"; }
        }

        public static List<String> allSolv = new List<String>();

        protected void cmdSubmit_Click(object sender, EventArgs e)
        {
            bool ex = false;
            string data = "";
            string outs = "";
            if (IsPostBack)
            {
                if (!String.IsNullOrWhiteSpace(Code.Text))
                {
                    data = Code.Text;
                    data = data.Replace(" ", string.Empty);
                    Label1.Text = "Удачно!";
                    foreach (string s in allSolv)
                        if (String.Compare(data, s) == 0)
                        {
                            Label1.Text = "Это решение уже отправлялось на сервер!";
                            ex = true;
                        }
                    if (!ex)
                    {
                        allSolv.Add(data);
                        InFile(Code.Text);
                        Compile();
                        outs = OutLogs();
                        if (outs != "")
                        {
                            Label1.Text = "Compilation Error";
                            Problem.Text = outs;
                        }
                        else
                        {
                            switch (Checker("421 12", "433"))
                            {
                                case 1:
                                    Label1.Text = "Accepted";
                                    break;
                                case 2:
                                    Label1.Text = "Time Limit";
                                    break;
                                case 3:
                                    Label1.Text = "Wrong Answer";
                                    break;
                            }
                        };
                    }
                }
            }
        }
    }
}