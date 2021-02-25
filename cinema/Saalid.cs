using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cinema
{
    public partial class Saalid : Form
    {
        private Button btnAdd = new Button();
        private Button btnAdd1 = new Button();
        private Button btn = new Button();

        private PictureBox pic = new PictureBox();
        private PictureBox pic1 = new PictureBox();
        private PictureBox pi = new PictureBox();

        private PictureBox icarly = new PictureBox();
        private PictureBox drake = new PictureBox();
        private PictureBox bob = new PictureBox();
        private static Image Image;
        private LinkLabel label3 = new LinkLabel();//открывается ссылка на фильм и можно о нем все узнать прежде чем идти в кино
        private LinkLabel label = new LinkLabel();//открывается ссылка на фильм и можно о нем все узнать прежде чем идти в кино
        private LinkLabel label2 = new LinkLabel();//открывается ссылка на фильм и можно о нем все узнать прежде чем идти в кино
        /*private Label label3 = new Label();*/
        private LinkLabel label4 = new LinkLabel();//открывается ссылка на фильм и можно о нем все узнать прежде чем идти в кино
        private LinkLabel label5 = new LinkLabel();//открывается ссылка на фильм и можно о нем все узнать прежде чем идти в кино
        private LinkLabel label6 = new LinkLabel();//открывается ссылка на фильм и можно о нем все узнать прежде чем идти в кино



        public Saalid()
        {
            InitializeComponent();
            BackgroundImageLayout = ImageLayout.Stretch;
            BackgroundImage = Image.FromFile("../../Image/karl1.jpg");//фон


            label.Location = new System.Drawing.Point(145, 9);//lanel который открывает ссылку на фильм
            label.Size = new Size(48, 13);
            label.Text = "Джокер";
            this.Controls.Add(label);
            label.LinkClicked += Label_LinkClicked;

            label2.Location = new System.Drawing.Point(467, 9);
            label2.Size = new Size(48, 13);
            label2.Text = "Приво";
            this.Controls.Add(label2);
            label2.LinkClicked += Label2_LinkClicked;

            label3.Location = new System.Drawing.Point(777, 9);
            label3.Size = new Size(48, 13);
            label3.Text = "Побег";
            this.Controls.Add(label3);
            label3.LinkClicked += Label3_LinkClicked;
            

            label4.Location = new System.Drawing.Point(144, 381);
            label4.Size = new Size(48, 13);
            label4.Text = "Дрейк";
            this.Controls.Add(label4);
            label4.LinkClicked += Label4_LinkClicked;

            label5.Location = new System.Drawing.Point(472, 383);
            label5.Size = new Size(48, 13);
            label5.Text = "Айкарли";
            this.Controls.Add(label5);
            label5.LinkClicked += Label5_LinkClicked;

            label6.Location = new System.Drawing.Point(788, 374);
            label6.Size = new Size(48, 13);
            label6.Text = "Губка";
            this.Controls.Add(label6);
            label6.LinkClicked += Label6_LinkClicked;

            pi.BackColor = Color.Gray;
            pi.Location = new System.Drawing.Point(90, 25);
            pi.Image = Image.FromFile("../../Image/joker.jpg");
            pi.Size = new System.Drawing.Size(230, 330);
            this.Controls.Add(pi);
            pi.Click += Pi_Click;

            pic.BackColor = Color.Gray;
            pic.Location = new System.Drawing.Point(400, 25);
            pic.Image = Image.FromFile("../../Image/pritvorstvo.jpg");
            pic.Size = new System.Drawing.Size(230, 330);
            this.Controls.Add(pic);
            pic.Click += Pic_Click;

            pic1.BackColor = Color.Gray;
            pic1.Location = new System.Drawing.Point(700, 25);
            pic1.Image = Image.FromFile("../../Image/pobegg.jpg");
            pic1.Size = new System.Drawing.Size(230, 330);
            this.Controls.Add(pic1);
            pic1.Click += Pic1_Click;

            drake.BackColor = Color.Gray;
            drake.Location = new System.Drawing.Point(90, 400);
            drake.Image = Image.FromFile("../../Image/drake.jpg");
            drake.Size = new System.Drawing.Size(230, 320);
            this.Controls.Add(drake);
            drake.Click += Drake_Click;


            icarly.BackColor = Color.Gray;
            icarly.Location = new System.Drawing.Point(400, 400);
            icarly.Image = Image.FromFile("../../Image/icarly.jpg");
            icarly.Size = new System.Drawing.Size(230, 330);
            this.Controls.Add(icarly);
            icarly.Click += Icarly_Click;

            bob.BackColor = Color.Gray;
            bob.Location = new System.Drawing.Point(700, 400);
            bob.Image = Image.FromFile("../../Image/bob.jpg");
            bob.Size = new System.Drawing.Size(210, 330);
            this.Controls.Add(bob);
            bob.Click += Bob_Click;
        }

        private void Label6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//ссылка которая показывает все о фильме
        {
            System.Diagnostics.Process.Start("https://www.kinopoisk.ru/film/944043/"); 
        }

        private void Label5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.kinopoisk.ru/series/402528/");
        }

        private void Label4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.kinopoisk.ru/film/371100/");
        }

        private void Label2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.kinopoisk.ru/series/1179113/");
        }

        private void Label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.kinopoisk.ru/film/1048334/");
        }

        private void Label3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.kino-teatr.ru/kino/movie/hollywood/series/22637/annot/");
        }

        private void Bob_Click(object sender, EventArgs e)//берем фильм из базы данных и он открывает нам другую форму 
        {
            Form1 form1 = new Form1("Bob");
            form1.Show();
        }

        private void Icarly_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1("Icarly");
            form1.Show();
        }

        private void Drake_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1("Drake");
            form1.Show();
        }

        private void Pic1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1("Pobeg");
            form1.Show();
        }

        private void Pi_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1("Joker");
            form1.Show();
        }

        private void Pic_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1("Pritvorstvo");
            form1.Show();
        }
    }
}
