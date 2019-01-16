using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace XmlOkuma
{
    public partial class Form1 : Form
    {
        Bitmap image; 
        string base64Text; 

        public int[] a= new int[7];
        
        public double toplam,x,y,z;
        public Form1()
        {
            InitializeComponent();
            btnolustur.Click += Btnolustur_Click;
            btnoku.Click += Btnoku_Click;
            btnkdv.Click += Btnkdv_Click;
            btnresim.Click += Btnresim_Click;
        }

        private void Btnresim_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG" +
            "|All files(*.*)|*.*";
            dialog.CheckFileExists = true;
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName);
               

                byte[] imageArray = System.IO.File.ReadAllBytes(dialog.FileName);
                base64Text = Convert.ToBase64String(imageArray); //base64Text must be global but I'll use  richtext
                txtresim.Text = base64Text;
            }
        }

      
        private void Btnkdv_Click(object sender, EventArgs e)
        {
            txtkdv.Text = Convert.ToString(Math.Ceiling(((toplam + x + y + z) * 118) / 100));
        }

        private void Btnoku_Click(object sender, EventArgs e)
        {
            
            using (OpenFileDialog of = new OpenFileDialog())
            {
                of.Filter = "XML Dosyaları|*.xml";
                if (of.ShowDialog() == DialogResult.OK)
                {
                    
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(of.FileName);
                    { 
                    XmlNodeList list = xmldoc.SelectNodes("//Gruplar/A_Grubu");

                    foreach (XmlNode A in list)
                    {

                        string b1= A["A1"].InnerText;
                        string b2= A["A2"].InnerText;
                        string b3= A["A3"].InnerText;
                        string b4= A["A4"].InnerText;
                        string b5= A["A5"].InnerText;
                        string b6= A["A6"].InnerText;
                        string b7= A["A7"].InnerText;
                        
                        a[0] = Convert.ToInt32(b1.ToString());
                        a[1] = Convert.ToInt32(b2.ToString());
                        a[2] = Convert.ToInt32(b3.ToString());
                        a[3] = Convert.ToInt32(b4.ToString());
                        a[4] = Convert.ToInt32(b5.ToString());
                        a[5] = Convert.ToInt32(b6.ToString());
                        a[6] = Convert.ToInt32(b7.ToString());
                        for (int i = 0; i < 7; i++)
                        {
                            toplam = toplam + a[i];
                        }
                        txtokunanA.Text = Convert.ToString(toplam);
                       
                    };
                    }
                    { 
                    XmlNode B = xmldoc.SelectSingleNode("//Gruplar/B_Grubu");
                        string b1 = B["B"].InnerText;
                        x = Convert.ToInt32(b1.ToString());
                        txtokunanB.Text = B["B"].InnerText;
                    }
                    {
                        XmlNode C = xmldoc.SelectSingleNode("//Gruplar/C_Grubu");
                        string b1 = C["C"].InnerText;
                        y = Convert.ToInt32(b1.ToString());
                        txtokunanC.Text = C["C"].InnerText;
                    }
                    {
                        XmlNode D = xmldoc.SelectSingleNode("//Gruplar/D_Grubu");
                        string b1 = D["D"].InnerText;
                        z = Convert.ToInt32(b1.ToString());
                        txtokunanD.Text = D["D"].InnerText;
                    }
                }
            }
        }
        private void Btnolustur_Click(object sender, EventArgs e)
        {
            foreach (Control c in groupBox1.Controls)
            {
                if (c is TextBox)
                {
                    if (c.Text == "")
                    {
                        return;
                    }
                }
            }

            XmlDocument xmldoc = new XmlDocument();
            XmlNode root = xmldoc.CreateElement("Gruplar");
            xmldoc.AppendChild(root);

            XmlNode A = xmldoc.CreateElement("A_Grubu");

            XmlNode A1 = xmldoc.CreateElement("A1");
            XmlNode A2 = xmldoc.CreateElement("A2");
            XmlNode A3 = xmldoc.CreateElement("A3");
            XmlNode A4 = xmldoc.CreateElement("A4");
            XmlNode A5 = xmldoc.CreateElement("A5");
            XmlNode A6 = xmldoc.CreateElement("A6");
            XmlNode A7 = xmldoc.CreateElement("A7");
            XmlNode B = xmldoc.CreateElement("B_Grubu");
            XmlNode B1 = xmldoc.CreateElement("B");
            XmlNode C = xmldoc.CreateElement("C_Grubu");
            XmlNode C1 = xmldoc.CreateElement("C");
            XmlNode D = xmldoc.CreateElement("D_Grubu");
            XmlNode D1 = xmldoc.CreateElement("D");


            A1.InnerText = txtA1.Text;
            A2.InnerText = txtA2.Text;
            A3.InnerText = txtA3.Text;
            A4.InnerText = txtA4.Text;
            A5.InnerText = txtA5.Text;
            A6.InnerText = txtA6.Text;
            A7.InnerText = txtA7.Text;
            B1.InnerText = txtB1.Text;
            C1.InnerText = txtC1.Text;
            D1.InnerText = txtD1.Text;


            A.AppendChild(A1);
            A.AppendChild(A2);
            A.AppendChild(A3);
            A.AppendChild(A4);
            A.AppendChild(A5);
            A.AppendChild(A6);
            A.AppendChild(A7);
            B.AppendChild(B1);
            C.AppendChild(C1);
            D.AppendChild(D1);

            root.AppendChild(A);
            root.AppendChild(B);
            root.AppendChild(C);
            root.AppendChild(D);

            using (SaveFileDialog sf = new SaveFileDialog())
            {
                sf.Filter = "XML Dosyası|*.xml";
                if (sf.ShowDialog() == DialogResult.OK)
                {
                    xmldoc.Save(sf.FileName);
                    MessageBox.Show($"{sf.FileName} dosyası oluşturuldu.");
                }
            }
        }



    }
}
