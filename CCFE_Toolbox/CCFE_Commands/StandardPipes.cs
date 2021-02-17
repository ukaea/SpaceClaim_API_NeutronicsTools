using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace CCFE_Toolbox.CCFE_Commands
{
    class StandardPipeDia
    {
        public static Dictionary<string, List<double>> getStandardPipeDia()
        {
            //StreamReader FileReader = new StreamReader(@);
            string FileContents= CCFE_Toolbox.Properties.Resources.asme_pipes;
            //FileContents = FileReader.ReadToEnd();
            Dictionary<string, List<double>> pipe_diameters = new Dictionary<string, List<double>>();

            string[] lines = FileContents.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            foreach (string line in lines)
            {
                string[] lineparts = line.Split(',');
                List<double> dia = new List<double>();
                dia.Add(Convert.ToDouble(lineparts[2])/2/1000.0);
                dia.Add(((Convert.ToDouble(lineparts[2])/2.0) - Convert.ToDouble(lineparts[3]))/1000.0);
                pipe_diameters.Add(lineparts[0] + lineparts[1], dia);
            }

            return (pipe_diameters);
        }
    }
}
