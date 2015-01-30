namespace Notey
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Media.Imaging;
    using System.Xml;
    using System.Xml.Linq;

    public partial class PadHost : Window
    {
        //dynamic d = Activator.CreateInstance(Type.GetType("Notey.LabelPad"));
        //TreeHelpers.ShallowConvert<Pad, LabelPad>(p, d);
        //this.PadHostContainer.Children.Add(d);

        private string saveFile = @".\Notey.xml";

        private bool isChangingColor;

        private bool isEditing = false;

        private bool isCopyingContent = false;

        private Pad ClickedSourcePad;

        public PadHost()
        {
            InitializeComponent();
            this.LoadPads();
        }

        public void ClearEditFocus()
        {
            this.PadHostBackground.Source = null;
            Keyboard.ClearFocus();
            this.TogglePadsVisibiliity(true);
        }

        private static BitmapSource CopyScreen()
        {
            var width = (int)SystemParameters.PrimaryScreenWidth;
            var height = (int)SystemParameters.PrimaryScreenHeight;

            using (var screenBmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                using (var bmpGraphics = Graphics.FromImage(screenBmp))
                {
                    bmpGraphics.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size(width, height));
                    return Imaging.CreateBitmapSourceFromHBitmap(
                        screenBmp.GetHbitmap(),
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                }
            }
        }

        private void LoadPads()
        {
            if (File.Exists(saveFile))
            {
                XDocument doc = XDocument.Load(saveFile);
                var pads = doc.Descendants("Notey.LabelPad");
                foreach (var pad in pads)
                {
                    LabelPad p = new LabelPad();
                    foreach (var prop in pad.Descendants())
                    {
                        switch (prop.Name.ToString())
                        {
                            case "PadSerialContent":
                                {
                                    p.PadSerialContent = prop.Value;
                                    break;
                                }
                            case "PadLeft":
                                {
                                    p.PadLeft = double.Parse(prop.Value);
                                    break;
                                }
                            case "PadTop":
                                {
                                    p.PadTop = double.Parse(prop.Value);
                                    break;
                                }
                            case "PadForecolor":
                                {
                                    p.PadForecolor = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(prop.Value);
                                    break;
                                }
                            case "PadBackcolor":
                                {
                                    p.PadBackcolor = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(prop.Value);
                                    break;
                                }
                            case "FontSize":
                                {
                                    p.FontSize = double.Parse(prop.Value);
                                    break;
                                }
                            case "FontFamily":
                                {
                                    p.FontFamily = new System.Windows.Media.FontFamily(prop.Value);
                                    break;
                                }
                            case "PadFontWeight":
                                {
                                    p.PadFontWeight = (FontWeight)new FontWeightConverter().ConvertFromString(prop.Value);
                                    break;
                                }
                            case "PadFontStyle":
                                {
                                    p.PadFontStyle = (System.Windows.FontStyle)new FontStyleConverter().ConvertFromString(prop.Value);
                                    break;
                                }
                            case "PadUnderlined":
                                {
                                    p.PadUnderlined = bool.Parse(prop.Value);
                                    break;
                                }
                            default:
                                { break; }
                        }
                    }
                    this.PadHostContainer.Children.Add(p);
                }

                pads = doc.Descendants("Notey.TimeDatePad");
                foreach (var pad in pads)
                {
                    TimeDatePad p = new TimeDatePad();
                    foreach (var prop in pad.Descendants())
                    {
                        switch (prop.Name.ToString())
                        {
                            case "PadLeft":
                                {
                                    p.PadLeft = double.Parse(prop.Value);
                                    break;
                                }
                            case "PadTop":
                                {
                                    p.PadTop = double.Parse(prop.Value);
                                    break;
                                }
                            case "PadForecolor":
                                {
                                    p.PadForecolor = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(prop.Value);
                                    break;
                                }
                            case "PadBackcolor":
                                {
                                    p.PadBackcolor = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(prop.Value);
                                    break;
                                }
                            case "FontSize":
                                {
                                    p.FontSize = double.Parse(prop.Value);
                                    break;
                                }
                            case "FontFamily":
                                {
                                    p.FontFamily = new System.Windows.Media.FontFamily(prop.Value);
                                    break;
                                }
                            case "PadFontWeight":
                                {
                                    p.PadFontWeight = (FontWeight)new FontWeightConverter().ConvertFromString(prop.Value);
                                    break;
                                }
                            case "PadFontStyle":
                                {
                                    p.PadFontStyle = (System.Windows.FontStyle)new FontStyleConverter().ConvertFromString(prop.Value);
                                    break;
                                }
                            default:
                                { break; }
                        }
                    }
                    this.PadHostContainer.Children.Add(p);
                }

                pads = doc.Descendants("Notey.ImagePad");
                foreach (var pad in pads)
                {
                    ImagePad p = new ImagePad();
                    foreach (var prop in pad.Descendants())
                    {
                        switch (prop.Name.ToString())
                        {
                            case "PadSerialContent":
                                {
                                    p.PadSerialContent = prop.Value;
                                    break;
                                }
                            case "PadLeft":
                                {
                                    p.PadLeft = double.Parse(prop.Value);
                                    break;
                                }
                            case "PadTop":
                                {
                                    p.PadTop = double.Parse(prop.Value);
                                    break;
                                }
                            case "FontSize":
                                {
                                    p.FontSize = double.Parse(prop.Value);
                                    break;
                                }
                            case "FontFamily":
                                {
                                    p.FontFamily = new System.Windows.Media.FontFamily(prop.Value);
                                    break;
                                }
                            case "ActualWidth":
                                {
                                    p.Width = double.Parse(prop.Value);
                                    break;
                                }
                            case "ActualHeight":
                                {
                                    p.Height = double.Parse(prop.Value);
                                    break;
                                }
                            default:
                                { break; }
                        }
                    }
                    this.PadHostContainer.Children.Add(p);
                }

                pads = doc.Descendants("Notey.RichPad");
                foreach (var pad in pads)
                {
                    RichPad p = new RichPad();
                    foreach (var prop in pad.Descendants())
                    {
                        switch (prop.Name.ToString())
                        {
                            case "PadSerialContent":
                                {
                                    p.PadSerialContent = prop.Value;
                                    break;
                                }
                            case "PadLeft":
                                {
                                    p.PadLeft = double.Parse(prop.Value);
                                    break;
                                }
                            case "PadTop":
                                {
                                    p.PadTop = double.Parse(prop.Value);
                                    break;
                                }
                            case "FontSize":
                                {
                                    p.FontSize = double.Parse(prop.Value);
                                    break;
                                }
                            case "FontFamily":
                                {
                                    p.FontFamily = new System.Windows.Media.FontFamily(prop.Value);
                                    break;
                                }
                            case "ActualWidth":
                                {
                                    p.Width = double.Parse(prop.Value);
                                    break;
                                }
                            case "PadForecolor":
                                {
                                    p.PadForecolor = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(prop.Value);
                                    break;
                                }
                            case "PadBackcolor":
                                {
                                    p.PadBackcolor = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(prop.Value);
                                    break;
                                }
                            default:
                                { break; }
                        }
                    }
                    this.PadHostContainer.Children.Add(p);
                }

                pads = doc.Descendants("Notey.CalendarPad");
                foreach (var pad in pads)
                {
                    CalendarPad p = new CalendarPad();
                    foreach (var prop in pad.Descendants())
                    {
                        switch (prop.Name.ToString())
                        {
                            case "PadLeft":
                                {
                                    p.PadLeft = double.Parse(prop.Value);
                                    break;
                                }
                            case "PadTop":
                                {
                                    p.PadTop = double.Parse(prop.Value);
                                    break;
                                }
                        }
                    }
                    this.PadHostContainer.Children.Add(p);
                }
            }
            else
            {
                this.ClickedSourcePad = new LabelPad() { FontSize = 72, PadForecolor = System.Windows.Media.Colors.White };
                this.ClickedSourcePad.SetDefaults();
                this.PadHostContainer.Children.Add(this.ClickedSourcePad);
            }
        }

        private void SavePads()
        {
            XmlWriterSettings xs = new XmlWriterSettings();
            xs.Indent = true;
            using (XmlWriter writer = XmlWriter.Create(saveFile, xs))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Pads");
                foreach (UIElement uie in this.PadHostContainer.Children)
                {
                    if (uie.GetType().IsSubclassOf(typeof(Pad)))
                    {
                        Pad p = ((Pad)uie);
                        writer.WriteStartElement(uie.GetType().ToString());
                        foreach (var prop in p.GetType().GetProperties())
                        {
                            string sv = "";
                            if (prop.GetValue(p, null) != null)
                            {
                                sv = prop.GetValue(p, null).ToString();
                            }
                            
                            writer.WriteElementString(prop.Name, sv);
                        }
                        writer.WriteEndElement();
                    }
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        private void SetZIndex()
        {
            if (((Keyboard.GetKeyStates(Key.LeftCtrl) | Keyboard.GetKeyStates(Key.RightCtrl)) & KeyStates.Down) > 0)
            {
                int i = 0;
                foreach(UIElement p in this.PadHostContainer.Children)
                {
                    Canvas.SetZIndex(p, i++);
                }
                Canvas.SetZIndex(this.ClickedSourcePad, Canvas.GetZIndex(this.ClickedSourcePad) + i);
            }
        }

        private void EditPad()
        {
            ((Pad)this.ClickedSourcePad).StartEdit();
            if (this.ClickedSourcePad.GetType() == typeof(LabelPad))
            {
                BitmapSource bs = CopyScreen();
                this.PadHostBackground.Source = bs;
                this.TogglePadsVisibiliity(false);
            }

            this.isEditing = false;
        }

        private void CopyPad()
        {
            ((Pad)this.ClickedSourcePad).CopyContent();
            this.isCopyingContent = false;
        }

        private void ChangeColor()
        {
            ((Pad)this.ClickedSourcePad).ChangeColor();
            this.isChangingColor = false;
        }

        private void PadHostClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.SavePads();
        }

        private void PadHostMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            UIElement uie = (UIElement)e.OriginalSource;
            this.ClickedSourcePad = TreeHelpers.FindParent<Pad>(uie);
        }

        private void PadHostContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (this.ClickedSourcePad.IsLocked)
            {
                this.MiLockPad.Header = "UnLock";
            }
            else
            {
                this.MiLockPad.Header = "Lock";
            }
        }

        private void PadHostContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            if (this.isEditing)
            {
                this.EditPad();
            }

            if (this.isCopyingContent)
            {
                this.CopyPad();
            }

            if (this.isChangingColor)
            {
                this.ChangeColor();
            }
        }

        private void PadHostMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.ClearEditFocus();
            UIElement uie = (UIElement)e.OriginalSource;
            this.ClickedSourcePad = TreeHelpers.FindParent<Pad>(uie);
            this.SetZIndex();
        }

        private void MiColorPadClick(object sender, RoutedEventArgs e)
        {
            if (this.ClickedSourcePad != null)
            {
                this.isChangingColor = true;
            }
        }

        private void MiDeleteClick(object sender, RoutedEventArgs e)
        {
            this.PadHostContainer.Children.Remove(this.ClickedSourcePad);
        }

        private void MiExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MiNewLabelClick(object sender, RoutedEventArgs e)
        {
            this.ClickedSourcePad = new LabelPad();
            this.ClickedSourcePad.SetDefaults();
            this.isEditing = true;
            this.PadHostContainer.Children.Add(this.ClickedSourcePad);
        }

        private void MiNewTimeDateClick(object sender, RoutedEventArgs e)
        {
            this.ClickedSourcePad = new TimeDatePad();
            this.ClickedSourcePad.SetDefaults();
            this.PadHostContainer.Children.Add(this.ClickedSourcePad);
        }

        private void MiNewCalendarClick(object sender, RoutedEventArgs e)
        {
            this.ClickedSourcePad = new CalendarPad();
            this.ClickedSourcePad.SetDefaults();
            this.PadHostContainer.Children.Add(this.ClickedSourcePad);
        }

        private void MiNewRichNoteClick(object sender, RoutedEventArgs e)
        {
            this.ClickedSourcePad = new RichPad();
            this.ClickedSourcePad.SetDefaults();
            this.PadHostContainer.Children.Add(this.ClickedSourcePad);
        }

        private void MiNewStaticImageClick(object sender, RoutedEventArgs e)
        {
            this.PadHostContainer.Children.Add(new ImagePad());
        }

        private void MiEditPadClick(object sender, RoutedEventArgs e)
        {
            if (this.ClickedSourcePad != null)
            { 
                this.isEditing = true;
            }
        }

        private void MiCopyContentPadClick(object sender, RoutedEventArgs e)
        {
            if (this.ClickedSourcePad != null)
            {
                this.isCopyingContent = true;
            }
        }

        private void MiLockPadClick(object sender, RoutedEventArgs e)
        {
            if (this.ClickedSourcePad.IsLocked)
            {
                this.ClickedSourcePad.IsLocked = false;
                this.MiLockPad.Header = "Lock";
            }
            else
            {
                this.ClickedSourcePad.IsLocked = true;
                this.MiLockPad.Header = "UnLock";
            }
        }

        private void MiSaveClick(object sender, RoutedEventArgs e)
        {
            this.SavePads();
        }

        private void TogglePadsVisibiliity(bool visible, bool preserveEdit = true)
        {
            foreach (Pad p in this.PadHostContainer.Children)
            {
                if (visible)
                { p.Visibility = System.Windows.Visibility.Visible; }
                else
                { p.Visibility = System.Windows.Visibility.Collapsed; }
            }

            if (!visible && preserveEdit)
            {
                this.ClickedSourcePad.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
