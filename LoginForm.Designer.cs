namespace Net4Sage
{
    /// <summary>
    /// The Login Form
    /// </summary>
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.fraMain = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.fraServer = new System.Windows.Forms.GroupBox();
            this.cbxCompany = new System.Windows.Forms.ComboBox();
            this.companyBS = new System.Windows.Forms.BindingSource(this.components);
            this.lblCompany = new System.Windows.Forms.Label();
            this.cbxDatabase = new System.Windows.Forms.ComboBox();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.fraUser = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.fraMain.SuspendLayout();
            this.fraServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.companyBS)).BeginInit();
            this.fraUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // fraMain
            // 
            this.fraMain.Controls.Add(this.btnConnect);
            this.fraMain.Controls.Add(this.fraServer);
            this.fraMain.Controls.Add(this.fraUser);
            this.fraMain.Location = new System.Drawing.Point(12, 12);
            this.fraMain.Name = "fraMain";
            this.fraMain.Size = new System.Drawing.Size(261, 356);
            this.fraMain.TabIndex = 0;
            this.fraMain.TabStop = false;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(72, 322);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(105, 23);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.On_Connect);
            // 
            // fraServer
            // 
            this.fraServer.Controls.Add(this.cbxCompany);
            this.fraServer.Controls.Add(this.lblCompany);
            this.fraServer.Controls.Add(this.cbxDatabase);
            this.fraServer.Controls.Add(this.lblDatabase);
            this.fraServer.Controls.Add(this.txtServer);
            this.fraServer.Controls.Add(this.lblServer);
            this.fraServer.Location = new System.Drawing.Point(7, 152);
            this.fraServer.Name = "fraServer";
            this.fraServer.Size = new System.Drawing.Size(242, 164);
            this.fraServer.TabIndex = 1;
            this.fraServer.TabStop = false;
            this.fraServer.Text = "Server";
            // 
            // cbxCompany
            // 
            this.cbxCompany.DataSource = this.companyBS;
            this.cbxCompany.DisplayMember = "CompanyID";
            this.cbxCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCompany.FormattingEnabled = true;
            this.cbxCompany.Location = new System.Drawing.Point(10, 126);
            this.cbxCompany.Name = "cbxCompany";
            this.cbxCompany.Size = new System.Drawing.Size(219, 21);
            this.cbxCompany.TabIndex = 5;
            this.cbxCompany.ValueMember = "CompanyID";
            // 
            // companyBS
            // 
            this.companyBS.AllowNew = false;
            this.companyBS.DataSource = typeof(Net4Sage.CompanyPairsValues);
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Location = new System.Drawing.Point(10, 109);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(51, 13);
            this.lblCompany.TabIndex = 4;
            this.lblCompany.Text = "Company";
            // 
            // cbxDatabase
            // 
            this.cbxDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDatabase.FormattingEnabled = true;
            this.cbxDatabase.Location = new System.Drawing.Point(10, 81);
            this.cbxDatabase.Name = "cbxDatabase";
            this.cbxDatabase.Size = new System.Drawing.Size(219, 21);
            this.cbxDatabase.TabIndex = 3;
            this.cbxDatabase.SelectedIndexChanged += new System.EventHandler(this.On_Database_Change);
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Location = new System.Drawing.Point(7, 65);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(53, 13);
            this.lblDatabase.TabIndex = 2;
            this.lblDatabase.Text = "Database";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(10, 37);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(219, 20);
            this.txtServer.TabIndex = 1;
            this.txtServer.Leave += new System.EventHandler(this.On_Control_Leave);
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(7, 21);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(79, 13);
            this.lblServer.TabIndex = 0;
            this.lblServer.Text = "Server Address";
            // 
            // fraUser
            // 
            this.fraUser.Controls.Add(this.txtPassword);
            this.fraUser.Controls.Add(this.lblPassword);
            this.fraUser.Controls.Add(this.txtUsername);
            this.fraUser.Controls.Add(this.lblUsername);
            this.fraUser.Location = new System.Drawing.Point(7, 20);
            this.fraUser.Name = "fraUser";
            this.fraUser.Size = new System.Drawing.Size(242, 125);
            this.fraUser.TabIndex = 0;
            this.fraUser.TabStop = false;
            this.fraUser.Text = "User Login";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(13, 81);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(216, 20);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.Leave += new System.EventHandler(this.On_Control_Leave);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(13, 64);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(13, 37);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(216, 20);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.Leave += new System.EventHandler(this.On_Control_Leave);
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(10, 20);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(55, 13);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Username";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 380);
            this.Controls.Add(this.fraMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login to the Server";
            this.Load += new System.EventHandler(this.Form_Load);
            this.fraMain.ResumeLayout(false);
            this.fraServer.ResumeLayout(false);
            this.fraServer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.companyBS)).EndInit();
            this.fraUser.ResumeLayout(false);
            this.fraUser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox fraMain;
        private System.Windows.Forms.GroupBox fraServer;
        private System.Windows.Forms.GroupBox fraUser;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.BindingSource companyBS;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.ComboBox cbxDatabase;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.ComboBox cbxCompany;
    }
}