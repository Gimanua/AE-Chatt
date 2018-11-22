namespace AE_Chatt
{
    partial class ChatForm
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
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem("Göran");
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem("Pelle");
            this.listViewOthers = new System.Windows.Forms.ListView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControlConversations = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tabControlConversations.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewOthers
            // 
            this.listViewOthers.CheckBoxes = true;
            listViewItem13.StateImageIndex = 0;
            listViewItem14.StateImageIndex = 0;
            this.listViewOthers.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem13,
            listViewItem14});
            this.listViewOthers.Location = new System.Drawing.Point(12, 58);
            this.listViewOthers.MultiSelect = false;
            this.listViewOthers.Name = "listViewOthers";
            this.listViewOthers.Size = new System.Drawing.Size(121, 380);
            this.listViewOthers.TabIndex = 1;
            this.listViewOthers.UseCompatibleStateImageBehavior = false;
            this.listViewOthers.View = System.Windows.Forms.View.List;
            this.listViewOthers.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listView1_ItemChecked_1);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(121, 40);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "Server Status";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tabControlConversations
            // 
            this.tabControlConversations.Controls.Add(this.tabPage1);
            this.tabControlConversations.Location = new System.Drawing.Point(139, 12);
            this.tabControlConversations.Name = "tabControlConversations";
            this.tabControlConversations.SelectedIndex = 0;
            this.tabControlConversations.Size = new System.Drawing.Size(649, 346);
            this.tabControlConversations.TabIndex = 4;
            this.tabControlConversations.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControlConversations_Selecting);
            this.tabControlConversations.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControlConversations_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(641, 320);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "test";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(3, 3);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(635, 314);
            this.textBox2.TabIndex = 0;
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControlConversations);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listViewOthers);
            this.Name = "ChatForm";
            this.Text = "ChatForm";
            this.tabControlConversations.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView listViewOthers;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabControl tabControlConversations;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox textBox2;
    }
}