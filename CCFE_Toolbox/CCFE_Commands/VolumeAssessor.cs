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

    class VolumeAssessor : CommandCapsule
    {
        // This command name must match that in the Ribbon.xml file
        //----------------------------------------------------------
        public bool first = true;
        public string directory = @"C:\ProgramData\SpaceClaim\AddIns\Samples\V18\CCFE_Toolkit\Resources\PythonScripts\Volume_assessor.scscript";
        public const string CommandName = "CCFE_Toolbox.C#.V18.VA";

        public VolumeAssessor() : base(CommandName, Resources.VAText, Resources.va_image, Resources.VAHint)
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

                // Dialogue variables
                string filePath;
                double depth;

                // Get save location & tree depth via user input
                using (var dialogue = new UI.VolumeAssessorForm())
                {

                    if (dialogue.ShowDialog() != DialogResult.OK)
                        return;

                    var nameAndRendering = new List<KeyValuePair<string, Graphic>>();
                    var partToGraphic = new Dictionary<Part, Graphic>();

                    var style = new GraphicStyle
                    {
                        EnableDepthBuffer = true
                    };

                    filePath = dialogue.FileName;
                    depth = dialogue.Depth;

                }

                if (window == null)
                    return;

                // To pass args to python script
                var scriptParams = new Dictionary<string, object>();
                scriptParams.Add("d", depth);
                scriptParams.Add("filepath", filePath);

                // Run the script w/ args
                SpaceClaim.Api.V18.Application.RunScript(directory, scriptParams);
                
                // show txt output
                Process.Start(filePath);
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
