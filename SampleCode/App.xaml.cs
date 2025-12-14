using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Models;
using Models.Navigation;
using SampleCode.Interfaces;
using SampleCode.Main;
using SampleCode.Other;
using SampleCode.Services.Navigation;
using SampleCode.ViewModels.Data;
using SampleCode.ViewModels.Page.Navigation;
using SampleCode.Views.Navigation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using Windows.Storage;

namespace SampleCode;

public partial class App : Application
{
    public Window? LoginWindow;
    public Window? SetupWindow;
    public Window? MainWindow;
    public UserViewModel? CurrentUser { get; set; } = null;
    public static Window? Window { get; set; }
    public bool unpackedApp = false;
    public static AppSetting? AppSettings { get; set; }
    public static string LocalAppDataPath = "";
    public static string AppDataFolderPath = "";
    public static string AppName = "SampleApp";
    public static string SettingsFileName = "app_settings.json";
    public const string SettingsContainer = "SettingsContainer";
    public static ApplicationDataContainer? Settings = null;                

    public IServiceProvider Services { get; }

    private static IHost _host;

    public App()
    {            
        Application.Current.DispatcherShutdownMode = DispatcherShutdownMode.OnLastWindowClose;        
        
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Register your services and ViewModels here
                
                services.AddDbContext<SampleDbContext>();
                
                services.AddScoped<AddressService>();
                services.AddScoped<RouteAddressService>();
                //services.AddScoped<IPageService<RouteModel>, RouteService>();
                services.AddScoped<RouteService>();
                services.AddScoped<StreetTypeService>();
                services.AddScoped<SuburbService>();
                
                services.AddTransient<RoutePageViewModel>();
                services.AddTransient<RoutePage>();                
            })
            .Build();
        
        //_host.Se
        //ConfigureServices();        

        this.InitializeComponent();
        InitializeDb();
        AppType();            
        MainWindow = new MainWindow();
        Window = new Window();
    }

    public void AppType()
    {
        if (unpackedApp)
        {
            Debug.WriteLine("-- Unpacked App --");
            AppSettings = new AppSetting("", "");
            LocalAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            AppDataFolderPath = System.IO.Path.Combine(LocalAppDataPath, AppName);
            Directory.CreateDirectory(AppDataFolderPath); // Create the app folder if it doesn't exist
            string filePath = System.IO.Path.Combine(AppDataFolderPath, SettingsFileName);                          
            File.WriteAllText(filePath, "Settings data");                
            string data = File.ReadAllText(filePath);
        }
        else
        {
            Debug.WriteLine("-- Packed App --");
            Settings = ApplicationData.Current.LocalSettings;
            ApplicationDataContainer container = Settings.CreateContainer(SettingsContainer, ApplicationDataCreateDisposition.Always);
        }
    }        
    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        Debug.WriteLine("ON LAUNCHED CALLED");
        _host.Start();
        Application.Current.DispatcherShutdownMode = DispatcherShutdownMode.OnLastWindowClose;            
        UseSqlite();            
        if (!CheckForFirstTimeRun())
        {
            Debug.WriteLine("not checkfirstrun");
            LoginWindow = new LoginWindow();
            LoginWindow.Activate();
        }
        else
        {
            Debug.WriteLine("Is first run");
            SetupWindow = new LoginWindow();
            SetupWindow.Activate();
        }
    }

    // Helper method to resolve services statically
    public static T GetService<T>() where T : class
    {
        return _host.Services.GetRequiredService<T>();
    }

    private static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddDbContext<SampleDbContext>();

        // ConfigureServices(serviceCollection)        
        services.AddScoped<IPageService<AddressModel>, AddressService>();
        services.AddScoped<IPageService<RouteAddressModel>, RouteAddressService>();
        services.AddScoped<IPageService<RouteModel>, RouteService>();
        services.AddScoped<IPageService<StreetTypeModel>, StreetTypeService>();
        services.AddScoped<IPageService<SuburbModel>, SuburbService>();

        services.AddScoped<RoutePageViewModel>();
        //serviceProvider.GetRequiredService<RoutePageViewModel>();
        return services.BuildServiceProvider();
    }

    /// <summary>
    /// Configures settings for a first time run.        
    /// </summary>
    private bool CheckForFirstTimeRun()
    {
        Debug.WriteLine("-- CheckForFirstTimeRun --");
        bool firstTime = false;
        string settingsFileName = "appSettings.json";
        string filePath = System.IO.Path.Combine(AppDataFolderPath, settingsFileName);
        if (unpackedApp)
        {
            if (Directory.Exists(AppDataFolderPath))
            {                    
                if (!File.Exists(filePath))
                {
                    firstTime = true;
                }
                else
                {                        
                    b(filePath);
                }
            }
            else
            {
                firstTime = true;
                Directory.CreateDirectory(AppDataFolderPath); // Create the app folder if it doesn't exist  
                b(filePath);
            }
        }
        else
        {
            if (Settings.Containers.ContainsKey(SettingsContainer))
            {
                Debug.WriteLine("contains settings");
                if (Settings.Containers[SettingsContainer].Values.ContainsKey(KeyWord.IS_FIRST_TIME))
                {                        
                    firstTime = (bool)Settings.Containers[SettingsContainer].Values[KeyWord.IS_FIRST_TIME];
                }
                else
                {
                    Debug.WriteLine("setting not found");
                    firstTime = true;
                }
            }
            else
            {
                Debug.WriteLine("Container not found: SettingsContainer");
            }
            if (firstTime)
            {

            }
        }
        return firstTime;
    }
    private void b(string filePath)
    {
        Directory.CreateDirectory(AppDataFolderPath);
                                                   
        AppSetting appSettings = new AppSetting("", "");
        string data = JsonSerializer.Serialize(appSettings);                       
        File.WriteAllText(filePath, "Settings data");            
    }

    /// <summary>
    /// Configures the app to use the Sqlite data source. If no existing Sqlite database exists,         
    /// </summary>
    private void UseSqlite()
    {
        Debug.WriteLine("in usesql lite");
        if (unpackedApp)
        {
            Debug.WriteLine("UnPackaged App");
        }
        else
        {
            Debug.WriteLine("Packaged App");
            
        }                                   
        using (var scope = _host.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetService<SampleDbContext>();
            if (dbContext != null)
            {
                dbContext.Database.Migrate();
            }
        }
    }

    private void InitializeDb()
    {
        using (var scope = _host.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetService<SampleDbContext>();
            if (dbContext != null)
            {
                dbContext.Database.Migrate();
                if (!dbContext.Users.Any())
                {
                    UserModel user1 = new UserModel
                    {
                        Id = 1,
                        Username = "Fred"
                    };

                    UserModel user2 = new UserModel
                    {
                        Id = 2,
                        Username = "Steve"
                    };
                    dbContext.Users.AddRange(user1, user2);
                    dbContext.SaveChanges();
                }
            }
        }
        //SetupStreetTypes();
        //SetupSuburbs();
    }

    private void SetupStreetTypes()
    {
        Debug.WriteLine("-- SetupStreetTypes --");
        string path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\street_types.csv");

        using (var reader = new StreamReader(path))
        {
            List<StreetTypeModel> streetTypes = new List<StreetTypeModel>();
            SampleDbContext context = new SampleDbContext();
            //context.StreetTypes.ExecuteDelete();
            using (context)
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split('\t');
                    context.StreetTypes.Add(new StreetTypeModel()
                    {
                        Id = int.Parse(values[0]),
                        Code = values[1],
                        Name = values[2]
                    });
                }
                context.SaveChanges();
            }
        }
    }
    private void SetupSuburbs()
    {
        Debug.WriteLine("-- SetupSuburbs --");
        string path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\suburbs.csv");

        using (var reader = new StreamReader(path))
        {
            SampleDbContext context = new SampleDbContext();
            //context.Suburbs.ExecuteDelete();
            using (context)
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line != null)
                    {
                        var values = line.Split(',');
                        Debug.WriteLine("poscode: " + values[0]);
                        context.Suburbs.Add(new SuburbModel()
                        {
                            PostCode = values[0],
                            Name = values[1]

                        });
                    }
                }
                context.SaveChanges();
            }
        }
    }
}