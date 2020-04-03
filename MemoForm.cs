using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calender
{
    public partial class MemoForm : Form
    {
        private string title;
        private string contents;
        private string color;
        public string TitleValue
        {
            get { return title; }
            set { title = value; }
        }
        public string ContentsValue
        {
            get { return contents; }
            set { contents = value; }
        }
        public string ColorValue
        {
            get { return color; }
            set { color = value; }
        }

        public MemoForm(string title, string contents, string color)
        {
            InitializeComponent();

            titleTxtBox.Text = title;
            contentTxtBox.Text = contents;

            // color combobox 그리기
            ArrayList ColorList = new ArrayList();
            Type colorType = typeof(System.Drawing.Color);
            PropertyInfo[] propInfoList = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);
            foreach (PropertyInfo c in propInfoList)
            {
                this.colorComBox.Items.Add(c.Name);
            }
            colorComBox.SelectedItem = parseColorName(color);
        }

        private void memoCloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void memoClearBtn_Click(object sender, EventArgs e)
        {
            titleTxtBox.Text = "";
            contentTxtBox.Text = "";
            colorComBox.SelectedItem = parseColorName(Color.White.ToString());
            titleLabel.Focus();
        }

        public void memoSaveBtn_Click(object sender, EventArgs e)
        {
            if (titleTxtBox.Text == "")
            {
                MessageBox.Show("제목을 입력하세요.", "오류", MessageBoxButtons.OK);
                titleLabel.Focus();
            }
            else 
            {
                TitleValue = titleTxtBox.Text;
                ContentsValue = contentTxtBox.Text;
                ColorValue = colorComBox.Text;
                this.Close();
            }
        }

        private void memoDeleteBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("삭제 하시겠습니까?", "삭제", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                TitleValue = titleTxtBox.Text;
                ContentsValue = contentTxtBox.Text;
                this.Close();
            }
        }

        private void colorComBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Arial", 9, FontStyle.Regular);
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 110, rect.Y + 3,rect.Width - 10, rect.Height - 3);
            }
        }

        public string parseColorName(string color)
        {
            int start = color.IndexOf("[");
            int end = color.LastIndexOf("]");
            return color.Substring(start + 1, end - start - 1);
        }
    }
}
