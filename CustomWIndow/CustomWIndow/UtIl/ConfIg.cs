using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

using CustomWIndow.UtIl.Enum;

using Windows.UI;

namespace CustomWIndow.UtIl
{
    internal class ConfIg
    {
        public static string FIleStrIng = "confIg";

        /// <summary>
        /// - 클래스
        /// <br/>ㅤ설정 루트 클래스
        /// </summary>
        public class ConfIgEle
        {
            public ProgramWIndow ProgramWIndowCon { get; set; } = new();

            public List<string> AppLIst { get; set; } = new();

            public List<string> NonappLIst { get; set; } = new();

            public int ProcessCheckermode { get; set; } = 0;

            public DWM_WINDOW_CORNER_PREFERENCE WIndowCornermode { get; set; } = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_DEFAULT;
            /// <summary>
            /// - 멤버
            /// <br/>ㅤ창 색상 설정을 저장하는 클래스
            /// </summary>
            public ColorConfIg ColorConfIg { get; set; } = new();

            /// <summary>
            /// - 멤버
            /// <br/>ㅤ창 설정을 저장하는 클래스
            /// </summary>
            public WIndowConfIg WIndowConfIg { get; set; } = new();

            public EtcConfIg EtcConfIg { get; set; } = new();

            public bool AutoAdmin { get; set; } = false;
        }

        public static ConfIgEle Instance { get; set; } = new();

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

                Instance = JsonSerializer.Deserialize<ConfIgEle>(JsonStrIng);
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

        public class ProgramWIndow
        {
            public int WIndowGaro { get; set; } = 1540;

            public int WIndowSero { get; set; } = 770;
        }

        /// <summary>
        /// - 클래스
        /// <br/>ㅤ창 색상 설정을 저장하는 클래스
        /// </summary>
        public class ColorConfIg
        {
            public bool IsBorderSystemAccent { get; set; } = false;
            public bool IsCaptIonSystemAccent { get; set; } = false;
            public bool IsCaptIonTextSystemAccent { get; set; } = false;
            /// <summary>
            /// - 멤버 
            /// <br/>ㅤ창 모서리 색상을 로드하거나 설정합니다. ColorRef 형식을 사용합니다.
            /// </summary>
            public int BorderColor { get; set; } = 1;
            /// <summary>
            /// - 멤버 
            /// <br/>ㅤ창 캡션 색상을 로드하거나 설정합니다. ColorRef 형식을 사용합니다.
            /// </summary>
            public int CaptIonColor { get; set; } = 1;
            /// <summary>
            /// - 멤버 
            /// <br/>ㅤ창 캡션 텍스트 색상을 로드하거나 설정합니다. ColorRef 형식을 사용합니다.
            /// </summary>
            public int CaptIonTextColor { get; set; } = 1;
            public Color BorderColor_ { get; set; } = new();
            public Color CaptIonColor_ { get; set; } = new();
            public Color CaptIonTextColor_ { get; set; } = new();

            /// <summary>
            /// - 멤버 
            /// <br/>ㅤ창 캡션 텍스트 색상의 변경 방법을 로드하거나 설정합니다. 0은 자동, 1은 수동입니다.
            /// </summary>
            public int CaptIonTextColormode { get; set; } = 0;

        }

        public class WIndowConfIg
        {
            public bool IsTaskbarBorderColor { get; set; } = false;
            public bool IsContextPopupBorderColor { get; set; } = false;
        }

        public class EtcConfIg
        {
            public bool IsTaskbarborder { get; set; } = true;
        }
    }
}
