using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Models.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace SampleCode.Main;

public sealed partial class SetupWindow : Window
{
    private string _existingDatabaseFile { get; set; } = "";
    private string _newDatabaseFolder { get; set; } = "";
    private string _outputFolder { get; set; } = "";
    public SetupWindow()
    {
        InitializeComponent();
    }        

    private async Task<StorageFile> PickFile(object sender, RoutedEventArgs e)
    {
        // Create a file picker
        var openPicker = new Windows.Storage.Pickers.FileOpenPicker();

        // Retrieve the window handle (HWND) of the current WinUI 3 window.
        //var window = WindowHelper.GetWindowForElement(this);
        var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

        // Initialize the file picker with the window handle (HWND).
        WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

        // Set options for your file picker
        openPicker.ViewMode = PickerViewMode.Thumbnail;
        openPicker.FileTypeFilter.Add("*");

        // Open the picker for the user to pick a file
        var file = await openPicker.PickSingleFileAsync();
        return file;
    }



    private async Task<StorageFolder> PickFolder(object sender, RoutedEventArgs e)
    {
        //disable the button to avoid double-clicking
        var senderButton = sender as Button;
        senderButton?.IsEnabled = false;

        // Create a folder picker
        FolderPicker openPicker = new Windows.Storage.Pickers.FolderPicker();

        // See the sample code below for how to make the window accessible from the App class.
        var window = App.Window;

        // Retrieve the window handle (HWND) of the current WinUI 3 window.
        //var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
        var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

        // Initialize the folder picker with the window handle (HWND).
        WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

        // Set options for your folder picker
        openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
        openPicker.FileTypeFilter.Add("*");
        StorageFolder folder = await openPicker.PickSingleFolderAsync();
        if (folder != null)
        {
            //StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);                                
        }
        else
        {
            Debug.WriteLine("Operation cancelled.");
        }

        senderButton?.IsEnabled = true;
        return folder;
    }



    private async void PickBaseFolderButton_Click(object sender, RoutedEventArgs e)
    {            
        // Clear previous returned file name, if it exists, between iterations of this scenario
        PickBaseFolderOutputTextBlock.Text = "";

        StorageFolder folder = await PickFolder(sender, e);
        if (folder != null)
        {
            PickBaseFolderOutputTextBlock.Text = "Picked folder: " + folder.Name;                
            _outputFolder = folder.Path;
        }
        else
        {
            PickBaseFolderOutputTextBlock.Text = "Operation cancelled.";
        }
    }

    private async void PickNewDatabaseFolderButton_Click(object sender, RoutedEventArgs e)
    {            
        // Clear previous returned file name, if it exists, between iterations of this scenario
        PickNewDatabaseFolderOutputTextBlock.Text = "";

        StorageFolder folder = await PickFolder(sender, e);
        if (folder != null)
        {
            PickNewDatabaseFolderOutputTextBlock.Text = "Picked folder: " + folder.Name;                
            _newDatabaseFolder = folder.Path;
        }
        else
        {
            PickNewDatabaseFolderOutputTextBlock.Text = "Operation cancelled.";
        }
    }
    bool loaded = false;

    private void StackPanel_Loaded(object sender, RoutedEventArgs e)
    {
        loaded = true;
        NewDatabaseSwitch.IsOn = true;

    }

    
    private async void PickExistingDatabaseButton_Click(object sender, RoutedEventArgs e)
    {
        // Clear previous returned file name, if it exists, between iterations of this scenario
        PickExistingDatabaseOutputTextBlock.Text = "";
        StorageFile databaseFile = await PickFile(sender, e);
        if (databaseFile != null)
        {
            PickExistingDatabaseOutputTextBlock.Text = "Picked file: " + databaseFile.Name;                
            //_appSettings.DatabaseFile= folder.Path;
            _existingDatabaseFile = databaseFile.Path;
        }
        else
        {
            PickExistingDatabaseOutputTextBlock.Text = "Operation cancelled.";
        }
    }



    private void DatabaseSwitch_Toggled(object sender, RoutedEventArgs e)
    {            
        //Control1Output.Text = string.Format("You selected {0}", (sender as RadioButton).Content.ToString());
        ToggleSwitch toggleSwitch = sender as ToggleSwitch;
        if (toggleSwitch != null)
        {
            if (ExistingDatabasePanel != null)
            {
                if (toggleSwitch.IsOn == true)
                {                        
                    ExistingDatabasePanel.Visibility = Visibility.Collapsed;
                    NewDatabasePanel.Visibility = Visibility.Visible;

                }
                else
                {
                    ExistingDatabasePanel.Visibility = Visibility.Visible;
                    NewDatabasePanel.Visibility = Visibility.Collapsed;
                }
            }
        }
    }

    private void SaveBtn_Click(object sender, RoutedEventArgs e)
    {
        bool error = false;
        if (App.Settings.Containers.ContainsKey(App.SettingsContainer))
        {
            if (_outputFolder == "")
            {                    
                error = true;
            }
            else
            {
                App.Settings.Containers[App.SettingsContainer].Values[KeyWord.OUTPUT_FOLDER] = _outputFolder;
            }
            if (NewDatabaseSwitch.IsOn) // new database
            {
                if (NewDatabaseFileName.Text == "")
                {
                    Debug.WriteLine("Error: New Database File name is blank");
                    error = true;
                }
                if (_newDatabaseFolder == "")
                {
                    Debug.WriteLine("Error: New Database Folder name is blank");
                    error = true;
                }
                if (!error)
                {
                    App.Settings.Containers[App.SettingsContainer].Values[KeyWord.DATABASE_FILE] = _newDatabaseFolder + "\\" + NewDatabaseFileName.Text;
                }
            }
            else
            {
                if (_existingDatabaseFile == "")
                {
                    Debug.WriteLine("Error: Existing Database File name is blank");
                    error = true;
                }
                else
                {
                    App.Settings.Containers[App.SettingsContainer].Values[KeyWord.DATABASE_FILE] = _existingDatabaseFile;
                }
            }
        }

        if (!error)
        {
            App.Settings.Containers[App.SettingsContainer].Values[KeyWord.IS_FIRST_TIME] = false;
            Debug.WriteLine("turned off firstime " + App.Settings.Containers[App.SettingsContainer].Values[KeyWord.IS_FIRST_TIME]);
            SetupDatabase();
            SetupSuburbs();
            SetupStreetTypes();

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Activate();
            this.Close();
        }
        else
        {
            Debug.WriteLine("Error saving");
        }            
    }

    public void SetupDatabase()
    {

    }

    public void SetupSuburbs()
    {
        Debug.WriteLine("-- SetupSuburbs --");
        string path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\suburbs.csv");            

        using (var reader = new StreamReader(path))
        {
            //List<SuburbModel> suburbs = new List<SuburbModel>();
            /*
            SampleDbContext context = new SampleDbContext();
            context.Suburbs.ExecuteDelete();
            using (context)
            {                    
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    //suburbs.Add(new SuburbModel(values[1], values[0]));

                    //context.Suburbs.Add(new SuburbModel(values[1], values[0]));
                    context.Suburbs.Add(new SuburbModel()
                    {
                        Id = 1,
                        
                    });
                }
                context.SaveChanges();

            }
            */
            /*
                foreach (Suburb suburb in suburbs)
                {
                    Debug.WriteLine($"{suburb.Name}");
                }
                */
        }
    }

    public void SetupStreetTypes()
    {
        Debug.WriteLine("-- SetupStreetTypes --");
        string path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\street_types.csv");            

        using (var reader = new StreamReader(path))
        {
            List<StreetTypeModel> streetTypes = new List<StreetTypeModel>();
            /*
            RosterDBContext context = new RosterDBContext();
            using (context)
            {
                DbSet<Suburb> c = context.Set<Suburb>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    suburbs.Add(new Suburb(values[1], values[0]));

                    c.Add(new Suburb(values[1], values[0]));
                }
                context.SaveChanges();

            }
            */
            /*
                foreach (Suburb suburb in suburbs)
                {
                    Debug.WriteLine($"{suburb.Name}");
                }
                */
        }
    }        
}
