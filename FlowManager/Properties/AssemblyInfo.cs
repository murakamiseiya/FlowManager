﻿///
/// ファイル名	: BaseModel.cs
/// 作成者		: murakami
/// 制作日		: 2020/1/30
/// 最終更新者	: なし
/// 最終更新日	: なし
///
/// 更新履歴
/// 名前			日付		内容
/// murakami		2020/01/30	新規作成
///

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// アセンブリに関する一般的情報は、以下の属性セットによって
// 制御されます。アセンブリに関連付けられている情報を変更するには、
// これらの属性値を変更します。
[assembly: AssemblyTitle("FlowManager")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("FlowManager")]
[assembly: AssemblyCopyright("Copyright ©  2020")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// ComVisible を false に設定すると、このアセンブリ内の型は
// COM コンポーネントから参照できなくなります。
// COM からこのアセンブリ内の型にアクセスする必要がある場合は、その型の ComVisible 属性を true に設定してください。
[assembly: ComVisible(false)]

// このプロジェクトが COM に公開される場合、次の GUID が typelib の ID になります。
[assembly: Guid("6eef801e-e3fe-4b87-92c1-5b92e4345558")]

// log4net configuration file
[assembly: log4net.Config.XmlConfigurator(ConfigFile = @"App_Data/log4net.xml", Watch = true)]


// アセンブリのバージョン情報は、以下の 4 つの値で構成されています:
//
//      メジャー バージョン
//      マイナー バージョン
//      ビルド番号
//      リビジョン
//
// すべての値を指定するか、以下のように "*" を使用してリビジョンとビルド番号を
// 既定値にすることができます:
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
