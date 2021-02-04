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

        public Saalid()
        {
            InitializeComponent();
            btnAdd.BackColor = Color.Gray;
            btnAdd.Location = new System.Drawing.Point(90, 25);
            btnAdd.Image = Image.FromFile("../../Image/pritvorstvo.jpg");
            btnAdd.Size = new System.Drawing.Size(230, 330);
            this.Controls.Add(btnAdd);
            btnAdd.Click += BtnAdd_Click;


            btnAdd1.BackColor = Color.Gray;
            btnAdd1.Location = new System.Drawing.Point(350, 25);
            btnAdd1.Image = Image.FromFile("../../Image/djoker.jpg");
            btnAdd1.Size = new System.Drawing.Size(230, 330);
            this.Controls.Add(btnAdd1);
            btnAdd1.Click += BtnAdd1_Click;



            btn.BackColor = Color.Gray;
            btn.Location = new System.Drawing.Point(600, 25);
            btn.Image = Image.FromFile("../../Image/pobegg.jpg");
            btn.Size = new System.Drawing.Size(230, 330);
            this.Controls.Add(btn);
            btn.Click += Btn_Click;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void BtnAdd1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
