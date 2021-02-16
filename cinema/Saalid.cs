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

        public Saalid()
        {
            InitializeComponent();

            BackgroundImage = Image.FromFile("../../Image/img.jpg");

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
            drake.Size = new System.Drawing.Size(230, 330);
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
            bob.Size = new System.Drawing.Size(230, 330);
            this.Controls.Add(bob);
            bob.Click += Bob_Click;
        }

        private void Bob_Click(object sender, EventArgs e)
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
