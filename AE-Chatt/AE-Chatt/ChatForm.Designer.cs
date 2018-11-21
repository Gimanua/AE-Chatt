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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Göran");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Pelle");
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.listViewOthers = new System.Windows.Forms.ListView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControlConversations = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControlConversations.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Location = new System.Drawing.Point(139, 364);
            this.textBoxMessage.Multiline = true;
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.Size = new System.Drawing.Size(649, 74);
            this.textBoxMessage.TabIndex = 0;
            // 
            // listViewOthers
            // 
            this.listViewOthers.CheckBoxes = true;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            this.listViewOthers.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.listViewOthers.Location = new System.Drawing.Point(12, 58);
            this.listViewOthers.MultiSelect = false;
            this.listViewOthers.Name = "listViewOthers";
            this.listViewOthers.Size = new System.Drawing.Size(121, 380);
            this.listViewOthers.TabIndex = 1;
            this.listViewOthers.UseCompatibleStateImageBehavior = false;
            this.listViewOthers.View = System.Windows.Forms.View.List;
            this.listViewOthers.ItemActivate += new System.EventHandler(this.listView1_ItemActivate);
            this.listViewOthers.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listView1_ItemChecked_1);
            this.listViewOthers.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewOthers_ItemSelectionChanged);
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
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(641, 320);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Conversation";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControlConversations);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listViewOthers);
            this.Controls.Add(this.textBoxMessage);
            this.Name = "ChatForm";
            this.Text = "ChatForm";
            this.tabControlConversations.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.ListView listViewOthers;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabControl tabControlConversations;
        private System.Windows.Forms.TabPage tabPage1;
    }
}