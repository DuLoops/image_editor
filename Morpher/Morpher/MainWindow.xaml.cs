using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Morpher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool imgLoaded = false;
        int imgW, imgH;
        BitmapImage bImgL, bImgR;
        bool isDrawing = false;
        bool isEditing = false;
        bool editMode = false;
        List<Line> linesL, linesR;
        int lineCount, selectedLine;
        Line editSelectedLine;
        bool selectedLineXY1;

        WriteableBitmap destination;

        public MainWindow()
        {
            InitializeComponent();
            linesL = new List<Line>();
            linesR = new List<Line>();
            lineCount = 0;
        }

        private void btnLoadL_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == true)
            {
                string selectedFileName = dlg.FileName;
                imgLLabel.Content = selectedFileName;
                bImgL = new BitmapImage();
                bImgL.BeginInit();
                if (!imgLoaded)
                {
                    bImgL.UriSource = new Uri(selectedFileName);
                    bImgL.EndInit();
                    imgH = bImgL.PixelHeight;
                    imgW = bImgL.PixelWidth;
                    imgLoaded = true;
                } else
                {
                    bImgL.CacheOption = BitmapCacheOption.OnDemand;
                    bImgL.CreateOptions = BitmapCreateOptions.DelayCreation;
                    bImgL.DecodePixelHeight = imgH;
                    bImgL.DecodePixelWidth = imgW;
                    bImgL.UriSource = new Uri(selectedFileName);
                    bImgL.EndInit();
                }
                imgL.Source = bImgL;
                imgL.Visibility = Visibility.Visible;
                btnLoadL.Visibility = Visibility.Hidden;
            }

        }

        private void btnLoadR_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == true)
            {
                string selectedFileName = dlg.FileName;
                imgRLabel.Content = selectedFileName;
                bImgR = new BitmapImage();
                bImgR.BeginInit();
                if (!imgLoaded)
                {
                    bImgR.UriSource = new Uri(selectedFileName);
                    bImgR.EndInit();
                    imgH = bImgR.PixelHeight;
                    imgW = bImgR.PixelWidth;
                    imgLoaded = true;
                }
                else
                {
                    bImgR.CacheOption = BitmapCacheOption.OnDemand;
                    bImgR.CreateOptions = BitmapCreateOptions.DelayCreation;
                    bImgR.DecodePixelHeight = imgH;
                    bImgR.DecodePixelWidth = imgW;
                    bImgR.UriSource = new Uri(selectedFileName);
                    bImgR.EndInit();
                }
                imgR.Source = bImgR;
                imgR.Visibility = Visibility.Visible;
                btnLoadR.Visibility = Visibility.Hidden;
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (editMode)
            {
                editMode = false;
                btnEdit.Content = "Edit Mode";
            } else
            {
                editMode = true;
                btnEdit.Content = "Drawing Mode";
            }
        }

        private void drawLine(Point location)
        {
            linesL.Add(new Line
            {
                Visibility = System.Windows.Visibility.Visible,
                StrokeThickness = 4,
                Stroke = System.Windows.Media.Brushes.Black,
                X1 = location.X,
                Y1 = location.Y,
                X2 = location.X,
                Y2 = location.Y
            }
                );

            linesR.Add(new Line
            {
                Visibility = System.Windows.Visibility.Visible,
                StrokeThickness = 4,
                Stroke = System.Windows.Media.Brushes.Black,
                X1 = location.X,
                Y1 = location.Y,
                X2 = location.X,
                Y2 = location.Y
            }
                );
        }

        private bool checkIfSelected(Line line, Point location)
        {
            int difference = 10;
            if (Math.Abs(line.X1 - location.X) < difference &&
                Math.Abs(line.Y1 - location.Y) < difference) {
                selectedLineXY1 = true;
                return true;
            } else if (Math.Abs(line.X2 - location.X) < difference &&
                Math.Abs(line.Y2 - location.Y) < difference)
            {
                selectedLineXY1 = false;
                return true;
            } else
            {
                return false;
            }
        }
        private void mouseDownL(object sender, MouseButtonEventArgs e)
        {
            Point location = e.MouseDevice.GetPosition(canvasL);

            if (!editMode)
            {
                drawLine(location);
                canvasL.Children.Add(linesL[lineCount]);
                canvasR.Children.Add(linesR[lineCount]);

                isDrawing = true;
            } else
            {
                foreach (Line selectedLine in linesL)
                {
                    if (checkIfSelected(selectedLine, location))
                    {
                        editSelectedLine = selectedLine;
                        isEditing = true;
                    }
                }
            }

        }

        private void mouseMoveL(object sender, MouseEventArgs e)
        {
            Point location = e.MouseDevice.GetPosition(canvasL);

            if (isDrawing)
            {
                linesL[lineCount].X2 = location.X;
                linesL[lineCount].Y2 = location.Y;

                linesR[lineCount].X2 = location.X;
                linesR[lineCount].Y2 = location.Y;
            }
            if (isEditing)
            {
                if (selectedLineXY1)
                {
                    editSelectedLine.X1 = location.X;
                    editSelectedLine.Y1 = location.Y;
                } else
                {
                    editSelectedLine.X2 = location.X;
                    editSelectedLine.Y2 = location.Y;
                }
            }
        }

        private void mouseUpL(object sender, MouseButtonEventArgs e)
        {
            Point location = e.MouseDevice.GetPosition(canvasL);

            if (isDrawing)
            {
                linesL[lineCount].X2 = location.X;
                linesL[lineCount].Y2 = location.Y;
                linesR[lineCount].X2 = location.X;
                linesR[lineCount].Y2 = location.Y;
                isDrawing = false;
                lineCount++;
            }
            if (isEditing)
            {
                if (selectedLineXY1)
                {
                    editSelectedLine.X1 = location.X;
                    editSelectedLine.Y1 = location.Y;
                }
                else
                {
                    editSelectedLine.X2 = location.X;
                    editSelectedLine.Y2 = location.Y;
                }
                isEditing = false;
            }
        }

       

        private void mouseDownR(object sender, MouseButtonEventArgs e)
        {
            Point location = e.MouseDevice.GetPosition(canvasR);

            if (!editMode)
            {
                drawLine(location);
                canvasR.Children.Add(linesR[lineCount]);
                canvasL.Children.Add(linesL[lineCount]);

                isDrawing = true;
            }
            else
            {
                
                foreach (Line selectedLine in linesR)
                {
                    if (checkIfSelected(selectedLine, location))
                    {
                        editSelectedLine = selectedLine;
                        isEditing = true;
                    }
                }
                
            }

        }

        private void mouseMoveR(object sender, MouseEventArgs e)
        {
            Point location = e.MouseDevice.GetPosition(canvasR);

            if (isDrawing)
            {
                linesL[lineCount].X2 = location.X;
                linesL[lineCount].Y2 = location.Y;

                linesR[lineCount].X2 = location.X;
                linesR[lineCount].Y2 = location.Y;
            } else if (isEditing)
            {
                if (selectedLineXY1)
                {
                    editSelectedLine.X1 = location.X;
                    editSelectedLine.Y1 = location.Y;
                }
                else
                {
                    editSelectedLine.X2 = location.X;
                    editSelectedLine.Y2 = location.Y;
                }
            }

        }

        private void mouseUpR(object sender, MouseButtonEventArgs e)
        {
            Point location = e.MouseDevice.GetPosition(canvasR);

            if (isDrawing)
            {
                linesL[lineCount].X2 = location.X;
                linesL[lineCount].Y2 = location.Y;

                linesR[lineCount].X2 = location.X;
                linesR[lineCount].Y2 = location.Y;
                isDrawing = false;
                lineCount++;
            } else if (isEditing)
            {
                if (selectedLineXY1)
                {
                    editSelectedLine.X1 = location.X;
                    editSelectedLine.Y1 = location.Y;
                }
                else
                {
                    editSelectedLine.X2 = location.X;
                    editSelectedLine.Y2 = location.Y;
                }
                isEditing = false;
            }
        }
        private byte[] BitmapSourceToArray(BitmapSource bitmapSource)
        {
            // Stride = (width) x (bytes per pixel)
            int stride = (int)bitmapSource.PixelWidth * (bitmapSource.Format.BitsPerPixel / 8);
            byte[] pixels = new byte[bitmapSource.PixelHeight * stride];

            bitmapSource.CopyPixels(pixels, stride, 0);

            //---
/*            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            //encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            encoder.QualityLevel = 100;
            // byte[] bit = new byte[0];
            using (MemoryStream stream = new MemoryStream())
            {
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                encoder.Save(stream);
                byte[] bit = stream.ToArray();
                stream.Close();
            }


            //-------

            byte[] bArrayImg;
            JpegBitmapEncoder encoder2 = new JpegBitmapEncoder();

            encoder2.Frames.Add(BitmapFrame.Create(bitmapSource));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder2.Save(ms);
                bArrayImg = ms.ToArray();
            }
*/

            return pixels;
        }

        private void morph()
        {
           

            destination = new WriteableBitmap(bImgL);

            byte[] sourcePixelByte = BitmapSourceToArray(bImgL);
            byte[] rgbVal = new byte[4];
            Int32Rect rect;

            Vector T_;
            Vector weightedSourcePoint;
            morphing morpher = new morphing();
            List<Vector> _x;
            double[] w = new double[linesL.Count];
            double weightSum;
            Vector P, Q, T, P_, Q_;

            for (int i = 0; i < destination.PixelWidth; i++)
            {
                for (int j = 0; j < destination.PixelHeight; j++)
                {
                    weightedSourcePoint = new Vector(0, 0);
                    weightSum = 0;
                    _x = new List<Vector>(linesL.Count);

                    for (int lineIndex = 0; lineIndex < linesL.Count; lineIndex++)
                    {
                        P = new Vector(linesR[lineIndex].X1, linesR[lineIndex].Y1); // Destination (right side)
                        Q = new Vector(linesR[lineIndex].X2, linesR[lineIndex].Y2);
                        T = new Vector(i, j); // Destination

                        P_ = new Vector(linesL[lineIndex].X1, linesL[lineIndex].Y1); //source
                        Q_ = new Vector(linesL[lineIndex].X2, linesL[lineIndex].Y2);

                        T_ = morpher.getSourceP(P, Q, T, P_, Q_);
                        //_x.Add(new Vector(T_.X - T.X, T_.Y - T.Y));
                        _x.Add(new Vector(T_.X, T_.Y));
                        w[lineIndex] = morpher.getWeight();
                        weightSum += w[lineIndex];
                    }

                    for (int lineIndex = 0; lineIndex < linesL.Count; lineIndex++)
                    {
                        weightedSourcePoint.X += w[lineIndex] * _x[lineIndex].X;
                        weightedSourcePoint.Y += w[lineIndex] * _x[lineIndex].Y;
                    }

                    weightedSourcePoint.X /= weightSum;
                    weightedSourcePoint.Y /= weightSum;

                    weightedSourcePoint.X = (int)Math.Clamp(weightedSourcePoint.X, 0, destination.PixelWidth - 1);
                    weightedSourcePoint.Y = (int)Math.Clamp(weightedSourcePoint.Y, 0, destination.PixelHeight - 1);

                    rect = new Int32Rect(i, j, 1, 1);
                    int srcOffSet = (int)(weightedSourcePoint.Y * destination.PixelWidth + weightedSourcePoint.X) * 4;
                    Buffer.BlockCopy(sourcePixelByte, srcOffSet, rgbVal, 0, 4);

                    destination.WritePixels(rect, rgbVal, 4, 0);
                }
            }

            Image img = new Image();
            RenderOptions.SetBitmapScalingMode(img, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetEdgeMode(img, EdgeMode.Aliased);
            img.Source = destination;

        }

        private void morphTest()
        {

            byte[] rgbVal = new byte[4];
            Int32Rect rect;

            Vector T_;
            Vector weightedSourcePoint = new Vector(0, 0);
            morphing morpher = new morphing();
            List<Vector> _x = new List<Vector>(2);
            double[] w = new double[2];
            double weightSum = 0;

            //#1
            Vector P = new Vector(5, 16);
            Vector Q = new Vector(1, 20);
            Vector T = new Vector(10, 10);

            Vector P_ = new Vector(1, 40);
            Vector Q_ = new Vector(5, 1);

            T_ = morpher.getSourceP(P, Q, T, P_, Q_);
            //_x.Add(new Vector(T_.X - T.X, T_.Y - T.Y));
            _x.Add(new Vector(T_.X, T_.Y));

            w[0] = morpher.getWeight();
            weightSum += w[0];

            //#2
            P = new Vector(5, 30);
            Q = new Vector(15, 35);
            T = new Vector(10, 10);

            P_ = new Vector(8, 1);
            Q_ = new Vector(40, 40);

            T_ = morpher.getSourceP(P, Q, T, P_, Q_);
            //_x.Add(new Vector(T_.X - T.X, T_.Y - T.Y));
            _x.Add(new Vector(T_.X, T_.Y));
            w[1] = morpher.getWeight();
            weightSum += w[1];


            for (int lineIndex = 0; lineIndex < 2; lineIndex++)
            {
                weightedSourcePoint.X += w[lineIndex] * _x[lineIndex].X;
                weightedSourcePoint.Y += w[lineIndex] * _x[lineIndex].Y;
            }

            weightedSourcePoint.X /= weightSum;
            weightedSourcePoint.Y /= weightSum;
            weightSum = 0;

            weightedSourcePoint.X = (int)Math.Clamp(weightedSourcePoint.X, 0, destination.PixelWidth - 1);
            weightedSourcePoint.Y = (int)Math.Clamp(weightedSourcePoint.Y, 0, destination.PixelHeight - 1);

            rect = new Int32Rect(10, 10, 1, 1);
            int srcOffSet = (int)(weightedSourcePoint.Y * destination.PixelWidth + weightedSourcePoint.X) * 4;

            destination.WritePixels(rect, rgbVal, 4, 0);
        }

        private void btnMorph_Click(object sender, RoutedEventArgs e)
        {
            //morphTest();
            morph();
            imgR.Source = destination;
        }


        private void btnJpeg_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
