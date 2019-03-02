using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PIA;
using System.IO;
using System.Management;
using System.Threading;

namespace PIA
{
    public partial class Form1 : Form
    {
        string regKey = "";
        HWprocesser hwp = new HWprocesser();
        private DriveDetector driveDetector = null;
        chk chkObj = new chk();
        string roll, copy;
        public Form1(string rolln)
        {

            if (rolln != null)
            {
                //roll = rolln.Split('-')[0].ToString();
                copy = "1";// rolln.Split('-')[1].ToString();
            }
            copy = "1";

            InitializeComponent();
            checkBoxAskMe.Visible = false;
            btn_disconnect.Visible = false;
            //pic_searchPD.Visible = false;

            //driveDetector = new DriveDetector();
            //driveDetector.DeviceArrived += new DriveDetectorEventHandler(OnDriveArrived);
            //driveDetector.DeviceRemoved += new DriveDetectorEventHandler(OnDriveRemoved);
            //driveDetector.QueryRemove += new DriveDetectorEventHandler(OnQueryRemove);
        }

        // Called by DriveDetector when removable device in inserted 
        private void OnDriveArrived(object sender, DriveDetectorEventArgs e)
        {
            // Report the event in the listbox.
            // e.Drive is the drive letter for the device which just arrived, e.g. "E:\\"
            string s = "One Pendrive Connected with System " + e.Drive + "\\";
            MessageBox.Show(s);
            if (checkBoxAskMe.Checked != true)
            {
                checkPD();
            }
            //   Additm(e.Drive + "\\");
            //  comboBox1.Enabled = true;

        }

        // Called by DriveDetector after removable device has been unpluged 
        private void OnDriveRemoved(object sender, DriveDetectorEventArgs e)
        {
            // TODO: do clean up here, etc. Letter of the removed drive is in e.Drive;

            // Just add report to the listbox
            // removeitm(e.Drive + "\\");
            string s = "Pendrive removed " + e.Drive + "\\";
            MessageBox.Show(s);
            if (checkBoxAskMe.Checked != true)
            {
                checkPD();
            }


        }

        // Called by DriveDetector when removable drive is about to be removed
        private void OnQueryRemove(object sender, DriveDetectorEventArgs e)
        {
            // Should we allow the drive to be unplugged?
            if (checkBoxAskMe.Checked)
            {
                if (MessageBox.Show("Do you want to stop the process of Connected pendrive ?", "PIA Utility App", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    e.Cancel = false;
                    diconnctPD();
                }   // Allow removal

                else
                {
                    e.Cancel = true;
                    MessageBox.Show("Safely remove the pendrive from PIA utility app. else this may harm your pendrive", "PIA Utility Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);

                }      // Cancel the removal of the device  
            }
        }


        // User checked the "Ask me before drive can be disconnected box"        
        private void checkBoxAskMe_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAskMe.Checked)
            {
                // Is QueryRemove enabled? 
                // If not, we will enable it for the drive which is selected 
                // in the listbox. 
                // If the listbox is empty, no drive has been detected yet so do nothing now.
                if (!driveDetector.IsQueryHooked && comboBox1.Items.Count > 0)
                {
                    if (comboBox1.SelectedItem == null)
                    {
                        //MessageBox.Show("Please choose the drive for which you wish to be asked in the list (select its message).");
                        MessageBox.Show("Please choose the drive from the list");
                        // checkBoxAskMe.Checked = false;
                        return;
                    }

                    bool ok = false;
                    string s = (string)comboBox1.Text;
                    int n = s.IndexOf(':');
                    if (n > 0)
                    {
                        s = s.Substring(n - 1, 3);  // Gets drive letter from the message, (e.g. "E:\\") 

                        // Tell DriveDetector to monitor this drive
                        ok = driveDetector.EnableQueryRemove(s);
                    }

                    if (!ok)
                        MessageBox.Show("Sorry, for some reason notification for QueryRemove did not work out.");

                }

            }
            else
            {
                // "unchecked" the box so disable query remove message
                if (driveDetector.IsQueryHooked)
                    driveDetector.DisableQueryRemove();
            }
        }

        //private void buttonClose_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}
        private void Form1_Load(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            timer1.Enabled = false;
            checkPD();
        }
        Form2 f2;
        Form3 f3;
        uploadPDF PlrPDF;
        uploadPPT ppt;



        public void removeitm(string a)
        {
            comboBox1.Items.Remove(a);
        }
        public void Additm(string b)
        {
            comboBox1.Items.Add(b);
        }



        int mouseX = 0, mouseY = 0;
        bool mouseDown;
        private void button1_Click(object sender, EventArgs e)
        {
            lbl_ConnectedPD.Text = comboBox1.SelectedText;
            pic_searchPD.Enabled = false;
            pic_searchPD.Cursor = Cursors.No;



        }
        void f2_FormClosed(object sender, FormClosedEventArgs e)
        {
            f2 = null;
            //throw new NotImplementedException();
        }

        //Point lastPoint1;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
            //mouseDown = true;

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { this.Left += e.X - lastPoint.X; this.Top += e.Y - lastPoint.Y; }

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            //  mouseDown = false;
        }



        void f3_FormClosed(object sender, FormClosedEventArgs e)
        {
            f3 = null;
            //this.Refresh();
            //Form1_Load(this, null);
            //throw new NotImplementedException();
        }

        private void lbl_Closeform_Click(object sender, EventArgs e)
        {
            if (lbl_ConnectedPD.Text != "Not Connected")
            {
                if (MessageBox.Show(string.Format("Do you really want to Exit ?  \n due to this data can be currupted. First click on Disconnect."), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
            else
            {
                if (MessageBox.Show(string.Format("Do you really want to Exit ?"), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }

        }


        private void lbl_Closeform_MouseHover(object sender, EventArgs e)
        {
            lbl_Closeform.ForeColor = Color.White;
        }

        private void lbl_Closeform_MouseLeave(object sender, EventArgs e)
        {
            lbl_Closeform.ForeColor = Color.Black;
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Image = PIA.Properties.Resources.Video2;
            //ToolTip tt = new ToolTip();
            //tt.SetToolTip(this.pictureBox1, "Upload Video");
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = PIA.Properties.Resources.Video1;
        }

        private void picBox_pdf_MouseHover(object sender, EventArgs e)
        {
            picBox_pdf.Image = PIA.Properties.Resources.PDF2;
            //ToolTip tt = new ToolTip();
            //tt.SetToolTip(this.picBox_pdf, "Upload PDF");
        }

        private void picBox_pdf_MouseLeave(object sender, EventArgs e)
        {
            picBox_pdf.Image = PIA.Properties.Resources.PDF;
        }

        private void picBox_ppt_MouseHover(object sender, EventArgs e)
        {
            picBox_ppt.Image = PIA.Properties.Resources.PPT2;
            //ToolTip tt = new ToolTip();
            //tt.SetToolTip(this.picBox_ppt, "Upload PPT Videos");
        }

        private void picBox_ppt_MouseLeave(object sender, EventArgs e)
        {
            picBox_ppt.Image = PIA.Properties.Resources.PPT1;
        }

        private void picBox_Settings_MouseHover(object sender, EventArgs e)
        {
            picBox_Settings.Image = PIA.Properties.Resources.settings2; // PIA.Properties.Resources.settings2;
        }

        private void picBox_Settings_MouseLeave(object sender, EventArgs e)
        {
            picBox_Settings.Image = PIA.Properties.Resources.Settings1;
        }

        private void picBox_Report_MouseHover(object sender, EventArgs e)
        {
            picBox_Report.Image = PIA.Properties.Resources.Report2;
        }

        private void picBox_Report_MouseLeave(object sender, EventArgs e)
        {
            picBox_Report.Image = PIA.Properties.Resources.Report1;
        }

        private void picBox_profile_MouseHover(object sender, EventArgs e)
        {
            picBox_profile.Image = PIA.Properties.Resources.Purple2;
        }

        private void picBox_profile_MouseLeave(object sender, EventArgs e)
        {
            picBox_profile.Image = PIA.Properties.Resources.Profile1;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (checkBoxAskMe.Checked == true)
            {
                if (copy == "1")
                {
                    if (f3 == null)
                    {
                        f3 = new Form3(null);
                        //f2.MdiParent = this;
                        f3.FormClosed += new FormClosedEventHandler(f3_FormClosed);
                        f3.ShowDialog();

                    }
                    else
                    {

                        f3.Activate();
                    }
                }
                else
                {
                    MessageBox.Show("You are not using full version of PIA Utility App", "PIA Utility Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please Connect Pendrive with Utility app first", "PIA Utility Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void picBox_pdf_Click(object sender, EventArgs e)
        {
            if (checkBoxAskMe.Checked == true)
            {
                if (copy == "1")
                {
                    if (PlrPDF == null)
                    {
                        PlrPDF = new uploadPDF("");
                        //f2.MdiParent = this;
                        PlrPDF.FormClosed += new FormClosedEventHandler(PlrPDF_FormClosed);
                        PlrPDF.ShowDialog();

                    }
                    else
                    {

                        PlrPDF.Activate();
                    }
                }
                else
                {
                    MessageBox.Show("You are not using full version of PIA Utility App", "PIA Utility Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please Connect Pendrive with Utility app first", "PIA Utility Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        void PlrPDF_FormClosed(object sender, FormClosedEventArgs e)
        {
            PlrPDF = null;
            //throw new NotImplementedException();
        }

        private void picBox_ppt_Click(object sender, EventArgs e)
        {
            if (checkBoxAskMe.Checked == true)
            {
                if (copy == "1")
                {
                    if (ppt == null)
                    {
                        ppt = new uploadPPT("");
                        //f2.MdiParent = this;
                        ppt.FormClosed += new FormClosedEventHandler(ppt_FormClosed);
                        ppt.ShowDialog();

                    }
                    else
                    {

                        ppt.Activate();
                    }
                }
                else
                {
                    MessageBox.Show("You are not using full version of PIA Utility App", "PIA Utility Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please Connect Pendrive with Utility app first", "PIA Utility Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        void ppt_FormClosed(object sender, FormClosedEventArgs e)
        {
            ppt = null;
            //throw new NotImplementedException();
        }
        //private void Usb_Connect_MouseHover(object sender, EventArgs e)
        //{

        //}

        //private void Usb_Connect_MouseLeave(object sender, EventArgs e)
        //{

        //}

        private void picBox_quiz_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.ToolTipTitle = "Quiz";

            tt.ToolTipIcon = ToolTipIcon.Info;
            tt.UseFading = true;
            tt.UseAnimation = true;
            tt.IsBalloon = true;

            tt.ShowAlways = true;

            // tt.AutoPopDelay = 2000;
            //  tt.InitialDelay = 1000;
            tt.ReshowDelay = 500;

            tt.SetToolTip(this.picBox_quiz, "Add questions to quiz");
            picBox_quiz.Image = PIA.Properties.Resources.quiz2;
        }

        private void picBox_quiz_MouseLeave(object sender, EventArgs e)
        {
            picBox_quiz.Image = PIA.Properties.Resources.quiz1;
        }

        private void Usb_Connect_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picBox_profile_Click(object sender, EventArgs e)
        {

        }

        //private void pictureBox2_Click(object sender, EventArgs e)
        //{
        //    //using (Wait frm5 = new Wait(connectPD))
        //    //{
        //    //    frm5.ShowDialog(this);

        //    //}
        //    connectPD();
        //}

        public bool CheckconnectPD()
        {

            bool checkflg = false;
            var driveList = DriveInfo.GetDrives();


            foreach (DriveInfo drive in driveList)
            {
                if (drive.DriveType == DriveType.Removable)
                {
                    checkflg = true;


                }
            }


            return checkflg;

        }
        public void checkPD()
        {


            //  bool checkflg = true;
            // bool non = true;
            string ne = string.Empty;
            comboBox1.Items.Clear();
            if (comboBox1.Items.Count == 0)
            {
                comboBox1.Text = "";

            }


            var driveList = DriveInfo.GetDrives();
            string fdata = string.Empty;
            foreach (DriveInfo drive in driveList)
            {
                if (drive.DriveType == DriveType.Removable)
                {

                    long Totlsizepd = drive.TotalSize / (1024 * 1024 * 1024);
                    //long drivSize = drive.TotalSize / (1024 * 1024 * 1024);
                    if (Totlsizepd < 2)
                    {
                        fdata = "2GB";
                    }
                    if (Totlsizepd > 2 && Totlsizepd < 4)
                    {
                        fdata = "4GB";
                    }
                    else if (Totlsizepd > 4 && Totlsizepd < 8)
                    {
                        fdata = "8GB";
                    }
                    else if (Totlsizepd > 8 && Totlsizepd < 16)
                    {
                        fdata = "16GB";
                    }

                    comboBox1.Items.Add(drive.ToString() + "\\ , " + fdata.ToString());

                }
            }



        }

        private void pic_searchPD_Click(object sender, EventArgs e)
        {
            if (checkBoxAskMe.Checked != true)
            {
                if (CheckconnectPD())
                {
                    checkPD();
                }
                else
                {
                    MessageBox.Show("No pendrive found", "PIA Utility Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Pendrive Already Connected with PIA Utility app", "PIA Utility Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // connectPD();

        }
        public void diconnctPD()
        {

            string msg = "Not Connected";
            lbl_PDsize.Text = "--";
            lbl_freeSpace.Text = "--";
            lbl_ConnectedPD.Text = msg.ToString();
            //if (comboBox1.Items.Count == 0)
            //{ comboBox1.Enabled = false; }
            //else { comboBox1.Enabled = true; }
            comboBox1.Enabled = true;
            //pic_searchPD.Visible = true;
            Usb_Connect.Image = PIA.Properties.Resources.pd1;
            pic_searchPD.Image = PIA.Properties.Resources.search1;
            pic_searchPD.Cursor = Cursors.Hand;
            pic_searchPD.Enabled = true;
            btn_connect.Visible = true;
            btn_disconnect.Visible = false;
            checkBoxAskMe.Checked = false;
        }

        private void btn_disconnect_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Do you want to remove connected pendrive ?"), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                diconnctPD();
            }
        }

        public string getFreeSpace(string drv)
        {
            string ret = string.Empty;

            //String folder = "z:\\myfolder"; // It works
            ///folder = "\\mycomputer\\myfolder"; // It doesn't work

            System.IO.DriveInfo drive = new System.IO.DriveInfo(drv);
            System.IO.DriveInfo a = new System.IO.DriveInfo(drive.Name);
            long HDPercentageUsed = 100 - (100 * a.AvailableFreeSpace / a.TotalSize);
            if (HDPercentageUsed == 1 || HDPercentageUsed < 1)
            {
                ret = "0 %";
            }
            else
            {
                ret = HDPercentageUsed.ToString() + " %";
            }

            return ret;

        }

        public string getTotalSpace(string drv)
        {

            string fdata = string.Empty;
            System.IO.DriveInfo drive = new System.IO.DriveInfo(drv);
            System.IO.DriveInfo a = new System.IO.DriveInfo(drive.Name);
            long Totlsizepd = a.TotalSize / (1024 * 1024 * 1024);
            //long drivSize = drive.TotalSize / (1024 * 1024 * 1024);
            if (Totlsizepd < 2)
            {
                fdata = "2GB";
            }
            if (Totlsizepd > 2 && Totlsizepd < 4)
            {
                fdata = "4GB";
            }
            else if (Totlsizepd > 4 && Totlsizepd < 8)
            {
                fdata = "8GB";
            }
            else if (Totlsizepd > 8 && Totlsizepd < 16)
            {
                fdata = "16GB";
            }

            return fdata;
        }

        public void strtthrnd()
        {

            //Thread.Sleep(2000);
            for (long i = 0; i <= 100000000000; i++)
            {


            }
        }
        int count = 0;
        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();

        void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            btn_connect.Enabled = true;
            pic_searchPD.Image = PIA.Properties.Resources.nonsearch1;
            connctPD();

            //if (count != 0)
            //{
            //    btn_connect.Enabled = false;
            //    pic_searchPD.Image = PIA.Properties.Resources.searchF;
            //   // button1.Enabled = false;
            //    label1.Text = count.ToString() + " seconds more to Enable Refresh Button";
            //    count--;
            //}
            //else
            //{
            //    btn_connect.Enabled = false;
            //    pic_searchPD.Image = PIA.Properties.Resources.search1;
            //    timer1.Stop();
            //}
            //throw new NotImplementedException();
        }
        public void connctPD()
        {
            try
            {
                //checkPD();
                if (CheckconnectPD())
                {
                    if (comboBox1.Text != "")
                    {

                        lbl_freeSpace.Text = getFreeSpace(comboBox1.Text.Split(',')[0].ToString());
                        lbl_ConnectedPD.Text = comboBox1.Text.Split(',')[0].ToString() + " Pendrive";
                        comboBox1.Enabled = false;
                        Usb_Connect.Image = PIA.Properties.Resources.pd2;
                        pic_searchPD.Cursor = Cursors.No;
                        pic_searchPD.Image = PIA.Properties.Resources.nonsearch1;
                        pic_searchPD.Enabled = false;
                        btn_connect.Visible = false;
                        btn_disconnect.Visible = true;
                        string sizea = comboBox1.Text.Split(',')[1].ToString();
                        lbl_PDsize.Text = sizea.ToString();
                        checkBoxAskMe.Checked = true;

                    }
                    else
                    {
                        MessageBox.Show("Please select the pendrive from list.   If pendrive not available in list then Re-connect the pendrive", "PIA Utility Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No Pendrive found. Please connect pendrive to the system first.", "PIA Utility Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                MessageBox.Show("Internal Error Contact to service provider", "PIA Internal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_connect_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 3000;//one second
            timer1.Tick += new System.EventHandler(timer1_Tick);
            timer1.Start();
            btn_connect.Enabled = false;
            btnBurn.Enabled = true;
            pic_searchPD.Image = PIA.Properties.Resources.searchF;
        }

        private void pic_searchPD_MouseHover(object sender, EventArgs e)
        {
            pic_searchPD.Image = PIA.Properties.Resources.search11;
        }

        private void pic_searchPD_MouseLeave(object sender, EventArgs e)
        {
            pic_searchPD.Image = PIA.Properties.Resources.search1;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { this.Left += e.X - lastPoint.X; this.Top += e.Y - lastPoint.Y; }
        }
        Point lastPoint;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { this.Left += e.X - lastPoint.X; this.Top += e.Y - lastPoint.Y; }

        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {

            lastPoint = new Point(e.X, e.Y);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ///  if(comboBox1.sele)
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { this.Left += e.X - lastPoint.X; this.Top += e.Y - lastPoint.Y; }
        }
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
        private void picBox_quiz_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { this.Left += e.X - lastPoint.X; this.Top += e.Y - lastPoint.Y; }
        }

        private void picBox_quiz_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void lbl_minimize_Click(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized) this.WindowState = FormWindowState.Minimized;
        }

        private void btnBurn_Click(object sender, EventArgs e)
        {
            PDBurn();
        }

        public void PDBurn()
        {
            string PhfileName = "";
            DataTable dt = chkObj.GetUniversal_DTForBurnPd("dta", "");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count; i++)
                {
                    PhfileName = dt.Rows[0][3].ToString();
                    if (CopyFIle(PhfileName))// copy all files
                    {
                        if (IsDbEntry())// update DB
                        {
                            string pth = chkObj.PathReturn();
                            string[] ar = pth.Split('-');
                            string pth1 = ar[0].ToString();
                            string filennnmm = ar[1].ToString();

                            if (CopyFIleDBD(pth1, filennnmm))
                            {
                                //string clntApp = "app.msi";
                                //if (CopyFIleDBD(pth1, clntApp))
                                //{

                                if (regKey != "")
                                {

                                    MessageBox.Show("This Is Key For Pen Drive Registration " + regKey);

                                }
                                //regKey
                                //  }
                            }


                        }
                    }
                    else
                    {
                        MessageBox.Show("One file not found");
                    }
                }
            }
            else
            {
                MessageBox.Show("No data available for Burn Pendrive");
            }
        }




        public bool CopyFIleDBD(string fullPth, string filenm)
        {
            string destFile = "";
            string sourceFile = "";
            bool IsCopy = false;
            string desPath = "";
            string SorFile = fullPth + filenm;

            //sor
            string sourcePath = fullPth.ToString();   // @"C:\Users\Public\TestFolder";
            string targetPath = comboBox1.Text.Split(',')[0].ToString();// @"C:\Users\Public\TestFolder\SubDir";
            desPath = targetPath + "//" + filenm;

            if (getfileFormat.FileExists(SorFile))
            {
                sourceFile = System.IO.Path.Combine(sourcePath, filenm);
                destFile = System.IO.Path.Combine(targetPath, filenm);

            }

            System.IO.File.Copy(sourceFile, destFile, true);
            if (getfileFormat.FileExists(desPath))
            {
                IsCopy = true;
            }

            return IsCopy;

        }
        public bool CopyFIle(string FileName)
        {
            string destFile = "";
            string sourceFile = "";
            bool IsCopy = false;
            string desPath = "";
            string SorFile = "";
            string fileName = FileName.ToString();
            string sourcePath = getfileFormat.GetSourcePath();   // @"C:\Users\Public\TestFolder";
            if (sourcePath != "")
            {
                string targetPath = comboBox1.Text.Split(',')[0].ToString();// @"C:\Users\Public\TestFolder\SubDir";
                desPath = targetPath + "//" + fileName;
                SorFile = sourcePath + "//" + fileName;
                // Use Path class to manipulate file and directory paths.
                if (getfileFormat.FileExists(SorFile))
                {
                    sourceFile = System.IO.Path.Combine(sourcePath, fileName);
                    destFile = System.IO.Path.Combine(targetPath, fileName);

                }

                // To copy a folder's contents to a new location:
                // Create a new target folder, if necessary.
                //if (!System.IO.Directory.Exists(targetPath))
                //{
                //    System.IO.Directory.CreateDirectory(targetPath);
                //}

                // To copy a file to another location and 
                // overwrite the destination file if it already exists.
                System.IO.File.Copy(sourceFile, destFile, true);
                if (getfileFormat.FileExists(desPath))
                {
                    IsCopy = true;
                }
            }
            return IsCopy;
        }

        set settt = new set();

        public bool IsDbEntry()
        {
            bool isEntered = false;

            string[] a = comboBox1.Text.Split(',')[0].ToString().Split(':'); 
            string usbky = hwp.usbkey(a[0].ToString());
            regKey = settt.GetRegkey();
            int keyenble = 0;
            string response = chkObj.UpdateDbForBurning(usbky, keyenble, regKey);
            if (response == "1")
            {
                isEntered = true;
            }

            return isEntered;
        }
    }
}
