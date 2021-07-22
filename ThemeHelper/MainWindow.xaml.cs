using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
using System.Windows.Threading;
using TNO.Themes;
using TNO.Themes.Transformations;

namespace ThemeHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Properties
        private bool IsTransformValid = true;
        private bool IsColorValid = true;
        private TransformationCall Transformations;
        private Color BaseColor;
        private Color ResultColor;
        DispatcherTimer timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(0.5) };
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            LoadDlls();
            ThemeManager.AddTransformationsWithReflection();

            IsTransformValid = true;
            BaseColor = Parse(txtBase.Text);
            string part = txtTransform.Text;
            if (!part.StartsWith('.'))
                part = "Base." + part;
            else
                part = "Base" + part;
            CheckTransform(part);

            ApplyTransforms();
            UpdateResult();
            timer.Stop();

            timer.Tick += Timer_Tick;

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            try
            {
                string part = txtTransform.Text;
                if (!part.StartsWith("."))
                    part = "Base." + part;
                else
                    part = "Base" + part;

                CheckTransform(part);

                IsTransformValid = true;

                if (IsColorValid)
                {
                    ApplyTransforms();
                    UpdateResult();
                }
            }
            catch
            {
                IsTransformValid = false;
            }
        }

        #region Events
        private void txtTransform_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsTransformValid = false;
            timer.Stop();
            if (txtTransform.Text.Length > 0)
                timer.Start();
        }
        private void txtBase_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (brdBase == null || brdResult == null || txtResult == null) return;
            IsColorValid = false;
            if (txtBase.Text.Length > 0)
            {
                try
                {
                    BaseColor = Parse(txtBase.Text);
                    IsColorValid = true;
                    brdBase.Background = ToBrush(BaseColor);
                    if (IsTransformValid)
                    {
                        ApplyTransforms();
                        UpdateResult();
                        timer.Stop();
                    }
                }
                catch
                {
                    IsColorValid = false;
                }
            }

        }
        private void txtTransform_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && txtTransform.Text.Length > 0)
            {
                IsTransformValid = false;
                try
                {
                    string part = txtTransform.Text;
                    if (!part.StartsWith("."))
                        part = "Base." + part;
                    else
                        part = "Base" + part;

                    CheckTransform(part);

                    IsTransformValid = true;

                    if (IsColorValid)
                    {
                        ApplyTransforms();
                        UpdateResult();
                        timer.Stop();
                    }
                    

                }
                catch
                {
                    IsTransformValid = false;
                    MessageBox.Show("This transform is invalid");
                }
            }
        }
        #endregion

        #region Methods
        private void LoadDlls()
        {
            if (!Directory.Exists("transforms"))
                Directory.CreateDirectory("transforms");

            string[] files = Directory.GetFiles("transforms", "*.dll");
            foreach (string file in files)
            {
                try
                {
                    Assembly.LoadFrom(file);
                }
                catch(Exception e)
                {
                    Debug.WriteLine($"Error: {e}");
                }
            }
        }
        private Color Parse(string col) => (Color)ColorConverter.ConvertFromString(col);
        private SolidColorBrush ToBrush(Color c) => new SolidColorBrush(c);
        private string Hex(Color col, bool includeAlpha)
        {
            string s = "#";
            if (includeAlpha) s += $"{col.A:x2}";
            return s + $"{col.R:x2}{col.G:x2}{col.B:x2}";
        }
        private void UpdateResult()
        {
            brdResult.Background = ToBrush(ResultColor);
            txtResult.Text = Hex(ResultColor, BaseColor.A != ResultColor.A);
        }
        private void CheckTransform(string transform)
        {
            Transformations = ThemeManager.GenerateCall("Base", transform);
        }
        private void ApplyTransforms()
        {
            try
            {
                ResultColor = (Color)ThemeManager.ApplyTransformations(BaseColor, Transformations.ToApply);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        #endregion

    }
}
