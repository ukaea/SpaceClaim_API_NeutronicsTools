using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using SpaceClaim.Api.V18.Extensibility;
using SpaceClaim.Api.V18.Geometry;
using SpaceClaim.Api.V18.Modeler;
using System.Xml.Serialization;
using CCFE_Toolbox.Properties;
using System.Windows.Forms;
using SpaceClaim.Api.V18;
using SpaceClaim.Api.V18.Display;
using Point = SpaceClaim.Api.V18.Geometry.Point;
using SpaceClaim.Api.V18.Scripting;
using System.Text.RegularExpressions;

namespace CCFE_Toolbox.CCFE_Commands
{
    //class ScriptClass : 

    class CylinderPlaneSeperator : CommandCapsule
    {
        // This command name must match that in the Ribbon.xml file
        //----------------------------------------------------------
        public bool first = true;
        public string directory = @"C:\ProgramData\SpaceClaim\AddIns\Samples\V18\CCFE_Toolkit\Resources\PythonScripts\Cylinder-plane_splitter.scscript";
        public const string CommandName = "CCFE_Toolbox.C#.V18.CPS";

        public CylinderPlaneSeperator() : base(CommandName, Resources.CylPlSepText, Resources.cps_image, Resources.CylPlSepHint)
        {

        }

        //protected override void OnUpdate(Command command)
        //{
            /*
            if (first)
            {
                directory = Directory.GetCurrentDirectory();
                first = false;
            }
            */
            //Window window = Window.ActiveWindow;
            //command.IsEnabled = window != null && SelectionAllBodies(window);
        //}
        protected override void OnExecute(Command command, ExecutionContext context, Rectangle buttonRect)
        {
            if (File.Exists(directory))
            {

                // Instance common functions class
                InstanceClasses.CommonSpaceClaimFunctions FunctionsClass = new InstanceClasses.CommonSpaceClaimFunctions();

                // Variables
                Window window = Window.ActiveWindow;
                Document doc = window.Document;
                Part rootPart = doc.MainPart;


                // Run the cylinder plane seperator script
                SpaceClaim.Api.V18.Application.RunScript(directory);
            }
            else
            {
                MessageBox.Show("ERROR: Script not found.");
            }
        }

        public bool SelectionAllBodies(Window window)
        {
            ICollection<IDocObject> docObjects = window.ActiveContext.Selection;
            if (docObjects.Count == 0)
            {
                return true;
            }
            foreach (IDocObject obj in docObjects)
            {
                var body = obj as IDesignBody;
                if (body == null)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
