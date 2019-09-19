﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWP_Visual_Asset_Generator.ViewModels;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace UWP_Visual_Asset_Generator.UserControls
{
    public sealed partial class SidebarUserControl : UserControl
    {
        public MainViewModel mainViewModel { get; set; }

        public SidebarUserControl()
        {
            this.InitializeComponent();
            mainViewModel = App.mainViewModel;
        }

        private void Image_Original_Tapped(object sender, TappedRoutedEventArgs e)
        {
            mainViewModel.LoadOriginalImageFromFileAsync();
        }

        private async void Image_Original_Drop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();

                if (items.Count > 0 &&
                    items[0] is StorageFile &&
                    ((StorageFile)items[0]).FileType.ToLower().Equals(".png")) 
                {
                    mainViewModel.LoadOriginalImageFromFileAsync(items[0] as StorageFile);
                }
                else
                {
                    mainViewModel.ShowDialog("Information", "Only .PNG files are allowed.");
                }
            }
        }

        private void Image_Original_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Link;

            e.DragUIOverride.Caption = "Load file"; // Sets custom UI text
            //e.DragUIOverride.SetContentFromBitmapImage(null); // Sets a custom glyph
            e.DragUIOverride.IsCaptionVisible = true; // Sets if the caption is visible
            e.DragUIOverride.IsContentVisible = true; // Sets if the dragged content is visible
            e.DragUIOverride.IsGlyphVisible = true; // Sets if the glyph is visibile
        }
    }
}
