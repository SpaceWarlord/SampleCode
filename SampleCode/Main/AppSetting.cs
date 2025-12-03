namespace SampleCode.Main
{
    public class AppSetting
    {
        public string OutputFolder { get; set; }
        public string DatabaseFile { get; set; }
        public AppSetting(string outputFolder, string databaseFile)
        {
            OutputFolder = outputFolder;
            DatabaseFile = databaseFile;
        }
    }
}