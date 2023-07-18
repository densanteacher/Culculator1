using System;
namespace Calculator2
{
    public static class Constants
    {
        // DONE: const と static readonly について調べてみましょう。
        // const は定数と呼ばれ、コンパイル時に文字列として埋め込まれる動作になります。

        // <see cref="https://nyanblog2222.com/programming/c-sharp/1279/">このような記事がでました。</see>
        // constは文字列以外には使えないほか、コンパイル時に決定される特性がありますが、static readonlyはあらゆる値型を設定でき、実行時に決定されます。
        // 基本的にはconstよりもstatic readonllyの方が推奨であるとのことです。
        public static readonly string path = @"..\..\..\result.txt";
    }
}