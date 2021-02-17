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
using System.Linq.Expressions;
using System.Windows.Forms.VisualStyles;

namespace CCFE_Toolbox.CCFE_Commands
{
    //class ScriptClass : 

    class GeometryAssessor : CommandCapsule
    {
        // This command name must match that in the Ribbon.xml file
        //----------------------------------------------------------
        public bool first = true;
        public string directory = @"C:\ProgramData\SpaceClaim\AddIns\Samples\V18\CCFE_Toolkit\Resources\PythonScripts\Geometry_assessor.scscript";
        public const string CommandName = "CCFE_Toolbox.C#.V18.GA";

        public GeometryAssessor() : base(CommandName, Resources.GeomAssText, Resources.ga_image, Resources.GeomAssHint)
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
                string filename;
                Color coneclr;
                Color nurbsclr;
                Color onaclr;
                Color offaclr;
                Color procclr;

                // Get save location via user input
                //using (var dialogue = new SaveFileDialog())
                //{
                //    dialogue.FileName = "GeometryReport.txt";
                //    dialogue.DefaultExt = "txt";
                //    dialogue.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                //    dialogue.FilterIndex = 2;
                //    dialogue.OverwritePrompt = true;
                //
                //    if (dialogue.ShowDialog() != DialogResult.OK)
                //        return;
                //
                //    var nameAndRendering = new List<KeyValuePair<string, Graphic>>();
                //    var partToGraphic = new Dictionary<Part, Graphic>();
                //
                //    var style = new GraphicStyle
                //    {
                //        EnableDepthBuffer = true
                //    };
                //
                //    filePath = dialogue.FileName;
                //}

                //if (window == null)
                //    return;

                using(var form = new UI.GeometryAssessorForm())
                {
                    if (form.ShowDialog() != DialogResult.OK)
                        return;

                    var nameAndRendering = new List<KeyValuePair<string, Graphic>>();
                    var partToGraphic = new Dictionary<Part, Graphic>();

                    var style = new GraphicStyle
                    {
                        EnableDepthBuffer = true
                    };

                    filename = form.FileName;
                    coneclr = form.color1;
                    nurbsclr = form.color2;
                    onaclr = form.color3;
                    offaclr = form.color4;
                    procclr = form.color5;

                }

                if (window == null)
                    return;

                    // To pass args to python script
                var scriptParams = new Dictionary<string, object>();
                scriptParams.Add("filepath", filename);
                scriptParams.Add("conea", Convert.ToInt32(coneclr.A));
                scriptParams.Add("coner", Convert.ToInt32(coneclr.R));
                scriptParams.Add("coneg", Convert.ToInt32(coneclr.G));
                scriptParams.Add("coneb", Convert.ToInt32(coneclr.B));
                scriptParams.Add("nurba", Convert.ToInt32(nurbsclr.A));
                scriptParams.Add("nurbr", Convert.ToInt32(nurbsclr.R));
                scriptParams.Add("nurbg", Convert.ToInt32(nurbsclr.G));
                scriptParams.Add("nurbb", Convert.ToInt32(nurbsclr.B));
                scriptParams.Add("onaa", Convert.ToInt32(onaclr.A));
                scriptParams.Add("onar", Convert.ToInt32(onaclr.R));
                scriptParams.Add("onag", Convert.ToInt32(onaclr.G));
                scriptParams.Add("onab", Convert.ToInt32(onaclr.B));
                scriptParams.Add("offaa", Convert.ToInt32(offaclr.A));
                scriptParams.Add("offar", Convert.ToInt32(offaclr.R));
                scriptParams.Add("offag", Convert.ToInt32(offaclr.G));
                scriptParams.Add("offab", Convert.ToInt32(offaclr.B));
                scriptParams.Add("proca", Convert.ToInt32(procclr.A));
                scriptParams.Add("procr", Convert.ToInt32(procclr.R));
                scriptParams.Add("procg", Convert.ToInt32(procclr.G));
                scriptParams.Add("procb", Convert.ToInt32(procclr.B));

                // Run the script w/ args
                SpaceClaim.Api.V18.Application.RunScript(directory, scriptParams);

                // Show txt output
                Process.Start(filename);
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
