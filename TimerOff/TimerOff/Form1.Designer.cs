namespace TimerOff
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            buttonStartTimer = new Button();
            buttonStopTimer = new Button();
            labelTimeRemaining = new Label();
            dateTimePickerShutdown = new DateTimePicker();
            SuspendLayout();
            // 
            // buttonStartTimer
            // 
            buttonStartTimer.Location = new Point(12, 93);
            buttonStartTimer.Name = "buttonStartTimer";
            buttonStartTimer.Size = new Size(75, 23);
            buttonStartTimer.TabIndex = 0;
            buttonStartTimer.Text = "Запустить";
            buttonStartTimer.UseVisualStyleBackColor = true;
            buttonStartTimer.Click += buttonStartTimer_Click_1;
            // 
            // buttonStopTimer
            // 
            buttonStopTimer.Location = new Point(137, 93);
            buttonStopTimer.Name = "buttonStopTimer";
            buttonStopTimer.Size = new Size(75, 23);
            buttonStopTimer.TabIndex = 1;
            buttonStopTimer.Text = "Отменить";
            buttonStopTimer.UseVisualStyleBackColor = true;
            buttonStopTimer.Click += buttonStopTimer_Click_1;
            // 
            // labelTimeRemaining
            // 
            labelTimeRemaining.AutoSize = true;
            labelTimeRemaining.Location = new Point(12, 53);
            labelTimeRemaining.Name = "labelTimeRemaining";
            labelTimeRemaining.Size = new Size(149, 15);
            labelTimeRemaining.TabIndex = 2;
            labelTimeRemaining.Text = "Осталось до выключения";
            // 
            // dateTimePickerShutdown
            // 
            dateTimePickerShutdown.Format = DateTimePickerFormat.Custom;
            dateTimePickerShutdown.Location = new Point(12, 12);
            dateTimePickerShutdown.Name = "dateTimePickerShutdown";
            dateTimePickerShutdown.Size = new Size(200, 23);
            dateTimePickerShutdown.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(217, 126);
            Controls.Add(dateTimePickerShutdown);
            Controls.Add(labelTimeRemaining);
            Controls.Add(buttonStopTimer);
            Controls.Add(buttonStartTimer);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Shutdown timer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonStartTimer;
        private Button buttonStopTimer;
        private Label labelTimeRemaining;
        private DateTimePicker dateTimePickerShutdown;
    }
}
