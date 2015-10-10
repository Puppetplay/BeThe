﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BeThe.Test
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var manager = new BeThe.Manager.Manager();
            try
            {
                //manager.SelectAllSchedule();
                //manager.SelectRelay();
                //manager.SelectPlayer_W();
                manager.SelectPlayer();
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }
    }
}
