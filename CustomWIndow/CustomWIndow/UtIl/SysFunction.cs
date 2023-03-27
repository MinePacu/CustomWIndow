using System.Security.Principal;

using Microsoft.Win32;

namespace CustomWIndow.UtIl
{
    public static class SysFunction
    {
        public static bool IsAdmin()
        {
            WindowsIdentity Identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new(Identity);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static uint WinBuildVersion
        {
            get
            {
                dynamic build;
                if (!TryGetRegistryKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentBuild", out build))
                    return 0;

                return uint.Parse(build);
            }
        }
        private static bool TryGetRegistryKey(string path, string key, out dynamic value)
        {
            value = null;
            try
            {
                using (var rk = Registry.LocalMachine.OpenSubKey(path))
                {
                    if (rk == null) return false;
                    value = rk.GetValue(key);
                    return value != null;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
