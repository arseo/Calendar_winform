using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using System.Globalization;

namespace calender
{
    public partial class CalendarForm : Form
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

        public CalendarForm()
        {
            InitializeComponent();
            // Calendar 날짜 그리기 (4월을 메인으로 그림)
            int endDay = DrawCalendar(2020, 4);

            List<FlowLayoutPanel> panelList = new List<FlowLayoutPanel> {
            flowLayoutPanel1, flowLayoutPanel2, flowLayoutPanel3, flowLayoutPanel4, flowLayoutPanel5, flowLayoutPanel6, flowLayoutPanel7,
            flowLayoutPanel8, flowLayoutPanel9, flowLayoutPanel10, flowLayoutPanel11, flowLayoutPanel12, flowLayoutPanel13, flowLayoutPanel14,
            flowLayoutPanel15, flowLayoutPanel16, flowLayoutPanel17, flowLayoutPanel18, flowLayoutPanel19, flowLayoutPanel20, flowLayoutPanel21,
            flowLayoutPanel22, flowLayoutPanel23, flowLayoutPanel24, flowLayoutPanel25, flowLayoutPanel26, flowLayoutPanel27, flowLayoutPanel28,
            flowLayoutPanel29, flowLayoutPanel30, flowLayoutPanel31, flowLayoutPanel32, flowLayoutPanel33, flowLayoutPanel34, flowLayoutPanel35,
            flowLayoutPanel36, flowLayoutPanel37, flowLayoutPanel38, flowLayoutPanel39, flowLayoutPanel40, flowLayoutPanel41, flowLayoutPanel42
            };
            for (int i = 0; i < panelList.Count; i++)
            {
                panelList[i].WrapContents = false;
                panelList[i].AutoScroll = true;
                panelList[i].VerticalScroll.Visible = false;
                panelList[i].Click += new System.EventHandler(this.panelClick);
            }

            // db에 있는 일정들 그리기
            fillMemoOnTheCalendar(endDay);
            //deleteMemoOnTheCalendar();
        }
        public void panelClick(object sender, EventArgs e)
        {
            FlowLayoutPanel panel = (FlowLayoutPanel)sender;
            Control[] c = Controls.Find("label" + panel.Name.Substring(15), true);
            string day = c[0].Text;

            if (day != "")      // 날 없는 칸 선택 못하게
            {
                MemoForm memoForm = new MemoForm("", "", Color.White.ToString());
                if (memoForm.ShowDialog() == DialogResult.OK)       // 메모 저장
                {
                    TitleValue = memoForm.TitleValue;
                    ContentsValue = memoForm.ContentsValue;
                    ColorValue = memoForm.ColorValue;

                    OracleConnection conn = dbConnect();
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = conn;

                    OracleTransaction STrans = null;  //오라클 트랜젝션
                    STrans = conn.BeginTransaction();
                    cmd.Transaction = STrans;  //커맨드에 트랜젝션 명시

                    string date = yearLabel.Text + "-" + monthLabel.Text + "-" + day;
                    cmd.CommandText = "INSERT INTO memo VALUES(MEMO_ID_SEQUENCE.NEXTVAL, '" + TitleValue + "', '" + ContentsValue + "', '" + date + "', '" + ColorValue + "')";
                    cmd.ExecuteNonQuery();
                    cmd.Transaction.Commit();   //커밋

                    dbClose(conn);

                    Label l = Addlabel(TitleValue, ColorValue);
                    panel.Controls.Add(l);
                    l.DoubleClick += new System.EventHandler(this.labelDoubleClick);
                    return;
                }
            }
        }

        public void labelDoubleClick(object sender, EventArgs e)
        {
            Label titleLabel = (Label)sender;
            Control parent = titleLabel.Parent;
            Control[] c = Controls.Find("label" + parent.Name.Substring(15), true);
            string day = c[0].Text;
            string date = yearLabel.Text + "-" + monthLabel.Text + "-" + day;

            // 해당 날짜 + 제목의 정보 가져와서 memoForm에 뿌려줌
            OracleConnection conn = dbConnect();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM memo WHERE memo_date = :memo_date AND title = :title";
            cmd.Parameters.Add(new OracleParameter("memo_date", date));
            cmd.Parameters.Add(new OracleParameter("title", titleLabel.Text));

            OracleDataReader reader = cmd.ExecuteReader();

            string title = "";
            string contents = "";
            Color color = titleLabel.BackColor;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    title = reader.GetString(1);
                    if (reader.GetOracleString(2).IsNull == false)
                        contents = reader.GetString(2); 
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();
            dbClose(conn);


            MemoForm memoForm = new MemoForm(title, contents, color.ToString());
            DialogResult result = memoForm.ShowDialog();
            if (result == DialogResult.OK)      //메모 수정
            {
                TitleValue = memoForm.TitleValue;
                ContentsValue = memoForm.ContentsValue;
                ColorValue = memoForm.ColorValue;

                conn = dbConnect();
                cmd = new OracleCommand();
                cmd.Connection = conn;

                OracleTransaction STrans = null;  //오라클 트랜젝션
                STrans = conn.BeginTransaction();
                cmd.Transaction = STrans;  //커맨드에 트랜젝션 명시
                cmd.CommandText = "UPDATE memo SET title = '" + TitleValue + "', memo_contents = '" + ContentsValue + "', color = '" + ColorValue + "' WHERE title = '" + title + "' AND memo_date = '" + date + "'";
                cmd.ExecuteNonQuery();
                cmd.Transaction.Commit();   //커밋

                dbClose(conn);

                titleLabel.Text = TitleValue;
                titleLabel.BackColor = Color.FromName(ColorValue);
                return;
            }
            else if (result == DialogResult.No)       // 메모 삭제
            {
                TitleValue = memoForm.TitleValue;

                conn = dbConnect();
                cmd = new OracleCommand();
                cmd.Connection = conn;

                OracleTransaction STrans = null;  //오라클 트랜젝션
                STrans = conn.BeginTransaction();
                cmd.Transaction = STrans;  //커맨드에 트랜젝션 명시
                cmd.CommandText = "DELETE FROM memo WHERE title = '" + TitleValue + "' AND memo_date = '" + date + "'";
                cmd.ExecuteNonQuery();
                cmd.Transaction.Commit();   //커밋

                dbClose(conn);

                parent.Controls.Remove(titleLabel);
                titleLabel.Dispose();
            }

        }

        Label Addlabel(string titleValue, string backColor)
        {
            Label l = new Label();
            l.Text = titleValue;
            l.BackColor = Color.FromName(backColor);
            l.ForeColor = Color.Black;
            l.AutoSize = true;
            l.Margin = new Padding(3, 3, 3, 3);
            return l;
        }
        public int DrawCalendar(int year, int month)
        {
            this.monthLabel.Text = month.ToString();
            this.yearLabel.Text = year.ToString();
            List<Label> labelList = new List<Label> {
            label1, label2, label3, label4, label5, label6, label7,
            label8, label9, label10, label11, label12, label13, label14,
            label15, label16, label17, label18, label19, label20, label21,
            label22, label23, label24, label25, label26, label27, label28,
            label29, label30, label31, label32, label33, label34, label35,
            label36, label37, label38, label39, label40, label41, label42
            };

            //이전에 있던 내용 지우기
            for (int i = 0; i < labelList.Count; i++)
            {
                labelList[i].Text = "";
            }

            //날짜 계산
            int start_day = 0;      // 0=일요일, 1=월요일......
            int[] days_a_month = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            //2월의 윤년 계산
            if ((year % 4 == 0) && !(year % 100 == 0) || year % 400 == 0) days_a_month[1] = 29;

            //년 계산
            for (int i = 1; i < year; i++)
            {
                if ((i % 4 == 0) && !(i % 100 == 0) || i % 400 == 0)
                    start_day += 366;
                else
                    start_day += 365;
            }

            //월 계산
            for (int i = 0; i < month - 1; i++)
            {
                start_day += days_a_month[i];
            }

            start_day = start_day % 7;
            for (int i = 1; i <= days_a_month[month - 1]; i++)
            {
                if (start_day == 6)
                    start_day = -1;
                labelList[start_day + i].Text = ((int)i).ToString();
            }

            return days_a_month[month - 1];
        }

        // 날짜 클릭하면 메모box 나오고 db 저장 
        //private void monthTablePanel_MouseClick(object sender, MouseEventArgs e)  
        //{
        //    int row = 0;
        //    int verticalOffset = 0;
        //    foreach (int h in monthTablePanel.GetRowHeights())
        //    {
        //        int column = 0;
        //        int horizontalOffset = 0;
        //        foreach (int w in monthTablePanel.GetColumnWidths())
        //        {
        //            Rectangle rectangle = new Rectangle(horizontalOffset, verticalOffset, w, h);
        //            if (rectangle.Contains(e.Location))             // 마우스 클릭 컬럼 위치 표시
        //            {
        //                Console.WriteLine(String.Format("row {0}, column {1} was clicked", row, column));

        //                // 자식 label 찾기
        //                Control day = monthTablePanel.GetControlFromPosition(column, row);


        //                // 메모 창 띄우기
        //                MemoForm memoForm = new MemoForm();    
        //                if (memoForm.ShowDialog() == DialogResult.OK)
        //                {
        //                    TitleValue = memoForm.TitleValue;
        //                    ContentsValue = memoForm.ContentsValue;

        //                    //OracleConnection conn = dbConnect();
        //                    //OracleCommand cmd = new OracleCommand();
        //                    //cmd.Connection = conn;
        //                    //string date = yearLabel.Text + "-" + monthLabel.Text + "-" + day.Text;
        //                    //cmd.CommandText = "insert into memo values(MEMO_ID_SEQUENCE.NEXTVAL, '" + TitleValue + "', '" + ContentsValue + "', '" + date + "')";
        //                    //cmd.ExecuteNonQuery();
        //                    //dbClose(conn);
        //                    return;
        //                }
        //            }
        //            horizontalOffset += w;
        //            column++;
        //        }
        //        verticalOffset += h;
        //        row++;
        //    }
        //}

        private void previousBtn_Click(object sender, EventArgs e)
        {
            int month = int.Parse(this.monthLabel.Text);
            int year = int.Parse(this.yearLabel.Text);
            int endDay;
            if (month == 1)
            {
                month = 12;
                this.monthLabel.Text = month.ToString();
                this.yearLabel.Text = (--year).ToString();

                endDay = DrawCalendar(year, month);
                fillMemoOnTheCalendar(endDay);
                yearLabel.Focus();
                return ;
            }
            this.monthLabel.Text = (--month).ToString();
            endDay = DrawCalendar(year, month);
            fillMemoOnTheCalendar(endDay);
            yearLabel.Focus();

        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            int month = int.Parse(this.monthLabel.Text);
            int year = int.Parse(this.yearLabel.Text);
            int endDay;
            if (month == 12)
            {
                month = 1;
                this.monthLabel.Text = month.ToString();
                this.yearLabel.Text = (++year).ToString();

                endDay = DrawCalendar(year, month);
                fillMemoOnTheCalendar(endDay);
                yearLabel.Focus();
                return;
            }
            this.monthLabel.Text = (++month).ToString();
            endDay = DrawCalendar(year, month);
            fillMemoOnTheCalendar(endDay);
            yearLabel.Focus();
        }

        public OracleConnection dbConnect()
        {
            OracleConnection conn = new OracleConnection($"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.56.101)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));User ID=calendar;Password=flak0126;");
            conn.Open();

            return conn;
            //OracleConnection pgOraConn;
            //OracleCommand pgOraCmd;
            //try
            //{
            //    pgOraConn = new OracleConnection($"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.56.101)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));User ID=calendar;Password=flak0126;");
            //    pgOraConn.Open();

            //    OracleDataAdapter adapt = new OracleDataAdapter();
            //    adapt.SelectCommand = new OracleCommand("select * from memo", pgOraConn);
            //    DataSet ds = new DataSet();
            //    adapt.Fill(ds);
            //    //dataGridView1.DataSource = ds.Tables[0].DefaultView;

            //    pgOraCmd = pgOraConn.CreateCommand();
            //    Console.WriteLine("Success DB connecion.");
            //    //MessageBox.Show("Success DB connecion.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    //pgOraConn.Close();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("DB connection fail");
            //    //MessageBox.Show($"DB connection fail.\n {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        public void dbClose(OracleConnection conn)
        {
            conn.Close();
        }

        public void fillMemoOnTheCalendar(int endDay) 
        {
            deleteMemoOnTheCalendar();  // 자식 라벨들 다 삭제
            string startDate = yearLabel.Text + "-" + monthLabel.Text + "-01";
            string endDate = yearLabel.Text + "-" + monthLabel.Text + "-" + endDay.ToString();

            OracleConnection conn = dbConnect();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM memo WHERE memo_date  between TO_DATE('" + startDate + "', 'YYYY-MM-DD') AND TO_DATE('" + endDate + "', 'YYYY-MM-DD') + 0.99999";

            OracleDataReader reader = cmd.ExecuteReader();

            List<string> titleList = new List<string>();
            List<string> contentsList = new List<string>();
            List<DateTime> dateList = new List<DateTime>();
            List<string> colorList = new List<string>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    titleList.Add(reader.GetString(1));
                    if (reader.GetOracleString(2).IsNull == false)
                        contentsList.Add(reader.GetString(2));
                    else
                        contentsList.Add("");
                    dateList.Add(reader.GetDateTime(3));
                    colorList.Add(reader.GetString(4));
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();
            dbClose(conn);

            // 라벨 생성
            for (int i = 0; i < titleList.Count; i++)
            {
                string day = dateList[i].ToString().Substring(8, 2);
                List<Label> labelList = new List<Label> {
                label1, label2, label3, label4, label5, label6, label7,
                label8, label9, label10, label11, label12, label13, label14,
                label15, label16, label17, label18, label19, label20, label21,
                label22, label23, label24, label25, label26, label27, label28,
                label29, label30, label31, label32, label33, label34, label35,
                label36, label37, label38, label39, label40, label41, label42
                };
                Control parent = new Control();
                for (int j = 0; j < labelList.Count; j++)
                {
                    if (day.Substring(0, 1) == "0")
                    {
                        day = day.Substring(1, 1);
                    }
                    if (day == labelList[j].Text)
                    {
                        parent = labelList[j].Parent;
                        break;
                    }
                }
                // 라벨 생성
                Label l = Addlabel(titleList[i], colorList[i]);
                parent.Controls.Add(l);
                l.DoubleClick += new System.EventHandler(this.labelDoubleClick);
                Console.WriteLine();
            }
        }
        // memo label 삭제
        public void deleteMemoOnTheCalendar()
        {
            List<Label> labelList = new List<Label> {
            label1, label2, label3, label4, label5, label6, label7,
            label8, label9, label10, label11, label12, label13, label14,
            label15, label16, label17, label18, label19, label20, label21,
            label22, label23, label24, label25, label26, label27, label28,
            label29, label30, label31, label32, label33, label34, label35,
            label36, label37, label38, label39, label40, label41, label42
            };
            Control parent = new Control();
            for (int i = 0; i < labelList.Count; i++)
            {
                parent = labelList[i].Parent;
                List<Control> c = GetAllLabelControls(parent);

                for (int j = 1; j < c.Count; j++)
                {
                    parent.Controls.Remove(c[j]);
                }
            }
        }
        // 자식 라벨 다 가져옴
        private List<Control> GetAllLabelControls(Control container)
        {
            List<Control> controlList = new List<Control>();
            foreach (Control c in container.Controls)
            {
                controlList.AddRange(GetAllLabelControls(c));
                if (c is Label)
                    controlList.Add(c);
            }
            return controlList;
        }
    }
}