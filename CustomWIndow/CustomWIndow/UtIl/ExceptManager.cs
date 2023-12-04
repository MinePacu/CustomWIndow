using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

using static CustomWIndow.UtIl.ConfIg;

namespace CustomWIndow.UtIl
{
    public static class ExceptManager
    {
        private static string FIleStrIng = "ExceptClassStrings";
        public static List<string> ExceptHwndClassStrings = new(5);
        public static ExceptClassConfIg Instance { get; set; } = new();

        public static void AddClassString(string ClassString)
        {
            if (ExceptHwndClassStrings.Contains(ClassString) == false)
                ExceptHwndClassStrings.Add(ClassString);
        }

        public static void RemoveClassString(string ClassString)
        {
            if (ExceptHwndClassStrings.Contains(ClassString) == true)
                ExceptHwndClassStrings.Remove(ClassString);
        }

        public static void Load()
        {
            try
            {
                if (File.Exists($@"{Directory.GetCurrentDirectory()}\{FIleStrIng}.json") == false)
                {
                    File.WriteAllText($@"{Directory.GetCurrentDirectory()}\{FIleStrIng}.json", "{ }");
                }
                string JsonStrIng = File.ReadAllText($@"{Directory.GetCurrentDirectory()}\{FIleStrIng}.json");

                var optIons = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                Instance = JsonSerializer.Deserialize<ExceptClassConfIg>(JsonStrIng);
            }

            catch (Exception)
            {
                throw;
            }
        }

        public static void Save()
        {
            try
            {
                JsonSerializerOptions optIons = new()
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = true
                };

                string Json = JsonSerializer.Serialize(Instance, optIons);
                File.WriteAllText($@"{Directory.GetCurrentDirectory()}\{FIleStrIng}.json", Json);
            }

            catch (Exception)
            {
                throw;
            }
        }

        public static void Initialize()
        {
            if (Instance.IsFirst)
            {
            }
        }

        public class ExceptClassConfIg
        {
            public bool IsFirst { get; set; } = false;

            public List<string> ExceptHwndClassStrings { get; set; } = new List<string>(5);
        }
    }
}
