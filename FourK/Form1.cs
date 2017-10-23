using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace FourK
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        byte Average(byte a, byte b)
        {
            return (byte)((a + b) / 2);
        }

        public void button1_Click(object sender, EventArgs e)
        {
            string filePath;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.RestoreDirectory = true;

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                label1.Text = "Loaded";
                Bitmap highRes = new Bitmap(3840, 1080);
                Bitmap lowRes = new Bitmap(filePath);
                Bitmap ultraRes = new Bitmap(3840, 2160);
                

                for (int i = 1; i < 1080; i++)
                {
                    int diff = 0;
                    for (int j = 1; j < 1920; j++)
                    {
                        Color color1 = lowRes.GetPixel(j, i);
                        Color color2;
                        if(j == 1919)
                        {
                            color2 = lowRes.GetPixel(j, i);
                        }
                        else
                        {
                            color2 = lowRes.GetPixel(j + 1, i);
                        }
                        
                        
                        Color myColorMiddleHoriz = Color.FromArgb(Average(color1.A, color2.A), Average(color1.R, color2.R), Average(color1.G, color2.G), Average(color1.B, color2.B));
                        highRes.SetPixel(j + diff, i, color1);
                        highRes.SetPixel(j + j, i, myColorMiddleHoriz);
                        highRes.SetPixel(j + j + 1, i, color2);
                        diff++;
                    }
                }
                int differ = 0;
                for (int i = 1; i < 1080; i++)
                {
                    for (int j = 1; j < 3839; j++)
                    {
                        Color color1 = highRes.GetPixel(j, i);
                        Color color3;
                        if (j == 3840)
                        {
                            color3 = highRes.GetPixel(j, i);
                        }
                        else
                        {
                            color3 = highRes.GetPixel(j, i);
                        }
                        Color myColorMiddleHoriz = Color.FromArgb(Average(color1.A, color3.A), Average(color1.R, color3.R), Average(color1.G, color3.G), Average(color1.B, color3.B));
                        ultraRes.SetPixel(j, i + differ, color1);
                        ultraRes.SetPixel(j, i + i, myColorMiddleHoriz);
                        ultraRes.SetPixel(j, i + i + 1, color3);
                    }
                    differ++;

                }

                SaveFileDialog sfd = new SaveFileDialog();
                ImageFormat format = ImageFormat.Png;
                sfd.Filter = "Images|*.png;*.bmp;*.jpg";
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    highRes.Save(sfd.FileName, ImageFormat.Png);
                    ultraRes.Save(sfd.FileName, ImageFormat.Png);
                }
                label1.Text = "Done";
                


            }
        }
    }
}
