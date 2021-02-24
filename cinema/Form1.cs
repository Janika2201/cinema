using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cinema
{
    public partial class Form1 : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\source\repos\cinema\cinema\AppData\kino.mdf;Integrated Security=True");
        private SqlCommand command;
        private SqlDataAdapter adapter;
        Label[,] _arr = new Label[4, 4];
        Label[] read = new Label[4];
        string[,] arri = new string[4, 4] ;
        Button osta, kinni;
        bool ost = false;
        public string imagge = "";
        Image red = Image.FromFile("C:/Users/Admin/source/repos/cinema/cinema/Image/red.jpg");//пути к картинке 
        Image yellow = Image.FromFile("C:/Users/Admin/source/repos/cinema/cinema/Image/yellow.jpg");//пути к картинке 
        Image tool = Image.FromFile("C:/Users/Admin/source/repos/cinema/cinema/Image/tool1.jpg");//пути к картинке 
        public List<string> attachments = new List<string>();
       
        public string filmid, text;
        public Form1()
        {
            InitializeComponent();
            BackColor = Color.Gray;
        }

        
        private void Form1_Load(object sender, EventArgs e)//залы
        {
            for (int i = 0; i < 4; i++)
            {
                read[i] = new Label();
                read[i].Text = "Rida " + (i + 1);
                
                read[i].BackColor = Color.White;
                read[i].Size = new Size(100, 100);
                read[i].Location = new Point(1, i * 100);
               
                this.Controls.Add(read[i]);
                for (int j = 0; j < 4; j++)
                {
                    _arr[i, j] = new Label();
                    _arr[i, j].Size = new Size(100, 100);
                    
                    _arr[i, j].BackColor = Color.Green;
                    _arr[i, j].Image = tool;


                    if (connect.State == ConnectionState.Closed)
                    {
                        connect.Open();
                        var commandStr = "SELECT a,y from " + filmid;//берем значение ряд и место 
                        SqlCommand command = new SqlCommand(commandStr, connect);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (Convert.ToInt32(reader["a"]) == i && Convert.ToInt32(reader["y"]) == j)//ряд и место
                                {
                                    _arr[i, j].Image = red;
                                }
                            }
                        }

                        connect.Close();
                    }
                    _arr[i, j].BorderStyle = BorderStyle.Fixed3D;
                    _arr[i, j].Location = new Point(j * 100 + 100, i * 100);
                    this.Controls.Add(_arr[i, j]);
                    _arr[i, j].Tag = new int[] { i, j };
                    _arr[i, j].Click += new System.EventHandler(Form1_Click);
                }
            }
            /*
            osta = new Button();
            osta.Text = "Osta";
            osta.Location = new Point(200, 400);
            this.Controls.Add(osta);
            */

            kinni = new Button();
            kinni.Text = "Osta Koht";//покупаем
            kinni.Location = new Point(100, 400);
            kinni.Size = new Size(100, 50);
            kinni.BackColor = Color.Black;
            kinni.ForeColor = Color.AliceBlue;
            this.Controls.Add(kinni);
            kinni.Click += Kinni_Click;


            osta = new Button();
            osta.Text = "Kinni";//закрыть
            osta.Location = new Point(400, 400);
            osta.Size = new Size(100, 50);
            osta.BackColor = Color.Red;
            osta.ForeColor = Color.AliceBlue;
            this.Controls.Add(osta);
            osta.Click += Osta_Click;

        }

        private void Osta_Click(object sender, EventArgs e)
        {
            this.Close();//закрываем форму
        }

        

        private void Kinni_Click(object sender, EventArgs e)//бронируем билет 
        {
            var vastus = MessageBox.Show("Kas olete oma valikus kindel", "Küsib kino", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (vastus == DialogResult.Yes)
            {
                int t = 0;

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_arr[i, j].Image == yellow)
                        {
                            t++;
                            arri[i, j] = "busy";//место занято и когда выбираешь место оно берется только то , что ты выбрал
                            _arr[i, j].Image = red;
                            if (connect.State == ConnectionState.Closed)
                            {
                                connect.Open();
                                var commandStr = "INSERT Into " + filmid + "(a,y) values (" + i + "," + j + ")";//ряд и место
                                using (SqlCommand command = new SqlCommand(commandStr, connect))
                                    command.ExecuteNonQuery();

                                connect.Close();
                            }
                        }
                    }
                }
                sendemail();
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_arr[i, j].Image == yellow)
                        {
                           
                            _arr[i, j].Image = tool;//обычный стул
                        }
                    }
                }
            }
            void sendemail()
            {
                try
                {
                    text = "";
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (arri[i, j] == "busy")//место уже занято
                            {
                                text += "Rida: " + (i + 1) + "; Koht: " + (j + 1) + "<br>";
                            }

                        }
                    }
                    string emaill = "";//отправляем билет на почту
                    ShowInputDialog(ref emaill);
                    MailAddress from = new MailAddress("aani66407@gmail.com", "COCA-COLA PLAZA");
                    MailAddress to = new MailAddress(emaill);
                    MailMessage mael = new MailMessage(from, to);
                    mael.Subject = "COCA-COLA PLAZA";
                    //тело
                    mael.Body = "<h1>Teie pilet</h1>" + "<h1>Pealkiri oma valitud filmi:</h1>" + filmid + "<h2>Teie koht:</h2>" + text;
                    mael.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.Credentials = new NetworkCredential("aani66407@gmail.com", "janika12345");
                    smtp.EnableSsl = true;
                    smtp.Send(mael);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Veateade");
                }
            }

            static DialogResult ShowInputDialog(ref string input)//Dynamic creation of a dialog box. You can customize to your taste
            {
                System.Drawing.Size size = new System.Drawing.Size(200, 70);
                Form inputBox = new Form();

                inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                inputBox.ClientSize = size;
                inputBox.Text = "Email";

                System.Windows.Forms.TextBox textBox = new TextBox();
                textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
                textBox.Location = new System.Drawing.Point(5, 5);
                textBox.Text = input;
                inputBox.Controls.Add(textBox);

                Button okButton = new Button();
                okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
                okButton.Name = "okButton";
                okButton.Size = new System.Drawing.Size(75, 23);
                okButton.Text = "&OK";
                okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
                inputBox.Controls.Add(okButton);

                Button cancelButton = new Button();
                cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                cancelButton.Name = "cancelButton";
                cancelButton.Size = new System.Drawing.Size(75, 23);
                cancelButton.Text = "&Cancel";
                cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
                inputBox.Controls.Add(cancelButton);

                inputBox.AcceptButton = okButton;
                inputBox.CancelButton = cancelButton;

                DialogResult result = inputBox.ShowDialog();
                input = textBox.Text;
                return result;
            }
        }

        public Form1(string Name)//открывается форма с фильмом
        {
            filmid = Name;
            InitializeComponent();

        }

        void Form1_Click(object sender, EventArgs e)
        {
            int[] I = { }, J = { };
            var label = (Label)sender;
            var tag = (int[])label.Tag;
            if (_arr[tag[0], tag[1]].Image == red)
            {
                MessageBox.Show("Kas see koht on kinni!");
                /*Место уже занято*/
            }
            else
            {
                _arr[tag[0], tag[1]].Image = yellow;
            }


        }
    }
}