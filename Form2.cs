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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Testing
{
    public partial class Form2 : Form
    {
        private int remainingSeconds;
        string[] savollar;
        List<string[]> javoblar=new List<string[]>();

        

        public Form2()
        {
            InitializeComponent();
            // Устанавливаем начальное значение времени в секундах
            remainingSeconds =  1800; // 30 минут
            // Устанавливаем интервал таймера в 1 секунду
            timer.Interval = 1000;
            // Запускаем таймер
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //сборка ссылки
            string[] path = { @"C:\Users\user\source\repos\Testing\bin\Debug\savollar.txt",
            @"C:\Users\user\source\repos\Testing\bin\Debug\javoblar.txt"};
            //чтение ссылки
            string readAllFile_savollar = File.ReadAllText(path[0]);
            string readAllFile_javoblar = File.ReadAllText(path[1]);
            savollar = readAllFile_savollar.Split('\n');
            string[] lines = readAllFile_javoblar.Split('\n');

            
            foreach (string line in lines)
            {
                string[] items = line.Split(',');
                javoblar.Add(items);
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
            timer.Start();
            int x = 10, y = 10;

            Panel panel = new Panel();
            panel.Location = new Point(10, 10);
            panel.Size = new Size(400, 300);
            this.Controls.Add(panel);


            // Создание метки с текстом вопроса


            //Рандом
            Random random = new Random();
            int radioButtonOffset = 20;

            for (int j=0; j<savollar.Length; j++)
            {
                Label questionLabel = new Label();
                // Установка текста первого вопроса
                questionLabel.Text = savollar[j];

                questionLabel.Location = new Point(x, y);
                questionLabel.AutoSize = true;
                panel.Controls.Add(questionLabel);

                // Создание радиокнопок с вариантами ответа
                for (int i = 0; i < 3; i++)
                {
                    List<string> list = new List<string>(javoblar[j]);
                    int randomNumber = random.Next(0, list.Count);

                    RadioButton radioButton = new RadioButton();
                    radioButton.Location = new Point(x + 20, y + radioButtonOffset * (i + 1));
                    radioButton.Text = list[randomNumber];
                    panel.Controls.Add(radioButton);

                    list.RemoveAt(randomNumber);
                    javoblar[j] = list.ToArray();

                }
                y += radioButtonOffset * 4;
            }

            

            
        }
    }

        
    
}
