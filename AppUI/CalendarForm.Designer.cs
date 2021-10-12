namespace AppUI
{
    public partial class CalendarForm
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
            this.monthCalendar = new System.Windows.Forms.MonthCalendar();
            this.labelCalendar = new System.Windows.Forms.Label();
            this.listBoxBirthday = new System.Windows.Forms.ListBox();
            this.listBoxEvent = new System.Windows.Forms.ListBox();
            this.labelEvent = new System.Windows.Forms.Label();
            this.labelBirthday = new System.Windows.Forms.Label();
            this.checkedListBoxTask = new System.Windows.Forms.CheckedListBox();
            this.labelTask = new System.Windows.Forms.Label();
            this.textBoxAddTask = new System.Windows.Forms.TextBox();
            this.labelAddTask = new System.Windows.Forms.Label();
            this.buttonAddTask = new System.Windows.Forms.Button();
            this.panelAddTask = new System.Windows.Forms.Panel();
            this.labelType = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxTaskType = new System.Windows.Forms.ComboBox();
            this.comboBoxTaskPriority = new System.Windows.Forms.ComboBox();
            this.panelAddTask.SuspendLayout();
            this.SuspendLayout();
            // 
            // monthCalendar
            // 
            this.monthCalendar.Location = new System.Drawing.Point(232, 140);
            this.monthCalendar.Margin = new System.Windows.Forms.Padding(6);
            this.monthCalendar.Name = "monthCalendar";
            this.monthCalendar.TabIndex = 0;
            this.monthCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_DateSelected);
            // 
            // labelCalendar
            // 
            this.labelCalendar.AutoSize = true;
            this.labelCalendar.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelCalendar.Location = new System.Drawing.Point(263, 18);
            this.labelCalendar.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCalendar.Name = "labelCalendar";
            this.labelCalendar.Size = new System.Drawing.Size(146, 26);
            this.labelCalendar.TabIndex = 1;
            this.labelCalendar.Text = "My Calendar";
            // 
            // listBoxBirthday
            // 
            this.listBoxBirthday.FormattingEnabled = true;
            this.listBoxBirthday.Location = new System.Drawing.Point(485, 74);
            this.listBoxBirthday.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxBirthday.Name = "listBoxBirthday";
            this.listBoxBirthday.Size = new System.Drawing.Size(141, 69);
            this.listBoxBirthday.TabIndex = 2;
            // 
            // listBoxEvent
            // 
            this.listBoxEvent.FormattingEnabled = true;
            this.listBoxEvent.Location = new System.Drawing.Point(485, 266);
            this.listBoxEvent.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxEvent.Name = "listBoxEvent";
            this.listBoxEvent.Size = new System.Drawing.Size(141, 69);
            this.listBoxEvent.TabIndex = 3;
            // 
            // labelEvent
            // 
            this.labelEvent.AutoSize = true;
            this.labelEvent.Location = new System.Drawing.Point(485, 249);
            this.labelEvent.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelEvent.Name = "labelEvent";
            this.labelEvent.Size = new System.Drawing.Size(40, 13);
            this.labelEvent.TabIndex = 4;
            this.labelEvent.Text = "Events";
            // 
            // labelBirthday
            // 
            this.labelBirthday.AutoSize = true;
            this.labelBirthday.Location = new System.Drawing.Point(485, 59);
            this.labelBirthday.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelBirthday.Name = "labelBirthday";
            this.labelBirthday.Size = new System.Drawing.Size(50, 13);
            this.labelBirthday.TabIndex = 5;
            this.labelBirthday.Text = "Birthdays";
            // 
            // checkedListBoxTask
            // 
            this.checkedListBoxTask.FormattingEnabled = true;
            this.checkedListBoxTask.Location = new System.Drawing.Point(8, 190);
            this.checkedListBoxTask.Margin = new System.Windows.Forms.Padding(2);
            this.checkedListBoxTask.Name = "checkedListBoxTask";
            this.checkedListBoxTask.Size = new System.Drawing.Size(100, 154);
            this.checkedListBoxTask.TabIndex = 6;
            // 
            // labelTask
            // 
            this.labelTask.AutoSize = true;
            this.labelTask.Location = new System.Drawing.Point(8, 175);
            this.labelTask.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTask.Name = "labelTask";
            this.labelTask.Size = new System.Drawing.Size(36, 13);
            this.labelTask.TabIndex = 7;
            this.labelTask.Text = "Tasks";
            // 
            // textBoxAddTask
            // 
            this.textBoxAddTask.Location = new System.Drawing.Point(66, 4);
            this.textBoxAddTask.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxAddTask.Multiline = true;
            this.textBoxAddTask.Name = "textBoxAddTask";
            this.textBoxAddTask.Size = new System.Drawing.Size(110, 41);
            this.textBoxAddTask.TabIndex = 8;
            // 
            // labelAddTask
            // 
            this.labelAddTask.AutoSize = true;
            this.labelAddTask.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelAddTask.Location = new System.Drawing.Point(3, 4);
            this.labelAddTask.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAddTask.Name = "labelAddTask";
            this.labelAddTask.Size = new System.Drawing.Size(43, 17);
            this.labelAddTask.TabIndex = 9;
            this.labelAddTask.Text = "Task:";
            // 
            // buttonAddTask
            // 
            this.buttonAddTask.BackColor = System.Drawing.Color.Black;
            this.buttonAddTask.ForeColor = System.Drawing.Color.White;
            this.buttonAddTask.Location = new System.Drawing.Point(11, 153);
            this.buttonAddTask.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAddTask.Name = "buttonAddTask";
            this.buttonAddTask.Size = new System.Drawing.Size(179, 20);
            this.buttonAddTask.TabIndex = 10;
            this.buttonAddTask.Text = "Add new task";
            this.buttonAddTask.UseVisualStyleBackColor = false;
            this.buttonAddTask.Click += new System.EventHandler(this.buttonAddTask_Click);
            // 
            // panelAddTask
            // 
            this.panelAddTask.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAddTask.Controls.Add(this.labelType);
            this.panelAddTask.Controls.Add(this.label1);
            this.panelAddTask.Controls.Add(this.labelAddTask);
            this.panelAddTask.Controls.Add(this.comboBoxTaskType);
            this.panelAddTask.Controls.Add(this.textBoxAddTask);
            this.panelAddTask.Controls.Add(this.comboBoxTaskPriority);
            this.panelAddTask.Location = new System.Drawing.Point(11, 27);
            this.panelAddTask.Name = "panelAddTask";
            this.panelAddTask.Size = new System.Drawing.Size(179, 124);
            this.panelAddTask.TabIndex = 11;
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelType.Location = new System.Drawing.Point(3, 98);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(44, 17);
            this.labelType.TabIndex = 15;
            this.labelType.Text = "Type:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(3, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "Priority: ";
            // 
            // comboBoxTaskType
            // 
            this.comboBoxTaskType.FormattingEnabled = true;
            this.comboBoxTaskType.Items.AddRange(new object[] {
            "Studies",
            "Work",
            "Household"});
            this.comboBoxTaskType.Location = new System.Drawing.Point(66, 98);
            this.comboBoxTaskType.Name = "comboBoxTaskType";
            this.comboBoxTaskType.Size = new System.Drawing.Size(110, 21);
            this.comboBoxTaskType.TabIndex = 13;
            // 
            // comboBoxTaskPriority
            // 
            this.comboBoxTaskPriority.FormattingEnabled = true;
            this.comboBoxTaskPriority.Items.AddRange(new object[] {
            "High",
            "Medium ",
            "Low"});
            this.comboBoxTaskPriority.Location = new System.Drawing.Point(66, 61);
            this.comboBoxTaskPriority.Name = "comboBoxTaskPriority";
            this.comboBoxTaskPriority.Size = new System.Drawing.Size(110, 21);
            this.comboBoxTaskPriority.TabIndex = 12;
            // 
            // CalendarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::AppUI.Properties.Resources.Calendar2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(648, 352);
            this.Controls.Add(this.panelAddTask);
            this.Controls.Add(this.buttonAddTask);
            this.Controls.Add(this.labelTask);
            this.Controls.Add(this.checkedListBoxTask);
            this.Controls.Add(this.labelBirthday);
            this.Controls.Add(this.labelEvent);
            this.Controls.Add(this.listBoxEvent);
            this.Controls.Add(this.listBoxBirthday);
            this.Controls.Add(this.labelCalendar);
            this.Controls.Add(this.monthCalendar);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CalendarForm";
            this.Text = "Calendar";
            this.panelAddTask.ResumeLayout(false);
            this.panelAddTask.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MonthCalendar monthCalendar;
        private System.Windows.Forms.Label labelCalendar;
        private System.Windows.Forms.ListBox listBoxBirthday;
        private System.Windows.Forms.ListBox listBoxEvent;
        private System.Windows.Forms.Label labelEvent;
        private System.Windows.Forms.Label labelBirthday;
        private System.Windows.Forms.CheckedListBox checkedListBoxTask;
        private System.Windows.Forms.Label labelTask;
        private System.Windows.Forms.TextBox textBoxAddTask;
        private System.Windows.Forms.Label labelAddTask;
        private System.Windows.Forms.Button buttonAddTask;
        private System.Windows.Forms.Panel panelAddTask;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxTaskType;
        private System.Windows.Forms.ComboBox comboBoxTaskPriority;
    }
}