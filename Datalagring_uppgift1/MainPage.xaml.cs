using Datalagring_uppgift1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Datalagring_uppgift1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private ObservableCollection<string> CsvRows = new ObservableCollection<string>();

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.FileTypeFilter.Add(".csv");
            picker.FileTypeFilter.Add(".xml");
            picker.FileTypeFilter.Add(".json");
            picker.FileTypeFilter.Add(".txt");

            var file = await picker.PickSingleFileAsync();

            CsvRows.Clear();

            if (file.FileType == ".csv")
            {
                using (CsvFileReader csvReader = new CsvFileReader(await file.OpenStreamForReadAsync()))
                {
                    CsvRow row = new CsvRow();
                    while (csvReader.ReadRow(row))
                    {
                        string newRow = "";
                        for (int i = 0; i < row.Count; i++)
                        {
                            newRow += row[i] + ",";
                        }

                        CsvRows.Add(newRow);
                    }
                }

                CsvRowsListView.ItemsSource = CsvRows;
            }


            if (file.FileType == ".json")
            {
                string text = await FileIO.ReadTextAsync(file);
                List<Person> DeserializedProducts = JsonConvert.DeserializeObject<List<Person>>(text);
                JsonRowsListView.ItemsSource = DeserializedProducts;
            }

            if (file.FileType == ".xml")
            {



            }
        }
    }
}
