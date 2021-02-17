using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using System.IO;
using SpaceClaim.Api.V18;
using SpaceClaim.Api.V18.Extensibility;
using SpaceClaim.Api.V18.Geometry;
using SpaceClaim.Api.V18.Modeler;
#pragma warning disable CS0105 // The using directive for 'System.Windows.Media.Media3D' appeared previously in this namespace
using System.Windows.Media.Media3D;
#pragma warning restore CS0105 // The using directive for 'System.Windows.Media.Media3D' appeared previously in this namespace
using Point = SpaceClaim.Api.V18.Geometry.Point;
using SpaceClaim.Api.V18.Scripting;
using SpaceClaim.Api.V18.Display;
using CCFE_Toolbox.UI;


namespace CCFE_Toolbox.CCFE_Commands
{
    class Pipes_simp : CommandCapsule
    {
        // This command name must match that in the Ribbon.xml file
        //----------------------------------------------------------

        public const string CommandName = "CCFE_Toolbox.C#.V18.Pipes_simp";

        public Pipes_simp() : base(CommandName, Properties.Resources.Pipes_simp_text, Properties.Resources.pipe, Properties.Resources.Pipes_simp_hint)
        {
        }

        protected override void OnInitialize(Command command)
        {
            // Add a keyboard shortcut for this command.
            // A block will be created when Ctrl+M is pressed.
            const Keys shortcut = Keys.Control | Keys.M;
            if (Command.GetCommand(shortcut) == null) // else shortcut is already used by another command
                command.Shortcuts = new[] { shortcut };
        }

        protected override void OnUpdate(Command command)
        {
            Window window = Window.ActiveWindow;
            command.IsEnabled = window != null && SelectionAllBodies(window);
        }



        protected override void OnExecute(Command command, ExecutionContext context, Rectangle buttonRect)
        {

            Window window = Window.ActiveWindow;
            var allBodies = new List<IDesignBody>();
            Document doc = window.Document;
            Part rootPart = doc.MainPart;
            Part pipesPart = Part.Create(doc, "Simplified Pipes");
            Part brokenPipe = Part.Create(doc, "Broken Pipe");
            int i;
            List<DesignBody> allDesignBodies = new List<DesignBody>();
            List<DesignBody> brokenDesignBodies = new List<DesignBody>();
            List<double> radii = new List<double>();
            List<Color> chosen_colour;
            Component brokenComponent = Component.Create(rootPart, brokenPipe);


            //Selects the active window to work on and work on the solid bodies
            window.InteractionMode = InteractionMode.Solid;

            // -----------------------------------------------------------------
            // Get the selected IDesign bodies
            // -----------------------------------------------------------------
            var desBodies = window.ActiveContext.Selection;
            if (desBodies == null)
            {
                Debug.Fail("Unexpected case.", "Selection was not a single design body.");
                return;
            }


            // --------------------------------------------------------------
            // Open up an output file inorder to write out for testing
            // --------------------------------------------------------------
            //string outputfile = "H:/test_pipe_v2.txt";
            //using (StreamWriter sw = File.CreateText(outputfile))
            //{
            //    sw.WriteLine("OutputFile");
            //}

            //-----------------------------------------------------------------
            // Get User defined requirement from dialogue box
            //-----------------------------------------------------------------
            #region Dialogue Information
            using (var dialogue = new PipeRadiusOptions())
            {
                if (dialogue.ShowDialog() != DialogResult.OK) return;

                var nameAndRendering = new List<KeyValuePair<string, Graphic>>();
                var partToGraphic = new Dictionary<Part, Graphic>();

                var style = new GraphicStyle
                {
                    EnableDepthBuffer = true
                };

                if (dialogue.standardpipe)
                {
                    Dictionary<string, List<double>> standard_pipes = StandardPipeDia.getStandardPipeDia();
                    radii = standard_pipes[dialogue.get_standard_dia + dialogue.get_schedule_num];
                    pipesPart = Part.Create(doc, "Simplified Pipes DN" + dialogue.get_standard_dia + "/" + dialogue.get_schedule_num);

                }
                else if (dialogue.userdefined)
                {

                    radii.Add((double)dialogue.outRadius / 100.0);

                    if (dialogue.hollow)
                    {
                        radii.Add((double)dialogue.inRadius / 100.0);
                        pipesPart = Part.Create(doc, "Simplified Pipes OD=" + dialogue.outRadius.ToString() + " - ID=" + dialogue.inRadius.ToString()+" cm");

                    }
                    else
                    {
                        pipesPart = Part.Create(doc, "Simplified Pipes OD=" + dialogue.outRadius.ToString() + " cm");

                    }

                }
                else 
                {
                    pipesPart = Part.Create(doc, "Simplified Pipes");
                }
                
                chosen_colour = process_colour(dialogue.get_colour);
               
            }
            #endregion
            Component newComponent = Component.Create(rootPart, pipesPart);


            // --------------------------------------------------------------------
            // 
            // --------------------------------------------------------------------
            foreach (IDesignBody desBody in desBodies)
            {

                //try to simplify
                try
                { 
                    DesignBody desBodyMaster = desBody.Master;
                    Body currentBody = desBodyMaster.Shape;

                    List<SpaceClaim.Api.V18.Geometry.Point> pointsList = new List<SpaceClaim.Api.V18.Geometry.Point>();
                    List<Circle> circleList = new List<Circle>();
                    List<Face> faceList = new List<Face>();
                    List<Plane> planeList = new List<Plane>();

                    //Find the end planes of the pipe
                    List<Face> endFaces = findendFaces(currentBody);

                    //                    foreach (Face endFace in endFaces)
                    //                    {
                    //                        DatumPlane newPlane = DatumPlane.Create(rootPart, "end Plane", endFace.GetGeometry<Plane>());
                    //                        FileWriter(outputfile, "plane");
                    //                    }

                    // ---------------------------------------------------------------
                    // If no user defined radii are given work out inner (if present)
                    //  and outer radii of the pipe
                    // ---------------------------------------------------------------
                    try
                    {
                        // If we are just using the pipe radii 
                        if(!radii.Any()) radii = findRadii(endFaces.First());
                    }
                    catch
                    {
                        #region Dialogue Information
                        using (var dialogue = new Pipes_Radius())
                        {
                            if (dialogue.ShowDialog() != DialogResult.OK) return;

                            var nameAndRendering = new List<KeyValuePair<string, Graphic>>();
                            var partToGraphic = new Dictionary<Part, Graphic>();

                            var style = new GraphicStyle
                            {
                                EnableDepthBuffer = true
                            };

                            radii.Add((double)dialogue.outRadius);

                            if (dialogue.hollow)
                            {
                                radii.Add((double)dialogue.inRadius);
                            }
                            
                        }
                        #endregion
                        //                        FileWriter(outputfile, "Unable to find radii ");
                    }


                    //Start from the first end face in list and work along recording faces, circles and points
                    findcircles(endFaces.First(), endFaces.Last(), out pointsList, out circleList, out faceList);

                    //FileWriter(outputfile, "number of points");
                    //FileWriter(outputfile, "Number of points "+pointsList.Count().ToString()); 

//                    foreach (Point point in pointsList)
//                    {
//                        DatumPoint newPoint = DatumPoint.Create(rootPart, "Point", point);
//                    }

                    //For each toriodal face in the pipe create a new "inter" plane which can be used to cut the new cylindrical bodies
                    //This also moves each of the points found at the end of the cylinders to a new position 
                    for (i = 0; i < faceList.Count; i++)
                    {

                        if (faceList[i].GetGeometry<Torus>() != null)
                        {

                            Plane firstplane;
                            Plane secondplane;
                            bool isreversedfirst;
                            bool isreversedsecond;
                            int round = 2;
                            if (i == 0)
                            {
                                //FileWriter(outputfile, "Got Here --");

                                //Create a plane on one side of the tori from the circle and work out if the dirZ is reversed or not
                                firstplane = endFaces.First().GetGeometry<Plane>();
                                isreversedfirst = endFaces.First().IsReversed;
                                //DatumPlane newPlane = DatumPlane.Create(rootPart, "first Plane", firstplane);

                                //Create a plane on the other side of the tori from the circle and work out if the dirZ is reversed or not
                                Vector directionCylsecond;
                                secondplane = createPlane(circleList[i + 1], pointsList[i + 1], pointsList[i + 2], round, out isreversedsecond, out directionCylsecond);

                                //Move the cylinder points by the torus radius inorder to draw over the torus
                                pointsList[i + 1] = pointsList[i + 1] + directionCylsecond.Direction.UnitVector * (faceList[i].GetGeometry<Torus>().MajorRadius + faceList[i].GetGeometry<Torus>().MinorRadius + 0.1);
                            }
                            else if (i == faceList.Count)
                            {
                                //FileWriter(outputfile, "Got Here \\");
                                //Create a plane on one side of the tori from the circle and work out if the dirZ is reversed or not
                                Vector directionCylfirst;
                                firstplane = createPlane(circleList[i], pointsList[i], pointsList[i - 1], round, out isreversedfirst, out directionCylfirst);

                                //Create a plane on the other side of the tori from the circle and work out if the dirZ is reversed or not
                                secondplane = endFaces.Last().GetGeometry<Plane>();
                                isreversedsecond = endFaces.Last().IsReversed;

                                //Move the cylinder points by the torus radius inorder to draw over the torus
                                pointsList[i] = pointsList[i] + directionCylfirst.Direction.UnitVector * (faceList[i].GetGeometry<Torus>().MajorRadius + faceList[i].GetGeometry<Torus>().MinorRadius + 0.1);
                            }
                            else
                            {
                                //Create a plane on one side of the tori from the circle and work out if the dirZ is reversed or not
                                Vector directionCylfirst;
                                firstplane = createPlane(circleList[i], pointsList[i], pointsList[i - 1], round, out isreversedfirst, out directionCylfirst);

                                //Create a plane on the other side of the tori from the circle and work out if the dirZ is reversed or not
                                Vector directionCylsecond;
                                secondplane = createPlane(circleList[i + 1], pointsList[i + 1], pointsList[i + 2], round, out isreversedsecond, out directionCylsecond);

                                //Move the cylinder points by the torus radius inorder to draw over the torus
                                pointsList[i] = pointsList[i] + directionCylfirst.Direction.UnitVector * (faceList[i].GetGeometry<Torus>().MajorRadius + faceList[i].GetGeometry<Torus>().MinorRadius + 0.1);
                                pointsList[i + 1] = pointsList[i + 1] + directionCylsecond.Direction.UnitVector * (faceList[i].GetGeometry<Torus>().MajorRadius + faceList[i].GetGeometry<Torus>().MinorRadius + 0.1);

                            }
                            double angle = 0;
                            Vector3D unitvector;
                            SpaceClaim.Api.V18.Geometry.Point pointonintesect;
                            Plane interPlane = CreateInterPlane(firstplane, secondplane, isreversedfirst, isreversedsecond, out angle, out unitvector, out pointonintesect);
                            planeList.Add(interPlane);
                        }
                        else
                        {
                            planeList.Add(null);
                        }
                    }

                    //foreach (Point point in pointsList)
                    //{
                    //    DatumPoint newPoint = DatumPoint.Create(rootPart, "Point", point);
                    //}

                    //Draw out the new cylinders for each radii
                    //Loop over each of the faces
                    for (i = 0; i < faceList.Count(); i++)
                    {
                        //if the face is not a torus or it is the first or last face we need to draw it out
                        if (faceList[i].GetGeometry<Torus>() == null || i == 0 || i == faceList.Count() - 1)
                        {
                            //List of the bodies for each cylinder (maybe an inner and outer cylinder
                            List<Body> keepBodies = new List<Body>();

                            //for each of the pipe radii we will draw out a cylinder and split it on the relevant planes
                            foreach (Double pipeRadius in radii)
                            {
                                //Create cylinder
                                Body keepBody = createCylinder(faceList[i], endFaces.First(), endFaces.Last(), pipeRadius, i, faceList.Count(), pointsList[i + 1], pointsList[i]);

                                //keepBody = newBody;

                                //split cylinder on one of the relevant planes
                                if (i > 0 && planeList[i - 1] != null)
                                {
                                    keepBody = splitcylinder(keepBody, planeList[i - 1], circleList[i].Frame.Origin);
                                }

                                //Perform only if there is a torus at either end of the pipe
                                if ((i == 0 || i == faceList.Count() - 1) && faceList[i].GetGeometry<Torus>() != null)
                                {
                                    if (i == 0) keepBody = splitcylinder(keepBody, planeList[i], circleList[i].Frame.Origin);
                                    if (i == faceList.Count() - 1) keepBody = splitcylinder(keepBody, planeList[i], circleList[i + 1].Frame.Origin);
                                }

                                //split cylinder on second of the relevant planes
                                if (i < planeList.Count - 1 && planeList[i + 1] != null)
                                {
                                    keepBody = splitcylinder(keepBody, planeList[i + 1], circleList[i + 1].Frame.Origin);
                                }
                                keepBodies.Add(keepBody);


                            }

                            //transform the body back to its original position (if in a component)
                            Matrix reverseTrans = desBody.TransformToMaster.Inverse;

                            //FileWriter(outputfile, "Loop " + i + " of " + (faceList.Count() - 1).ToString());
                            //Draw out bodies
                            allDesignBodies.AddRange(drawnbodies(keepBodies, reverseTrans, pipesPart,chosen_colour));

                        }

                    }

                }

                //catch if we cannot simplify the pipe
                catch
                {
                    Body brokenBody = desBody.Master.Shape.Copy();
                    Matrix reverseTrans = desBody.TransformToMaster.Inverse;
                    brokenBody.Transform(reverseTrans);
                    brokenDesignBodies.Add(DesignBody.Create(brokenPipe, "Broken Pipe", brokenBody));
                }

            }

            var allbodies = pipesPart.Bodies;
            //FileWriter(outputfile, "Number of Bodies in part: " + allbodies.Count);


        }

        //Function to process the colour of the pipes
        //---------------------------------------------------------
        public static List<Color> process_colour(string chosenColour) 
        {
            List<Color> set_colour = new List<Color>();

            if (chosenColour == "Blue" || chosenColour=="")
            {
                set_colour.Add(Color.Blue);
                set_colour.Add(Color.LightBlue);
            }
            else if (chosenColour == "Red")
            {
                set_colour.Add(Color.DarkRed);
                set_colour.Add(Color.Red);
            }
            else if (chosenColour == "Green")
            {
                set_colour.Add(Color.Green);
                set_colour.Add(Color.LightGreen);
            }
            else if (chosenColour == "Orange")
            {
                set_colour.Add(Color.DarkOrange);
                set_colour.Add(Color.Orange);
            }
            else if (chosenColour == "Black")
            {
                set_colour.Add(Color.Black);
                set_colour.Add(Color.Gray);
            }
            else if (chosenColour == "Yellow")
            {
                set_colour.Add(Color.Yellow);
                set_colour.Add(Color.LightYellow);
            }
            else if (chosenColour == "Salmon Pink")
            {
                set_colour.Add(Color.DeepPink);
                set_colour.Add(Color.LightPink);
            }

            return (set_colour);
        }



        //Function to find the Planular faces at the end of the pipe
        //----------------------------------------------------------
        public static List<Face> findendFaces(Body fef_pipeBody)
        {


            List<Face> fef_endFaces = new List<Face>();
            //find the planes within the pipe route
            var fef_endFacesTemp = from fef_currentface in fef_pipeBody.Faces
                                   where fef_currentface.GetGeometry<Plane>() != null
                                   select fef_currentface;

            //Make sure they are acually end faces as all edges should be circles
            foreach (Face fef_face in fef_endFacesTemp)
            {
                var fef_noncircle = from edge in fef_face.Edges
                                    where edge.GetGeometry<Circle>() == null
                                    select edge;

                if (fef_noncircle.Count() == 0 && fef_face.Area > 5e-5) fef_endFaces.Add(fef_face);

            }

            //If we can't find faces with all circles then we will try to find faces who are next to cylinders or tori on every side
            if (fef_endFaces.Count() < 2)
            {
                foreach (Face fef_face in fef_endFacesTemp)
                {
                    var fef_cyl_edge = from edge in fef_face.Edges
                                       where fef_face.GetAdjacentFace(edge).GetGeometry<Cylinder>() != null
                                       select edge;
                    var fef_tor_edge = from edge in fef_face.Edges
                                       where fef_face.GetAdjacentFace(edge).GetGeometry<Torus>() != null
                                       select edge;
                    if ((fef_tor_edge.Count() == fef_face.Edges.Count() || fef_cyl_edge.Count() == fef_face.Edges.Count()) && fef_face.Area > 5e-5 && fef_face != fef_endFaces.First()) fef_endFaces.Add(fef_face);
                }
            }

            //If we still haven't found all the end faces try looking for planes which have more circle edges than straight lines
            if (fef_endFaces.Count() < 2)
            {
                foreach (Face fef_face in fef_endFacesTemp)
                {
                    var fef_noncircle = from edge in fef_face.Edges
                                        where edge.GetGeometry<Circle>() == null
                                        select edge;

                    if (fef_noncircle.Count() > fef_face.Edges.Count() - fef_noncircle.Count() && fef_face.Area > 5e-5 && fef_face != fef_endFaces.First()) fef_endFaces.Add(fef_face);
                }
            }
            //foreach(Face endFace in fef_endFaces)
            //{
            //    DatumPlane newPlane = DatumPlane.Create(Window.ActiveWindow.Document.MainPart, "EndPlanes", endFace.GetGeometry<Plane>());
            //}
            return fef_endFaces;

        }



        //Function to find the Radii of the pipe from the end faces (current the tool does not work on pipes with change radius)
        //----------------------------------------------------------------------------------------------------------------------
        public static List<double> findRadii(Face fr_endFace)
        {
            //var fr_radiiTemp = from fr_edge in fr_endFace.Edges
            //                   where fr_edge.GetGeometry<Circle>().Radius <= fr_endFace.Edges.First().GetGeometry<Circle>().Radius - 0.000001 || fr_edge.GetGeometry<Circle>().Radius >= fr_endFace.Edges.First().GetGeometry<Circle>().Radius + 0.000001
            //                   select fr_edge.GetGeometry<Circle>().Radius;
            //List<double> fr_radii = new List<double>();
            //if (fr_radiiTemp.Count() > 0) fr_radii.Add(fr_radiiTemp.First());
            //fr_radii.Add(fr_endFace.Edges.First().GetGeometry<Circle>().Radius);


            List<double> fr_tempRadius = new List<double>();
            List<double> fr_radii = new List<double>();

            //Find the face on the other side of each edge, if they 
            foreach (Edge fr_edge in fr_endFace.Edges)
            {
                try
                {
                    //See if it is cylinder
                    fr_tempRadius.Add(fr_endFace.GetAdjacentFace(fr_edge).GetGeometry<Cylinder>().Radius);
                }
                catch
                {
                    //if not it is a torus
                    fr_tempRadius.Add(fr_endFace.GetAdjacentFace(fr_edge).GetGeometry<Torus>().MinorRadius);
                }
            }
            //If the radius is different from the first radius then record it
            var fr_diff_radii = from fr_radius in fr_tempRadius
                                where fr_radius <= fr_tempRadius.First() - 0.000001 || fr_radius >= fr_tempRadius.First() + 0.000001
                                select fr_radius;
            //add the first radius and the first of any different radii
            if (fr_diff_radii.Count() > 0) fr_radii.Add(fr_diff_radii.First());
            fr_radii.Add(fr_tempRadius.First());

            return fr_radii;
        }



        //Functions to find the circles where the cylinders connect to the tori, it outputs a list of circles, circle origin points and the faces connecting the circle.
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------

        public static void findcircles(Face fc_currentFace, Face fc_endFace, out List<SpaceClaim.Api.V18.Geometry.Point> fc_pointsList, out List<Circle> fc_circleList, out List<Face> fc_faceList)
        {

            fc_pointsList = new List<SpaceClaim.Api.V18.Geometry.Point>();
            fc_circleList = new List<Circle>();
            fc_faceList = new List<Face>();
            double fc_currentpipeRadius = fc_currentFace.GetAdjacentFace(fc_currentFace.Edges.First()).GetGeometry<Cylinder>().Radius;

            //find the circles from the end face
            Line fc_cylinder_axis = fc_currentFace.GetAdjacentFace(fc_currentFace.Edges.First()).GetGeometry<Cylinder>().Axis;
            Plane fc_startPlane = fc_currentFace.GetGeometry<Plane>();
            // DatumPlane newPlane = DatumPlane.Create(Window.ActiveWindow.Document.MainPart, "new Plane", fc_startPlane);
            // DatumLine newLine = DatumLine.Create(Window.ActiveWindow.Document.MainPart, "new Line", fc_cylinder_axis);
            fc_pointsList.Add(linePlaneIntersect(fc_cylinder_axis, fc_startPlane));
            fc_circleList.Add(Circle.Create(Frame.Create(fc_pointsList.First(), fc_cylinder_axis.Direction), fc_currentpipeRadius));


            //Set next face equal to the face on the other side of the first circle in the list
            Face fc_nextFace = fc_currentFace.GetAdjacentFace(fc_currentFace.Edges.First());
            fc_currentFace = fc_nextFace;

            //While the next face isn't equal to the end face keep searching for circles.
            int fc_i = 0;

            while (fc_nextFace != fc_endFace)
            {

                foreach (Edge fc_currentEdge in fc_currentFace.Edges)
                {

                    //find a circular edge and point associated with it
                    if (fc_currentEdge.GetGeometry<Circle>() != null && (fc_currentEdge.GetGeometry<Circle>().Radius <= fc_currentpipeRadius + 0.0000001 && fc_currentEdge.GetGeometry<Circle>().Radius >= fc_currentpipeRadius - 0.0000001) && fc_currentEdge.GetGeometry<Circle>().Frame.Origin != fc_pointsList.Last())
                    {

                        fc_pointsList.Add(fc_currentEdge.GetGeometry<Circle>().Frame.Origin);
                        fc_circleList.Add(fc_currentEdge.GetGeometry<Circle>());
                        fc_faceList.Add(fc_currentFace);

                        fc_nextFace = fc_currentFace.GetAdjacentFace(fc_currentEdge);

                        break;
                    }
                }


                var fc_endEdge = from fc_currentEdge in fc_nextFace.Edges
                                 where fc_nextFace.GetAdjacentFace(fc_currentEdge) == fc_endFace
                                 select fc_currentEdge;

                if (fc_endEdge.Count() != 0)
                {
                    Line fc_endcylinder_axis = fc_nextFace.GetGeometry<Cylinder>().Axis;
                    Plane fc_endPlane = fc_endFace.GetGeometry<Plane>();
                    //DatumPlane newPlane2 = DatumPlane.Create(Window.ActiveWindow.Document.MainPart, "new Plane", fc_endPlane);
                    //DatumLine newLine2 = DatumLine.Create(Window.ActiveWindow.Document.MainPart, "new Line", fc_endcylinder_axis);
                    fc_pointsList.Add(linePlaneIntersect(fc_endcylinder_axis, fc_endPlane));
                    fc_circleList.Add(Circle.Create(Frame.Create(fc_pointsList.Last(), fc_endcylinder_axis.Direction), fc_currentpipeRadius));
                    fc_faceList.Add(fc_endFace);

                    break;
                }


                //Break clause just incase we get stuck in a infinate loop
                //Change this if there are more than 500 tori+cylinders in your pipe!!
                fc_i++;
                if (fc_i > 1000) break;
                fc_currentFace = fc_nextFace;

            }
        }


        // Function to find the intersection of an axis and a plane
        //---------------------------------------------------------
        public static Point linePlaneIntersect(Line LPI_line,Plane LPI_Plane)
        {
            double LPI_d = LPI_Plane.Frame.DirZ.UnitVector.X * LPI_Plane.Frame.Origin.X + LPI_Plane.Frame.DirZ.UnitVector.Y * LPI_Plane.Frame.Origin.Y + LPI_Plane.Frame.DirZ.UnitVector.Z * LPI_Plane.Frame.Origin.Z;
            double LPI_division = (LPI_Plane.Frame.DirZ.UnitVector.X * LPI_line.Origin.X + LPI_Plane.Frame.DirZ.UnitVector.Y * LPI_line.Origin.Y + LPI_Plane.Frame.DirZ.UnitVector.Z * LPI_line.Origin.Z - LPI_d) /
                                 (LPI_Plane.Frame.DirZ.UnitVector.X * LPI_line.Direction.UnitVector.X + LPI_Plane.Frame.DirZ.UnitVector.Y * LPI_line.Direction.UnitVector.Y + LPI_Plane.Frame.DirZ.UnitVector.Z * LPI_line.Direction.UnitVector.Z);
            return(LPI_line.Origin - LPI_line.Direction.UnitVector * LPI_division);
        }

        // Function to create planes at each of the circles inorder to calculate the "interplane"
        //---------------------------------------------------------------------------------------

        public static Plane createPlane(Circle cp_circle, SpaceClaim.Api.V18.Geometry.Point cp_point1, SpaceClaim.Api.V18.Geometry.Point cp_point2, int cp_round, out bool cp_isreversed, out Vector cp_directionCyl)
        {
            Plane cp_plane = cp_circle.Plane;
            cp_directionCyl = cp_point1 - cp_point2;
            Vector cp_dir = Vector.Create(Math.Round(cp_directionCyl.Direction.UnitVector.X, cp_round), Math.Round(cp_directionCyl.Direction.UnitVector.Y, cp_round), Math.Round(cp_directionCyl.Direction.UnitVector.Z, cp_round));
            Vector cp_planedir = Vector.Create(Math.Round(cp_plane.Frame.DirZ.UnitVector.X, cp_round), Math.Round(cp_plane.Frame.DirZ.UnitVector.Y, cp_round), Math.Round(cp_plane.Frame.DirZ.UnitVector.Z, cp_round));
            cp_isreversed = cp_dir.Direction.UnitVector == cp_planedir.Direction.UnitVector;
            return cp_plane;
        }


        // Function to create the cylindrical bodies
        //------------------------------------------

        public static Body createCylinder(Face cc_face, Face cc_startFace, Face cc_endFace, double cc_pipeRadius, int cc_i, int cc_MaxFaces, SpaceClaim.Api.V18.Geometry.Point cc_point1, SpaceClaim.Api.V18.Geometry.Point cc_point2)
        {
            //If the first face and the face is a torus draw out a cylindrical body
            if (cc_i == 0 && cc_face.GetGeometry<Torus>() != null)
            {
                double cc_length = cc_face.GetGeometry<Torus>().MajorRadius + cc_face.GetGeometry<Torus>().MinorRadius + 0.1;
                cc_length = cc_startFace.IsReversed ? cc_length : -cc_length;
                Body cc_newBody = Body.ExtrudeProfile(new CircleProfile(cc_startFace.GetGeometry<Plane>(), cc_pipeRadius), cc_length);
                return cc_newBody;
            }

            //else if it is the end face and it is a torus also draw out a cylindrical body
            else if (cc_i == cc_MaxFaces - 1 && cc_face.GetGeometry<Torus>() != null)
            {
                double cc_length = cc_face.GetGeometry<Torus>().MajorRadius + cc_face.GetGeometry<Torus>().MinorRadius + 0.1;
                cc_length = cc_endFace.IsReversed ? cc_length : -cc_length;
                Body cc_newBody = Body.ExtrudeProfile(new CircleProfile(cc_endFace.GetGeometry<Plane>(), cc_pipeRadius), cc_length);
                return cc_newBody;
            }

            //else draw out a cylinder
            else
            {
                Vector cc_deltad = cc_point1 - cc_point2;
                Frame cc_circleFrame = Frame.Create(cc_point2, cc_deltad.Direction);
                Plane cc_circlePlane = Plane.Create(cc_circleFrame);

                Body cc_newBody = Body.ExtrudeProfile(new CircleProfile(cc_circlePlane, cc_pipeRadius), cc_deltad.Magnitude);
                return cc_newBody;

            }
        }



        //function to split the cylinders by the interplane
        //-------------------------------------

        public static Body splitcylinder(Body sc_body, Plane sc_plane, SpaceClaim.Api.V18.Geometry.Point sc_point)
        {
            Body sc_keepBody = null;
            Tracker sc_tracker = null;
            sc_body.Split(sc_plane, sc_tracker);
            var sc_splitBodies = sc_body.SeparatePieces();

            foreach (Body sc_splitBody in sc_splitBodies)
            {
                if (sc_splitBody.ContainsPoint(sc_point)) sc_keepBody = sc_splitBody;
            }
            return sc_keepBody;
        }



        //Function to split the subtracted inner bodies of pipe and then draw them out
        //--------------------------------------------------------------------------

        public static List<DesignBody> drawnbodies(List<Body> db_keepBodies, Matrix db_reverseTrans, Part db_Part, List<Color> chosen_colour)
        {
            //If more than one body subtract one from other
            List<DesignBody> dp_drawnbodies = new List<DesignBody>();
            if (db_keepBodies.Count == 2)
            {
                if (db_keepBodies[0].Volume > db_keepBodies[1].Volume)
                {

                    db_keepBodies[0].Subtract(db_keepBodies[1].Copy());
                    int k = 0;
                    foreach (Body db_bod in db_keepBodies)
                    {
                        db_bod.Transform(db_reverseTrans);
                        dp_drawnbodies.Add(DesignBody.Create(db_Part, "New_Pipe", db_bod));
                        
                        dp_drawnbodies[k].SetColor(null, chosen_colour[k]);
                        k++;
                    }
                }
                else if (db_keepBodies[0].Volume < db_keepBodies[1].Volume)
                {
                    db_keepBodies[1].Subtract(db_keepBodies[0].Copy());
                    int k = 1;
                    foreach (Body bod in db_keepBodies)
                    {
                        bod.Transform(db_reverseTrans);
                        dp_drawnbodies.Add(DesignBody.Create(db_Part, "New_Pipe", bod));
                        dp_drawnbodies[k].SetColor(null, chosen_colour[k]);
                        k--;
                    }
                }
            }

            //else just draw it!
            else
            {
                db_keepBodies[0].Transform(db_reverseTrans);
                dp_drawnbodies.Add(DesignBody.Create(db_Part, "New_Pipe", db_keepBodies[0]));
            }
            return dp_drawnbodies;
        }



        //Function to select all the bodies that have been selected in the window
        //-----------------------------------------------------------------------
        private bool SelectionAllBodies(Window window)
        {
            ICollection<IDocObject> docObjects = window.ActiveContext.Selection;
            if (docObjects.Count == 0)
            {
                return false;
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



        //Function to write 'output' to a file 'path'
        //-------------------------------------------
        private static void FileWriter(String path, String output)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(output);
            }
        }


        //Function to create a plane mid way between two other planes
        //-----------------------------------------------------------

        public static Plane CreateInterPlane(Plane CIP_firstPlane, Plane CIP_secondPlane, bool CIP_normreversed1, bool CIP_normreversed2, out double CIP_angleBetweenPlane, out Vector3D CIP_unitvector3, out SpaceClaim.Api.V18.Geometry.Point CIP_pointOnIntersection)
        {
            Plane CIP_rotatedPlane = null;
            Plane CIP_outplane;
            CIP_angleBetweenPlane = 0;
            CIP_unitvector3 = new Vector3D();

            //Creates the Vector normals for the two planes inorder to do vector arithmetic
            Vector3D CIP_vector1 = new Vector3D(CIP_firstPlane.Frame.DirZ.X, CIP_firstPlane.Frame.DirZ.Y, CIP_firstPlane.Frame.DirZ.Z);
            Vector3D CIP_vector2 = new Vector3D(CIP_secondPlane.Frame.DirZ.X, CIP_secondPlane.Frame.DirZ.Y, CIP_secondPlane.Frame.DirZ.Z);

            //Creates the distance from the origin of the plane (d in the equation Ax+By+Cz+d=0)
            double CIP_d1 = -CIP_firstPlane.Frame.DirZ.X * CIP_firstPlane.Frame.Origin.X - CIP_firstPlane.Frame.DirZ.Y * CIP_firstPlane.Frame.Origin.Y - CIP_firstPlane.Frame.DirZ.Z * CIP_firstPlane.Frame.Origin.Z;
            double CIP_d2 = -CIP_secondPlane.Frame.DirZ.X * CIP_secondPlane.Frame.Origin.X - CIP_secondPlane.Frame.DirZ.Y * CIP_secondPlane.Frame.Origin.Y - CIP_secondPlane.Frame.DirZ.Z * CIP_secondPlane.Frame.Origin.Z;

            //Creates new Vector normal for the line of intersection of the two planes
            Vector3D CIP_vector3 = new Vector3D();
            CIP_vector3 = Vector3D.CrossProduct(CIP_vector1, CIP_vector2);

            //Creates a new unitvector which always points in a direction which means the rotation will go from 2-1
            if (CIP_normreversed1 == CIP_normreversed2) CIP_unitvector3 = CIP_vector3 / CIP_vector3.Length;
            else if (CIP_normreversed1 != CIP_normreversed2) CIP_unitvector3 = -CIP_vector3 / CIP_vector3.Length;

            //Works out point on the line of intersection
            Vector3D CIP_pointresult = Vector3D.CrossProduct((CIP_vector1 * CIP_d2 - CIP_vector2 * CIP_d1), CIP_vector3) / Vector3D.DotProduct(CIP_vector3, CIP_vector3);
            CIP_pointOnIntersection = SpaceClaim.Api.V18.Geometry.Point.Create(CIP_pointresult.X, CIP_pointresult.Y, CIP_pointresult.Z);

            //Works out the angle between the planes normals
            CIP_angleBetweenPlane = Math.Acos(Vector3D.DotProduct(CIP_vector1, CIP_vector2) / (CIP_vector1.Length * CIP_vector2.Length));

            //If the normals are both not revered or reversed change angle to 180 degree-angle
            if ((CIP_normreversed1 & CIP_normreversed2) | (CIP_normreversed1 == false & CIP_normreversed2 == false))
            {
                CIP_angleBetweenPlane = Math.PI - CIP_angleBetweenPlane;
            }
            double CIP_newangle = CIP_angleBetweenPlane / 2;

            //Works out the new normal of the plane mid way between the two using Rodrigues' Rotation formula and creates a new plane
            CIP_rotatedPlane = rotatePlane(CIP_newangle, CIP_vector2, CIP_unitvector3, CIP_pointOnIntersection);


            //Check to ensure that we are rotating the correct plane
            double CIP_checkangle = 0;
            Vector3D CIP_checkvector = new Vector3D(CIP_rotatedPlane.Frame.DirZ.X, CIP_rotatedPlane.Frame.DirZ.Y, CIP_rotatedPlane.Frame.DirZ.Z);
            if (CIP_normreversed1 == true) CIP_checkangle = Math.Acos(Math.Abs(Vector3D.DotProduct(-CIP_vector1, CIP_checkvector)) / (CIP_vector1.Length * CIP_checkvector.Length));
            else if (CIP_normreversed1 == false) CIP_checkangle = Math.Acos(Math.Abs(Vector3D.DotProduct(CIP_vector1, CIP_checkvector)) / (CIP_vector1.Length * CIP_checkvector.Length));
            //FileWriter("h:/test.out", "Angle Between planes " + CIP_angleBetweenPlane.ToString() + " Angle " + CIP_newangle.ToString() + " CheckAngle " + CIP_checkangle.ToString());
            if (CIP_checkangle >= CIP_angleBetweenPlane) CIP_rotatedPlane = rotatePlane(CIP_newangle, CIP_vector1, CIP_unitvector3, CIP_pointOnIntersection);

            return CIP_outplane = Plane.Create(CIP_rotatedPlane.Frame);
        }



        //Function to rotate a plane by a give angle
        //------------------------------------------
        private static Plane rotatePlane(double rp_angle, Vector3D rp_vector1, Vector3D rp_axisunitvec, SpaceClaim.Api.V18.Geometry.Point rp_pointOnIntersection)
        {
            Vector3D rp_rotatednorm = (rp_vector1 * Math.Cos(rp_angle)) + ((Vector3D.CrossProduct(rp_axisunitvec, rp_vector1)) * Math.Sin(rp_angle)) + (rp_axisunitvec * (Vector3D.DotProduct(rp_axisunitvec, rp_vector1)) * (1 - Math.Cos(rp_angle)));
            Direction rp_newNorm = Direction.Create(rp_rotatednorm.X, rp_rotatednorm.Y, rp_rotatednorm.Z);
            Frame rp_frame1 = Frame.Create(rp_pointOnIntersection, rp_newNorm);
            return Plane.Create(rp_frame1);
        }

        public static void test()
        {

        }

    }
}

