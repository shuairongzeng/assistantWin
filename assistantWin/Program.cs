using Microsoft.Win32;
using Microsoft.Win32;
namespace assistantWin
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //string contextMenuXmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CopyFileContextMenu.xml");
            string appExePath = Application.ExecutablePath;
            string command = $"\"{appExePath}\" \"%1\" /copyFile";

            string registryKeyPath = @"HKEY_CLASSES_ROOT\*\shell\YourApp.CopyFileCommand";

            bool registryKeyExists = Registry.GetValue(registryKeyPath, null, null) != null;

            if (!registryKeyExists)
            {
                // ע�������ڣ�ִ��ע�����Ĵ�������
                // ...
                Registry.SetValue(@"HKEY_CLASSES_ROOT\*\shell\YourApp.CopyFileCommand\command", null, command);
                Registry.SetValue(@"HKEY_CLASSES_ROOT\Directory\shell\YourApp.CopyFileCommand\command", null, command);
                Registry.SetValue(@"HKEY_CLASSES_ROOT\Directory\Background\shell\YourApp.CopyFileCommand\command", null, command);
                Registry.SetValue(@"HKEY_CLASSES_ROOT\Drive\shell\YourApp.CopyFileCommand\command", null, command);
                Registry.SetValue(@"HKEY_CLASSES_ROOT\AllFilesystemObjects\shell\YourApp.CopyFileCommand\command", null, command);

                Console.WriteLine("ע������Ѵ�����");
            }
            else
            {
                // ע������Ѵ��ڣ���������
                Console.WriteLine("ע������Ѵ��ڣ�����������");
            }




            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}