﻿// Image Processing filters demo
// AForge.NET framework
// http://www.aforgenet.com/framework/
//
// Copyright © AForge.NET, 2006-2011
// contacts@aforgenet.com
//
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Imaging.Textures;
using System.ComponentModel;
using Image = System.Drawing.Image;


namespace ImageEditing
{
    public partial class Form1 : Form
    {
        Image immagine;
        private System.Drawing.Bitmap sourceImage;
        private System.Drawing.Bitmap filteredImage;
        private string imagePath = "";
        //Bitmap DrawArea;
        //int x = 150;

        public Form1()
        {
            InitializeComponent();

            //DrawArea = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);

            //pictureBox1.Image = DrawArea;
            Panel_Salva.Location = new System.Drawing.Point(371, 232);          //Graphics g;
            //g = Graphics.FromImage(DrawArea);
            //g.Clear(Color.Transparent);
            //g.Dispose();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            LoadImageFromDialog();
        }

        void LoadImageFromDialog()
        {
            var dlg = new OpenFileDialog();
            dlg.Title = "Scegli l'immagine";
            dlg.Filter = "all files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Image = System.Drawing.Image.FromFile(dlg.FileName);
                imagePath = dlg.FileName;
                LoadImage();
                if( ! CheckImageIntegrity())
                {
                    pictureBox1.Image = null;
                    MessageBox.Show("L'applicazione supporta solo immagini a colori", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void LoadImage()
        {
            using (OpenFileDialog imm = new OpenFileDialog() { Multiselect = false, ValidateNames = true, Filter = "all files (*.*)|*.*" })
            {
                if (imm.ShowDialog() == DialogResult.OK)
                {
                    pictureBox.Image = Image.FromFile(imm.FileName);
                    immagine = pictureBox.Image;
                }

                pictureBox.Image = System.Drawing.Image.FromFile(dlg.FileName);
                //pictureBox1.BackgroundImage = Image.FromFile(dlg.FileName);
                pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                pictureBox.Image = System.Drawing.Image.FromFile(dlg.FileName);
                
            pictureBox1.Image = System.Drawing.Image.FromFile(imagePath);
            sourceImage = (Bitmap)Bitmap.FromFile(imagePath);
        }

        bool CheckImageIntegrity()
        {
            if ((sourceImage.PixelFormat == PixelFormat.Format16bppGrayScale) ||
                        (Bitmap.GetPixelFormatSize(sourceImage.PixelFormat) > 32))
            {
                sourceImage.Dispose();
                sourceImage = null;
                return false;
            }
            else
            {
                // make sure the image has 24 bpp format
                if (sourceImage.PixelFormat != PixelFormat.Format24bppRgb)
                {
                    Bitmap temp = AForge.Imaging.Image.Clone(sourceImage, PixelFormat.Format24bppRgb);
                    sourceImage.Dispose();
                    sourceImage = temp;
                }
            }
            return true;
        }

        private void caricaImmagineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadImageFromDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Zoom.BorderStyle = default;
            Matita.BorderStyle = BorderStyle.FixedSingle;
            Gomma.BorderStyle = default;
            Testo.BorderStyle = default;
            Riempi.BorderStyle = default;
            //Graphics g;
            //g = Graphics.FromImage(DrawArea);

            //Pen mypen = new Pen(Color.Black);

            //g.DrawLine(mypen, 0, 0, 200, x);

            //pictureBox1.Image = DrawArea;

            //g.Dispose();
            //x++;
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox.SizeMode = PictureBoxSizeMode.Normal;
        }

        private void centeredToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        private void stretchedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }



        private void button1_Click(object sender, PaintEventArgs e)
        {
            var dlg = new OpenFileDialog();
            System.Drawing.Image newImage = System.Drawing.Image.FromFile(dlg.FileName);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            float x = 0.0F;
            float w = 0.0F;
            float a = 0.0F;
            float y = 0.0F;


            x = float.Parse(textBox1.Text);

            label1.Text = "dimenzioni per l'asse x";
            x = float.Parse(textBox1.Text);

            label1.Text = "dimenzioni per l'asse y";
            y = float.Parse(textBox1.Text);


            x = float.Parse(textBox1.Text);

            y = float.Parse(textBox2.Text);

            a = float.Parse(textBox3.Text);


            w = float.Parse(textBox4.Text);

            RectangleF A = new RectangleF(x, y, w, a);
            GraphicsUnit h = GraphicsUnit.Pixel;


            Image newImage = pictureBox.Image;
            RectangleF A = new RectangleF(x, y, w, a);
            Image newImage = pictureBox.Image;
            RectangleF p = new RectangleF(x, y, w, a);
            newImage = ClassLibrary1.Class1.Taglia(x, y, a, w, newImage);
            pictureBox.Image = newImage; 

        }

        private void salvaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Panel_Salva.Visible = true;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string nome = textBox2.Text;
            textBox2.Clear();
            if (File.Exists(nome + "Png"))
            {
                MessageBox.Show("Nome gia usato");
                Panel_Salva.Visible = false;
                return;
            }
            pictureBox.Image.Save(nome + ".Png", ImageFormat.Png);
            MessageBox.Show("Immagine salvata");
            Panel_Salva.Visible = false;
        }

        private void Riempi_Click(object sender, EventArgs e)
        {
            Zoom.BorderStyle = default;
            Matita.BorderStyle = default;
            Gomma.BorderStyle = BorderStyle.FixedSingle;
            Testo.BorderStyle = default;
            Riempi.BorderStyle = default;
        }

        private void Zoom_Click(object sender, EventArgs e)
        {
            Zoom.BorderStyle = BorderStyle.FixedSingle;
            Matita.BorderStyle = default;
            Gomma.BorderStyle = default;
            Testo.BorderStyle = default;
            Riempi.BorderStyle = default;
        }

        private void Testo_Click(object sender, EventArgs e)
        {
            Zoom.BorderStyle = default;
            Matita.BorderStyle = default;
            Gomma.BorderStyle = default;
            Testo.BorderStyle = BorderStyle.FixedSingle;
            Riempi.BorderStyle = default;
        }

        private void Riempi_Click_1(object sender, EventArgs e)
        {
            Zoom.BorderStyle = default;
            Matita.BorderStyle = default;
            Gomma.BorderStyle = default;
            Testo.BorderStyle = default;
            Riempi.BorderStyle = BorderStyle.FixedSingle;
        }

        Image Zooom(Image img, Size size)
        {
            Bitmap bmp = new Bitmap(img, img.Width + (img.Width * size.Width / 100), img.Height + (img.Height * size.Height / 100));
            Graphics g = Graphics.FromImage(bmp);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bmp;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBar1.Value > 0)
            {
                pictureBox.Image = Zooom(immagine, new Size(trackBar1.Value, trackBar1.Value));
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (pictureBox.Image != null)
                pictureBox.Dispose();
        }
        // On Filters->Color filtering
        private void colorFiltersItem_Click(object sender, System.EventArgs e)
        {
            ApplyFilter(new ColorFiltering(new IntRange(25, 230), new IntRange(25, 230), new IntRange(25, 230)));
            //colorFiltersItem.Checked = true;
            //sta roba non funziona
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Graphics g;
            //g = Graphics.FromImage(DrawArea);

            //Pen mypen = new Pen(Brushes.Black);
            //g.DrawLine(mypen, 0, 0, 200, 200);
            //g.Clear(Color.White);
            //g.Dispose();
        }


        // Clear current image in picture box
        private void ClearCurrentImage()
        {
            // clear current image from picture box
            pictureBox.Image = null;
            // free current image
            if ((noneToolStripMenuItem.Checked == false) && (filteredImage != null))
            {
                filteredImage.Dispose();
                filteredImage = null;
            }
            // uncheck all menu items
            //foreach (MenuItem item in filtersItem.MenuItems)
            //    item.Checked = false;
        }

        // Apply filter to the source image and show the filtered image
        private void ApplyFilter(IFilter filter)
        {
            ClearCurrentImage();
           
            filteredImage = filter.Apply(sourceImage);
            
            pictureBox1.Image = filteredImage;
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearCurrentImage();
            // display source image
            pictureBox.Image = sourceImage;
            LoadImage();
            noneToolStripMenuItem.Checked = true;
            UncheckAllMenuItems();
        }

        void UncheckAllMenuItems()
        {
            rayscaleFiltersItem.Checked = false;
            sepiaToolStripMenuItem.Checked = false;
            invertToolStripMenuItem.Checked = false;
            rotateChannelToolStripMenuItem.Checked = false;
            colorFilteringToolStripMenuItem.Checked = false;
            hueModifierToolStripMenuItem.Checked = false;
            saturationAdjustingToolStripMenuItem.Checked = false;
            contrastAdjustingToolStripMenuItem.Checked = false;
            yCbCrLinearCorrectionToolStripMenuItem.Checked = false;
            thresholdBinarizationToolStripMenuItem.Checked = false;
            orderedDitheringToolStripMenuItem.Checked = false;
            sharpenToolStripMenuItem.Checked = false;
            differenceEdgeDetectorToolStripMenuItem.Checked = false;
            homogenityEdgeDetectorToolStripMenuItem.Checked = false;
            sobelEdgeDetectorToolStripMenuItem.Checked = false;
            levelsLinearCorrectionToolStripMenuItem.Checked = false;
            brightnessAdjustingToolStripMenuItem.Checked = false;
            hSLFilteringToolStripMenuItem.Checked = false;
            yCbCrFilteringToolStripMenuItem.Checked = false;
            floydSteinbergDitheringToolStripMenuItem.Checked = false;
            convolutionToolStripMenuItem.Checked = false;
            gaussianBlurToolStripMenuItem.Checked = false;
            toolStripMenuItem2.Checked = false;
            toolStripMenuItem1.Checked = false;
            jitterToolStripMenuItem.Checked = false;
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems();
            ApplyFilter(Grayscale.CommonAlgorithms.BT709);
            rayscaleFiltersItem.Checked = true;
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems();
            ApplyFilter(new Sepia());
            sepiaToolStripMenuItem.Checked = true;
        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems();
            ApplyFilter(new Invert());
            invertToolStripMenuItem.Checked = true;
        }

        private void rotateChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems();
            ApplyFilter(new RotateChannels());
            rotateChannelToolStripMenuItem.Checked = true;
        }

        private void colorFilteringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems();
            ApplyFilter(new ColorFiltering(new IntRange(25, 230), new IntRange(25, 230), new IntRange(25, 230)));
            colorFilteringToolStripMenuItem.Checked = true;
        }

        private void hueModifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems();
            ApplyFilter(new HueModifier(50));
            hueModifierToolStripMenuItem.Checked = true;
        }

        private void saturationAdjustingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems();
            ApplyFilter(new SaturationCorrection(0.15f));
            saturationAdjustingToolStripMenuItem.Checked = true;
        }

        private void contrastAdjustingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems();
            ApplyFilter(new ContrastCorrection());
            contrastAdjustingToolStripMenuItem.Checked = true;
        }

        private void yCbCrLinearCorrectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems();
            YCbCrLinear filter = new YCbCrLinear();

            filter.InCb = new Range(-0.3f, 0.3f);

            ApplyFilter(filter);
            yCbCrLinearCorrectionToolStripMenuItem.Checked = true;
        }

        private void thresholdBinarizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems();
            Bitmap originalImage = sourceImage;
            
            sourceImage = Grayscale.CommonAlgorithms.RMY.Apply(sourceImage);
            
            ApplyFilter(new Threshold());
            
            sourceImage.Dispose();
            sourceImage = originalImage;

            thresholdBinarizationToolStripMenuItem.Checked = true;
        }

        private void orderedDitheringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems();
            Bitmap originalImage = sourceImage;
            
            sourceImage = Grayscale.CommonAlgorithms.RMY.Apply(sourceImage);
            
            ApplyFilter(new OrderedDithering());
            
            sourceImage.Dispose();
            sourceImage = originalImage;

            orderedDitheringToolStripMenuItem.Checked = true;
        }

        private void sharpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems();
            ApplyFilter(new Sharpen());
            sharpenToolStripMenuItem.Checked = true;
        }

        private void differenceEdgeDetectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems();
            Bitmap originalImage = sourceImage;
            
            sourceImage = Grayscale.CommonAlgorithms.RMY.Apply(sourceImage);
            
            ApplyFilter(new DifferenceEdgeDetector());
            
            sourceImage.Dispose();
            sourceImage = originalImage;

            differenceEdgeDetectorToolStripMenuItem.Checked = true;
        }

        private void homogenityEdgeDetectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems();
            Bitmap originalImage = sourceImage;
            
            sourceImage = Grayscale.CommonAlgorithms.RMY.Apply(sourceImage);
            
            ApplyFilter(new HomogenityEdgeDetector());
            
            sourceImage.Dispose();
            sourceImage = originalImage;

            homogenityEdgeDetectorToolStripMenuItem.Checked = true;
        }

        private void sobelEdgeDetectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems();
            Bitmap originalImage = sourceImage;
            
            sourceImage = Grayscale.CommonAlgorithms.RMY.Apply(sourceImage);
            
            ApplyFilter(new SobelEdgeDetector());
            
            sourceImage.Dispose();
            sourceImage = originalImage;

            sobelEdgeDetectorToolStripMenuItem.Checked = true;
        }

        private void levelsLinearCorrectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems();
            LevelsLinear filter = new LevelsLinear();

            filter.InRed = new IntRange(30, 230);
            filter.InGreen = new IntRange(50, 240);
            filter.InBlue = new IntRange(10, 210);

            ApplyFilter(filter);
            levelsLinearCorrectionToolStripMenuItem.Checked = true;
        }

        private void brightnessAdjustingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems();
            ApplyFilter(new BrightnessCorrection());
            brightnessAdjustingToolStripMenuItem.Checked = true;
        }
            
        private void hSLFilteringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems();
            ApplyFilter(new HSLFiltering(new IntRange(330, 30), new Range(0, 1), new Range(0, 1)));
            hSLFilteringToolStripMenuItem.Checked = true;
        }

        private void yCbCrFilteringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems();
            ApplyFilter(new YCbCrFiltering(new Range(0.2f, 0.9f), new Range(-0.3f, 0.3f), new Range(-0.3f, 0.3f)));
            yCbCrFilteringToolStripMenuItem.Checked = true;
        }

        private void floydSteinbergDitheringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems();
            Bitmap originalImage = sourceImage;

            sourceImage = Grayscale.CommonAlgorithms.RMY.Apply(sourceImage);

            ApplyFilter(new FloydSteinbergDithering());

            sourceImage.Dispose();
            sourceImage = originalImage;

            floydSteinbergDitheringToolStripMenuItem.Checked = true;
        }

        private void convolutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems();
            ApplyFilter(new Convolution(new int[,] {
                                { 1, 2, 3, 2, 1 },
                                { 2, 4, 5, 4, 2 },
                                { 3, 5, 6, 5, 3 },
                                { 2, 4, 5, 4, 2 },
                                { 1, 2, 3, 2, 1 } }));
            convolutionToolStripMenuItem.Checked = true;
        }

        private void gaussianBlurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems();
            ApplyFilter(new GaussianBlur(2.0, 7));
            gaussianBlurToolStripMenuItem.Checked = true;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems();
            ApplyFilter(new Texturer(new TextileTexture(), 1.0, 0.8));
            toolStripMenuItem2.Checked = true;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems();
            ApplyFilter(new OilPainting());
            toolStripMenuItem1.Checked = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            float x = 0.0F;
            float w = 0.0F;
            float a = 0.0F;
            float y = 0.0F;

            x = float.Parse(textBox1.Text);

            y = float.Parse(textBox2.Text);

            a = float.Parse(textBox3.Text);

            w = float.Parse(textBox4.Text);

            RectangleF A = new RectangleF(x, y, w, a);

            System.Drawing.Image newImage = pictureBox1.Image;
            // ATTENZIONE: MANCA RIFERIMENTO A SYSTEM.DRAWING IN CLASSLIBRARY2
            //newImage = ClassLibrary2.Class1.Taglia(x, y, a, w, newImage);
            pictureBox1.Image = newImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!(radioButton1.Checked == true || radioButton2.Checked == true || radioButton3.Checked == true))
                return;
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var bitmap1 = Bitmap.FromFile(dlg.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                pictureBox1.Image = bitmap1;

                if (radioButton1.Checked == true)
                {
                    bitmap1.RotateFlip(RotateFlipType.Rotate90FlipX);
                    pictureBox1.Image = bitmap1;
                }

                if (radioButton2.Checked == true)
                {
                    bitmap1.RotateFlip(RotateFlipType.Rotate180FlipX);
                    pictureBox1.Image = bitmap1;
                }

                if (radioButton3.Checked == true)
                {
                    bitmap1.RotateFlip(RotateFlipType.Rotate270FlipX);
                    pictureBox1.Image = bitmap1;
                }
            }
        }

        private void jitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAllMenuItems();
            ApplyFilter(new Jitter());
            jitterToolStripMenuItem.Checked = true;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

