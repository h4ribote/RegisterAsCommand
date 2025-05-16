# RegisterAsCommand

Windows 11で実行ファイルをコマンドとして簡単に登録するツールです。右クリックメニューから「コマンドとして登録」を選択することで、実行ファイルをバッチファイルとして登録し、コマンドプロンプトから直接呼び出せるようにします。

## 機能

- バッチファイルを `%AppData%\Local\RegisterAsCommand\commands` に保存
- ユーザーのPATHに `%AppData%\Local\RegisterAsCommand\commands` を追加
- 右クリックメニューに「コマンドとして登録」を追加
- 登録された実行ファイルをコマンドプロンプトから実行可能

## 必要条件

- Windows 11
- .NET Framework 4.0 以上
- `register_as_command.exe`（`setup_tool.exe`と同じディレクトリに配置）

## インストール

1. リポジトリをクローンまたはダウンロードします：
   ```bash
   git clone https://github.com/h4ribote/RegisterAsCommand.git
   ```
2. プロジェクトをビルドします（Visual Studio または MSBuild を使用）：
   - ターゲットフレームワーク：.NET Framework 4.0
   - 出力：`setup_tool.exe` および `register_as_command.exe`
3. `setup_tool.exe` と `register_as_command.exe` を同じディレクトリに配置します。

## 使用方法

1. **セットアップ**：
   - `setup_tool.exe` を実行します。
   - 自動的に `%AppData%\Local\RegisterAsCommand` に `register_as_command.exe` がコピーされ、 `%AppData%\Local\RegisterAsCommand\commands` がPATHに追加されます。
   - ログアウトとログインを行ってPATHの変更を反映します。

2. **コマンドの登録**：
   - 任意の実行ファイル（例：`test.exe`）を右クリックし、「さらにオプションを表示」を選択。
   - 「コマンドとして登録」を選択すると、 `%AppData%\Local\RegisterAsCommand\commands` にバッチファイル（例：`test.bat`）が作成されます。

3. **コマンドの実行**：
   - コマンドプロンプトを開き、登録したコマンド名（例：`test`）を入力して実行します。

## 注意点

- PATHの変更を反映するには、ログアウトとログインが必要です。
- 「コマンドとして登録」は、Windows 11のレガシーコンテキストメニューに表示されます。
- エラーが発生した場合は、コンソール出力を確認してください。
