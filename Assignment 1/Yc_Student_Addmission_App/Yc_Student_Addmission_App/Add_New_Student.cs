using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Yc_Student_Addmission_App
{
    public partial class frm_Add_New_Student : Form
    {
        public frm_Add_New_Student()
        {
            InitializeComponent();
        }

        SqlConnection DBCon = new SqlConnection(@"Data Source=DESKTOP-AHT8DKD;Initial Catalog=Yc_Addmission_App_DB;Integrated Security=True");
        void S_Con_Open()
        {
            if(DBCon.State != ConnectionState.Open)
            {
                DBCon.Open();
            }
        }

        void S_Con_Close()
        {
            if(DBCon.State != ConnectionState.Closed)
            {
                DBCon.Close();
            }
        }

        private void Only_Numeric(object sender, KeyPressEventArgs e)
        {
            if(!(char.IsDigit(e.KeyChar)||(e.KeyChar==(char)Keys.Back)))
            {
                e.Handled = true;
            }
          
        }

        private void Only_Text(object sender, KeyPressEventArgs e)
        {
            if(!(char.IsLetter(e.KeyChar)||(e.KeyChar==(char)Keys.Back)||(e.KeyChar==(char)Keys.Space)||(e.KeyChar==(char)Keys.ShiftKey)||(e.KeyChar==(char)Keys.CapsLock)))
            {
                e.Handled = true;
            }
        }


        void Clear_Controls()
        {
            tb_Roll_No.Clear();
            tb_Name.Clear();
            dtp_Date_Of_Birth.ResetText();
            tb_Mobile_No.Clear();
            cmb_Course.SelectedIndex = -1;
        }
        private void 
            btn_Refresh_Click(object sender, EventArgs e)
        {
            Clear_Controls();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            S_Con_Open();

            if(tb_Roll_No.Text != "" && tb_Name.Text != "" && tb_Mobile_No.Text != "" && cmb_Course.Text != "")
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = DBCon;
                cmd.CommandText = "Insert Into Student_Details Values (@Rno, @Nm,@Dob , @Mob, @course)";

                cmd.Parameters.Add("Rno", SqlDbType.Int).Value = tb_Roll_No.Text;
                cmd.Parameters.Add("Nm", SqlDbType.VarChar).Value = tb_Name.Text;
                cmd.Parameters.Add("Dob", SqlDbType.Date).Value = dtp_Date_Of_Birth.Text;
                cmd.Parameters.Add("Mob", SqlDbType.Decimal).Value = tb_Mobile_No.Text;
                cmd.Parameters.Add("course", SqlDbType.NVarChar).Value = cmb_Course.Text;

                cmd.ExecuteNonQuery();

                MessageBox.Show("Information Saved Successfully","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);

                Clear_Controls();

             }

            else
            {
                MessageBox.Show("First Fill All Fields", "Incomplete Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            S_Con_Close();
        }

        private void btn_List_Of_Students_Click(object sender, EventArgs e)
        {
            frm_List_Of_Students obj = new frm_List_Of_Students();
            obj.Show();
            this.Hide();
        }

        private void btn_Log_Out_Click(object sender, EventArgs e)
        {
            frm_Login_Form obj = new frm_Login_Form();
            obj.Show();
            this.Hide();
        }

       
    }
}
