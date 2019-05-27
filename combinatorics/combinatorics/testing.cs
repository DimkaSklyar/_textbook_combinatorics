using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using System.IO;
using System.Linq;

namespace combinatorics
{
    public partial class testing : Telerik.WinControls.UI.RadForm
    {
        List<string> list;
        int pointQuestion = 0;
        Point pointRadio;
        int countAnswer = 0;
        int countQuestion = 0; //количество вопросов
        int numberQuestion = 1; //номер вопроса по порядку
        List<int> posQuestion = new List<int>(); //позиции вопросов
        List<string> posRightAnswer = new List<string>(); //позиции правильных ответов
        bool[] checkAnswer; //позиции нажатых чекбоксов

        private RadCheckBox addAnswerRadio(int nameCount, string text)
        {
            RadCheckBox radRadioButton = new RadCheckBox();
            radRadioButton.ThemeName = "Fluent";
            radRadioButton.Name = "radAnswerRadio" + ++nameCount;
            radRadioButton.Text = text;
            this.pointRadio.Y = this.pointRadio.Y + 35;
            radRadioButton.Location = this.pointRadio;
            radRadioButton.Checked = checkAnswer[nameCount-1];
            return radRadioButton;
        }

        RadForm1 radForm1;
        public testing(List<string> list, RadForm1 radForm1)
        {
            InitializeComponent();
            this.list = list;
            this.radForm1 = radForm1;
            
        }

        private void nextQuestion()
        {
            countAnswer = 0;
            pointRadio = new Point(15, -22);

            bool flag = true;
            for (int i = posQuestion[numberQuestion - 1]; i < list.Count; i++)
            {
                if (list[i].Substring(0, 1) == "?" && flag)
                {
                    flag = false;
                    radTextBox1.Text = list[i].Substring(1);
                    continue;
                }

                if (list[i].Substring(0, 1) == "?" && !flag)
                {
                    pointQuestion = i;
                    break;
                }
                radPanel1.Controls.Add(addAnswerRadio(i, list[i].Substring(1)));

                posRightAnswer.Add(list[i]);

                countAnswer++;
            }
        }


        private void testing_Shown(object sender, EventArgs e)
        {
            //подсчёт количества вопросов
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Substring(0, 1) == "?")
                {
                    countQuestion++;
                    posQuestion.Add(i);
                }
            }

            checkAnswer = new bool[list.Count];

            radLabel1.Text = numberQuestion + " из " + countQuestion;
            nextQuestion();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            numberQuestion++;
            for (int i = 0; i < countAnswer; i++)
            {
                foreach (RadCheckBox item in radPanel1.Controls.OfType<RadCheckBox>())
                {


                    checkAnswer[int.Parse(item.Name.Substring(item.Name.Length - 1)) - 1] = item.Checked;

                    radPanel1.Controls.Remove(item);
                    break; //important step
                }
            }
            
            posRightAnswer.Clear();

            //если вопросы закончились выводим результат и заканчиваем тест
            if (numberQuestion <= countQuestion)
            {
                nextQuestion();
                radLabel1.Text = numberQuestion + " из " + countQuestion;

                if (numberQuestion == 1)
                {
                    radButton2.Enabled = false;
                }
                else
                {
                    radButton2.Enabled = true;
                }
            }
            else
            {
                radTextBox1.Text = "";
                double result = 0;
                for (int i = 0; i < checkAnswer.Length; i++)
                {
                    if (checkAnswer[i] && list[i].Substring(0,1) == "+")
                    {
                        result++;
                    }
                    if (checkAnswer[i] && list[i].Substring(0, 1) == "-")
                    {
                        result-=.75;
                    }
                }
                int countRightAnswer = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Substring(0,1) == "+")
                    {
                        countRightAnswer++;
                    }
                }
                double rez = result / Convert.ToDouble(countRightAnswer) * 100;
                DialogResult dialogResult = MessageBox.Show("Ваш результат: "+ Math.Round(rez,2) + "%","Результат",MessageBoxButtons.OK,MessageBoxIcon.Information);
                if (dialogResult == DialogResult.OK)
                {
                    this.Close();
                    radForm1.Enabled = true;
                }
            }
            ////////////////////////////////////
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            numberQuestion--;
            for (int i = 0; i < countAnswer; i++)
            {
                foreach (RadCheckBox item in radPanel1.Controls.OfType<RadCheckBox>())
                {
                    radPanel1.Controls.Remove(item);
                    break; //important step
                }
            }

            posRightAnswer.Clear();

            nextQuestion();

            radLabel1.Text = numberQuestion + " из " + countQuestion;
            if (numberQuestion == 1)
            {
                radButton2.Enabled = false;
            }
            else
            {
                radButton2.Enabled = true;
            }
        }

        private void testing_FormClosed(object sender, FormClosedEventArgs e)
        {
            radForm1.Enabled = true;
        }
    }
}
