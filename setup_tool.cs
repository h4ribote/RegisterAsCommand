// setup_tool.cs (setup_tool.exe)

using System;
using System.IO;
using Microsoft.Win32;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // 実行ファイルのディレクトリを取得
            string toolDir = AppDomain.CurrentDomain.BaseDirectory;

            // AppData\Local\RegisterAsCommand と commands ディレクトリを設定
            string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string registerDir = Path.Combine(appDataDir, "RegisterAsCommand");
            string batDir = Path.Combine(registerDir, "commands");

            // ディレクトリを作成
            if (!Directory.Exists(registerDir))
            {
                Directory.CreateDirectory(registerDir);
                Console.WriteLine(string.Format("Created directory: {0}", registerDir));
            }
            if (!Directory.Exists(batDir))
            {
                Directory.CreateDirectory(batDir);
                Console.WriteLine(string.Format("Created directory: {0}", batDir));
            }

            // register_as_command.exe を AppData\Local\RegisterAsCommand にコピー
            string sourceExePath = Path.Combine(toolDir, "register_as_command.exe");
            string destExePath = Path.Combine(registerDir, "register_as_command.exe");
            if (File.Exists(sourceExePath))
            {
                File.Copy(sourceExePath, destExePath, true);
                Console.WriteLine(string.Format("Copied register_as_command.exe to: {0}", destExePath));
            }
            else
            {
                Console.WriteLine(string.Format("Warning: register_as_command.exe not found in {0}", toolDir));
            }

            // ユーザーのPATHにバッチディレクトリを追加（未登録の場合）
            string path = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);
            if (path == null || path.IndexOf(batDir, StringComparison.OrdinalIgnoreCase) == -1)
            {
                string newPath = string.IsNullOrEmpty(path) ? batDir : string.Format("{0};{1}", path, batDir);
                Environment.SetEnvironmentVariable("PATH", newPath, EnvironmentVariableTarget.User);
                Console.WriteLine(string.Format("Added {0} to PATH. Please log out and log in to apply changes.", batDir));
            }
            else
            {
                Console.WriteLine(string.Format("{0} is already in PATH.", batDir));
            }

            // AppData\Local\RegisterAsCommand に config.txt を作成
            string configPath = Path.Combine(registerDir, "config.txt");
            File.WriteAllText(configPath, batDir);
            Console.WriteLine(string.Format("Created config.txt at: {0}", configPath));

            // コンテキストメニューを登録
            string regPath = @"Software\Classes\*\shell\RegisterAsCommand";
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(regPath))
            {
                key.SetValue("", "Register as Command");
            }

            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(string.Format(@"{0}\command", regPath)))
            {
                key.SetValue("", string.Format("\"{0}\" \"%1\"", destExePath));
            }

            Console.WriteLine("Setup completed. Right-click a file and select 'Register as Command' to use.");
			Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine(string.Format("Error during setup: {0}", ex.Message));
        }
    }
}
