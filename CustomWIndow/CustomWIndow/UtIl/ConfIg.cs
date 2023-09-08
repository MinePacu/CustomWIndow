using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

using CustomWIndow.UtIl.Config;

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
        public class ConfIgElement
        {
            /// <summary>
            /// - 멤버
            /// <br/>ㅤ프로그램 설정 창과 이 프로그램에 의해 관리되는 윈도우에 대한 Config 인스턴스
            /// </summary>
            public WindowConfig WindowConfig { get; set; } = new();

            /// <summary>
            /// - 멤버
            /// <br/>ㅤ작업 표시줄에 대한 Config 인스턴스
            /// </summary>
            public TaskBarConfig TaskBarConfig { get; set; } = new();

            /// <summary>
            /// - 멤버
            /// <br/>ㅤ앱 리스트
            /// </summary>
            public List<string> AppLIst { get; set; } = new();

            /// <summary>
            /// - 멤버
            /// <br/>ㅤ앱 제외 리스트
            /// </summary>
            public List<string> NonappLIst { get; set; } = new();

            /// <summary>
            /// 프로세스의 창 주소를 찾는 모드 설정<br/>
            /// 0은 특정 윈도우만 찾는 모드, 1은 일부 윈도우를 뺀 나머지 대부분의 윈도우를 찾는 모드입니다.
            /// </summary>
            public int ProcessCheckermode { get; set; } = 0;

            /// <summary>
            /// - 멤버
            /// <br/>ㅤ창 색상 설정을 저장하는 인스턴스
            /// </summary>
            public ColorConfig ColorConfIg { get; set; } = new();

            /// <summary>
            /// - 멤버
            /// <br/>ㅤ기타 설정 인스턴스
            /// </summary>
            public EtcConfIg EtcConfIg { get; set; } = new();

            /// <summary>
            /// - 멤버
            /// <br/>ㅤ개발자 설정 인스턴스
            /// </summary>
            public DeveloperConfig DeveloperConfig { get; set; } = new();

            public bool AutoAdmin { get; set; } = false;
        }

        public static ConfIgElement Instance { get; set; } = new();

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

                Instance = JsonSerializer.Deserialize<ConfIgElement>(JsonStrIng);
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

        public class EtcConfIg
        {
            public bool IsTray { get; set; } = false;

            /// <summary>
            /// 프로그램을 끌 때, 작업 표시줄을 포함한 모든 윈도우의 설정을 기본값으로 복원할지를 저장하는 인스턴스
            /// <br/><c>true</c>이면, 프로그램을 끌 떄, 모든 윈도우의 설정을 기본값으로 복원하며, <c>false</c>이면 윈도우 설정을 기본값으로 복원하지 않습니다.
            /// </summary>
            public bool IsRestoreDefaultWindowSetting { get; set; } = true;

            /// <summary>
            /// 윈도우가 부팅된 후에, 자동으로 프로그램을 실행할지 여부를 저장하는 인스턴스
            /// <br/><c>true</c>이면, 윈도우가 켜진 후에, 자동으로 이 프로그램을 실행합니다. 기본값은 <c>false</c>입니다.
            /// </summary>
            public bool IsTurnOninBoot { get; set; } = false;
        }
    }
}
