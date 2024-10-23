using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace TimerOff
{
    public partial class Form1 : Form
    {
        private DateTime shutdownTime;
        private Timer updateTimer;

        private string shutdownTimeFile = "shutdown_time.txt";

        public Form1()
        {
            InitializeComponent();

            // ��������� ����������� ��������� �������� �����
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // ��������� ������ ������������ � �����������
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // �������� ��������� ��������� �������
            this.SizeGripStyle = SizeGripStyle.Hide;

            // ����������� DateTimePicker ��� ������ ���� � �������
            dateTimePickerShutdown.Format = DateTimePickerFormat.Custom;
            dateTimePickerShutdown.CustomFormat = "HH:mm:ss";
            dateTimePickerShutdown.ShowUpDown = true;

            // ������������� ����������� �������� �� ������� ���� � �����
            dateTimePickerShutdown.MinDate = DateTime.Now;

            // �������������� ������ ��� ���������� ����������� �������
            updateTimer = new Timer();
            updateTimer.Interval = 1000; // ��������� ������ �������
            updateTimer.Tick += UpdateTimer_Tick;

            // ��������� ����������� ����� ����������, ���� ��� ����
            LoadShutdownTime();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                TimeSpan timeRemaining = shutdownTime - DateTime.Now;

                if (timeRemaining.TotalSeconds <= 0)
                {
                    // ����� �����
                    updateTimer.Stop();
                    labelTimeRemaining.Text = "��������� ����� ��������.";
                }
                else
                {
                    labelTimeRemaining.Text = $"��������: {timeRemaining.ToString(@"dd\.hh\:mm\:ss")}";
                }

                // ������� ������� ���� � ����� � ��������� ����� ��� ��������
                this.Text = DateTime.Now.ToString("HH:mm:ss");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��������� ������ ��� ���������� �������: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveShutdownTime()
        {
            File.WriteAllText(shutdownTimeFile, shutdownTime.ToString("O")); // ���������� Round-trip ������
        }

        private void LoadShutdownTime()
        {
            if (File.Exists(shutdownTimeFile))
            {
                string timeText = File.ReadAllText(shutdownTimeFile);
                if (DateTime.TryParse(timeText, null, System.Globalization.DateTimeStyles.RoundtripKind, out shutdownTime))
                {
                    if (shutdownTime > DateTime.Now)
                    {
                        // ��������� ������ ����������
                        updateTimer.Start();
                    }
                    else
                    {
                        // ����� ������, ������� ����
                        File.Delete(shutdownTimeFile);
                    }
                }
            }
        }

        private void buttonStartTimer_Click_1(object sender, EventArgs e)
        {
            shutdownTime = dateTimePickerShutdown.Value;

            if (shutdownTime <= DateTime.Now)
            {
                MessageBox.Show("����� ���������� ������ ���� � �������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TimeSpan timeUntilShutdown = shutdownTime - DateTime.Now;
            int totalSeconds = (int)timeUntilShutdown.TotalSeconds;

            // ��������� ������� shutdown
            Process.Start("shutdown", $"/s /t {totalSeconds}");

            // ��������� ����� ����������
            SaveShutdownTime();

            // ��������� ������ ��� ���������� ����������� �������
            updateTimer.Start();

            // ��������� ����� ����� ����� ������� �������
            UpdateTimer_Tick(null, null);
        }

        private void buttonStopTimer_Click_1(object sender, EventArgs e)
        {
            // �������� ��������������� ����������
            Process.Start("shutdown", "/a");

            // ������� ����������� �����
            if (File.Exists(shutdownTimeFile))
            {
                File.Delete(shutdownTimeFile);
            }

            // ������������� ������ ����������
            updateTimer.Stop();

            labelTimeRemaining.Text = "������ ��������.";
        }
    }
}
