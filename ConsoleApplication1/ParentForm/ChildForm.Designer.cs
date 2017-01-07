namespace ParentForm
{
    partial class ChildForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.ChildTextBox = new System.Windows.Forms.RichTextBox();
            this.ChildWindowStrip = new System.Windows.Forms.MenuStrip();
            this.FormatMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToggleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChildWindowStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChildTextBox
            // 
            this.ChildTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChildTextBox.Location = new System.Drawing.Point(0, 48);
            this.ChildTextBox.Name = "ChildTextBox";
            this.ChildTextBox.Size = new System.Drawing.Size(404, 233);
            this.ChildTextBox.TabIndex = 1;
            this.ChildTextBox.Text = "";
            this.ChildTextBox.TextChanged += new System.EventHandler(this.ChildTextBox_TextChanged);
            // 
            // ChildWindowStrip
            // 
            this.ChildWindowStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FormatMenuItem});
            this.ChildWindowStrip.Location = new System.Drawing.Point(0, 24);
            this.ChildWindowStrip.Name = "ChildWindowStrip";
            this.ChildWindowStrip.Size = new System.Drawing.Size(404, 24);
            this.ChildWindowStrip.TabIndex = 2;
            this.ChildWindowStrip.Text = "menuStrip2";
            // 
            // FormatMenuItem
            // 
            this.FormatMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToggleMenuItem});
            this.FormatMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.FormatMenuItem.Name = "FormatMenuItem";
            this.FormatMenuItem.Size = new System.Drawing.Size(57, 20);
            this.FormatMenuItem.Text = "F&ormat";
            // 
            // ToggleMenuItem
            // 
            this.ToggleMenuItem.Name = "ToggleMenuItem";
            this.ToggleMenuItem.Size = new System.Drawing.Size(173, 22);
            this.ToggleMenuItem.Text = "&ToggleForeground";
            this.ToggleMenuItem.Click += this.ToggleMenuItem_Click;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(404, 281);
            this.Controls.Add(this.ChildTextBox);
            this.Controls.Add(this.ChildWindowStrip);
            this.Name = "Form1";
            this.Controls.SetChildIndex(this.ChildWindowStrip, 0);
            this.Controls.SetChildIndex(this.ChildTextBox, 0);
            this.ChildWindowStrip.ResumeLayout(false);
            this.ChildWindowStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox ChildTextBox;
        private System.Windows.Forms.MenuStrip ChildWindowStrip;
        private System.Windows.Forms.ToolStripMenuItem FormatMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToggleMenuItem;
    }
}
