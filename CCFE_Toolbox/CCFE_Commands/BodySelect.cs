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

namespace CCFE_Toolbox.CCFE_Commands
{

    class ToolClass : Tool
    {
        public ToolClass() : base(InteractionMode.Solid)
        {
           

        }

#if false
		protected override bool OnEscape() {
			Window.SetTool(new SketchTool());
			return true;
		}
#endif
        /*
        public void ProduceIndicator(IDesignFace face)
        {
            var tempList = new List<IDesignFace>();
            tempList.Add(face);
            ICollection<IDesignFace> faces = tempList;
            CreateIndicator(faces);
        }

        public void LineGraphic()
        {
            
            var testStyle = new GraphicStyle
            {
                LineColor = Color.Red,
                LineWidth = 2
            };

            LineSegment lineSeg = new LineSegment(Point.Create(0, 0, 0), Point.Create(2, 2, 2));
            Rendering = Graphic.Create(testStyle, LinePrimitive.Create(lineSeg));
        }
        */

    }


        class BodySelect : CommandCapsule
    {
        // This command name must match that in the Ribbon.xml file
        //----------------------------------------------------------

        public const string CommandName = "CCFE_Toolbox.C#.V18.BodySelect";

        public BodySelect() : base(CommandName, Resources.BodySelectText, Resources.CreateAssembly, Resources.BodySelectHint)
        {

        }
        protected override void OnInitialize(Command command)
        {

        }
        protected override void OnUpdate(Command command)
        {
           
        }
        protected override void OnExecute(Command command, ExecutionContext context, Rectangle buttonRect)
        {
            /*
             * Provide dialogue box and user inputs what they want to locate
             * These selections are Tori, Small Holes or Small Volumes
             * For Small Volumes and Holes the user will need to input a value
             * Each will be handled seperately and find matching bodies in all part design bodies
             * All will be then added to a final selection list for the user
            */

            // Instance common functions class
            InstanceClasses.CommonSpaceClaimFunctions FunctionsClass = new InstanceClasses.CommonSpaceClaimFunctions();

            // Variables
            Window window = Window.ActiveWindow;
            window.SetTool(new ToolClass());
            Document doc = window.Document;
            Part rootPart = doc.MainPart;
            InteractionContext interContext = window.ActiveContext;

            List<IDesignBody> selectionBodies = new List<IDesignBody>();
            List<IDesignBody> matchingBodies = new List<IDesignBody>();
            List<Face> facesList = new List<Face>();
            List<IDesignFace> iFacesList = new List<IDesignFace>();
            List<IDesignFace> ifinalFacesList = new List<IDesignFace>();
            ICollection<IDesignFace> IcollectionFaces = null;

            double minradius;
            double minVolume;
            double minFace;

            bool findCone = false;
            bool findTori = false;
            bool findHoles = false;
            bool findVolume = false;
            bool findFace = false;
#pragma warning disable CS0219 // The variable 'faceCounter' is assigned but its value is never used
            int faceCounter = 0;
#pragma warning restore CS0219 // The variable 'faceCounter' is assigned but its value is never used


#pragma warning disable CS0219 // The variable 'intMode' is assigned but its value is never used
            InteractionMode intMode = InteractionMode.Solid;
#pragma warning restore CS0219 // The variable 'intMode' is assigned but its value is never used

           

            if (window == null)
                return;

            #region Dialogue

            // Create dialogue box
            using (var dialogue = new UI.BodySelectForm())
            {
                if (dialogue.ShowDialog() != DialogResult.OK)
                    return;

                var nameAndRendering = new List<KeyValuePair<string, Graphic>>();
                var partToGraphic = new Dictionary<Part, Graphic>();

                var style = new GraphicStyle
                {
                    EnableDepthBuffer = true
                };

                findCone = dialogue.LocateCone;
                findTori = dialogue.LocateTori;
                findHoles = dialogue.LocateHoles;
                findVolume = dialogue.LocateVolume;
                findFace = dialogue.LocateFace;

                minradius = dialogue.minRadius;
                minVolume = dialogue.minVolume;
                minFace = dialogue.minFace;               
            }

            #endregion        

            // Gather all design bodies
            //-----------------------------------------------
            selectionBodies.AddRange(FunctionsClass.GatherAllVisibleBodies(rootPart, window));

            // Check if each body fits wanted criteria
            //-----------------------------------------------
            foreach (IDesignBody iDesBod in selectionBodies)
            {
                DesignBody master = iDesBod.Master;
                Body body = master.Shape;

                var faces = body.Faces;
                facesList.AddRange(faces);
                var iFaces = iDesBod.Faces;
                iFacesList.AddRange(iFaces);

                Matrix masterTrans = iDesBod.TransformToMaster;
                Matrix reverseTrans = masterTrans.Inverse;

                // If body fits volume criteria
                //----------------------------------------
                if (findVolume && body.Volume * 1e6 < minVolume)
                {
                    matchingBodies.Add(iDesBod);
                }
                else
                {
                    // If body fits tori or hole criteria
                    //----------------------------------------
                    for (int i = 0; i < iFacesList.Count; i++)
                    {
                        IDesignFace face = iFacesList[i];                             
                        
                        if (face.Area * 1e4 < minFace)
                        {
                            ifinalFacesList.Clear();
                            ifinalFacesList.Add(face);
                            window.ActiveTool.CreateIndicator(ifinalFacesList);
                        }

                        if (face.Shape.GetGeometry<Cone>() != null && findCone)
                        {
                            ifinalFacesList.Clear();
                            ifinalFacesList.Add(face);
                            window.ActiveTool.CreateIndicator(ifinalFacesList);
                        }

                        if (face.Shape.GetGeometry<Torus>() != null && findTori)
                        {    
                            ifinalFacesList.Clear();
                            ifinalFacesList.Add(face);
                            window.ActiveTool.CreateIndicator(ifinalFacesList);
                        }
                        else if (face.Shape.GetGeometry<Cylinder>() != null && face.Shape.GetGeometry<Cylinder>().Radius * 1e2  < minradius && findHoles)
                        {
                            //FunctionsClass.ColourFace(facesList[i], master);
                            ifinalFacesList.Clear();
                            ifinalFacesList.Add(face);
                            window.ActiveTool.CreateIndicator(ifinalFacesList);
                            var radius = face.Shape.GetGeometry<Cylinder>().Radius;
                            /*
                            if (IsHole(face, radius))
                            {
                                ifinalFacesList.Clear();
                                ifinalFacesList.Add(face);
                                window.ActiveTool.CreateIndicator(ifinalFacesList);
                            }
                            */
                        }
                        else
                        {
                            // Set all the other face to slightly translucent
                            //FunctionsClass.SetFacetranslucent(face, master);
                        }
                        
                    }

                }

                facesList.Clear();
                iFacesList.Clear();

                
            }
            //ICollection<IDocObject> bodyCollection = null;

            // For each matching body change the colour to red
            //----------------------------------------------------
            foreach (IDesignBody finalBody in matchingBodies)
            {
                //bodyCollection.Add(finalBody);
                DesignBody finalMaster = finalBody.Master;
                finalMaster.SetColor(null, Color.Red);
            }
            var point = Point.Create(1, 1, 1);
            IcollectionFaces = ifinalFacesList;
            
            // Also set them as selected by the user
            //----------------------------------------------------           
            //interContext.Selection = bodyCollection;



            

        }

        // Identify if hole and if has width / radius less than specified
        //------------------------------------------------------------------


        private bool IsHole (IDesignFace face, double radius)
        {
            // Take face and find the adjacent faces
            var adjFaces = face.AdjacentFaces;
            var adjFaceList = new List<IDesignFace>();            

            // Hole type [1] Cyl plane with two adjacent planes at 90 degrees
            if (adjFaceList.Count == 2)
            {     
                // if 2 adjacent planes                          
                if (adjFaceList[0].Shape.GetGeometry<Plane>() != null && adjFaceList[1].Shape.GetGeometry<Plane>() != null)
                {
                    // Get the angles of both planes
                    return true;
                                   
                }
                else
                {
                    return false;
                }
                
            }

            
            else
            {
                return false;
            }
        }

        
    }

   
}
