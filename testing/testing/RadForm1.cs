using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using System.IO;

namespace testing
{
    public partial class RadForm1 : Telerik.WinControls.UI.RadForm
    {
        int nameCount = 1;
        Point pointRadio;
        Point pointTextBox;
        Point pointButton;
        List<string> list = new List<string>();
        RadRadioButton[] arreyRadio = new RadRadioButton[10];
        RadTextBox[] arreyTextBox = new RadTextBox[10];
        int stopQuestion = 0;
        int countQuestion = 0;
        int qualityQuestion = 1;
        int numberQuestion = 1;
        private RadRadioButton addAnswerRadio(int nameCount)
        {
            RadRadioButton radRadioButton = new RadRadioButton();
            radRadioButton.ThemeName = "Fluent";
            radRadioButton.Name = "radAnswerRadio" + ++nameCount;
            radRadioButton.Text = "";
            this.pointRadio.Y = this.pointRadio.Y + 35;
            radRadioButton.Location = this.pointRadio;

            return radRadioButton;
        }

        private RadTextBox addAnswerTextBox(int nameCount)
        {
            RadTextBox radTextBox = new RadTextBox();
            radTextBox.ThemeName = "Fluent";
            radTextBox.Name = "radAnswerTextBox" + ++nameCount;
            radTextBox.Text = "";
            pointTextBox.Y = pointTextBox.Y + 35;
            radTextBox.Location = pointTextBox;
            radTextBox.Font = radAnswerTextBox1.Font;
            radTextBox.Width = 429;

            return radTextBox;
        }

        private void addAnswer()
        {
            radPanel1.Controls.Add(addAnswerRadio(this.nameCount));
            radPanel1.Controls.Add(addAnswerTextBox(this.nameCount));
            pointButton.Y = pointButton.Y + 35;
            radButton1.Location = pointButton;
            radButton5.Location = new Point(radButton5.Location.X, pointButton.Y);
            nameCount++;
            if (nameCount == 6)
            {
                radButton1.Enabled = false;
            }
            else
            {
                radButton1.Enabled = true;
            }
            if (nameCount > 1)
            {
                radButton5.Enabled = true;
            }
        }
        private void removeAnswer()
        {
            foreach (Control item in radPanel1.Controls)
            {
                if (item.Name == "radAnswerRadio" + this.nameCount)
                {
                    radPanel1.Controls.Remove(item);
                    break; //important step
                }
            }
            foreach (Control item in radPanel1.Controls)
            {
                if (item.Name == "radAnswerTextBox" + this.nameCount)
                {
                    radPanel1.Controls.Remove(item);
                    break; //important step
                }
            }
            nameCount--;
            pointRadio.Y -= 35;
            pointTextBox.Y -= 35;
            pointButton.Y -= 35;
            radButton5.Location = new Point(radButton5.Location.X, radButton5.Location.Y - 35);
            radButton1.Location = new Point(radButton1.Location.X, radButton1.Location.Y - 35);
            if (nameCount == 1)
            {
                radButton5.Enabled = false;
            }
            if (nameCount == 6)
            {
                radButton1.Enabled = false;
            }
            else
            {
                radButton1.Enabled = true;
            }
        }


        private void prevQuestion(int length)
        {
            int br = 0;
            int countAnswer = 0;
            for (int i = length - 1; i >= 0; i--)
            {
                if (list[i].Substring(0, 1) == "?")
                {
                    if (br == qualityQuestion - numberQuestion)
                    {
                        stopQuestion = i;
                        break;
                    }
                    countAnswer = -1;
                    br++;
                }
                countAnswer++;
            }

            int countremove = 0;
            foreach (RadTextBox textBox in radPanel1.Controls.OfType<RadTextBox>())
            {
                countremove++;
            }

            for (int w = 0; w < countremove - 1; w++)
            {
                removeAnswer();
            }

            for (int i = 0; i < countAnswer - 1; i++)
            {
                addAnswer();
            }

            radTextBox2.Text = list[stopQuestion].Substring(1);

            int stopAnswer = stopQuestion + 1;
            int a = 0;
            int b = 0;

            foreach (RadTextBox textBox in radPanel1.Controls.OfType<RadTextBox>())
            {
                arreyTextBox[a] = textBox;
                a++;
            }

            for (int i = 0; i < countAnswer; i++)
            {
                arreyTextBox[i].Text = list[stopAnswer].Substring(1);
                stopAnswer++;
            }
            Array.Clear(arreyTextBox, 0, arreyTextBox.Length);
            Array.Clear(arreyRadio, 0, arreyRadio.Length);
            stopAnswer = stopQuestion + 1;
            a = 0;
            b = 0;
            foreach (RadTextBox textBox in radPanel1.Controls.OfType<RadTextBox>())
            {
                arreyTextBox[a] = textBox;
                a++;
            }
            foreach (RadRadioButton radio in radPanel1.Controls.OfType<RadRadioButton>())
            {
                arreyRadio[b] = radio;
                b++;
            }
            for (int w = 0; w < 10; w++)
            {
                if (arreyRadio[w] == null)
                {
                    break;
                }
                if (arreyTextBox[w].Name.Substring(arreyTextBox[w].Name.Length - 1) == arreyRadio[w].Name.Substring(arreyRadio[w].Name.Length - 1) && list[stopAnswer].Substring(0, 1) == "+")
                {
                    arreyRadio[w].IsChecked = true;
                }
                stopAnswer++;
            }

            numberQuestion--;
            qualityQuestionLabel.Text = numberQuestion.ToString() + " из " + qualityQuestion.ToString();
        }


        public RadForm1()
        {
            InitializeComponent();
            pointTextBox = new Point(radAnswerTextBox1.Location.X, radAnswerTextBox1.Location.Y);
            pointRadio = new Point(radAnswerRadio1.Location.X, radAnswerRadio1.Location.Y);
            pointButton = new Point(radButton1.Location.X, radButton1.Location.Y);
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            addAnswer();
        }

        private void radButton5_Click(object sender, EventArgs e)
        {
            removeAnswer();
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            if (qualityQuestion == numberQuestion || qualityQuestion - numberQuestion == 1) //если номер вопроса равен количеству вопросов то добавляется новый в конец массива
            {
                list.Add("?" + radTextBox2.Text);
                int i = 0;
                int j = 0;
                foreach (RadTextBox textBox in radPanel1.Controls.OfType<RadTextBox>())
                {
                    arreyTextBox[i] = textBox;
                    i++;
                }
                foreach (RadRadioButton radio in radPanel1.Controls.OfType<RadRadioButton>())
                {
                    arreyRadio[j] = radio;
                    j++;
                }
                for (int w = 0; w < 10; w++)
                {
                    if (arreyRadio[w] == null)
                    {
                        break;
                    }
                    if (arreyTextBox[w].Name.Substring(arreyTextBox[w].Name.Length - 1) == arreyRadio[w].Name.Substring(arreyRadio[w].Name.Length - 1) && arreyRadio[w].IsChecked)
                    {
                        list.Add("+" + arreyTextBox[w].Text);
                    }
                    else
                    {
                        list.Add("-" + arreyTextBox[w].Text);
                    }
                }

                for (int w = 0; w < i - 1; w++)
                {
                    removeAnswer();
                }

                radAnswerRadio1.IsChecked = true;
                radTextBox2.Text = "";
                radAnswerTextBox1.Text = "";
                Array.Clear(arreyRadio, 0, arreyRadio.Length);
                Array.Clear(arreyTextBox, 0, arreyTextBox.Length);
                
                if (qualityQuestion == numberQuestion)
                {
                    qualityQuestion++;
                }
                numberQuestion++;
                qualityQuestionLabel.Text = numberQuestion.ToString() + " из " + qualityQuestion.ToString();
            }
            else
            {
                numberQuestion+=2;
                qualityQuestionLabel.Text = numberQuestion.ToString() + " из " + qualityQuestion.ToString();
                prevQuestion(list.Count);
            }
            countQuestion++;
            if (countQuestion > 0)
            {
                radButton4.Enabled = true;
            }
            else
            {
                radButton4.Enabled = false;
            }

        }

        private void radButton4_Click(object sender, EventArgs e)
        {
            prevQuestion(list.Count);
            countQuestion--;
            if (countQuestion > 0)
            {
                radButton4.Enabled = true;
            }
            else
            {
                radButton4.Enabled = false;
            }
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog1.FileName;
                for (int i = 0; i < list.Count; i++)
                {
                    list[i] = Crypto.EncryptStringAES(list[i], "test");
                }
                File.WriteAllLines(fileName,list);
                DialogResult result = MessageBox.Show("Тест успешно сохранён, приступить с созданию нового теста?","Информация", MessageBoxButtons.OKCancel,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1);
                if (result == DialogResult.OK)
                {
                    list.Clear();
                    stopQuestion = 0;
                    countQuestion = 0;
                    qualityQuestion = 1;
                    numberQuestion = 1;
                    qualityQuestionLabel.Text = "0";
                }
            }
        }
    }
}

        