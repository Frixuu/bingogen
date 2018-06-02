using PdfSharpCore.Fonts;
using System.IO;
using System.Reflection;

namespace Frixu.BingoGen
{
    /// <summary>Resolves fonts from embedded resources.</summary>
    public class BingoFontResolver : IFontResolver
    {
        public FontResolverInfo ResolveTypeface(string name, bool bold, bool italic) =>
            new FontResolverInfo(name, bold, italic);

        /// <summary>Returns .ttf file for the requested font.</summary>
        public byte[] GetFont(string name)
        {
            switch(name)
            {
                case "OpenSans":
                    var ms = new MemoryStream();
                    var assembly = Assembly.GetEntryAssembly();
                    var stream = assembly.GetManifestResourceStream("BingoGen.Fonts.OpenSans-Regular.ttf");
                    stream.CopyTo(ms);
                    return ms.ToArray();
                default:
                    return new byte[0];
            }
        }

        public string DefaultFontName => "OpenSans";
    }
}
