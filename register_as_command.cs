// register_as_command.cs (register_as_command.exe)
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Error: Please specify the file path.");
            return;
        }

        string filePath = args[0];
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Error: The specified file does not exist.");
            return;
        }

        // AppData\Local\RegisterAsCommand から config.txt を読み込む
        string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string registerDir = Path.Combine(appDataDir, "RegisterAsCommand");
        string configPath = Path.Combine(registerDir, "config.txt");

        // config.txt からバッチファイルの保存先ディレクトリを読み込む
        if (!File.Exists(configPath))
        {
            Console.WriteLine("Error: config.txt not found.");
            return;
        }

        string batDir = File.ReadAllText(configPath).Trim();
        if (!Directory.Exists(batDir))
        {
            Directory.CreateDirectory(batDir);
            Console.WriteLine(string.Format("Created directory: {0}", batDir));
        }

        // バッチファイル名を生成（元のファイル名から拡張子を除く）
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
        string batFilePath = Path.Combine(batDir, fileNameWithoutExtension + ".bat");

        // バッチファイルの内容
        string batContent = string.Format("@echo off\n\"{0}\" \"%1\"", filePath);

        // バッチファイルを書き込む
        try
        {
            File.WriteAllText(batFilePath, batContent);
            Console.WriteLine(string.Format("Batch file created: {0}", batFilePath));
        }
        catch (Exception ex)
        {
            Console.WriteLine(string.Format("Error: Failed to create batch file: {0}", ex.Message));
        }
    }
}
