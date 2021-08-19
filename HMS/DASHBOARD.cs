using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace HMS
{
    public partial class DASHBOARD : Form
    {


        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
         );

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        private bool m_aeroEnabled;                     // variables for box shadow
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        public struct MARGINS                           // struct for box shadow
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        private const int WM_NCHITTEST = 0x84;          // variables for dragging the form
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();

                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW;

                return cp;
            }
        }

        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:                        // box shadow
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 1,
                            rightWidth = 1,
                            topHeight = 1
                        };
                        DwmExtendFrameIntoClientArea(this.Handle, ref margins);

                    }
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)
            {// drag the form
                m.Result = (IntPtr)HTCAPTION;
            }
        }
            public DASHBOARD()
        {
            InitializeComponent();
            m_aeroEnabled = false;

            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void DASHBOARD_Load(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void button_WOC1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addpatient_Click(object sender, EventArgs e)
        {
            addpatient.ForeColor = System.Drawing.Color.Blue;
            addpatienthistory.ForeColor = System.Drawing.Color.White;
            addhospitalinfo.ForeColor = System.Drawing.Color.White;
            addnewinformation.ForeColor = System.Drawing.Color.White;
            pictureBox5.Visible = false;
            panel3.Visible = true;
            PANEL_3.Visible = false;
           
        }

        private void addnewinformation_Click(object sender, EventArgs e)
        {
            addnewinformation.ForeColor = System.Drawing.Color.Blue;
            addpatient.ForeColor = System.Drawing.Color.White;
            addpatienthistory.ForeColor = System.Drawing.Color.White;
            addhospitalinfo.ForeColor = System.Drawing.Color.White;
          
            PANEL_3.Visible = true;
            pictureBox5.Visible = false;
            panel_4.Visible = false;
            panels.Visible = true;


        }

        private void addpatienthistory_Click(object sender, EventArgs e)
        {
            addpatienthistory.ForeColor = System.Drawing.Color.Blue;
            addnewinformation.ForeColor = System.Drawing.Color.White;
            addpatient.ForeColor = System.Drawing.Color.White;
            addhospitalinfo.ForeColor = System.Drawing.Color.White;
            panel_4.Visible = true;
            panels.Visible = false;

            string connectiontring = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =C:\Users\faisal malik\Documents\HOSPITALMANAGMENTSYSTEM.accdb";

            OleDbConnection con = new OleDbConnection(connectiontring);
            con.Open();
            string TEXT = "SELECT * FROM NEWPATEINT  INNER JOIN  NEWPATEINTMORE ON  NEWPATEINT.PATID = NEWPATEINTMORE.pid";

            string query = TEXT;

            OleDbCommand cmd = new OleDbCommand(query, con);

            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];



        }

        private void addhospitalinfo_Click(object sender, EventArgs e)
        {
            addhospitalinfo.ForeColor = System.Drawing.Color.Blue;
            addnewinformation.ForeColor = System.Drawing.Color.White;
            addpatient.ForeColor = System.Drawing.Color.White;
            addpatienthistory.ForeColor = System.Drawing.Color.White;
            panels.Visible = true;
       


        }

        private void BTNSAVE_Click(object sender, EventArgs e)
        {
         
                string name = txtname.Text;
                string address = txtaddress.Text;
            string conatact = txtcntnumber.Text;
                string age = txtage.Text;
                string gender = txtgender.Text;
                string blood = txtbloodgroup.Text;
                string any = txtdiease.Text;
           
            
            string pid = txtpatid.Text;
 string connectiontring = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =C:\Users\faisal malik\Documents\HOSPITALMANAGMENTSYSTEM.accdb";

            OleDbConnection con = new OleDbConnection(connectiontring);
                string TEXT = "INSERT INTO NEWPATEINT ([username],[address],[number],[any],[PATID],[blood],[gender],[age]) VALUES ('" + name + "','" + address + "','" + conatact + "','" + any + "','" + pid + "','" + blood + "','" + gender + "','" + age + "')";

                string query = TEXT;

                OleDbCommand cmd = new OleDbCommand(query, con);


                con.Open();

                int a = cmd.ExecuteNonQuery();


                con.Close();

            if (a > 0)
            {
                MessageBox.Show("NEW PATIENT RECORD ADDED SUCCESFULLY");

                txtname.Clear();
                txtaddress.Clear();
                txtage.Clear();
                txtbloodgroup.Clear();
                txtdiease.Clear();
                txtpatid.Clear();
                txtcntnumber.Clear();
                txtgender.ResetText();
            }
            else
            {
                MessageBox.Show("PLEASE ENTER CORRECT RECORD");
            }

                
     


            

        }

        private void label9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_WOC1_Click_1(object sender, EventArgs e)
        {
           
            USERCHECKER userc = new USERCHECKER();
            this.Hide();
            userc.ShowDialog();
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                int PID = Convert.ToInt32(textBox1.Text);
                string connectiontring = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =C:\Users\faisal malik\Documents\HOSPITALMANAGMENTSYSTEM.accdb";

                OleDbConnection con = new OleDbConnection(connectiontring);
                con.Open();
                string TEXT = "SELECT * FROM NEWPATEINT  WHERE PATID = '" + PID + "'";

                string query = TEXT;

                OleDbCommand cmd = new OleDbCommand(query, con);

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
             

                int a = cmd.ExecuteNonQuery();


                con.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string symptoms = txtsym.Text;

            string medicines = txtmed.Text;
            string diagnosis = txtdiag.Text;
            string wardtype = combOREQ.Text;
            string ward = comboWARD.Text;
            string pid = textBox1.Text;


            string connectiontring = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =C:\Users\faisal malik\Documents\HOSPITALMANAGMENTSYSTEM.accdb";

            OleDbConnection con = new OleDbConnection(connectiontring);
            string TEXT = "INSERT INTO NEWPATEINTMORE ([symptoms],[diagnosis],[medicines],[wardtype],[pid],[ward]) VALUES ('" + symptoms + "','" + diagnosis + "','" + medicines + "','" + ward + "','" + pid + "','" + wardtype + "')";

            string query = TEXT;

            OleDbCommand cmd = new OleDbCommand(query, con);


            con.Open();


            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("NEW PATEINT RECORD INSERTED");
            }

            else
            {
                MessageBox.Show("PLEASE INSERT CORRECT INFO");
            }

            con.Close();


            txtdiag.Clear();
            txtmed.Clear();
            txtsym.Clear();
            combOREQ.ResetText();
            comboWARD.ResetText();
            textBox1.Clear();



        }

        private void PRINT_Click(object sender, EventArgs e)
        {
            printDocument1.Print();

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            Bitmap bm = new Bitmap(this.dataGridView2.Width, this.dataGridView2.Height);



            dataGridView2.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView2.Width, this.dataGridView2.Height));

            e.Graphics.DrawImage(bm, 10, 10);
        }
    }
}
