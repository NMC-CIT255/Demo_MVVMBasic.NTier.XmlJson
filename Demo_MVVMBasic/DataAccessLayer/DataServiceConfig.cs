using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_MVVMBasic.DataAccessLayer
{
    public class DataServiceConfig
    {
        //
        // set the type of persistence
        //
        private DataType _dataType = DataType.XML;
        //private DataType _dataType = DataType.JSON;

        //
        // set data paths for the Json and XML files
        //
        public static string DataPathJson => @"DataAccessLayer\DataJson\Widgets.json";
        public static string DataPathXml => @"DataAccessLayer\DataXml\Widgets.xml";

        /// <summary>
        /// instantiate and return the correct data service (XML or Json) 
        /// </summary>
        /// <returns>data service object</returns>
        public IDataService SetDataService()
        {
            switch (_dataType)
            {
                case DataType.XML:
                    return new DataServiceXml();

                case DataType.JSON:
                    return new DataServiceJson();

                default:
                    throw new Exception();
            }
        }
    }
}
