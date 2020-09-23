using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace Demo_MVVMBasic.DataAccessLayer
{
    public class DataServiceXml : IDataService
    {
        private string _dataFilePath;
        private List<Widget> _widgets;

        /// <summary>
        /// read the xml file and load a list of widget objects
        /// </summary>
        /// <returns>list of widgets</returns>
        public IEnumerable<Widget> ReadAll()
        {
            List<Widget> widgets = new List<Widget>();

            XmlSerializer serializer = new XmlSerializer(typeof(List<Widget>));

            try
            {
                StreamReader reader = new StreamReader(_dataFilePath);
                using (reader)
                {
                    widgets = (List<Widget>)serializer.Deserialize(reader);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return widgets;
        }

        /// <summary>
        /// write the current list of widgets to the xml data file
        /// </summary>
        /// <param name="_widgets">list of widgets</param>
        public void WriteAll()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Widget>), new XmlRootAttribute("ArrayOfWidget"));

            try
            {
                StreamWriter writer = new StreamWriter(_dataFilePath);
                using (writer)
                {
                    serializer.Serialize(writer, _widgets);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Widget> GetAll()
        {
            return ReadAll();
        }

        public Widget GetById(int id)
        {
            return _widgets.FirstOrDefault(w => w.Id == id);
        }

        public void Add(Widget widget)
        {
            _widgets.Add(widget);
            WriteAll();
        }

        public void Update(Widget widget)
        {
            _widgets.Remove(_widgets.FirstOrDefault(w => w.Id == widget.Id));
            _widgets.Add(widget);
            WriteAll();
        }

        public void Delete(int id)
        {
            _widgets.Remove(_widgets.FirstOrDefault(w => w.Id == id));
            WriteAll();
        }

        public DataServiceXml()
        {
            _dataFilePath = DataServiceConfig.DataPathXml;
            _widgets = ReadAll() as List<Widget>;
        }
    }
}
