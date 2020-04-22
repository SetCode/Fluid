using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.WNetConnect
{
    public enum ERROR_ID
    {
        ERROR_SUCCESS = 0,  // Success 
        ERROR_BUSY = 170,
        ERROR_MORE_DATA = 234,
        ERROR_NO_BROWSER_SERVERS_FOUND = 6118,
        ERROR_INVALID_LEVEL = 124,
        ERROR_ACCESS_DENIED = 5,
        ERROR_INVALID_PASSWORD = 86,
        ERROR_INVALID_PARAMETER = 87,
        ERROR_BAD_DEV_TYPE = 66,
        ERROR_NOT_ENOUGH_MEMORY = 8,
        ERROR_NETWORK_BUSY = 54,
        ERROR_BAD_NETPATH = 53,
        ERROR_NO_NETWORK = 1222,
        ERROR_INVALID_HANDLE_STATE = 1609,
        ERROR_EXTENDED_ERROR = 1208,
        ERROR_DEVICE_ALREADY_REMEMBERED = 1202,
        ERROR_NO_NET_OR_BAD_PATH = 1203
    }

    public enum RESOURCE_SCOPE
    {
        RESOURCE_CONNECTED = 1,
        RESOURCE_GLOBALNET = 2,
        RESOURCE_REMEMBERED = 3,
        RESOURCE_RECENT = 4,
        RESOURCE_CONTEXT = 5
    }

    public enum RESOURCE_TYPE
    {
        RESOURCETYPE_ANY = 0,
        RESOURCETYPE_DISK = 1,
        RESOURCETYPE_PRINT = 2,
        RESOURCETYPE_RESERVED = 8,
    }

    public enum RESOURCE_USAGE
    {
        RESOURCEUSAGE_CONNECTABLE = 1,
        RESOURCEUSAGE_CONTAINER = 2,
        RESOURCEUSAGE_NOLOCALDEVICE = 4,
        RESOURCEUSAGE_SIBLING = 8,
        RESOURCEUSAGE_ATTACHED = 16,
        RESOURCEUSAGE_ALL = (RESOURCEUSAGE_CONNECTABLE | RESOURCEUSAGE_CONTAINER | RESOURCEUSAGE_ATTACHED),
    }

    public enum RESOURCE_DISPLAYTYPE
    {
        RESOURCEDISPLAYTYPE_GENERIC = 0,
        RESOURCEDISPLAYTYPE_DOMAIN = 1,
        RESOURCEDISPLAYTYPE_SERVER = 2,
        RESOURCEDISPLAYTYPE_SHARE = 3,
        RESOURCEDISPLAYTYPE_FILE = 4,
        RESOURCEDISPLAYTYPE_GROUP = 5,
        RESOURCEDISPLAYTYPE_NETWORK = 6,
        RESOURCEDISPLAYTYPE_ROOT = 7,
        RESOURCEDISPLAYTYPE_SHAREADMIN = 8,
        RESOURCEDISPLAYTYPE_DIRECTORY = 9,
        RESOURCEDISPLAYTYPE_TREE = 10,
        RESOURCEDISPLAYTYPE_NDSCONTAINER = 11
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NETRESOURCE
    {
        public RESOURCE_SCOPE dwScope;
        public RESOURCE_TYPE dwType;
        public RESOURCE_DISPLAYTYPE dwDisplayType;
        public RESOURCE_USAGE dwUsage;

        [MarshalAs(UnmanagedType.LPStr)]
        public string lpLocalName;

        [MarshalAs(UnmanagedType.LPStr)]
        public string lpRemoteName;

        [MarshalAs(UnmanagedType.LPStr)]
        public string lpComment;

        [MarshalAs(UnmanagedType.LPStr)]
        public string lpProvider;
    }
    public class WNetConnection
    {
        [DllImport("Mpr.dll", EntryPoint = "WNetAddConnection2", CallingConvention = CallingConvention.Winapi)]
        public static extern int WNetAddConnection2(NETRESOURCE[] lpNetResource, string lpPassword, string lpUserName, int dwFlags);

        [DllImport("Mpr.dll", EntryPoint = "WNetCancelConnection2", CallingConvention = CallingConvention.Winapi)]
        public static extern int WNetCancelConnection2(string sharename, int dwFlags, int fForce);

        /// <summary>
        /// 映射方式连接共享服务器
        /// </summary>
        /// <param name="remotePath"></param>
        /// <param name="localPath"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static int Connect(string remotePath, string localPath, string username, string password)
        {
            NETRESOURCE[] share_driver = new NETRESOURCE[1];
            share_driver[0].dwScope = RESOURCE_SCOPE.RESOURCE_GLOBALNET;
            share_driver[0].dwType = RESOURCE_TYPE.RESOURCETYPE_DISK;
            share_driver[0].dwDisplayType = RESOURCE_DISPLAYTYPE.RESOURCEDISPLAYTYPE_SERVER;
            share_driver[0].dwUsage = RESOURCE_USAGE.RESOURCEUSAGE_ALL;
            share_driver[0].lpLocalName = null;
            share_driver[0].lpRemoteName = remotePath;

            //Disconnect(localPath);
            //响应慢
            int ret = WNetAddConnection2(share_driver, password, username, 1);

            return ret;
        }

        public static int Disconnect(string localpath)
        {
            return WNetCancelConnection2(localpath, 1, 1);
        }
        /// <summary>
        /// 通过dos连接共享服务器
        /// </summary>
        /// <param name="remotePath"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static int DosConnect(string remotePath, string userName, string password,out string errorMsg)
        {
            int res = 0;
            errorMsg = "";
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                string dosLine = "net use " + remotePath + " " + password + " /user:" + userName + " /persistent:yes";
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (!proc.HasExited)
                {
                    proc.WaitForExit(1000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                proc.StandardError.Close();
                if (string.IsNullOrEmpty(errormsg))
                {
                    res = 0;
                }
                else if (errormsg.Contains("1219"))
                {
                    res = 1219;
                }
                else
                {
                    errorMsg = errormsg;
                    res = 999999;
                }
            }
            catch (Exception ex)
            {
                res = 999999;
                throw ex;
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return res;
        }
    }
}
