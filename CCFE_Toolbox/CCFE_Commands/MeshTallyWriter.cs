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

    class MeshTallyWriter : CommandCapsule
    {
        // This command name must match that in the Ribbon.xml file
        //----------------------------------------------------------
        public bool first = true;
        public string directory = @"C:\ProgramData\SpaceClaim\AddIns\Samples\V18\CCFE_Toolkit\Resources\PythonScripts\Mesh_tally_writer.scscript";
        public const string CommandName = "CCFE_Toolbox.C#.V18.MTW";

        public MeshTallyWriter() : base(CommandName, Resources.MTWText, Resources.mtw_image, Resources.MTWHint)
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

                int meshes=0;
                foreach (IPart part in WalkParts(rootPart))
                {
                    Part partMaster = part.Master;
                    if (GetPartName(partMaster).Equals("MESH"))
                    {
                        
                        meshes = part.Bodies.Count;
                    }
                }

                // Dialogue variables
                string filePath;

                // mesh tally save location user input
                using (var dialogue = new SaveFileDialog())
                {
                    dialogue.FileName = "meshcard.txt";
                    dialogue.DefaultExt = "txt";
                    dialogue.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    dialogue.FilterIndex = 2;
                    dialogue.OverwritePrompt = true;

                    if (dialogue.ShowDialog() != DialogResult.OK)
                        return;

                    var nameAndRendering = new List<KeyValuePair<string, Graphic>>();
                    var partToGraphic = new Dictionary<Part, Graphic>();

                    var style = new GraphicStyle
                    {
                        EnableDepthBuffer = true
                    };
                    
                    filePath = dialogue.FileName;
                }

                var scriptParams = new Dictionary<string, object>();
                string particleType = "n";
                int tallyNum = 14;
                double xRes = 1.0;
                double yRes = 1.0;
                double zRes = 1.0;
                int i = 0;

                // get mesh properties via user input
                while (i < meshes)
                {
                    using (var dialogue = new UI.MeshTallyWriterForm2())
                    {
                        if (dialogue.ShowDialog() != DialogResult.OK)
                            return;

                        var nameAndRendering = new List<KeyValuePair<string, Graphic>>();
                        var partToGraphic = new Dictionary<Part, Graphic>();

                        var style = new GraphicStyle
                        {
                            EnableDepthBuffer = true
                        };

                        particleType = dialogue.ParticleSelection;
                        tallyNum = dialogue.TallyNumber;
                        xRes = dialogue.XResolution;
                        yRes = dialogue.YResolution;
                        zRes = dialogue.ZResolution;

                        scriptParams.Add("tallynum" + i.ToString(), tallyNum);
                        scriptParams.Add("particle" + i.ToString(), particleType);
                        scriptParams.Add("xres" + i.ToString(), xRes);
                        scriptParams.Add("yres" + i.ToString(), yRes);
                        scriptParams.Add("zres" + i.ToString(), zRes);

                        i++;
                    }
                }

                if (window == null)
                    return;

                scriptParams.Add("filepath", filePath);
                scriptParams.Add("meshquant", meshes);

                // Run the script w/ args
                SpaceClaim.Api.V18.Application.RunScript(directory, scriptParams);
            }
            else
            {
                MessageBox.Show("ERROR: Script not found.");
            }
        }

        static IEnumerable<IPart> WalkParts(Part part)
        {
            Debug.Assert(part != null);

            // GetDescendants goes not include the object itself
            yield return part;

            foreach (IPart descendant in part.GetDescendants<IPart>())
                yield return descendant;
        }

        static string GetPartName(Part partMaster)
        {
            /*
			 * The Structure View shows the name of the document if the document has been saved, 
			 * and only uses the name of the part itself if the document has not been saved.
			 * For consistency, we shall do the same.
			 */
            Document doc = partMaster.Document;
            if (partMaster == doc.MainPart)
            {
                string path = doc.Path;
                if (!string.IsNullOrEmpty(path)) // doc has been saved - use file name
                    return Path.GetFileNameWithoutExtension(path);
            }

            return partMaster.Name;
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
