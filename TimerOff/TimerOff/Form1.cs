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

            // Отключаем возможность изменения размеров формы
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // Отключаем кнопки максимизации и минимизации
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Скрываем индикатор изменения размера
            this.SizeGripStyle = SizeGripStyle.Hide;

            // Настраиваем DateTimePicker для выбора даты и времени
            dateTimePickerShutdown.Format = DateTimePickerFormat.Custom;
            dateTimePickerShutdown.CustomFormat = "HH:mm:ss";
            dateTimePickerShutdown.ShowUpDown = true;

            // Устанавливаем минимальное значение на текущую дату и время
            dateTimePickerShutdown.MinDate = DateTime.Now;

            // Инициализируем таймер для обновления оставшегося времени
            updateTimer = new Timer();
            updateTimer.Interval = 1000; // Обновляем каждую секунду
            updateTimer.Tick += UpdateTimer_Tick;

            // Загружаем сохраненное время выключения, если оно есть
            LoadShutdownTime();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                TimeSpan timeRemaining = shutdownTime - DateTime.Now;

                if (timeRemaining.TotalSeconds <= 0)
                {
                    // Время вышло
                    updateTimer.Stop();
                    labelTimeRemaining.Text = "Компьютер будет выключен.";
                }
                else
                {
                    labelTimeRemaining.Text = $"Осталось: {timeRemaining.ToString(@"dd\.hh\:mm\:ss")}";
                }

                // Выводим текущие дата и время в заголовок формы для проверки
                this.Text = DateTime.Now.ToString("HH:mm:ss");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при обновлении времени: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveShutdownTime()
        {
            File.WriteAllText(shutdownTimeFile, shutdownTime.ToString("O")); // Используем Round-trip формат
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
                        // Запускаем таймер обновления
                        updateTimer.Start();
                    }
                    else
                    {
                        // Время прошло, удаляем файл
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
                MessageBox.Show("Время выключения должно быть в будущем.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TimeSpan timeUntilShutdown = shutdownTime - DateTime.Now;
            int totalSeconds = (int)timeUntilShutdown.TotalSeconds;

            // Выполняем команду shutdown
            Process.Start("shutdown", $"/s /t {totalSeconds}");

            // Сохраняем время выключения
            SaveShutdownTime();

            // Запускаем таймер для обновления оставшегося времени
            updateTimer.Start();

            // Обновляем метку сразу после запуска таймера
            UpdateTimer_Tick(null, null);
        }

        private void buttonStopTimer_Click_1(object sender, EventArgs e)
        {
            // Отменяем запланированное выключение
            Process.Start("shutdown", "/a");

            // Удаляем сохраненное время
            if (File.Exists(shutdownTimeFile))
            {
                File.Delete(shutdownTimeFile);
            }

            // Останавливаем таймер обновления
            updateTimer.Stop();

            labelTimeRemaining.Text = "Таймер выключен.";
        }
    }
}
