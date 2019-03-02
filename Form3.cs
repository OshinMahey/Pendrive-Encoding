using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;


namespace PIA
{
    public partial class Form3 : Form
    {
        DataRow dr;
        set getset = new set();
        chk chkk = new chk();
        getfileFormat GFF = new getfileFormat();
        tempcon con = new tempcon();
        bool flg1 = false;
        System.Windows.Forms.Timer timer2 = new System.Windows.Forms.Timer();

        public Form3(string abc)
        {
            InitializeComponent();
            // progressBar1.Visible = false;
            pictureBox3.Visible = false;
            errorFileSelect.Visible = false;
            errorSession.Visible = false;
            errorFileNm.Visible = false;
            pic_loading.Visible = false;
            pictureBox2.Visible = false;
            txt_path.Enabled = false;

            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "";
            checkBoxColumn.Width = 30;
            checkBoxColumn.Name = "checkBoxColumn";
            gridview.Columns.Insert(0, checkBoxColumn);
            ////Hide the last blank line
            gridview.AllowUserToAddRows = false;
            if (abc == "reload")
            {

                flg1 = true;


                // BindComboBox1();
            }
        }


        public void cool()
        {
            for (int u = 0; u < 500; u++)
            {
                System.Threading.Thread.Sleep(100);
            }
        }


        public DataTable tablenw()
        {
            DataTable dt2 = new DataTable();
            //dt2 = null;
            dt2.Columns.Add("ID", typeof(string));
            dt2.Columns.Add("Name", typeof(string));
            dt2.Columns.Add("Format", typeof(string));


            //dt.Columns.Add("Action", typeof(string));
            return dt2;
        }
        public void binddata(string val)
        {

            int sno;
            DataTable dt1 = new DataTable();
            dt1 = null;
            if (val == "")
            {
                dt1 = con.CheckCategWit_val("dta", "");
            }
            else
            {
                dt1 = con.CheckDataWit_val("dta", val);
            }
            DataTable dt5 = tablenw();
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    sno = i + 1;
                    dr = dt5.NewRow();
                    dr["ID"] = sno.ToString();
                    dr["Name"] = dt1.Rows[i][2].ToString();
                    dr["Format"] = dt1.Rows[i][5].ToString();

                    dt5.Rows.Add(dr);
                }

                // gridview.DataSource = null;
                gridview.DataSource = dt5;
                lbl_error.Text = "";
                //    dataGridView1.AutoResizeColumns(
                //DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                //dt5.Clear();
                //dt5 = null;
            }
            else
            {

                gridview.DataSource = dt5;
                lbl_error.Text = "No data found Or Re-select Sesstion from list";
                //   MessageBox.Show("No data found.", "Session Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void lbl_Closeform_Click(object sender, EventArgs e)
        {
            // this.Close();



        }
        Form1 f1;
        private void lbl_close_Click(object sender, EventArgs e)
        {

            this.Close();
            f1 = new Form1("");
            //  f1.ShowDialog();
            f1.Activate();

            //f3.ShowDialog();
            //    f3.FormClosed += new FormClosedEventHandler(f3_FormClosed);
            //}

        }

        //void f3_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    f3 = null;
        //    //throw new NotImplementedException();
        //}





        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            //mouseDown = true;
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            //mouseDown = false;
        }
        Point lastPoint;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { this.Left += e.X - lastPoint.X; this.Top += e.Y - lastPoint.Y; }
            //if (mouseDown)
            //{
            //    mouseX = MousePosition.X - 300;
            //    mouseY = MousePosition.Y - 25;

            //    this.SetDesktopLocation(mouseX, mouseY);
            //}
        }
        void savedata()
        {

            //OpenFileDialog dilog = new OpenFileDialog();
            //dilog.ShowDialog();
            //if(dilog.ShowDialog==ShowDialo)
            for (int i = 0; i <= 500; i++)
            {
                Thread.Sleep(10);
            }
        }

        //public bool chk1()
        //{
        //    bool flg = true;
        //    //string msg;
        //    if (comboBox1.SelectedIndex == -1)
        //    {
        //        flg = false;
        //        //  msg += " Please Select Sesstion";//   MessageBox.Show("","Please select Sesstion first","",);
        //    }
        //    else if (txt_Vname.Text.Trim() == "")
        //    {
        //        flg = false;

        //    }

        //    if (flg == false)
        //    {

        //    }
        //    return flg;

        //}

        //private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    MessageBox.Show(" \n Selected index is >> " + comboBox1.SelectedIndex + " \n and selected text >> " + comboBox1.Text);
        //    //if (comboBox1.SelectedIndex == 0)
        //    //{

        //    //}
        //    //else if (comboBox1.SelectedIndex >= 0)
        //    //{
        //    //    string a = comboBox1.SelectedValue.ToString();
        //    //    MessageBox.Show(comboBox1.SelectedValue.ToString());
        //    //}
        //}

        public void BindComboBox1()
        {
            fillcombo(combo1, "categnm", "UID");
            fillcombo2(combo2, "categnm", "UID");
        }
        public void fillcombo2(ComboBox combo2, string displyMember, string valuMamber)
        {
            DataTable dt1 = new DataTable();
            dt1 = con.CheckCategWit_val("vctg", "");
            if (dt1.Rows.Count > 0)
            {
                combo2.DataSource = dt1;
                combo2.DisplayMember = displyMember;
                combo2.ValueMember = valuMamber;
            }
            else
            {

            }
        }

        public void fillcombo(ComboBox combo, string displyMember, string valuMamber)
        {

            DataTable dt1 = new DataTable();
            dt1 = con.CheckCategWit_val("vctg", "");
            if (dt1.Rows.Count > 0)
            {

                combo.DataSource = dt1;
                combo.DisplayMember = displyMember;
                combo.ValueMember = valuMamber;

                pictureBox2.Visible = false;
                pic_refresh.Visible = false;
                link_categ.Visible = true;



            }
            else if (dt1.Rows.Count == 0)
            {
                pictureBox2.Visible = true;
                pic_refresh.Visible = false;
                link_categ.Visible = false;
            }


        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // binddata("");
            progressBar1.Visible = false;
            lbl_combo_txt.Visible = false;
            lbl_combo_value.Visible = false;
            pic_refresh.Visible = false;
            BindComboBox1();
            if (flg1 == true)
            {
                pic_refresh.Visible = true;
                pictureBox2.Visible = false;
            }
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.Image = PIA.Properties.Resources.anserBlack;
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.pictureBox2, "Add sesstion.");
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = PIA.Properties.Resources.anser;
        }
        CategVideo cv;
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (cv == null)
            {
                // this.Hide();
                cv = new CategVideo();
                cv.FormClosed += new FormClosedEventHandler(cv_FormClosed);
                cv.ShowDialog();


            }
            else
            {
                cv.Activate();
            }
        }

        void cv_FormClosed(object sender, FormClosedEventArgs e)
        {
            cv = null;
            //this.Refresh();
            this.Form3_Load(null, null);
            //  Form3_Load(this, null);
            //throw new NotImplementedException();
        }

        private void pic_refresh_Click(object sender, EventArgs e)
        {

            if (flg1 == true)
            {
                BindComboBox1();
                flg1 = false;
            }
        }

        private void link_categ_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (cv == null)
            {
                // this.Hide();
                cv = new CategVideo();
                cv.FormClosed += new FormClosedEventHandler(cv_FormClosed);
                cv.ShowDialog();


            }
            else
            {
                cv.Activate();
            }
        }

        private void btn_brows_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.ToolTipTitle = "Browse";
            //  tt.SetToolTip(this.pic_back, "Back");

            tt.ToolTipIcon = ToolTipIcon.Info;
            tt.UseFading = true;
            tt.UseAnimation = true;
            tt.IsBalloon = true;
            tt.ShowAlways = true;

            // tt.AutoPopDelay = 2000;
            //  tt.InitialDelay = 1000;
            tt.ReshowDelay = 500;
            tt.SetToolTip(this.btn_brows, "Click here for Upload Videos. Only .avi, .mp4, .wmv file formats are valid");
        }

        private void pic_back_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.ToolTipTitle = "Home";
            tt.ToolTipIcon = ToolTipIcon.Info;
            tt.UseFading = true;
            tt.UseAnimation = true;
            tt.IsBalloon = true;
            tt.ShowAlways = true;
            tt.AutoPopDelay = 2000;
            //  tt.InitialDelay = 1000;
            tt.ReshowDelay = 500;
            tt.SetToolTip(this.pic_back, "Click here to go back");
        }

        private void pic_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_bulkUpload_MouseHover(object sender, EventArgs e)
        {
            //ToolTip tt = new ToolTip();
            //tt.ToolTipTitle = "Bulk Upload";
            //tt.ToolTipIcon = ToolTipIcon.Warning;
            //tt.UseFading = true;
            //tt.UseAnimation = true;
            //tt.IsBalloon = true;
            //tt.ShowAlways = true;
            //tt.AutoPopDelay = 2000;
            //tt.ReshowDelay = 500;
            //tt.SetToolTip(this.btn_bulkUpload, "Generally it will take long time then single file upload");
        }
        bool svDataFlag;//= true;
        private void button1_Click(object sender, EventArgs e)
        {
            if (chckFields())
            {
                Thread t1 = new Thread(() =>
                    {
                        fileUploadfnl();
                        Action action = () => resetWait();
                        this.BeginInvoke(action);
                    });
                t1.Start();

                // lblStatus.Visible = true;      // view Message
                progressBar1.Visible = true;   // Progress Bar
                pic_loading.Visible = true;    // Loading....
                lblStatus.Text = "Please wait...";

                btn_addvideo.Enabled = false;    //  Disable button

                //  Timer 2 Start here.....
                timer2.Enabled = true;
                timer2.Interval = 50;
                timer2.Tick += new EventHandler(timer2_Tick);
                timer2.Start();
            }
            else
            {

                MessageBox.Show("Please provide an input");

            }
            //using (Wait frm5 = new Wait(fileUploadfnl))
            //{
            //    frm5.ShowDialog(this);
            //    clearContrl();
            //}
        }
        int ss = 0;
        bool flg99 = false;
        void timer2_Tick(object sender, EventArgs e)
        {
            if (timer1.Interval <= 2000)  //  check how much time passed through the process
            {
            }
            ss += 1;
            if (ss < 101)
            {
                progressBar1.Value = ss;
            }
            if (ss == 110)
            {
                lblStatus.Text = "Almost done....";
            }
            else if (ss == 150)
            {
                lblStatus.Text = "Just few moments...";
            }
            //if (progressBar1.Value == 100)
            //{
            //    progressBar1.Value = 0;

            //}
            if (flg99 == true)
            {
                timer2.Enabled = false;
                ss = 0;
                flg99 = false;
                //progressBar1.Value = 0;
                //progressBar1.Visible = false;
                //clearContrl();
            }


            //throw new NotImplementedException();
        }
        public void resetWait()
        {
            timer1.Enabled = true;
            timer1.Interval = 4000;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
            flg99 = true;
            if (svDataFlag)
            {
                lblStatus.Text = "!! File Saved Successfully !!";
                //btn_addvideo.Enabled = true;
                //pic_loading.Visible = false;
                progressBar1.Value = 0;

                string val = lbl_combo_value.Text.ToString();
                binddata(val);
                clearContrl();
            }
            else
            {
                lblStatus.Text = "";
            }
            btn_addvideo.Enabled = true;
            pic_loading.Visible = false;
            progressBar1.Value = 0;
            clearContrl();
        }

        void timer1_Tick(object sender, EventArgs e)
        {

            ss = 0;
            flg99 = false;

            //if (svDataFlag)
            //{
            lblStatus.Text = "";
            progressBar1.Value = 0;
            progressBar1.Visible = false;
            timer1.Enabled = false;
            //}


        }

        private void btn_brows_Click(object sender, EventArgs e)
        {
            uploadFile();
        }

        private string GetFile()
        {
            try
            {
                openFileDialog1.Filter = "Solution Files (*.*)|*.*";
                openFileDialog1.Multiselect = true;
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    return openFileDialog1.FileName;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return string.Empty;
        }
        public void uploadFile()
        {
            var file = GetFile();
            var lst = new string[] { ".avi", ".mp4", ".wmv" };
            if (!lst.Contains(Path.GetExtension(file)))
            {
                MessageBox.Show("Please select proper file.");
            }
            else
            {
                string a = Path.GetFullPath(file);
                txt_path.Text = a.ToString();

                //var result = SaveToDataBase(Path.GetFileName(file), GetCompressedData(file, ConvertFileToByteData(file)));
                //if (result)
                //{
                //    cmbPlayList.Items.Add(Path.GetFileName(file));
                //    MessageBox.Show("!! File Saved Successfully !!");
                //}
            }
        }


        public void fileUploadfnl()
        {


            //comboBox1.SelectedValue
            var file = openFileDialog1.FileName;
            var Extn = Path.GetExtension(file);
            string Categ = lbl_combo_value.Text;// "ss";//.ToString;// ;

            var type = "V";
            var usrSpecifyNm = txt_Vname.Text.Trim();
            var result = SaveToDataBase(usrSpecifyNm.ToString(), Path.GetFileNameWithoutExtension(file), getfileFormat.GetCompressedData(file, getfileFormat.ConvertFileToByteData(file)), Categ, type, Extn);
            if (result)
            {
                ////binddata("");
                ////cmbPlayList.Items.Add(Path.GetFileName(file));
                //MessageBox.Show("!! File Saved Successfully !!");

            }
        }



        public void clearContrl()
        {
            btn_addvideo.Enabled = true;
            txt_path.Text = string.Empty;
            txt_Vname.Text = string.Empty;
            combo1.SelectedIndex = 0;
        }

        public bool chckFields()
        {
            bool flg55 = true;
            if (lbl_combo_value.Text == "System.Data.DataRowView" || lbl_combo_value.Text == "")//.SelectedIndex == 0)
            {
                errorSession.Visible = true;
                flg55 = false;
            }
            else
            {
                errorSession.Visible = false;

            }

            if (txt_Vname.Text.Trim() == "")
            {
                errorFileNm.Visible = true;

                flg55 = false;
            }
            else
            {
                errorFileNm.Visible = false;
            }

            if (txt_path.Text.Trim() == "")
            {
                errorFileSelect.Visible = true;
                flg55 = false;
            }
            else
            {
                errorFileSelect.Visible = false;
            }


            return flg55;
        }


        //private void btn_bulkUpload_Click(object sender, EventArgs e)
        //{

        //}



        private bool SaveToDataBase(string UserSpecifyNm, string fileName, byte[] data, string session, string type, string extn)
        {
            svDataFlag = true;
            var str = string.Empty;
            try
            {
                string tb = " dta";
                string UID = getset.getUID();
                string reslt = chkk.checkandInsertVideo(UserSpecifyNm, tb, UID, session, type, extn);
                if (reslt == "1")
                {
                    // txt_session.Text = null;
                    if (getfileFormat.CreateDirectories())
                    {
                        string path2;
                        var path = Path.GetTempPath();
                        path2 = path + "data" + "\\allFiles";
                        if (getfileFormat.DirectoryExists(path2))
                        {
                            str = path2.ToString();
                            // Directory.CreateDirectory(path2); // Create Directories
                        }
                        str += "\\" + UID;
                        var file = (new StreamWriter(new FileStream(str, FileMode.OpenOrCreate, FileAccess.Write)));
                        string img1bt = Convert.ToBase64String(data);
                        file.Write(img1bt);
                        file.Close();
                        svDataFlag = true;
                    }
                    //  MessageBox.Show("Data Saved Successfully ");


                }
                else
                {
                    string[] sp = reslt.Split(',');
                    if (sp[0] == "0")
                    {

                        MessageBox.Show("Error :" + sp[1].ToString(), "PIA Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        svDataFlag = false;

                    }
                }

                //try
                //{
                //    var path = Path.GetTempPath();
                //    str = path + "\\" + targetFileName;
                //    if (File.Exists(str))
                //        File.Delete(str);
                //}
                //catch (Exception) { }

                //FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);


                //var bw = new BinaryWriter(File.Open(@"C:\Users\Gaurav\Desktop\Encrypted file", FileMode.OpenOrCreate,FileAccess.Write));
                //bw.Write(data);

                //===========================================// Single Un Comment

                //if (getfileFormat.CreateDirectories())
                //{
                //    string path1, path2;
                //    //                    var a = Path.
                //    var path = Path.GetTempPath();
                //    // path1 = path + "\\data";
                //    // path2 = "D:\\"+"data" + "\\allFiles" ;
                //    path2 = path + "data" + "\\allFiles";
                //    if (getfileFormat.DirectoryExists(path2))
                //    {
                //        str = path2.ToString();
                //        // Directory.CreateDirectory(path2); // Create Directories
                //    }
                //    str += "\\mytext";
                //    //StreamWriter file1 = new StreamWriter(str,FileMode.OpenOrCreate,FileAccess.Write));
                //    ////                StreamWriter file1 = new StreamWriter("Mytext2.text");
                //    //string img1bt = Convert.ToBase64String(data);
                //    //file1.Write(img1bt);
                //    //file1.Close();

                //    var file = (new StreamWriter(new FileStream(str, FileMode.OpenOrCreate, FileAccess.Write)));
                //    string img1bt = Convert.ToBase64String(data);
                //    file.Write(img1bt);
                //    file.Close();
            }


                //Stream stream = ofdPlayer.FileName; //;
            //BinaryReader binaryReader = new BinaryReader(stream);
            //byte[] bytes = binaryReader.ReadBytes((int)stream.Length);
            //string img1bt = Convert.ToBase64String(bytes);
            //ViewState["img1"] = img1bt.ToString();
            ///  File.WriteAllBytes(@"C:\Users\Gaurav\Desktop\Encrypted file", data);



                //  tc.Add(fileName, data);


                // var ds = new DataSet();
            // var cmd = new SqlCommand("insert into MyPlay (id,FileName,FileData) values(@file,@content)");

                //// SqlCommand cmd = new SqlCommand("insert into MyPlay (id,FileName,FileData) values('" + Guid.NewGuid() + "','" + fileName + "',@content)");
            // cmd.Parameters.Add("@file", SqlDbType.Text).Value = fileName;
            // cmd.Parameters.Add("@content", SqlDbType.VarBinary).Value = data;

                //------------------------------------//

                //                SqlParameter param = cmd.Parameters.Add("@content", SqlDbType.VarBinary);
            //SqlCommand cmd = new SqlCommand("insert into MyPlay values('" + Guid.NewGuid() + "','" + fileName + "',@content)", con);
            //SqlParameter param = cmd.Parameters.Add("@content", SqlDbType.VarBinary);
            //        param.Value = data;
            //  con.Open();
            //  cmd.Connection = new SqlConnection(con);
            // cmd.CommandTimeout = 0;
            ///cmd.Connection.Open();
            //cmd.ExecuteNonQuery();
            //con.Close();
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            }
            return svDataFlag;
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
            //return false;
        }

        private void combo1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string val = combo1.SelectedValue.ToString();
            if (val != "System.Data.DataRowView")
            {
                lbl_combo_txt.Text = combo1.Text;
                lbl_combo_value.Text = combo1.SelectedValue.ToString();

            }

        }

        private void combo2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (combo2.SelectedIndex == 0)
            {
                string val = combo2.SelectedValue.ToString();
                if (val != "System.Data.DataRowView")
                {
                    binddata(val);
                }
            }
            else if (combo2.SelectedIndex > 0)
            {
                string val = combo2.SelectedValue.ToString();
                binddata(val);

            }

        }

        private void gridview_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //int selectedRow = e.RowIndex;
            //DataGridViewRow row = gridview.Rows[e.RowIndex];
            //string rowValue = row.Cells[1].Value.ToString();
            //if (MessageBox.Show(String.Format("Do you want to delete ?  " + rowValue), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{



            //}
        }

        private void gridview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //int selectedRow = e.RowIndex;
            //DataGridViewRow row = gridview.Rows[e.RowIndex];
            //string rowValue = row.Cells[1].Value.ToString();
            //if (MessageBox.Show(String.Format("Do you want to Edit ?  " + rowValue), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{



            //}
        }

        private void Form3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { this.Left += e.X - lastPoint.X; this.Top += e.Y - lastPoint.Y; }
        }

        private void Form3_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void gridview_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            //         

        }

        playerVidioSer PlayerVideo;
        private void btn_playVideos_Click(object sender, EventArgs e)
        {
            if (PlayerVideo == null)
            {
                // this.Hide();
                PlayerVideo = new playerVidioSer();
                PlayerVideo.FormClosed += new FormClosedEventHandler(PlayerVideo_FormClosed);
                PlayerVideo.ShowDialog();


            }
            else
            {
                PlayerVideo.Activate();
            }
        }

        void PlayerVideo_FormClosed(object sender, FormClosedEventArgs e)
        {
            PlayerVideo = null;
            //throw new NotImplementedException();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            int a = 0;
            int b = 0;
            int c = 0;

            List<DataGridViewRow> selectedRows = (from row in gridview.Rows.Cast<DataGridViewRow>()
                                                  where Convert.ToBoolean(row.Cells["checkBoxColumn"].Value) == true
                                                  select row).ToList();
            if (MessageBox.Show(string.Format("Do you want to delete {0} rows?", selectedRows.Count), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in selectedRows)
                {
                    object VName = row.Cells["Name"].Value;
                    string rslt = chkk.deletfileWith_record(VName.ToString());
                    if (rslt == "1")
                    {
                        a += 1;
                        // number of success deltetion

                    }
                    else if (rslt == "0")
                    {
                        b += 1;
                        // number of fail  deltetion
                    }
                    else if (rslt == "not found")
                    {
                        // number of file not found
                        c += 1;
                    }
                    // MessageBox.Show(anc.ToString());
                    //string str="delete from dta where name=",;
                }

                if (selectedRows.Count == a)
                {
                    MessageBox.Show(a + " Total file Deleted successfully");
                }
                else
                {
                    MessageBox.Show(a + "Total file deleted, " + b + "Unable to deleted, " + c + "File Not Found");
                }

                if (combo2.SelectedIndex == 0)
                {
                    string val = combo2.SelectedValue.ToString();
                    if (val != "System.Data.DataRowView")
                    {
                        binddata(val);
                    }
                }
                else if (combo2.SelectedIndex > 0)
                {
                    string val = combo2.SelectedValue.ToString();
                    binddata(val);

                }
                // this.binddata(combo);
            }


        }

        private void btn_DeleteVal_Click(object sender, EventArgs e)
        {
            int a = 0;
            int b = 0;
            int c = 0;

            List<DataGridViewRow> selectedRows = (from row in gridview.Rows.Cast<DataGridViewRow>()
                                                  where Convert.ToBoolean(row.Cells["checkBoxColumn"].Value) == true
                                                  select row).ToList();
            if (selectedRows.Count == 0)
            {
                MessageBox.Show("Please Select At Least One Record");
            }
            else
            {
                if (MessageBox.Show(string.Format("Do you want to delete {0} rows?", selectedRows.Count), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in selectedRows)
                    {
                        object VName = row.Cells["Name"].Value;
                        string rslt = chkk.deletfileWith_record(VName.ToString());
                        if (rslt == "1")
                        {
                            a += 1;
                            // number of success deltetion

                        }
                        else if (rslt == "0")
                        {
                            b += 1;
                            // number of fail  deltetion
                        }
                        else if (rslt == "not found")
                        {
                            // number of file not found
                            c += 1;
                        }
                        // MessageBox.Show(anc.ToString());
                        //string str="delete from dta where name=",;
                    }

                    if (selectedRows.Count == a)
                    {
                        MessageBox.Show(a + " Total file Deleted successfully");
                    }
                    else
                    {
                        MessageBox.Show(a + "Total file deleted, " + b + "Unable to deleted, " + c + "File Not Found");
                    }

                    if (combo2.SelectedIndex == 0)
                    {
                        string val = combo2.SelectedValue.ToString();
                        if (val != "System.Data.DataRowView")
                        {
                            binddata(val);
                        }
                    }
                    else if (combo2.SelectedIndex > 0)
                    {
                        string val = combo2.SelectedValue.ToString();
                        binddata(val);

                    }
                    // this.binddata(combo);
                }

            }

        }


    }
}



