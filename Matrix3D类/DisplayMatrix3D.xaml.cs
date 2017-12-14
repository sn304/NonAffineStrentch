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

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace Matrix3D类
{
    public sealed partial class DisplayMatrix3D : UserControl
    {
        public DisplayMatrix3D()
        {
            this.InitializeComponent();
        }
        static DependencyProperty matrix3Dproperty = DependencyProperty.Register("Matrix3D", typeof(Matrix3D), typeof(DisplayMatrix3D), new PropertyMetadata(Matrix3D.Identity, OnMatrixpropertychanged));
        private static void OnMatrixpropertychanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as DisplayMatrix3D).Onpropertychanged(e);
        }
    public    Matrix3D Matrix3D { get { return (Matrix3D)GetValue(matrix3Dproperty); } set { SetValue(matrix3Dproperty, value); } }
        private void Onpropertychanged(DependencyPropertyChangedEventArgs e)
        {
            m11.Text = this.Matrix3D.M11.ToString("f3");
            m12.Text = this.Matrix3D.M12.ToString("f3");
            m13.Text = this.Matrix3D.M13.ToString("f3");
            m14.Text = this.Matrix3D.M14.ToString("f6");

            m21.Text = this.Matrix3D.M21.ToString("f3");
            m22.Text = this.Matrix3D.M22.ToString("f3");
            m23.Text = this.Matrix3D.M23.ToString("f3");
            m24.Text = this.Matrix3D.M24.ToString("f6");

            m31.Text = this.Matrix3D.M31.ToString("f3");
            m32.Text = this.Matrix3D.M32.ToString("f3");
            m33.Text = this.Matrix3D.M33.ToString("f3");
            m34.Text = this.Matrix3D.M34.ToString("f6");

            m41.Text = this.Matrix3D.OffsetX.ToString("f0");
            m42.Text = this.Matrix3D.OffsetY.ToString("f0");
            m43.Text = this.Matrix3D.OffsetZ.ToString("f0");
            m44.Text = this.Matrix3D.M44.ToString("f0");


        }
    }
}
