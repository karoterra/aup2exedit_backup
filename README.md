# aup2exedit_backup

[AviUtl](http://spring-fragrance.mints.ne.jp/aviutl/)
プロジェクトファイルから拡張編集のバックアップファイルを生成するツールです。

## インストール

[Releases](https://github.com/karoterra/aup2exedit_backup/releases)
から最新版の ZIP ファイルをダウンロードし、好きな場所に展開してください。

- `aup2exedit_backup-<バージョン>-win64-fd.zip`
  - .NET 8 ランタイムをインストール済みの場合
- `aup2exedit_backup-<バージョン>-win64-sc.zip`
  - .NET 8 ランタイムをインストールせずに使う場合
  - よく分からないがとにかく使いたい場合

アンインストール時には展開したフォルダを削除してください。

## 使い方

### コンソールを開かない場合
AviUtl プロジェクトファイル（*.aup）を`aup2exedit_backup.exe`にドラッグ&ドロップしてください。
プロジェクトファイルと同じ場所に`<元のファイル名>.exedit_backup`が生成されます。

### コンソールから使う場合
```
> aup2exedit_backup --help
aup2exedit_backup 0.2.0
Copyright © 2021 karoterra
USAGE:
バックアップファイルを出力する:
  aup2exedit_backup C:\path\to\project.aup
出力ファイルのパスを指定する:
  aup2exedit_backup --out C:\path\to\backup.exedit_backup C:\path\to\project.aup

  -o, --out            出力するexoファイルのパス

  --help               Display this help screen.

  --version            Display version information.

  filename (pos. 0)    Required. aupファイルのパス
```

## 更新履歴

更新履歴は [CHANGELOG](CHANGELOG.md) を参照してください。

## ライセンス

このソフトウェアは、MITライセンスのもとで公開されます。
詳細は [LICENSE](LICENSE) を参照してください。

使用したライブラリ等については[CREDITS](CREDITS.md) を参照してください。
