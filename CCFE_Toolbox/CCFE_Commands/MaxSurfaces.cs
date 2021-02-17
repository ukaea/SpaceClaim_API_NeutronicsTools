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
    class MaxSurfaces : CommandCapsule
    {
        // This command name must match that in the Ribbon.xml file
        //----------------------------------------------------------

        public const string CommandName = "CCFE_Toolbox.C#.V18.MaxSurfaces";

        public MaxSurfaces() : base(CommandName, Resources.MaxSurfaceText, Resources.SceneGraph, Resources.MaxSurfaceHint)
        {

        }

        protected override void OnUpdate(Command command)
        {
            command.IsEnabled = Window.ActiveWindow != null;
        }
        protected override void OnExecute(Command command, ExecutionContext context, Rectangle buttonRect)
        {
            /*
             * Max Surface Tool 
             * A. Burns 06/11/2017
             * Read user inuput max surfaces per body
             * Hightlight body with greater number of faces
             * Optionally can chnage the bodies colour 
             * as well as highlighting
            */



            // Instance common functions class
            InstanceClasses.CommonSpaceClaimFunctions FunctionsClass = new InstanceClasses.CommonSpaceClaimFunctions();

            // Essential Variables
            Window window = Window.ActiveWindow;
            Document doc = window.Document;
            Part rootPart = doc.MainPart;
            InteractionContext interContext = window.ActiveContext;

            // Other Variables
            List<IDesignBody> allBodies = new List<IDesignBody>();
            List<IDesignFace> ifinalFacesList = new List<IDesignFace>();
            window.SetTool(new ToolClass());

            int faceCount;
            int maxFaces;
            bool changeColours;
            string colourString;

            using (var dialogue = new UI.MaxSurfacesForm())
            {
                if (dialogue.ShowDialog() != DialogResult.OK)
                    return;

                var nameAndRendering = new List<KeyValuePair<string, Graphic>>();
                var partToGraphic = new Dictionary<Part, Graphic>();

                var style = new GraphicStyle
                {
                    EnableDepthBuffer = true
                };

                maxFaces = (int)dialogue.MaxSurfaces;
                changeColours = dialogue.ChangeColour;
                colourString = dialogue.ColourSelection;
            }

            if (window == null)
                return;

            allBodies.AddRange(FunctionsClass.GatherAllVisibleBodies(rootPart, window));
           

            foreach (IDesignBody iDesBody in allBodies)
            {
                DesignBody desBody = iDesBody.Master;
                Body body = desBody.Shape.Copy();

                var faces = body.Faces;
                var iFaces = iDesBody.Faces;
                // ifinalFacesList.AddRange(iDesBody.Faces);
                faceCount = faces.Count;

                if (faceCount > maxFaces)
                {
                    window.ActiveTool.CreateIndicator(iFaces);
                    if (changeColours)
                    {
                        if (colourString == "Blue")
                        {
                            desBody.SetColor(null, Color.Blue);
                        }
                        else if (colourString == "Red")
                        {
                            desBody.SetColor(null, Color.Red);
                        }
                        else if (colourString == "Green")
                        {
                            desBody.SetColor(null, Color.Green);
                        }
                        else if (colourString == "Orange")
                        {
                            desBody.SetColor(null, Color.Orange);
                        }
                        else if (colourString == "Black")
                        {
                            desBody.SetColor(null, Color.Black);
                        }
                        else if (colourString == "Yellow")
                        {
                            desBody.SetColor(null, Color.Yellow);
                        }
                        else if (colourString == "Salmon Pink")
                        {
                            desBody.SetColor(null, Color.Salmon);
                        }
                    }
                }

            }
        }
    }
}
