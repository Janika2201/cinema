﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cinema
{
    public partial class Form1 : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\opilane\source\repos\cinema\cinema\AppData\kino.mdf;Integrated Security=True");
        private SqlCommand command;
        private SqlDataAdapter adapter;
        Label[,] _arr = new Label[4, 4];
        Label[] read = new Label[4];
        Button osta, kinni;
        StreamWriter to_file;
        bool ost = false;
        List<string> arr_pilet;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                read[i] = new Label();
                read[i].Text = "Rida " + (i + 1);
                read[i].Size = new Size(100, 100);
                read[i].Location = new Point(1, i * 100);
                this.Controls.Add(read[i]);
                for (int j = 0; j < 4; j++)
                {
                    _arr[i, j] = new Label();
                    _arr[i, j].Text = " Koht" + (j + 1);//"Rida " + i +
                    _arr[i, j].Size = new Size(100, 100);
                    _arr[i, j].BackColor = Color.Green;
                    _arr[i, j].Image = Image.FromFile("../../Image/tool.jpg");
                    _arr[i, j].BorderStyle = BorderStyle.Fixed3D;
                    _arr[i, j].Location = new Point(j * 100 + 100, i * 100);
                    this.Controls.Add(_arr[i, j]);
                    _arr[i, j].Tag = new int[] { i, j };
                    _arr[i, j].Click += new System.EventHandler(Form1_Click);
                }
            }
            osta = new Button();
            osta.Text = "Osta";
            osta.Location = new Point(200, 400);
            this.Controls.Add(osta);
            osta.Click += Osta_Click;
            kinni = new Button();
            kinni.Text = "Kinni";
            kinni.Location = new Point(100, 400);
            this.Controls.Add(kinni);
            kinni.Click += Kinni_Click;
        }

        private void Kinni_Click(object sender, EventArgs e)
        {
            string text = "";
            to_file = new StreamWriter("Kino.txt", false);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (_arr[i, j].BackColor == Color.Yellow)
                    {
                        Osta_Click_Func();
                    }
                }
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (_arr[i, j].BackColor == Color.Red)
                    {
                        text += i + "," + j + ",true;";
                    }
                    else
                    {
                        text += i + "," + j + ",false;";
                    }
                }
                text += "\n";
            }
            to_file.Write(text);
            to_file.Close();
            this.Close();
        }

        private void Osta_Click_Func()
        {
            arr_pilet = new List<string>();
            if (ost == true)
            {
                var vastus = MessageBox.Show("Kas oled kindel?", "Appolo küsib", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (vastus == DialogResult.Yes)
                {
                    int t = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (_arr[i, j].BackColor == Color.Yellow)
                            {
                                t++;
                                _arr[i, j].BackColor = Color.Red;
                                

                                //Сохранить каждый билет в файл
                                StreamWriter pilet = new StreamWriter("Pilet" + (t).ToString() + "Rida" + (i + 1).ToString() + "koht" + (j + 1).ToString() + ".txt");
                                //arr_pilet[t-1]="Pilet" + (t).ToString() + "Rida" + (i+1).ToString() + "koht" + (j+1).ToString() + ".txt";
                                arr_pilet.Add("Pilet" + (t).ToString() + "Rida" + (i + 1).ToString() + "koht" + (j + 1).ToString() + ".txt");
                                pilet.WriteLine("Pilet" + (t).ToString() + "Rida" + (i + 1).ToString() + "koht" + (j + 1).ToString());
                                pilet.Close();
                                Insert_To_DataBase(t, i, j);

                            }
                        }
                    }
                    //MessageBox.Show(arr_pilet.Count().ToString());
                    Pilet_saada();
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (_arr[i, j].BackColor == Color.Yellow)
                            {
                                _arr[i, j].Text = " Koht" + (j + 1);
                                _arr[i, j].BackColor = Color.Green;
                                ost = false;
                            }
                        }
                    }
                }
            }
            else { MessageBox.Show("On vaja midagi valida!"); }
        }

        private void Pilet_saada()
        {
            string adress = Interaction.InputBox("Sisesta e-mail", "Kuhu saada", "marina.oleinik@tthk.ee");
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    //string password = Interaction.InputBox("Sisesta salasõna");
                    Credentials = new System.Net.NetworkCredential("mvc.programmeerimine@gmail.com", "3.Kuursus"),
                    EnableSsl = true
                };
                mail.From = new MailAddress("mvc.programmeerimine@gmail.com");
                mail.To.Add(adress);
                mail.Subject = "Pilet";
                mail.Body = "Pilet on ostetud ja ta on maanuses";//"Rida: ",rida.ToString()," Koht: ",koht.ToString();

                foreach (var item in arr_pilet)
                {
                    mail.Attachments.Add(new Attachment(item));
                }
                smtpClient.Send(mail);
                MessageBox.Show("Pilet oli saadetud mailile: " + adress);
            }
            catch (Exception)
            {
                MessageBox.Show("Viga");
            }
        }

        private void Insert_To_DataBase(int t, int i, int j)
        {
            string connectionString;
            SqlConnection con;
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\opilane\source\repos\cinema\cinema\AppData\kino.mdf;Integrated Security=True";
            con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                MessageBox.Show("Andmebaas on avatud");
            }
            catch (Exception e)
            {
                MessageBox.Show("Andmebaasi avamiseks tekkis viga" + e.Message);
            }
            SqlCommand command;
            string sql = "INSERT INTO Ostetud_Piletid(Id,Rida,Koht) VALUES(" + (t) + "," + (i + 1) + "," + (j + 1) + ")";
            command = new SqlCommand(sql, con);
            command.ExecuteNonQuery();
            command.Dispose();
            con.Close();
        }

        private void Osta_Click(object sender, EventArgs e)
        {
            var vastus = MessageBox.Show("Kas oled kindel?",
                "Appolo küsib", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (vastus == DialogResult.Yes)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_arr[i, j].BackColor == Color.Yellow)
                        {
                            _arr[i, j].BackColor = Color.Transparent;
                            _arr[i, j].Image = Image.FromFile("../../Image/red.jpg");

                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_arr[i, j].BackColor == Color.Yellow)
                        {
                            _arr[i, j].Text = " Koht" + (j + 1);
                            _arr[i, j].BackColor = Color.Green;
                        }
                    }
                }
            }

        }

        void Form1_Click(object sender, EventArgs e)
        {
            var label = (Label)sender;//запомникли на какую надпись нажали
            var tag = (int[])label.Tag;//определили координаты надписи

            if (_arr[tag[0], tag[1]].Text != "Kinni")
            {
                _arr[tag[0], tag[1]].Text = "Kinni";
                _arr[tag[0], tag[1]].BackColor = Color.Yellow;
                _arr[tag[0], tag[1]].Image = Image.FromFile("../../Image/yellow.jpg");
            }
            else
            {
                MessageBox.Show("Koht " + (tag[0] + 1) + (tag[1] + 1) + " juba ostetud!");
            }


        }
    }
}