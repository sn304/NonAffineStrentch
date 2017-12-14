using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Media3D;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace Matrix3D类
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Rect rect = new Rect(0, 0, 320, 400);
        public MainPage()
        {
            this.InitializeComponent();
            Loaded += (s, e) => Calculatenewtransform();
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Thumb thumb = sender as Thumb;
            TransformGroup transformGroup = thumb.RenderTransform as TransformGroup;
            TranslateTransform translateTransform = transformGroup.Children[1] as TranslateTransform;
            translateTransform.X += e.HorizontalChange;
            translateTransform.Y += e.VerticalChange;
            Calculatenewtransform();
        }
        void Calculatenewtransform()
        {
            matrixprojection.ProjectionMatrix = Calculatenewtransform(rect, new Point(ult.X,ult.Y), new Point(urt.X, urt.Y), new Point(llt.X, llt.Y), new Point(lrt.X, lrt.Y));

        }
        Matrix3D Calculatenewtransform(Rect rect,Point ult,Point urt,Point llt,Point lrt)
        {
            Matrix3D S = new Matrix3D//开始矩阵把图像变换成一个单位长的正方形
            {
                M11 = 1 / rect.Width,
                M22 = 1 / rect.Height,
                OffsetX = -rect.Left / rect.Width,
                OffsetY = -rect.Top / rect.Height,
                M44 = 1
            };
            Matrix3D A = new Matrix3D//仿射变换矩阵（三个点最终点可以确定第四个点的值）由(0,0)->(x0,y0),(0,1)->(x1,y1),(1,0)->(x2,y2),(a,b)->(x3,y3)[因为是仿射变换所以(x3,y3)由其余三个点决定]
            {
                OffsetX = ult.X,
                OffsetY = ult.Y,
                M11 = urt.X - ult.X,
                M12 = urt.Y - ult.Y,
                M21 = llt.X - ult.X,
                M22 = llt.Y - ult.Y,
                M44 = 1

            };
            Matrix3D B = new Matrix3D();
            double den = A.M11 * A.M22 - A.M12 * A.M21;
            double a = (A.M22 * lrt.X - A.M21 * lrt.Y + A.M21 * A.OffsetY - A.M22 * A.OffsetX) / den;
            double b = (A.M11 * lrt.Y - A.M12 * lrt.X + A.M12 * A.OffsetX- A.M11 * A.OffsetY) / den;//反过来由矩阵计算a b的值
            B.M11 = a / (a + b - 1);//计算B矩阵，由(0,0)->(0,0),(0,1)->(0,1),(1,0)->(1,0),(1,1)->(a,b)
            B.M22 = b / (a + b - 1);
            B.M14 = B.M11 - 1;
            B.M24 = B.M22 - 1;
            B.M44 = 1;
            return S * B * A;
        }
    }
}
