﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS
{
    public partial class staffdashboard : Form
    {
        public staffdashboard()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            USERCHECKER userc = new USERCHECKER();
            this.Hide();
            userc.ShowDialog();
            this.Close()
        }
    }
}
