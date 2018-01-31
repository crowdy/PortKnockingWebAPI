using PortKnockService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;

namespace PortKockWebApi.Controllers
{
    public class PortKnockController : ApiController
    {

        [HttpGet]
        public PortKnockResponse DoGet(PortKnockRequest req)
        {
            return new PortKnockResponse()
            {
                Result = "NG",
                Message = "unknown method"
            };
        }


        [HttpPost]
        public PortKnockResponse DoPost(PortKnockRequest req)
        {

            // need to log here.

            if (req.Action.ToLower() == "allow") {
                try
                {
                    FirewallUtils.AllowAddressPort(req.Ip, req.Port);
                    return new PortKnockResponse()
                    {
                        Result = "OK",
                        Message = null
                    };

                }
                catch (Exception e)
                {
                    return new PortKnockResponse()
                    {
                        Result = "NG",
                        Message = e.Message
                    };
                }
            }
            else if (req.Action.ToLower() == "deny")
            {
                try
                {
                    FirewallUtils.CloseAddressPort(req.Ip, req.Port);
                    return new PortKnockResponse()
                    {
                        Result = "OK",
                        Message = null
                    };

                }
                catch (Exception e)
                {
                    return new PortKnockResponse()
                    {
                        Result = "NG",
                        Message = e.Message
                    };
                }
            }

            return new PortKnockResponse()
            {
                Result = "NG",
                Message = "unknown error"
            };
        }
    }

    [Serializable]
    [DataContract(Name = "")]
    public class PortKnockRequest
    {
        [DataMember(Name = "action", IsRequired = true)]
        public string Action { get; set; }

        [DataMember(Name = "ip", IsRequired = true)]
        public string Ip { get; set; }

        [DataMember(Name = "port", IsRequired = true)]
        public int Port { get; set; }
    }

    [Serializable]
    [DataContract(Name = "")]
    public class PortKnockResponse
    {
        [DataMember(Name = "result")]
        public string Result { get; set; }

        [DataMember(Name = "message", IsRequired =false)]
        public string Message { get; set; }

    }
}
