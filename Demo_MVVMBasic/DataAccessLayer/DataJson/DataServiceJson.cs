using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using Newtonsoft;
using Newtonsoft.Json;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Demo_MVVMBasic.DataAccessLayer
{
    public class DataServiceJson : IDataService
    {
        private string _dataFilePath;
        private List<Widget> _widgets;

        /// <summary>
        /// read the json file and load a list of widget objects
        /// </summary>
        /// <returns>list of widgets</returns>
        public IEnumerable<Widget> ReadAll()
        {
            List<Widget> widgets;

            try
            {
                using (StreamReader sr = new StreamReader(_dataFilePath))
                {
                    string jsonString = sr.ReadToEnd();

                    widgets = JsonConvert.DeserializeObject<List<Widget>>(jsonString);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return widgets;
        }

        /// <summary>
        /// write the current list of widgets to the json data file
        /// </summary>
        /// <param name="widgets">list of widgets</param>
        public void WriteAll()
        {
            string jsonString = JsonConvert.SerializeObject(_widgets, Formatting.Indented);

            try
            {
                StreamWriter writer = new StreamWriter(_dataFilePath);
                using (writer)
                {
                    writer.WriteLine(jsonString);
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

        public DataServiceJson()
        {
            _dataFilePath = DataServiceConfig.DataPathJson;
            _widgets = ReadAll() as List<Widget>;
        }
    }
}
