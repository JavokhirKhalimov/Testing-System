using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Testing
{
    public partial class Form2 : Form
    {
        private int remainingSeconds;
        string[] savollar;
        List<string[]> variantlar = new List<string[]>();
        string[] path= { @"C:\Users\user\source\repos\Testing\bin\Debug\savollar.txt",
            @"C:\Users\user\source\repos\Testing\bin\Debug\variantlar.txt",
            @"C:\Users\user\source\repos\Testing\bin\Debug\javoblar.txt"

        };
        Panel panel = new Panel();




        public Form2()
        {
            InitializeComponent();
            // Устанавливаем начальное значение времени в секундах
            remainingSeconds = 1800; // 30 минут
            // Устанавливаем интервал таймера в 1 секунду
            timer.Interval = 1000;
            // Запускаем таймер

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //сборка ссылки
            
            //чтение ссылки
            string readAllFile_savollar = File.ReadAllText(path[0]);
            string readAllFile_javoblar = File.ReadAllText(path[1]);
            savollar = readAllFile_savollar.Split('\n');
            string[] lines = readAllFile_javoblar.Split('\n');


            foreach (string line in lines)
            {
                string[] items = line.Split(',');
                variantlar.Add(items);
            }


        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // Уменьшаем оставшееся время на 1 секунду
            remainingSeconds--;

            // Преобразуем оставшееся время в формат ММ:СС
            TimeSpan remainingTime = TimeSpan.FromSeconds(remainingSeconds);
            string formattedTime = string.Format("{0:D2}:{1:D2}", remainingTime.Minutes, remainingTime.Seconds);

            // Обновляем значение Label
            label1.Text = formattedTime;

            // Если время вышло, останавливаем таймер и выполняем дополнительные действия
            if (remainingSeconds == 0)
            {
                timer.Stop();
                MessageBox.Show("Время вышло!");
                this.Close(); // Закрытие формы
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            timer.Start();
            int x = 10, a = 555, b = 0;

            // Panel panel = new Panel();
            panel.Location = new Point(10, 10);
            panel.Size = new Size(800, 600);
            this.Controls.Add(panel);


            // Создание метки с текстом вопроса


            //Рандом
            Random random = new Random();
            int radioButtonOffset = 20;

            for (int j = 0; j < savollar.Length; j++)
            {
                GroupBox questionGroup = new GroupBox();
                questionGroup.Location = new Point(3, 5 + b);
                questionGroup.Size = new Size(555, 155);
                questionGroup.Text = savollar[j];
                panel.Controls.Add(questionGroup);
                b += 155;

                // Создание радиокнопок с вариантами ответа
                for (int i = 0; i < 3; i++)
                {
                    List<string> list = new List<string>(variantlar[j]);
                    int randomNumber = random.Next(0, list.Count);

                    RadioButton radioButton = new RadioButton();
                    radioButton.Location = new Point(x, 30 + radioButtonOffset * i);
                    radioButton.Text = '\u200B' + list[randomNumber];
                    questionGroup.Controls.Add(radioButton);

                    list.RemoveAt(randomNumber);
                    variantlar[j] = list.ToArray();

                }



            }




        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            
            string[] lines = File.ReadAllLines(path[2]);


            try
            {
                timer.Stop();

                // Переменная для подсчета правильных ответов
                int correctAnswers = 0;
                int count = 0;
                // Перебираем все GroupBox на панели
                foreach (GroupBox questionGroup in panel.Controls.OfType<GroupBox>())
                {
                    
                    // Находим выбранную радиокнопку внутри GroupBox
                    RadioButton selectedRadioButton = questionGroup.Controls.OfType<RadioButton>()
                        .FirstOrDefault(radioButton => radioButton.Checked);

                    // Получаем индекс правильного ответа для текущего вопроса
                    int questionIndex = panel.Controls.IndexOf(questionGroup);

                    // Проверяем ответ студента
                    if (selectedRadioButton != null && selectedRadioButton.Text != null &&
                    selectedRadioButton.Text.Trim() == '\u200B' + lines[count].Trim())
                    {
                        // Ответ верный
                        correctAnswers++;
                    }
                    //MessageBox.Show(selectedRadioButton.Text + ":" + lines[count]);
                    count++;
                    
                }

                // Выводим результаты
                MessageBox.Show($"Количество правильных ответов: {correctAnswers}");

                // Закрытие формы
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }



}