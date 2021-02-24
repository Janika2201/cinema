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
    public partial class Saal : Form
    {
        int i, j;
        int x = 10;
        int y = 10;
        int p;
        Label[,] _arr;
        CheckBox[] piletide_tüübid = new CheckBox[3];
        List<string> pil_tübid = new List<string> { "täis pilet", "sooduspilet", "supersoodne pilet" };
        public Saal(int i_, int j_)
        {
            InitializeComponent();
            _arr = new Label[i_, j_];
            this.Size = new Size(i_ * 60, j_ * 60);
            this.Text = "Ap_polo_kino";
            for (i = 0; i < i_; i++)
            {
                for (j = 0; j < j_; j++)
                {
                    _arr[i, j] = new Label();
                    _arr[i, j].BackColor = Color.DarkRed;
                    _arr[i, j].Text = " Koht" + (j + 1);
                    _arr[i, j].Size = new Size(50, 50);
                    _arr[i, j].BorderStyle = BorderStyle.Fixed3D;
                    _arr[i, j].Location = new Point(j * 50 + 50, i * 50 + 50);
                    this.Controls.Add(_arr[i, j]);
                    _arr[i, j].Tag = new int[] { i, j };
                    _arr[i, j].Click += new System.EventHandler(Saalid_Click);
                }
            }
            for (int i = 0; i < piletide_tüübid.Length; i++)
            {
                piletide_tüübid[i] = new CheckBox() { Text = pil_tübid[i] };
                piletide_tüübid[i].Size = new Size(150, 40);
                piletide_tüübid[i].Font = new Font(DefaultFont.FontFamily, 14);
                piletide_tüübid[i].Location = new Point(x, y);
                x = x + 150;
                this.Controls.Add(piletide_tüübid[i]);
            }
            foreach (var item in piletide_tüübid)
            {
                item.CheckedChanged += Item_CheckedChanged;
            }
        }

        private void Saalid_Click(object sender, EventArgs e)
        {
            var label = (Label)sender;//запомникли на какую надпись нажали
            var tag = (int[])label.Tag;//определили координаты надписи
            if (_arr[tag[0], tag[1]].Text != "Kinni")
            {
                _arr[tag[0], tag[1]].Text = "Kinni";
                _arr[tag[0], tag[1]].BackColor = Color.Yellow;
            }
            else
            {
                MessageBox.Show("Pilet rida:" + (tag[0] + 1) + " Koht:" + (tag[1] + 1) + " juba ostetud!");
            }
        }

        private void Item_CheckedChanged(object sender, EventArgs e)
        {
            string message = string.Empty;
            for (int i = 0; i < piletide_tüübid.Length; i++)
            {
                if (piletide_tüübid[i].Checked && piletide_tüübid[i].Enabled)
                {
                    message += string.Format("Piletid {0} oli valitud\n", piletide_tüübid[i].Text);
                }
            }
            MessageBox.Show(message);
        }
    }
}