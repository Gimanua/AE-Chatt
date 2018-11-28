namespace AE_Chatt
{
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
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.labelUserName = new System.Windows.Forms.Label();
            this.textBoxPassWord = new System.Windows.Forms.TextBox();
            this.labelPassWord = new System.Windows.Forms.Label();
            this.buttonLogIn = new System.Windows.Forms.Button();
            this.buttonRegister = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxUserName.Location = new System.Drawing.Point(3, 76);
            this.textBoxUserName.MaxLength = 64;
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(173, 20);
            this.textBoxUserName.TabIndex = 0;
            this.textBoxUserName.TextChanged += new System.EventHandler(this.textBoxUserName_TextChanged);
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUserName.Location = new System.Drawing.Point(3, 48);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(173, 25);
            this.labelUserName.TabIndex = 1;
            this.labelUserName.Text = "Användarnamn";
            this.labelUserName.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.labelUserName.Click += new System.EventHandler(this.labelUserName_Click);
            // 
            // textBoxPassWord
            // 
            this.textBoxPassWord.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxPassWord.Location = new System.Drawing.Point(3, 222);
            this.textBoxPassWord.MaxLength = 64;
            this.textBoxPassWord.Name = "textBoxPassWord";
            this.textBoxPassWord.Size = new System.Drawing.Size(173, 20);
            this.textBoxPassWord.TabIndex = 1;
            this.textBoxPassWord.UseSystemPasswordChar = true;
            this.textBoxPassWord.TextChanged += new System.EventHandler(this.textBoxPassWord_TextChanged);
            // 
            // labelPassWord
            // 
            this.labelPassWord.AutoSize = true;
            this.labelPassWord.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelPassWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPassWord.Location = new System.Drawing.Point(3, 194);
            this.labelPassWord.Name = "labelPassWord";
            this.labelPassWord.Size = new System.Drawing.Size(173, 25);
            this.labelPassWord.TabIndex = 3;
            this.labelPassWord.Text = "Lösenord";
            this.labelPassWord.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.labelPassWord.Click += new System.EventHandler(this.labelPassWord_Click);
            // 
            // buttonLogIn
            // 
            this.buttonLogIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonLogIn.Location = new System.Drawing.Point(182, 76);
            this.buttonLogIn.Name = "buttonLogIn";
            this.buttonLogIn.Size = new System.Drawing.Size(174, 67);
            this.buttonLogIn.TabIndex = 2;
            this.buttonLogIn.Text = "Logga In";
            this.buttonLogIn.UseVisualStyleBackColor = true;
            // 
            // buttonRegister
            // 
            this.buttonRegister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonRegister.Location = new System.Drawing.Point(182, 222);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(174, 67);
            this.buttonRegister.TabIndex = 3;
            this.buttonRegister.Text = "Registrera";
            this.buttonRegister.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.buttonRegister, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelUserName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonLogIn, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxUserName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelPassWord, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxPassWord, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(359, 292);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 292);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "LoginForm";
            this.Text = "Login";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.TextBox textBoxPassWord;
        private System.Windows.Forms.Label labelPassWord;
        private System.Windows.Forms.Button buttonRegister;
        private System.Windows.Forms.Button buttonLogIn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

