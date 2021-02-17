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
using SpaceClaim.Api.V18.Display;
using System.ComponentModel;
using System.Runtime.ExceptionServices;

namespace CCFE_Toolbox.CCFE_Commands

{
    class Tori_simp : CommandCapsule
    {
        // This command name must match that in the Ribbon.xml file
        //----------------------------------------------------------
        public const string CommandName = "CCFE_Toolbox.C#.V18.Tori_simp";

        public Tori_simp()
            : base(CommandName, Properties.Resources.toriText, Properties.Resources.tori_image, Properties.Resources.toriHint)
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
            Part toriPart = Part.Create(doc, "Simp Tori");
            double maxDiv;
            int maxCyl = 10;
            double cmtom = 0.01;

            var desBodies = window.ActiveContext.Selection;
            if (desBodies == null)
            {
                Debug.Fail("Unexpected case.", "Selection was not a single design body.");
                return;
            }

            // --------------------------------------------------------------
            // Open up an output file inorder to write out for testing
            // --------------------------------------------------------------
            /* string outputfile = "H:/tori.txt";
            using (StreamWriter sw = File.CreateText(outputfile))
            {
                sw.WriteLine("OutputFile");
            }
            */

            List<double> radii = new List<double>();
            bool full_torus = false;
            bool end_planes = false;

            // Work out inner (if present) and outer radii of the pipe
            // List<double> radii = Pipesv2Capsule.findRadii(endFaces.First());
            #region Dialogue Information
            using (var dialogue = new tori_simp())
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
                    radii.Reverse();
                }
                else if (dialogue.userdefined)
                {

                    radii.Add((double)dialogue.Radius1 * cmtom);
                    if (dialogue.radii2)
                    {
                        radii.Add((double)dialogue.Radius2 * cmtom);
                    }
                    if (dialogue.radii3)
                    {
                        radii.Add((double)dialogue.Radius3 * cmtom);
                    }
                    if (dialogue.radii4)
                    {
                        radii.Add((double)dialogue.Radius4 * cmtom);
                    }

                }

                if (dialogue.fulltorus)
                {
                    full_torus = true;
                }


                maxDiv = (double)dialogue.maximumCyl;
            }
            #endregion

 
            //maxCyl = 20;
            //radii.Add(0.02);
            //radii.Add(0.03);
            //radii.Add(0.04);

            // --------------------------------------------------------------------
            // 
            // --------------------------------------------------------------------
            //FileWriter(outputfile, "Number of Bodies" + desBodies.Count());
            foreach (IDesignBody desBody in desBodies)
            {
                DesignBody desBodyMaster = desBody.Master;
                Part currentPart = desBodyMaster.Parent;
                Body currentBody = desBodyMaster.Shape;
                int i;

                //Find all the toridal faces
                var tori = from currentface in currentBody.Faces
                           where currentface.GetGeometry<Torus>() != null
                           select currentface.GetGeometry<Torus>();

                // if no radii are added just use the minor radius of the torus
                if (!radii.Any()) radii.Add(tori.First().MinorRadius);

                //Find all the planular faces
                var faces = from currentface in currentBody.Faces
                             where currentface.GetGeometry<Plane>() != null
                             select currentface;

                //Get planes for faces
                var planes = from face in faces
                             where face.GetGeometry<Plane>() != null
                             select face.GetGeometry<Plane>();

                /* //Find intersection of torus with planes
                Vector3D cir_U = new Vector3D(tori.First().MajorRadius * tori.First().Frame.DirX.UnitVector.X, tori.First().MajorRadius * tori.First().Frame.DirX.UnitVector.Y, tori.First().MajorRadius * tori.First().Frame.DirX.UnitVector.Z);
                Vector3D cir_V = new Vector3D(tori.First().MajorRadius * tori.First().Frame.DirY.UnitVector.X, tori.First().MajorRadius * tori.First().Frame.DirY.UnitVector.Y, tori.First().MajorRadius * tori.First().Frame.DirY.UnitVector.Z);
                Vector3D cir_c0 = new Vector3D(tori.First().Frame.Origin.Vector.X, tori.First().Frame.Origin.Vector.Y, tori.First().Frame.Origin.Vector.Z);
                Vector3D plane1_N = new Vector3D(planes.First().Frame.DirZ.UnitVector.X, planes.First().Frame.DirZ.UnitVector.Y, planes.First().Frame.DirZ.UnitVector.Z);
                Vector3D plane1_P = new Vector3D(planes.First().Frame.Origin.Vector.X, planes.First().Frame.Origin.Vector.Y, planes.First().Frame.Origin.Vector.Z);
                List<Vector3D> inter_points = intersect_points(cir_c0, cir_U, cir_V, plane1_N, plane1_P);
                DatumPoint.Create(rootPart,"point1", SpaceClaim.Api.V18.Geometry.Point.Create(inter_points.First().X, inter_points.First().Y, inter_points.First().Z));
                DatumPoint.Create(rootPart, "point2", SpaceClaim.Api.V18.Geometry.Point.Create(inter_points.Last().X, inter_points.Last().Y, inter_points.Last().Z));
                */

                //Use the Planular faces to work out where the end of the tori section is
                double anglediff;
                Vector3D startingVector = findStartAngle(tori.First(), planes.First(), planes.Last(), faces.First().IsReversed, faces.Last().IsReversed, out anglediff);
                //FileWriter(outputfile, "Starting Vec " + startingVector + " angle " + anglediff);

                //If we are doing a full torus then set the angle to 360
                if (full_torus) anglediff = 2 * Math.PI;

                //Work out the maximum number of cylinders
                maxCyl = (int)Math.Ceiling(Math.Abs(anglediff / (maxDiv * Math.PI / 180)));
                //FileWriter(outputfile, "maxCyl " + maxCyl);

                //Create a list of points along the length of the Torus in order to split it up
                List<SpaceClaim.Api.V18.Geometry.Point> pointList = getPoints(tori.First(), startingVector, anglediff, maxCyl);


                //Create planes at each of the points
                List<Plane> cylplanes = makePlanes(pointList, maxCyl);

                //Create interplanes at the ends of each of the cylinders
                List<Plane> interplanes = new List<Plane>();

                //dummy variables which aren't used
                double planeangle;
                Vector3D vecunit;
                SpaceClaim.Api.V18.Geometry.Point intersectpoint;
                // If we are using the original end planes use these else use interplane
                if (end_planes) interplanes.Add(planes.First());
                else interplanes.Add(Pipes_simp.CreateInterPlane(cylplanes[1], cylplanes[2], true, false, out planeangle, out vecunit, out intersectpoint));
                // Add rest of the interplanes
                for (i = 2; i < maxCyl-1; i++)
                {
                    interplanes.Add(Pipes_simp.CreateInterPlane(cylplanes[i * 2 - 1], cylplanes[i * 2], true, false, out planeangle, out vecunit, out intersectpoint));
                }
                // If we are using the original end planes use these else use interplane
                if (end_planes) interplanes.Add(planes.Last());
                else interplanes.Add(Pipes_simp.CreateInterPlane(cylplanes[(maxCyl-1)*2-1], cylplanes[(maxCyl-1)*2], true, false, out planeangle, out vecunit, out intersectpoint));

                //Create cylinders along the tori
                List<Body> keepbodies=new List<Body>();


                keepbodies = makeCylinders(pointList, maxCyl, radii, interplanes);


                //Subtract the inner cylinders from the others
                int j;
                //Subtract from each other
                for (i = 0; i < maxCyl; i++)
                {
                    for (j = radii.Count() - 1; j > 0; j--)
                    {

                       keepbodies[j * maxCyl + i].Subtract(keepbodies[(j - 1) * maxCyl + i].Copy());

                    }
                }

                //Create design bodes in new components
                Matrix masterTransform = desBody.TransformToMaster;
                for (i = 0; i < radii.Count; i++)
                {
                    toriPart = Part.Create(doc, "Simp Tori " + (i + 1).ToString());
                    SpaceClaim.Api.V18.Component newComponent = SpaceClaim.Api.V18.Component.Create(rootPart, toriPart);
                    for (j = 0; j < maxCyl; j++)
                    {

                        DesignBody newBody = DesignBody.Create(toriPart, "newBody", keepbodies[i * maxCyl + j]);

                    }
                    //Cover back to master geometry
                    newComponent.Transform(masterTransform.Inverse);
                    
                }

            }


        }
        

        //Function to find the start angle of the torus
        private Vector3D findStartAngle(Torus FSA_tori, Plane FSA_startPlane, Plane FSA_endPlane, bool startplane_rev, bool endplane_rev, out double FSA_anglediff)
        {

            // Create vector3D of the normal to the plane the tori sits in
            Vector3D newZ = new Vector3D(FSA_tori.Frame.DirZ.UnitVector.X, FSA_tori.Frame.DirZ.UnitVector.Y, FSA_tori.Frame.DirZ.UnitVector.Z);

            // Create vector3d of the starting plane normal
            Vector3D start_planeNorm = new Vector3D(FSA_startPlane.Frame.DirZ.UnitVector.X, FSA_startPlane.Frame.DirZ.UnitVector.Y, FSA_startPlane.Frame.DirZ.UnitVector.Z);

            // Create vector3d of the end plane normal
            Vector3D end_planeNorm = new Vector3D(FSA_endPlane.Frame.DirZ.UnitVector.X, FSA_endPlane.Frame.DirZ.UnitVector.Y, FSA_endPlane.Frame.DirZ.UnitVector.Z);

            // Check the direction of the vectors and modify if they both point out of the body or into the body
            if ((startplane_rev && endplane_rev) || (!startplane_rev && !endplane_rev)) 
            { 
                end_planeNorm = end_planeNorm * -1.0;
                endplane_rev = !endplane_rev;
            }

            // Use atan2 to aquire the angle between the two planes (with direction)
            FSA_anglediff = Math.Atan2(Vector3D.DotProduct(newZ, Vector3D.CrossProduct(start_planeNorm, end_planeNorm)), Vector3D.DotProduct(start_planeNorm, end_planeNorm));

            // Find the unit vector in the direction of the starting plane
            Vector3D FSA_newstartVec = Vector3D.CrossProduct(newZ, start_planeNorm);

            //Check to make sure the starting vector points in the correct direction
            if (reverse_line(FSA_newstartVec, endplane_rev, end_planeNorm)) FSA_newstartVec = FSA_newstartVec * -1.0;

            // Pass back the unit vector in the direction of the starting plane
            return (FSA_newstartVec);

        }

        /* private List<Vector3D> intersect_points(Vector3D cir_c0, Vector3D cir_U, Vector3D cir_V, Vector3D plane_N, Vector3D plane_P)
        {
            string outputfile = "H:/tori.txt";
            FileWriter(outputfile, "Circle Centre " + cir_c0 + " Cir U " + cir_U + " Cir V " + cir_V + " Plane Normal " + plane_N + " Point on plane " + plane_P);
            // Work out the angle of intersection between plane and point (there should be two)
            double theta;
            double UdotN = Vector3D.DotProduct(cir_U,plane_N);
            double VdotN = Vector3D.DotProduct(cir_V, plane_N); ;
            double PdotN = Vector3D.DotProduct(plane_P, plane_N);
            double C0dotN = Vector3D.DotProduct(cir_c0, plane_N);
            List<Vector3D> points = new List<Vector3D>();

            theta = Math.Acos((PdotN - C0dotN) / Math.Sqrt(VdotN * VdotN + UdotN * UdotN)) + Math.Atan(VdotN / UdotN);
            // theta = Math.Asin((PdotN - C0dotN) / Math.Sqrt(UdotN * UdotN + VdotN * VdotN)) + Math.Acos(VdotN / Math.Sqrt(UdotN * UdotN + VdotN * VdotN));
            points.Add(cir_c0 + Math.Cos(theta) * cir_U + Math.Sin(theta) * cir_V);

            theta = -Math.Acos((PdotN - C0dotN) / Math.Sqrt(VdotN * VdotN + UdotN * UdotN)) + Math.Atan(VdotN / UdotN);
            // theta = Math.Asin((PdotN - C0dotN) / (Math.Sqrt(UdotN * UdotN + VdotN * VdotN))) + Math.Asin(UdotN / (Math.Sqrt(UdotN * UdotN + VdotN * VdotN)));
            points.Add(cir_c0 + Math.Cos(theta) * cir_U + Math.Sin(theta) * cir_V);

            return (points);
        }
        */

        private bool reverse_line(Vector3D vec, bool reversed, Vector3D plane_norm)
        {

            if (!reversed) plane_norm = plane_norm * -1.0;
            double angle = Vector3D.DotProduct(plane_norm, vec);
            if (angle >= 0 && angle <= 1)
            {
                return (false);
            }
            else
            {
                return (true);
            }
        }

        //Function to create points along the tori
        private List<SpaceClaim.Api.V18.Geometry.Point> getPoints(Torus GP_tori, Vector3D GP_startVector, double GP_anglediff, int GP_numSplits)
        {
            int GP_i;
            List<SpaceClaim.Api.V18.Geometry.Point> GP_pointList = new List<SpaceClaim.Api.V18.Geometry.Point>();

            for (GP_i = 0; GP_i <= GP_numSplits; GP_i++)
            {

                //Creates a vector for the origin of the torus
                Vector3D originVec = new Vector3D(GP_tori.Frame.Origin.X, GP_tori.Frame.Origin.Y, GP_tori.Frame.Origin.Z);
                //Creates a vector normal for the torus
                Vector3D GP_vectornorm = new Vector3D(GP_tori.Frame.DirZ.UnitVector.X, GP_tori.Frame.DirZ.UnitVector.Y, GP_tori.Frame.DirZ.UnitVector.Z);

                //Calculate the angle to rotate the vector by
                double GP_angle = (GP_anglediff / GP_numSplits) * (GP_i);

                //Rotate the start vector about the torus normal by the angle
                var GP_pointVec = GP_tori.MajorRadius * Math.Cos(GP_angle) * GP_startVector + GP_tori.MajorRadius * Math.Sin(GP_angle) * Vector3D.CrossProduct(GP_vectornorm, GP_startVector) + originVec;

                //Add the point to a list
                GP_pointList.Add(SpaceClaim.Api.V18.Geometry.Point.Create(GP_pointVec.X, GP_pointVec.Y, GP_pointVec.Z));

            }
            return (GP_pointList);
        }


        //Function to make planes at the end of each cylinder
        private List<Plane> makePlanes(List<SpaceClaim.Api.V18.Geometry.Point> MP_points, int MP_numSplit)
        {
            int MP_i;
            List<Plane> MP_cylplanes = new List<Plane>();

            //Create planes at the end of each cylinder.
            for (MP_i = 0; MP_i < MP_numSplit; MP_i++)
            {
                Vector MP_delta = MP_points[MP_i + 1] - MP_points[MP_i];
                MP_cylplanes.Add(Plane.Create(Frame.Create(MP_points[MP_i], MP_delta.Direction)));
                MP_cylplanes.Add(Plane.Create(Frame.Create(MP_points[MP_i + 1], MP_delta.Direction)));
            }
            return (MP_cylplanes);
        }

        //Makes cylinders and splits them on the interplanes
        private List<Body> makeCylinders(List<SpaceClaim.Api.V18.Geometry.Point> MC_points, int MC_numSplit, List<double> MC_radii, List<Plane> MC_interplanes)
        {

            int MC_i;

            List<Body> MC_keepbodies = new List<Body>();

            foreach (double MC_radius in MC_radii)
            {
                List<Body> MC_cylbodies = new List<Body>();

                //Create cylinders
                for (MC_i = 0; MC_i < MC_numSplit; MC_i++)
                {
                    //Direction of cylinder
                    Vector MC_delta = MC_points[MC_i + 1] - MC_points[MC_i];

                    double MC_length;
                    SpaceClaim.Api.V18.Geometry.Point MC_temppoint;
                    //If not first point move back to ensure overlap with previous cylinder
                    if (MC_i > 0)
                    {
                        MC_temppoint = MC_points[MC_i] + MC_delta.Direction.UnitVector * (-MC_radius);
                    }
                    //else dont move
                    else
                    {
                        MC_temppoint = MC_points[MC_i];
                    }

                    //If not last point move forward to ensure overlap with next cylinder
                    if (MC_i < MC_numSplit - 1)
                    {
                        MC_length = (MC_points[MC_i + 1] - MC_temppoint).Magnitude + MC_radius;
                    }
                    //else dont move
                    else
                    {
                        MC_length = (MC_points[MC_i + 1] - MC_temppoint).Magnitude;
                    }

                    //Make body
                    MC_cylbodies.Add(Body.ExtrudeProfile(new CircleProfile(Plane.Create(Frame.Create(MC_temppoint, MC_delta.Direction)), MC_radius), MC_length));

                }

                //Split the cylinders on the interplanes
                for (MC_i = 0; MC_i < MC_numSplit; MC_i++)
                {
                    if (MC_i == 0)
                    {
                        MC_keepbodies.Add(Pipes_simp.splitcylinder(MC_cylbodies[MC_i], MC_interplanes[MC_i], MC_points[MC_i + 1]));
                    }
                    if (MC_i == MC_numSplit - 1)
                    {
                        MC_keepbodies.Add(Pipes_simp.splitcylinder(MC_cylbodies[MC_i], MC_interplanes[MC_i - 1], MC_points[MC_i + 1]));
                    }
                    if (MC_i > 0 && MC_i < MC_numSplit - 1)
                    {

                        Body MC_tempbody = Pipes_simp.splitcylinder(MC_cylbodies[MC_i], MC_interplanes[MC_i - 1], MC_points[MC_i + 1]);
                        MC_keepbodies.Add(Pipes_simp.splitcylinder(MC_tempbody, MC_interplanes[MC_i], MC_points[MC_i + 1]));

                    }

                }

            }
            return (MC_keepbodies);
        }

        //Function to write 'output' to a file 'path'
        private void FileWriter(String path, String output)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(output);
            }
        }

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
    }
}
