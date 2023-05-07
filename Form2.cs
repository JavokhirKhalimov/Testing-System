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
            
            Panel panel = new Panel();
            panel.Location = new Point(10, 10);
            panel.Size = new Size(300, 200);
            this.Controls.Add(panel);

            

            // Создание метки с текстом вопроса
            Label questionLabel = new Label();
            questionLabel.Location = new Point(10, 10);
            questionLabel.AutoSize = true;
            panel.Controls.Add(questionLabel);
            //Рандом
            Random random = new Random();
            
            for (int j=0; j<savollar.Length; j++)
            {
                // Установка текста первого вопроса
                questionLabel.Text = savollar[j];

                // Создание радиокнопок с вариантами ответа
                for (int i = 0; i < 3; i++)
                {
                    int randomNumber = random.Next(0, javoblar[j].Length);
                    string element = javoblar[j][randomNumber];
                    RadioButton radioButton = new RadioButton();
                    radioButton.Location = new Point(10, 40 + i * 30);
                    radioButton.Text = javoblar[j][randomNumber];
                    panel.Controls.Add(radioButton);
                    javoblar[j].ToList().RemoveAll(element);
                }
            }

            

            
        }
    }

        
    
}
