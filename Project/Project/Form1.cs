using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
namespace Project
{
    public partial class Form1 : Form
    {
        Label[] la;
        PictureBox[] pb;
        public Form1()
        {
            InitializeComponent();
            la = new Label[]{label1,label2,label3,label4, label5,label6,label7,label8,label9,label10,label11,                           //지역명(1~11)
                            label12,label13,label14,label15,label16,label17,label18,label19,label20,label21,label22,label23};                                   //온도(12~22)
            pb = new PictureBox[]{pictureBox1,pictureBox2,pictureBox3,pictureBox4,pictureBox5,pictureBox6,pictureBox7,pictureBox8,pictureBox9,pictureBox10,
                                  pictureBox11};
            Imageinitialize();            
        }
        void Imageinitialize()
        {            
            for (int i = 0; i < 23; i++)
            {
                var pos = this.PointToScreen(la[i].Location);
                pos = bg.PointToClient(pos);
                la[i].Parent = bg;
                la[i].Location = pos;
                la[i].BackColor = Color.Transparent;
            }           
        }
        void func(string url, Label lb,PictureBox pb)
        {
           
            Image img1 = new Bitmap("맑음.jpg");
            Image img2 = new Bitmap("흐림.jpg");
            Image img3 = new Bitmap("구름 조금.jpg");
            Image img4 = new Bitmap("비.jpg");
            XElement xElement = XElement.Load(url);
            var xmlQuery1 = from item in xElement.Descendants("pubDate")
                           select new Today
                           {
                               today = item.Value
                           };
            label23.Text = "기준 "+xmlQuery1.ElementAt(0).today;
            var xmlQuery = from item in xElement.Descendants("data")
                           select new Weather
                           {
                               Temp = item.Element("temp").Value,
                               WfKor = item.Element("wfKor").Value
                           };
            lb.Text = xmlQuery.ElementAt(0).Temp + " C";
            string Text = xmlQuery.ElementAt(0).WfKor;
            if ((Text).Equals("맑음")) pb.Image = img1;
            else if ((Text).Equals("흐림")) pb.Image = img2;
            else if ((Text).Equals("구름 조금") || (Text).Equals("구름 많음")) pb.Image = img3;
            else if ((Text).Equals("비")) pb.Image = img4;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string[] url = new String[]{"http://www.kma.go.kr/wid/queryDFSRSS.jsp?zone=1168066000",         //서울
                                        "http://www.kma.go.kr/wid/queryDFSRSS.jsp?zone=4211067500",         //춘천
                                        "http://www.kma.go.kr/wid/queryDFSRSS.jsp?zone=4215061500",         //강릉
                                        "http://www.kma.go.kr/wid/queryDFSRSS.jsp?zone=3023052000",         //대전
                                        "http://www.kma.go.kr/wid/queryDFSRSS.jsp?zone=4311133000",         //청주
                                        "http://www.kma.go.kr/wid/queryDFSRSS.jsp?zone=2720065000",         //대구
                                        "http://www.kma.go.kr/wid/queryDFSRSS.jsp?zone=2920054000",         //광주
                                        "http://www.kma.go.kr/wid/queryDFSRSS.jsp?zone=4511358000",         //전주
                                        "http://www.kma.go.kr/wid/queryDFSRSS.jsp?zone=2644058000",         //부산
                                        "http://www.kma.go.kr/wid/queryDFSRSS.jsp?zone=5013025300",         //제주도
                                        "http://www.kma.go.kr/wid/queryDFSRSS.jsp?zone=4794033000"          //울릉/독도  
            };
            for (int i = 0,j=11; i < 11&&j<22; i++,j++)
            {
                func(url[i],la[j],pb[i]);
            }
        }
        private Color c= Color.Black;
        private int count =0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (count == 0) c = Color.Red;
            else if (count == 1) c = Color.Green;
            else if (count == 2) c = Color.Black;
            else if (count == 3) c = Color.White;
            else if (count == 4) c = Color.Blue;
            else if (count == 5) c = Color.LightGray;
            else if (count == 6)
            {
                count = 0;
                c = Color.LightBlue;
            }
            this.BackColor = c;
            count++;
        }
        private void colorButton_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;            
        }

    }

}
