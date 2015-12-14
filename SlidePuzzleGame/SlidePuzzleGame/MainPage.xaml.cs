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
using Windows.UI.Xaml.Navigation;
using System.Windows;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Graphics.Imaging;


//using System.Windows.Controls;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SlidePuzzleGame
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            // This keeps the information on the page saved
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            this.InitializeComponent();
        }

        private async void btnGetPicture_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file
                //OutputTextBlock.Text = "Picked photo: " + file.Name;
            }
            else
            { 
               // OutputTextBlock.Text = "Operation cancelled.";
            }

            //http://stackoverflow.com/questions/8881865/saving-a-wpf-canvas-as-an-image
           // RenderTargetBitmap rtb = new RenderTargetBitmap((int)MainCanvas.RenderSize.Width,
           //(int)MainCanvas.RenderSize.Height, 96d, 96d, System.Windows.Media.PixelFormats.Default);
           // await rtb.RenderAsync(MainCanvas);
           // //rtb.Render(MainCanvas);

           // var crop = new CroppedBitmap(rtb, new Int32Rect(50, 50, 250, 250));

           // BitmapEncoder pngEncoder = new PngBitmapEncoder();
           // pngEncoder.Frames.Add(BitmapFrame.Create(crop));

           // using (var fs = File.OpenWrite("logo.png"))
           // {
           //     pngEncoder.Save(fs);
           // }
        }
    }
}
