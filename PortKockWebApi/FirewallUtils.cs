using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NetFwTypeLib;

namespace PortKnockService
{
    public class FirewallUtils
    {
        public static bool IsPortOpen(string remoteAddress, int port)
        {
            EnsureSetup();

            Type progID = Type.GetTypeFromProgID("HNetCfg.FwMgr");
            INetFwMgr firewall = Activator.CreateInstance(progID) as INetFwMgr;
            INetFwOpenPorts ports = firewall.LocalPolicy.CurrentProfile.GloballyOpenPorts;
            IEnumerator portEnumerate = ports.GetEnumerator();
            while ((portEnumerate.MoveNext()))
            {
                INetFwOpenPort currentPort = portEnumerate.Current as INetFwOpenPort;
                if (currentPort.Port == port && currentPort.RemoteAddresses == remoteAddress)
                    return true;
            }
            return false;
        }

        public static void AllowAddressPort(string remoteAddress, int port)
        {
            EnsureSetup();

            if (IsPortOpen(remoteAddress, port))
                return;

            INetFwOpenPort openPort = GetInstance("INetOpenPort") as INetFwOpenPort;
            openPort.Port = port;
            openPort.Protocol = NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
            openPort.Name = "allow_" + remoteAddress + "_" + port.ToString();
            openPort.RemoteAddresses = remoteAddress;

            INetFwOpenPorts openPorts = sm_fwProfile.GloballyOpenPorts;
            openPorts.Add(openPort);
        }

        public static void CloseAddressPort(string remoteAddress, int port)
        {
            EnsureSetup();

            if (!IsPortOpen(remoteAddress, port))
                return;

            INetFwOpenPorts ports = sm_fwProfile.GloballyOpenPorts;
            ports.Remove(port, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP);
        }

        private static object GetInstance(string typeName)
        {
            Type tpResult = null;
            switch (typeName)
            {
                case "INetFwMgr":
                    tpResult = Type.GetTypeFromCLSID(new Guid("{304CE942-6E39-40D8-943A-B913C40C9CD4}"));
                    return Activator.CreateInstance(tpResult);
                case "INetAuthApp":
                    tpResult = Type.GetTypeFromCLSID(new Guid("{EC9846B3-2762-4A6B-A214-6ACB603462D2}"));
                    return Activator.CreateInstance(tpResult);
                case "INetOpenPort":
                    tpResult = Type.GetTypeFromCLSID(new Guid("{0CA545C6-37AD-4A6C-BF92-9F7610067EF5}"));
                    return Activator.CreateInstance(tpResult);
                default:
                    throw new Exception("Unknown type name: " + typeName);
            }
        }

        private static void EnsureSetup()
        {
            if (sm_fwProfile != null)
                return;

            INetFwMgr fwMgr = GetInstance("INetFwMgr") as INetFwMgr;
            sm_fwProfile = fwMgr.LocalPolicy.CurrentProfile;
        }

        private static INetFwProfile sm_fwProfile = null;
    }
    
}