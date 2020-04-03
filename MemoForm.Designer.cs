namespace calender
{
    partial class MemoForm
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
            this.memoSaveBtn = new System.Windows.Forms.Button();
            this.memoClearBtn = new System.Windows.Forms.Button();
            this.memoCloseBtn = new System.Windows.Forms.Button();
            this.memoDeleteBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.colorComBox = new System.Windows.Forms.ComboBox();
            this.contentTxtBox = new System.Windows.Forms.TextBox();
            this.contentLabel = new System.Windows.Forms.Label();
            this.titleTxtBox = new System.Windows.Forms.TextBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.colorLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // memoSaveBtn
            // 
            this.memoSaveBtn.CausesValidation = false;
            this.memoSaveBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.memoSaveBtn.Location = new System.Drawing.Point(3, 3);
            this.memoSaveBtn.Name = "memoSaveBtn";
            this.memoSaveBtn.Size = new System.Drawing.Size(119, 42);
            this.memoSaveBtn.TabIndex = 4;
            this.memoSaveBtn.TabStop = false;
            this.memoSaveBtn.Text = "Save";
            this.memoSaveBtn.UseVisualStyleBackColor = true;
            this.memoSaveBtn.Click += new System.EventHandler(this.memoSaveBtn_Click);
            // 
            // memoClearBtn
            // 
            this.memoClearBtn.Location = new System.Drawing.Point(128, 3);
            this.memoClearBtn.Name = "memoClearBtn";
            this.memoClearBtn.Size = new System.Drawing.Size(119, 42);
            this.memoClearBtn.TabIndex = 5;
            this.memoClearBtn.TabStop = false;
            this.memoClearBtn.Text = "Clear";
            this.memoClearBtn.UseVisualStyleBackColor = true;
            this.memoClearBtn.Click += new System.EventHandler(this.memoClearBtn_Click);
            // 
            // memoCloseBtn
            // 
            this.memoCloseBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.memoCloseBtn.Location = new System.Drawing.Point(378, 3);
            this.memoCloseBtn.Name = "memoCloseBtn";
            this.memoCloseBtn.Size = new System.Drawing.Size(119, 42);
            this.memoCloseBtn.TabIndex = 6;
            this.memoCloseBtn.TabStop = false;
            this.memoCloseBtn.Text = "Close";
            this.memoCloseBtn.UseVisualStyleBackColor = true;
            this.memoCloseBtn.Click += new System.EventHandler(this.memoCloseBtn_Click);
            // 
            // memoDeleteBtn
            // 
            this.memoDeleteBtn.DialogResult = System.Windows.Forms.DialogResult.No;
            this.memoDeleteBtn.Location = new System.Drawing.Point(253, 3);
            this.memoDeleteBtn.Name = "memoDeleteBtn";
            this.memoDeleteBtn.Size = new System.Drawing.Size(119, 42);
            this.memoDeleteBtn.TabIndex = 7;
            this.memoDeleteBtn.TabStop = false;
            this.memoDeleteBtn.Text = "Delete";
            this.memoDeleteBtn.UseVisualStyleBackColor = true;
            this.memoDeleteBtn.Click += new System.EventHandler(this.memoDeleteBtn_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.memoSaveBtn, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.memoCloseBtn, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.memoDeleteBtn, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.memoClearBtn, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 395);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(500, 48);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // colorComBox
            // 
            this.colorComBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.colorComBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colorComBox.FormattingEnabled = true;
            this.colorComBox.Location = new System.Drawing.Point(112, 332);
            this.colorComBox.Name = "colorComBox";
            this.colorComBox.Size = new System.Drawing.Size(363, 26);
            this.colorComBox.TabIndex = 9;
            this.colorComBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.colorComBox_DrawItem);
            // 
            // contentTxtBox
            // 
            this.contentTxtBox.Location = new System.Drawing.Point(112, 72);
            this.contentTxtBox.Multiline = true;
            this.contentTxtBox.Name = "contentTxtBox";
            this.contentTxtBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.contentTxtBox.Size = new System.Drawing.Size(363, 237);
            this.contentTxtBox.TabIndex = 3;
            // 
            // contentLabel
            // 
            this.contentLabel.AutoSize = true;
            this.contentLabel.Location = new System.Drawing.Point(34, 75);
            this.contentLabel.Name = "contentLabel";
            this.contentLabel.Size = new System.Drawing.Size(37, 15);
            this.contentLabel.TabIndex = 2;
            this.contentLabel.Text = "내용";
            // 
            // titleTxtBox
            // 
            this.titleTxtBox.Location = new System.Drawing.Point(112, 25);
            this.titleTxtBox.Name = "titleTxtBox";
            this.titleTxtBox.Size = new System.Drawing.Size(363, 25);
            this.titleTxtBox.TabIndex = 1;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(34, 31);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(37, 15);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "제목";
            // 
            // colorLabel
            // 
            this.colorLabel.AutoSize = true;
            this.colorLabel.Location = new System.Drawing.Point(34, 335);
            this.colorLabel.Name = "colorLabel";
            this.colorLabel.Size = new System.Drawing.Size(37, 15);
            this.colorLabel.TabIndex = 10;
            this.colorLabel.Text = "색상";
            // 
            // MemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 455);
            this.Controls.Add(this.colorLabel);
            this.Controls.Add(this.colorComBox);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.contentTxtBox);
            this.Controls.Add(this.contentLabel);
            this.Controls.Add(this.titleTxtBox);
            this.Controls.Add(this.titleLabel);
            this.Name = "MemoForm";
            this.Text = "메모";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button memoSaveBtn;
        private System.Windows.Forms.Button memoClearBtn;
        private System.Windows.Forms.Button memoCloseBtn;
        private System.Windows.Forms.Button memoDeleteBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox colorComBox;
        private System.Windows.Forms.TextBox contentTxtBox;
        private System.Windows.Forms.Label contentLabel;
        private System.Windows.Forms.TextBox titleTxtBox;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label colorLabel;
    }
}