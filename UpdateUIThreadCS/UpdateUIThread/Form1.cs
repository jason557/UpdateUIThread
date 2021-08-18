using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace UpdateUIThread
{
    public partial class Form1 : Form
    {
        Thread T1;
        Thread T2;
        Thread T3;
        int count = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            T1 = new Thread(DoWork1);
            T1.Start();

        }
        
        void DoWork1()
        {
            count = 0;
            DELupdateCounter1 job = updateCounter1;
            for (int i = 0; i <= 200; i++)
            {
                this.Invoke(job);
                Thread.Sleep(1);
            }
        }

        private delegate void DELupdateCounter1();
        private void updateCounter1()
        {
           
            label1.Text = count.ToString();
            count++;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            T2 = new Thread(DoWork2);
            T2.Start();

        }

        void DoWork2()
        {
            for (int i = 0; i <= 200; i++)
            {                
                SendString(i.ToString());
                Thread.Sleep(1);
            }
        }

        delegate void SendString_Delegate(string msg);

        public void SendString(string msg)
        {

            if (label2.InvokeRequired)
            {
                label2.Invoke(new SendString_Delegate(SendString), new string[] { msg });
                //label2.Invoke((Action)delegate () { label2.Text = msg; });
            }
            else
            {
                label2.Text = msg;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            T3= new Thread(dowork3);
            T3.Start();
        }

        void dowork3()
        {
            for (int i = 0; i <= 200; i++)
            {
                SendString3(i.ToString());
                Thread.Sleep(1);
            }
        }
        public void SendString3(string msg)
        {
            if (label3.InvokeRequired)
            { 
              label3.Invoke((Action)delegate () { label3.Text = msg; });
            }
            else
            {
                //label3.Text = msg;
            }
           
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (T1 !=null) { T1.Abort(); }
            if (T2 !=null) { T2.Abort(); }
            if (T3 !=null) { T3.Abort(); }
        }
    }
}
