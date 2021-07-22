using CrystalDecisions.CrystalReports.Engine;
using Net4Sage.Controls.DropDown;
using Net4Sage.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Objects;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Net4Sage
{
    public class SageSession : Component
    {
        #region "User Data"
        public string UserID { get; private set; }
        private string Password { get; set; }
        private bool EncryptPassword { get; set; }
        public DateTime BusinessDate { get; private set; }
        private bool TrustedConnection { get; set; }
        #endregion

        #region "Server Data"
        public string ServerName { get; private set; }
        public string DBName { get; private set; }
        public string CompanyID { get; private set; }
        #endregion

        #region "Task Data"
        public int TaskID { get; private set; }
        public TaskPermision TaskPerm { get; private set; }
        public Dictionary<string, string> Parameters { get; set; }
        #endregion

        #region "Session Data"
        public SessionStates State
        {
            get
            {
                if (this.dbObj == null || TaskPerm == TaskPermision.None) return SessionStates.Invalid;
                if (this.dbObj.State == System.Data.ConnectionState.Closed) return SessionStates.Disconnected;
                return SessionStates.Connected;
            }
        }
        private SqlConnection dbObj;
        #endregion

        #region "CompanyData"
        public string HomeCurrID { get; private set; }
        public string HomeCountryID { get; private set; }
        public Version ServerVersion { get; private set; }
        #endregion

        #region "Constructors"
        public SageSession() : base()
        {
            Parameters = new Dictionary<string, string>();
        }
        public SageSession(string[] Context) : this(string.Join(" ", Context))
        {
        }
        public SageSession(string Context) : this()
        {
            Context = Context.Replace("Value=", "value = \"");
            Context = Context.Replace("/>", "\"/>");
            Context = Context.Replace("Value", " value ").Replace("value", " value ");

            InitializeSession(Context);
        }
        #endregion

        #region "Initializers"
        /// <summary>
        /// Show the login form to initialize the Sage Session
        /// </summary>
        public void ShowLogin(int taskID)
        {
            this.TaskID = taskID;
            using (LoginForm frm = new LoginForm(this))
            {
                frm.ShowDialog();
                frm.Close();
            }
        }
        /// <summary>
        /// Initialize the Session using a string context with the Sage Session Data
        /// </summary>
        /// <param name="Context">string context with the Sage Session Data</param>
        public void InitializeSession(string Context)
        {
            try
            {
                //Set the XML settings
                System.Xml.XmlReaderSettings settings = new System.Xml.XmlReaderSettings()
                {
                    ValidationFlags = System.Xml.Schema.XmlSchemaValidationFlags.None,
                };
                //Create the XML reader object for the connection string 
                System.Xml.XmlReader reader = System.Xml.XmlReader.Create(new StringReader(Context));
                //Read the XML looking form item with the user data
                while (reader.Read())
                {
                    switch (reader.Name)
                    {
                        case "UserID":
                            UserID = reader.HasAttributes ? reader.GetAttribute(0) : "";
                            break;
                        case "Password":
                            Password = reader.HasAttributes ? reader.GetAttribute(0) : "";
                            break;
                        case "TrustedConnection":
                            TrustedConnection = reader.HasAttributes ? reader.GetAttribute(0).TrimEnd().ToLower() == "true" ? true : false : false;
                            break;
                        case "EncryptPassword":
                            EncryptPassword = reader.HasAttributes ? reader.GetAttribute(0).TrimEnd().ToLower() == "true" ? true : false : false;
                            break;
                        case "SQLServerName":
                            ServerName = reader.HasAttributes ? reader.GetAttribute(0) : "";
                            break;
                        case "DBName":
                            DBName = reader.HasAttributes ? reader.GetAttribute(0) : "";
                            break;
                        case "CompanyID":
                            CompanyID = reader.HasAttributes ? reader.GetAttribute(0).TrimEnd() : "";
                            break;
                        case "TaskID":
                            TaskID = int.Parse(reader.HasAttributes ? reader.GetAttribute(0).TrimEnd() : "0");
                            break;
                        default:
                            if (reader.HasAttributes)
                                Parameters.Add(reader.Name, reader.GetAttribute(0).TrimEnd());
                            break;
                    }
                }
                //Close the XML reader
                reader.Close();

            }
            catch (Exception e)
            {
                //In case of parsing error send a message with the error data
                MessageBox.Show("Error de carga del contexto: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Check if the password of the user is encrypted
            if (EncryptPassword)
                //Decrypt the user password
                Password = DecryptPassword(Encoding.ASCII.GetString(Convert.FromBase64String(Password)));

            InitializeSession();
        }
        /// <summary>
        /// Initialize the Session based on another Sage Session
        /// </summary>
        /// <param name="session">Source Sage Session</param>
        public void InitializeSession(SageSession session)
        {
            UserID = session.UserID;
            EncryptPassword = session.EncryptPassword;
            Password = session.Password;
            ServerName = session.ServerName;
            DBName = session.DBName;
            CompanyID = session.CompanyID;
            TaskID = session.TaskID;
            Parameters = session.Parameters;
            InitializeSession();
        }
        /// <summary>
        /// Initialize the Session 
        /// </summary>
        /// <param name="taskID">ID of the Current Task</param>
        /// <param name="serverName">Server Name or Address</param>
        /// <param name="dbName">Database where the Server is Hosted</param>
        /// <param name="companyID">ID of the Current Company</param>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        public void InitializeSession(int taskID, string serverName, string dbName, string companyID, string username, string password)
        {
            UserID = username;
            EncryptPassword = false;
            Password = password;
            ServerName = serverName;
            DBName = dbName;
            CompanyID = companyID;
            TaskID = taskID;
            Parameters = new Dictionary<string, string>();
            InitializeSession();
        }
        /// <summary>
        /// Initialize the Session based on another Sage Session
        /// </summary>
        /// <param name="taskID">ID of the Current Task</param>
        /// <param name="session">Source Sage Session</param>
        public void InitializeSession(int taskID, ref SageSession session)
        {
            UserID = session.UserID;
            EncryptPassword = session.EncryptPassword;
            Password = session.Password;
            ServerName = session.ServerName;
            DBName = session.DBName;
            CompanyID = session.CompanyID;
            TaskID = taskID;
            Parameters = session.Parameters;
            InitializeSession();
        }
        /// <summary>
        /// Initialize the Session based on another Sage Session
        /// </summary>
        /// <param name="binaryID">Name of the Binary for the TaskID</param>
        /// <param name="session">Source Sage Session</param>
        public void InitializeSession(string binaryID, ref SageSession session)
        {
            try
            {
                int id = int.Parse(session.Lookup("TaskID", "tsmTask", "TaskInterfaceType = 5 and TaskOleProgID like " + QuoteString("%" + binaryID)).ToString());
                InitializeSession(id, ref session);
            }
            catch (Exception exc)
            {
                throw new Exception("Not able to initialize", exc);
            }
        }
        /// <summary>
        /// Initialization Base
        /// </summary>
        private void InitializeSession()
        {
            try
            {
                //Create a connection to the database to check if the user is valid
                dbObj = new SqlConnection(GetConnectionString());
                dbObj.Open();
                //Get the data for the Sage Session
                LoadData();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        #endregion

        #region "Public Interfaces for Developers"
        public static string QuoteString(string s)
        {
            return '\'' + s + '\'';
        }
        public string GetConnectionString()
        {
            return new SqlConnectionStringBuilder()
            {
                DataSource = ServerName,
                InitialCatalog = DBName,
                UserID = UserID,
                Password = Password,
                IntegratedSecurity = TrustedConnection
            }.ToString();
        }
        public SqlConnection GetConnection()
        {
            return this.dbObj;
        }
        public void LogonSource(Table source)
        {
            source.LogOnInfo.ConnectionInfo.UserID = this.UserID;
            source.LogOnInfo.ConnectionInfo.Password = this.Password;
            source.ApplyLogOnInfo(source.LogOnInfo);
        }
        public void WriteLog(string Message)
        {
            string src = Environment.ExpandEnvironmentVariables(@"%ProgramData%\Sage Software\Sage MAS 500\Logs");
            if (!Directory.Exists(src))
                Directory.CreateDirectory(src);
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(new FileStream(src + "\\" + TaskID + ".log", FileMode.OpenOrCreate, FileAccess.Write));
                if (writer.BaseStream.Length > 0)
                    writer.BaseStream.Seek(writer.BaseStream.Length - 1, SeekOrigin.Begin);
                writer.WriteLine(DateTime.Now.ToString() + " - " + Message);
                writer.Flush();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message, "Sage MAS 500", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            };
        }
        public void WriteLog(Exception exception)
        {
            WriteLog(exception, "");
        }
        public void WriteLog(Exception exception, string Message = "")
        {
            Exception temp = exception;
            List<string> messages = new List<string>();
            try
            {
                do
                {
                    messages.Add(temp.Message);
                    temp = temp.InnerException;
                } while (temp != null);
            }
            catch
            {
                messages.Add(exception.Message);
            }
            WriteLog((Message.Length > 0 ? Message + " - " : "") + string.Join("\n -", messages));
        }
        public T CreateDBContext<T>(string datamodelName = "DataModel") where T : ObjectContext
        {
            System.Data.EntityClient.EntityConnectionStringBuilder connectionString = new System.Data.EntityClient.EntityConnectionStringBuilder()
            {
                Metadata = "res://*/" + datamodelName + ".csdl|res://*/" + datamodelName + ".ssdl|res://*/" + datamodelName + ".msl",
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = this.GetConnectionString()
            };
            object[] parameters = new object[1];
            parameters[0] = connectionString.ToString();
            return (T)Activator.CreateInstance(typeof(T), parameters);
        }

        public void ExecuteSP(string spName, IEnumerable<SpParameter> parameters = null)
        {
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(spName, GetConnection())
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                if(parameters != null)
                foreach (SpParameter i in parameters)
                    cmd.Parameters.Add(new SqlParameter(i.Name, i.Value)
                    {
                        DbType = i.ParameterType,
                        Direction = i.Direction,
                    });
                cmd.ExecuteNonQuery();
            }
            catch(Exception exc)
            {
                WriteLog(exc);
                throw;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }
        }
        #endregion

        #region "Data Lookup"
        private void SetPropertyValue(PropertyInfo property, object target, object value)
        {
            Type type = value.GetType();
            if (property.PropertyType == typeof(bool) && type == typeof(short))
                property.SetValue(target, (short)value != 0);
            else if (property.PropertyType == typeof(string) && type != typeof(string))
                property.SetValue(target, value.ToString());
            else
                property.SetValue(target, value);
        }
        public object Lookup(string field, string table, string where = "")
        {
            return Lookup<object>(field, table, where);
        }
        public T Lookup<T>(string field, string table, string where = "")
        {
            return Lookup<T>("select " + field + " from " + table + (where.Length > 0 ? " where " + where : where));
        }
        public T Lookup<T>(string query)
        {
            return LookupList<T>(query).FirstOrDefault();
        }
        public IEnumerable<T> LookupList<T>(string field, string table, string where = "")
        {
            return LookupList<T>("select " + field + " from " + table + (where != null && where.Length > 0 ? " where " + where : where));
        }
        public IEnumerable<T> LookupList<T>(string query)
        {
            T temp;
            SqlConnection connection = new SqlConnection(GetConnectionString());
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader();
            }
            catch (Exception exc)
            {
                WriteLog(exc, "Executing Query: " + query);
            }
            if(reader != null && reader.HasRows)
            {
                while (reader.Read())
                {
                    try
                    {
                        temp = reader.GetFieldValue<T>(0);
                    }
                    catch (Exception exc)
                    {
                        WriteLog(exc, "Executing Query: " + query);
                        break;
                    }
                    yield return temp;
                }
            }
            if (reader != null && !reader.IsClosed)
                reader.Close();
            cmd.Dispose();
            connection.Close();
        }
        public T LookupObject<T>(string table, string where) where T : class
        {
            return LookupObject<T>("*", table, where);
        }
        public T LookupObject<T>(string fieldList, string table, string where) where T : class
        {
            return LookupObject<T>("select " + fieldList + " from " + table + (where != null && where.Length > 0 ? " where " + where : where));
        }
        public T LookupObject<T>(string query) where T : class
        {
            return LookupObjectList<T>(query).Last();
        }
        public IEnumerable<T> LookupObjectList<T>(string table, string where) where T : class
        {
            return LookupObjectList<T>("*", table, where);
        }
        public IEnumerable<T> LookupObjectList<T>(string fieldList, string table, string where) where T : class
        {
            return LookupObjectList<T>("select " + fieldList + " from " + table + (where != null && where.Length > 0 ? " where " + where : where));
        }
        public IEnumerable<T> LookupObjectList<T>(string query) where T : class
        {
            PropertyInfo property = null;
            IEnumerable<PropertyInfo> properties;
            string fieldName;
            SqlConnection connection = new SqlConnection(GetConnectionString());
            connection.Open();
            T temp;
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader();
            }
            catch (Exception exc)
            {
                WriteLog(exc, "Executing Query: " + query);
            }

            if (reader != null && reader.HasRows)
            {
                while (reader.Read())
                {
                    temp = null;
                    try
                    {
                        temp = (T)Activator.CreateInstance(typeof(T));
                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            fieldName = reader.GetName(i);
                            properties = typeof(T).GetProperties().Where(p => p.GetCustomAttributes<ColumnAttribute>().Where(s => s.Name == fieldName).Any());
                            if (properties != null && properties.Any())
                            {
                                foreach (PropertyInfo prop in properties)
                                {
                                    try
                                    {
                                        SetPropertyValue(prop, temp, reader.GetValue(i));
                                    }
                                    catch (Exception exc)
                                    {
                                        WriteLog(exc, "Executing Query: " + query);
                                    }
                                }
                            }
                            else
                            {
                                property = typeof(T).GetProperty(fieldName);
                                try
                                {
                                    if (property != null)
                                        SetPropertyValue(property, temp, reader.GetValue(i));
                                }
                                catch (Exception exc)
                                {
                                    WriteLog(exc, "Executing Query: " + query);
                                }
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        WriteLog(exc, "Executing Query: " + query);
                        break;
                    }

                    if (temp != null)
                        yield return temp;
                }
            }
            if (reader != null && !reader.IsClosed)
                reader.Close();
            cmd.Dispose();
            connection.Close();
        }
        public IEnumerable<StaticListEntry> GetStaticValues(string FieldName, string TableName)
        {
            return LookupObjectList<StaticListEntry>("vListValidationString", "TableName=" + SageSession.QuoteString(TableName) + " and ColumnName =" + SageSession.QuoteString(FieldName));
        }
        #endregion

        #region "Internal Methods"
        private static string DecryptPassword(string password)
        {
            string answer = "";
            string str1 = "", str2 = "J!jY<sWG2KUX_vFA 9{?w8&N[la5b3@O~Qo*y)B(ThmZk$.6Ix0>t=C7;-dge4+zEqMc,RDipu`%|#1]^frS}n:PVLH\\/";

            if (password.Length > 0)
            {
                int startIndex1 = Convert.ToByte(password[0]);
                if (!(startIndex1 < 1 | startIndex1 > str2.Length))
                {
                    string str4 = str2.Substring(startIndex1) + str2.Substring(0, checked(startIndex1 - 1));
                    int num1 = 1;
                    int num2 = checked(password.Length - 1);
                    int startIndex2 = num1;
                    while (startIndex2 <= num2)
                    {
                        string str5 = password.Substring(startIndex2, 1);
                        int startIndex3 = str4.IndexOf(str5);
                        if (startIndex3 >= 0)
                            str1 = str1 + str2.Substring(startIndex3, 1);
                        checked { ++startIndex2; }
                    }
                    answer = str1;
                }
            }
            return answer;

        }
        private void LoadData()
        {
            SysUser user;
            SysTaskPerm taskPerm;
            SysCompany company;

            user = LookupObject<SysUser>("tsmUser", "UserID = " + QuoteString(UserID));
            company = LookupObject<SysCompany>("tsmCompany", "CompanyID = " + QuoteString(CompanyID));
            if (user == null)
            {
                MessageBox.Show("The user is not valid", "Sage MAS 500", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            BusinessDate = user.UseSystemDate || !user.BusinessDate.HasValue ? DateTime.Now : user.BusinessDate.Value;
            HomeCurrID = company.HomeCurrID;
            HomeCountryID = company.HomeCountryID;
            try
            {
                taskPerm = LookupObject<SysTaskPerm>("t.*", "tsmUser as p join tsmUserCompanyGrp as s on p.UserID = s.UserID join tsmTaskPerm as t on t.UserGroupID = s.UserGroupID",
                    " p.UserID = " + QuoteString(UserID) + " and TaskID = " + QuoteString(TaskID.ToString()) + " and s.CompanyID = " + QuoteString(CompanyID) + " order by t.Rights");
                this.TaskPerm = taskPerm != null ? taskPerm.SecurityLvl : TaskPermision.None;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Sage MAS 500", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.TaskPerm = TaskPermision.None;
            }

            if (TaskPerm == TaskPermision.None)
            {
                MessageBox.Show("You don't have access to this task", "Sage MAS 500", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ServerVersion = Version.Parse(Lookup<string>("VersionNo", "tsmModule"));
            OnSessionUpdate?.Invoke(this, null);
        }
        internal void LogonSource(ReportClass report)
        {
            string registry = "HKEY_CURRENT_USER\\SOFTWARE\\ODBC\\ODBC.INI\\MAS 500";

            Microsoft.Win32.Registry.SetValue(registry, "Server", this.ServerName.Trim());
            Microsoft.Win32.Registry.SetValue(registry, "Database", this.DBName.Trim());
        }
        protected override void Dispose(bool disposing)
        {
            if (dbObj != null && dbObj.State != System.Data.ConnectionState.Closed)
                try
                {
                    dbObj.Close();
                }
                catch (Exception)
                {

                }
            base.Dispose(disposing);

        }
        #endregion

        #region "Events"
        public event EventHandler OnSessionUpdate;
        #endregion
    }
}