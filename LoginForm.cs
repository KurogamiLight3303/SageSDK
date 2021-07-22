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
using System.IO;
using System.Xml;

namespace Net4Sage
{
    public partial class LoginForm : Form
    {
        private SageSession _session;
        private SqlConnection DBObj;
        /// <summary>
        /// Create Login Form
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Create Login Form
        /// </summary>
        /// <param name="sageSession">Sage Session</param>
        public LoginForm(SageSession sageSession) : this()
        {
            _session = sageSession;
        }
        private void Load_Databases()
        {
            if (txtUsername.Text.Length > 0 && txtServer.Text.Length > 0 && ((txtUsername.Tag == null || txtUsername.Text != txtUsername.Tag.ToString()) || (txtServer.Tag == null || txtServer.Text != txtServer.Tag.ToString())))
            {
                try
                {

                    txtUsername.Tag = txtUsername.Text;
                    txtPassword.Tag = txtPassword.Text;
                    txtServer.Tag = txtServer.Text;
                    DBObj = new SqlConnection(new SqlConnectionStringBuilder()
                    {
                        DataSource = txtServer.Text,
                        UserID = txtUsername.Text,
                        Password = txtPassword.Text,
                    }.ToString());
                    DBObj.Open();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Sage MAS 500", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                try
                {
                    if(DBObj != null && DBObj.State == ConnectionState.Open)
                    {
                        using (SqlCommand cmd = new SqlCommand("select name from sys.databases", DBObj))
                        {
                            cbxDatabase.Items.Clear();
                            SqlDataReader dt = cmd.ExecuteReader();
                            while (dt.Read())
                            {
                                cbxDatabase.Items.Add(dt.GetValue(0).ToString());
                            }
                        }
                    }
                }catch(Exception e)
                {
                    MessageBox.Show(e.Message, "Sage MAS 500", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
                cbxDatabase.Items.Clear();
        }
        private void On_Control_Leave(object sender, EventArgs e) => Load_Databases();
        private void On_Connect(object sender, EventArgs e)
        {
            _session?.InitializeSession(_session.TaskID, txtServer.Text, cbxDatabase.SelectedItem.ToString(), cbxCompany.SelectedValue.ToString(), txtUsername.Text, txtPassword.Text);
            Close();
        }
        private void On_Database_Change(object sender, EventArgs e)
        {
            if (cbxDatabase.SelectedIndex > -1 && (cbxDatabase.Tag == null || cbxDatabase.Tag.ToString() != cbxDatabase.SelectedItem.ToString()))
            {
                try
                {
                    DBObj = new SqlConnection(new SqlConnectionStringBuilder()
                    {
                        DataSource = txtServer.Text,
                        UserID = txtUsername.Text,
                        Password = txtPassword.Text,
                        InitialCatalog = cbxDatabase.SelectedItem.ToString()
                    }.ToString());
                    DBObj.Open();

                    SqlCommand cmd = new SqlCommand("select 1 from INFORMATION_SCHEMA.TABLES where TABLE_TYPE = 'BASE TABLE' and TABLE_NAME = 'tsmSiteProfile'", DBObj);
                    SqlDataReader dt = cmd.ExecuteReader();
                    if (dt.Read())
                    {
                        dt.Close();
                        cmd = new SqlCommand("select 1 from tsmSiteProfile", DBObj);
                        dt = cmd.ExecuteReader();
                        if (dt.Read())
                        {
                            dt.Close();
                            cmd = new SqlCommand("select CompanyID, CompanyName from tsmCompany", DBObj);
                            dt = cmd.ExecuteReader();  
                            companyBS.Clear();
                            while (dt.Read())
                            {
                                companyBS.Add(new CompanyPairsValues()
                                {
                                    CompanyID = dt.GetValue(0).ToString(),
                                    CompanyName = dt.GetValue(1).ToString(),
                                });
                            }
                        }
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "Sage MAS 500", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void Form_Load(object sender, EventArgs e)
        {
            string src = Environment.ExpandEnvironmentVariables(@"%ProgramData%\Sage Software\Sage MAS 500\");
            if (!Directory.Exists(src))
                Directory.CreateDirectory(src);

            if(File.Exists(src + "\\Application.Config"))
            {
                StreamReader fReader = null;
                XmlReader reader = null;
                try
                {
                    fReader = new StreamReader(new FileStream(src + "\\Application.Config", FileMode.Open, FileAccess.Read));
                    //Set the XML settings
                    System.Xml.XmlReaderSettings settings = new System.Xml.XmlReaderSettings()
                    {
                        ValidationFlags = System.Xml.Schema.XmlSchemaValidationFlags.None,
                    };
                    //Create the XML reader object for the connection string 
                    reader = XmlReader.Create(new StringReader(fReader.ReadToEnd()));
                    //Read the XML 
                    while (reader.Read())
                    {
                        switch (reader.Name)
                        {
                            case "SQLServerName":
                                if(reader.Read())
                                    if(reader.Value.Trim().Length > 0)
                                        txtServer.Text = reader.Value;
                                break;
                        }
                    }
                    txtUsername.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1];
                }
                catch(Exception)
                {
                }
                finally
                {
                    if (reader != null)
                        reader.Close();
                    if (fReader != null)
                        fReader.Close();
                }
                
            }
        }
    }
}
