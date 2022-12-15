using System.Security.Principal;

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
    }
}
