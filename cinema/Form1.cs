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
        Button btnosta, kinni;
        StreamWriter to_file;
        bool ost = false;
        public string imagge = "";
        Image red = Image.FromFile("C:/Users/Admin/source/repos/cinema/cinema/Image/red.jpg");
        Image yellow = Image.FromFile("C:/Users/Admin/source/repos/cinema/cinema/Image/yellow.jpg");
        Image tool = Image.FromFile("C:/Users/Admin/source/repos/cinema/cinema/Image/tool.jpg");
        public List<string> attachments = new List<string>();
       
        public string name, text;
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
                    _arr[i, j].Size = new Size(100, 100);
                    _arr[i, j].BackColor = Color.Green;
                    _arr[i, j].Image = tool;


                    if (connect.State == ConnectionState.Closed)
                    {
                        connect.Open();
                        var commandStr = "SELECT a,y from " + name;
                        SqlCommand command = new SqlCommand(commandStr, connect);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (Convert.ToInt32(reader["a"]) == i && Convert.ToInt32(reader["y"]) == j)
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
            kinni.Text = "Osta Koht";
            kinni.Location = new Point(100, 400);
            kinni.Size = new Size(100, 50);
            this.Controls.Add(kinni);
            kinni.Click += Kinni_Click;

            /*
            btnosta = new Button();
            btnosta.Text = "Kinni";
            btnosta.Location = new Point(50, 200);
            btnosta.Click += Btnosta_Click;
            */
        }

        private void Btnosta_Click(object sender, EventArgs e)
        {
            Osta_Clik_Func();
        }

        void Osta_Clik_Func()
        {
            if (ost == true)
            {

                DialogResult result2 = MessageBox.Show("Вы точно хотите купить эти билеты?",
                "Покупка билета",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);
                if (result2 == DialogResult.Yes)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (_arr[i, j].Image == yellow)
                            {
                                _arr[i, j].Image = red;


                            }
                        }
                    }
                }
                if (result2 == DialogResult.No)
                {

                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (_arr[i, j].Image == yellow)
                            {
                                _arr[i, j].Image = tool;
                                _arr[i, j].Text = "" + (j + 1);
                            }

                        }
                    }
                }
            }
        }

        private void Kinni_Click(object sender, EventArgs e)
        {
            var vastus = MessageBox.Show("Are you sure of your choice", "Cinema asks", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                            arri[i, j] = "busy";
                            _arr[i, j].Image = red;
                            if (connect.State == ConnectionState.Closed)
                            {
                                connect.Open();
                                var commandStr = "INSERT Into " + name + "(a,y) values (" + i + "," + j + ")";
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
                           
                            _arr[i, j].Image = tool;
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
                            if (arri[i, j] == "busy")
                            {
                                text += "Rida: " + (i + 1) + "; Koht: " + (j + 1) + "<br>";
                            }

                        }
                    }
                    string emaill = "";
                    ShowInputDialog(ref emaill);
                    MailAddress from = new MailAddress("aani66407@gmail.com", "COCA-COLA PLAZA");
                    MailAddress to = new MailAddress(emaill);
                    MailMessage m = new MailMessage(from, to);
                    m.Subject = "Pilets";
                    m.Body = "<h1>Teie pilet</h1>" + "<h1>Filmi nimetus:</h1>" + name + "<h2>Koht:</h2>" + text;
                    m.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.Credentials = new NetworkCredential("aani66407@gmail.com", "janika12345");
                    smtp.EnableSsl = true;
                    smtp.Send(m);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Veateade");
                }
            }

            static DialogResult ShowInputDialog(ref string input)
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

        public Form1(string Name)
        {
            name = Name;
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
            }
            else
            {
                _arr[tag[0], tag[1]].Image = yellow;
            }


        }
    }
}