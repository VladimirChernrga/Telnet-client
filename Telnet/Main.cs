using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace Telnet
{


    public partial class Main : Form
    {
        Telnet tc = new Telnet();

        public Main()
        {
            InitializeComponent();
            //textBoxOutput.ScrollBars = ScrollBars.Both;
            //textBoxOutput.WordWrap = true;
            //textBoxOutput.ReadOnly = true;
            richTextBoxOutput.ReadOnly = true;
            

        }

        private void Main_Load(object sender, EventArgs e)
        {
            //create a new telnet connection to hostname "gobelijn" on port "23"
            
            tc.TelnetConnection("192.168.25.245", 23);

            textBoxSend.Focus();

            //login with user "root",password "rootpassword", using a timeout of 100ms, and show server output
            //string s = tc.Login("root", "rootpassword",100);
            //Console.Write(s);

            // server output should end with "$" or ">", otherwise the connection failed
            //string prompt = s.TrimEnd();
            //prompt = s.Substring(prompt.Length -1,1);
            //if (prompt != "$" && prompt != ">" )
            //  throw new Exception("Connection failed");

            //prompt = "";

            // while connected


            string prompt = "";
            int count = 0;
            while (tc.IsConnected && count < 1)
            {

                System.Threading.Thread.Sleep(1500);

                richTextBoxOutput.Text = tc.Read() + Environment.NewLine;
                //Console.Write(tc.Read());

                count++;

            }
            //Console.Write("!root");
            //SendKeys.Send("{ENTER}");

            /*
            while (tc.IsConnected)
            {
                // display server output
                //string console = tc.Read();



                // send client input to server
                prompt = Console.ReadLine();
                tc.WriteLine(prompt);

                // display server output
                Console.Write(tc.Read());

            }
            Console.WriteLine("***DISCONNECTED");
            Console.ReadLine();
            */
        }

       

        private void output()
        {
            
        }

        private void textBoxSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DateTime localDate = DateTime.Now;
               
                richTextBoxOutput.Text += textBoxSend.Text+ Environment.NewLine + localDate.ToString() + Environment.NewLine;
                
                
                tc.WriteLine(textBoxSend.Text);


                richTextBoxOutput.Text += tc.Read() + Environment.NewLine;

                textBoxSend.Clear();
            }
        }
    }
}
