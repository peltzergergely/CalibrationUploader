using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CalibrationUploader
{
    /// <summary>
    /// Gets a filename as parameter - done
    /// joins it to the predefined path in the config - done
    /// looks into the xml for serial and result - done
    /// converts to byte
    /// uploads: id, filename, html and xml to server
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            string fileName = args[0];
            string folderPath = ConfigurationManager.AppSettings["FolderPath"];
            string fullPath = folderPath + fileName + ".xml";

            XNamespace tr = "urn:IEEE-1636.1:2011:01:TestResults";

            XElement xmlRootElement = XElement.Load(fullPath);
            IEnumerable<XElement> getID = from resultIDs in xmlRootElement.Descendants(tr + "UUT") select resultIDs;
            Console.WriteLine((string)getID.FirstOrDefault());

            IEnumerable<XElement> getOutcome = from resultOutcomes in xmlRootElement.Descendants(tr + "Outcome") select resultOutcomes;
            Console.WriteLine((string)getOutcome.FirstOrDefault().Attribute("value"));
            Console.ReadKey();
        }
    }
}
