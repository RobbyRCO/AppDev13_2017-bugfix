using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;


namespace Internship.Desktop
{
    public class CsvReader
    {
        public static List<string> Read(string path)
        {
            try
            {
                //string file = Reformat(File.ReadAllText(path));
                List<string> lines = File.ReadAllLines(path).ToList();
                return lines;
            }
            catch (FormatException) { throw new FormatException("Error => kan het bestand niet omzetten");}
            catch (ArgumentNullException) { throw new ArgumentNullException(null, "Error => geen bestand opgegeven");}
        }

        //private static string Reformat(string text)
        //{
        //    string[] elements = ParseService.GetElements(text, false);
        //    List<string> formattedElements = new List<string>();
        //    StringBuilder formattedText = new StringBuilder();

        //    foreach (var s in elements)
        //    {
        //        var element = s;
        //        if (s.StartsWith(@"""") && s.EndsWith(@""""))
        //            element = s.Replace("\n", "");
        //        formattedElements.Add(element);
        //    }
        //    formattedElements.ForEach(e => formattedText.Append(e));
        //    return formattedText.ToString();
        //}
    }
}
