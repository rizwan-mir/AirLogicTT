using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AirLogicTT.Interface;

namespace AirLogicTT.BusinessLayer
{
    public class BLServiceFactory
    {
        public static IBLService GetBLServiceObj()
        {
            return new BLService();
        }
    }
}