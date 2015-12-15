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
//using System.Windows.Media.Imaging;
//using static System.Windows.Media.Imaging.CroppedBitmap;
using Windows.Media.Capture;
using System.Threading.Tasks;
using Windows.UI;




//using System.Windows.Controls;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SlidePuzzleGame
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Random rnd;
        public MainPage()
        {
            // This keeps the information on the page saved
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            this.InitializeComponent();
            rnd = new Random();
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
                BitmapImage btmp = await SetImageSource(file);
                ImageBrush image = new ImageBrush();

                cutImage(btmp);
               // image.ImageSource = btmp;

                //MainCanvas.Background = image;
            }
            else
            {
                txtError.Text = "Could not open the specified file.";
            }
        }

        public void cutImage(BitmapImage image)
        {
            //http://www.c-sharpcorner.com/uploadfile/mahesh/clipping-or-cropping-images-in-wpf/
            // Clipped Image
            int count = 0;
            Image clippedImage = new Image();
            clippedImage.Source = image;
            RectangleGeometry clipGeometry = new RectangleGeometry();
            for(count = 1; count <= 16; count++)
            {
                Rect piece = new Rect();
                int randX = rnd.Next(50, Convert.ToInt32(MainCanvas.ActualWidth - (1 + 150)));
                int randY = rnd.Next(50, Convert.ToInt32(MainCanvas.ActualHeight - (1 + 100)));
                piece.X = randX;
                piece.Y = randY;
                piece.Width = 150;
                piece.Height = 100;
                clipGeometry.Rect = piece;
               // clipGeometry.Rect = new Rect(200, 200, 100, 100);
                clippedImage.Clip = clipGeometry;
                // Need to call random placement function here as it probably just places each square in one place.
            }
            MainCanvas.Children.Add(clippedImage);

            //MainCanvas.Background = clippedImage;
            // LayoutRoot.Children.Add(clippedImage);
        }

        private async Task<BitmapImage> SetImageSource(StorageFile file)
        {
            //var file = await
            //  Windows.Storage.KnownFolders.PicturesLibrary.GetFileAsync(fileName);
            var stream = await file.OpenReadAsync();
            var bitmapImage = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
            bitmapImage.SetSource(stream);

            return bitmapImage;
        }

        public async void openCamera()
        {
            // Using Windows.Media.Capture.CameraCaptureUI API to capture a photo
            CameraCaptureUI dialog = new CameraCaptureUI();
            Size aspectRatio = new Size(16, 9);
            dialog.PhotoSettings.CroppedAspectRatio = aspectRatio;

            StorageFile file = await dialog.CaptureFileAsync(CameraCaptureUIMode.Photo);
            BitmapImage test = await SetImageSource(file);
            ImageBrush image = new ImageBrush();
            image.ImageSource = test;

            
            MainCanvas.Background = image;
        }

        private void btnCamera_Click(object sender, RoutedEventArgs e)
        {
            openCamera();
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            // This function should actually restart the positions of the image blocks
            MainCanvas.Children.Clear();
            //MainCanvas.Children.Remove<Image>();
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
