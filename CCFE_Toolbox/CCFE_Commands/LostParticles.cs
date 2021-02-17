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
    class LostParticles : CommandCapsule
    {
        // This command name must match that in the Ribbon.xml file
        //----------------------------------------------------------

        public const string CommandName = "CCFE_Toolbox.C#.V18.LostParticles";

        public LostParticles() : base(CommandName, Resources.LostParticlesText, Resources.FindMatchingFaces, Resources.LostParticlesHint)
        {

        }

        protected override void OnUpdate(Command command)
        {
            command.IsEnabled = Window.ActiveWindow != null;
        }
        protected override void OnExecute(Command command, ExecutionContext context, Rectangle buttonRect)
        {
            /*
             * Lost Particle Location Highlighter
             * Prompt user to select the MCNP output file which contains lost particle locations
             * Read in file and store locations
             * Highlight the point/plane which the particle loss occurs 
            */

            // Instance common functions class
            InstanceClasses.CommonSpaceClaimFunctions FunctionsClass = new InstanceClasses.CommonSpaceClaimFunctions();

            // Variables
            Window window = Window.ActiveWindow;
            window.SetTool(new ToolClass());
            Document doc = window.Document;
            Part rootPart = doc.MainPart;
            Part lostPart = Part.Create(doc, "Lost Particles");
            Part vectorPart = Part.Create(doc, "Vector Lines");
            Part curvePart = Part.Create(doc, "Lost Particle Curves");

            Component lp2c = Component.Create(rootPart, curvePart);

            string filepath;
            bool showVectors;
            bool fileLocated = false;
            bool maxParticlesBool;
            double maxParicles;

            int particleCounter = 0;
            int particleLineCounter = 0;
            bool stopParticles = false;
            bool stopLines = false;

            string[] lines = null;

            string lostParticleIndicator = "x,y,z coordinates:";
            string vectorIndicator = "u,v,w direction cosines:";

            char[] delimeterChars = { ' ', '\t' };
            char[] secondDelimChars = { '+', '-' };

            int pointCounter = 1;
            int lineCounter = 1;
            string pointName;
            string vectorName;
            string chosenColour;

            Point currentPoint = Point.Create(0, 0, 0);
            Point vectorPoint = Point.Create(0, 0, 0);

            List<double> xValues = new List<double>();
            List<double> yValues = new List<double>();
            List<double> zValues = new List<double>();
            List<Point> vectorPointList = new List<Point>();
            List<Point> pointList = new List<Point>();
            List<Line> lineList = new List<Line>();

            // Bring up windows form

            using (var dialogue = new UI.LostParticlesForm())
            {
                if (dialogue.ShowDialog() != DialogResult.OK)
                    return;

                var nameAndRendering = new List<KeyValuePair<string, Graphic>>();
                var partToGraphic = new Dictionary<Part, Graphic>();

                var style = new GraphicStyle
                {
                    EnableDepthBuffer = true
                };

                filepath = dialogue.FileName;
                showVectors = dialogue.ShowVectors;
                maxParticlesBool = dialogue.MaxParticleBool;
                maxParicles = dialogue.MaxParticle;
                chosenColour = dialogue.ColourSelection;
            }

            if (window == null)
                return;

            //Debug.WriteLine(filepath);

            try
            {
                lines = File.ReadAllLines(filepath);
                fileLocated = true;
            }
            catch
            {
                MessageBox.Show("File Could Not Be Located");
                //SpaceClaim.Api.V18.Application.Exit();
            }

            // Get x,y,z coordinates of lost particle points
            //-------------------------------------------------------------   

            if (fileLocated)
            {
                // Check if MCNP output or text file
                if (lines[0].Contains("Code Name & Version") || lines[0].Contains(lostParticleIndicator) || lines[0].Contains(vectorIndicator))
                {
                    //Debug.WriteLine("MCNP File");
                    // MCNP File
                    foreach (string line in lines)
                    {
                        if (line.Contains(lostParticleIndicator))
                        {
                            //Debug.WriteLine(line);
                            if (!stopParticles)
                            {
                                string[] tempWords = line.Split(delimeterChars, StringSplitOptions.RemoveEmptyEntries);

                                // Convert API values in metres to cm for SpaceClaim
                                currentPoint = Point.Create(Convert.ToDouble(tempWords[2]) / 100, Convert.ToDouble(tempWords[3]) / 100, Convert.ToDouble(tempWords[4]) / 100);
                                pointList.Add(currentPoint);

                                particleCounter++;

                                if (particleCounter >= maxParicles && maxParticlesBool)
                                {
                                    stopParticles = true;
                                }
                            }
                        }

                        else if (line.Contains(vectorIndicator))
                        {
                            string[] tempWords = line.Split(delimeterChars, StringSplitOptions.RemoveEmptyEntries);

                            //Debug.WriteLine(tempWords[3] + "  " + tempWords[4] + "  " + tempWords[5]);

                            Point origin = Point.Create(0, 0, 0);
                            Direction direction = Direction.Create(Convert.ToDouble(tempWords[3]), Convert.ToDouble(tempWords[4]), Convert.ToDouble(tempWords[5]));
                            vectorPoint = Point.Create(currentPoint.X + Convert.ToDouble(tempWords[3]) / 1, currentPoint.Y + Convert.ToDouble(tempWords[4]) / 1, currentPoint.Z + Convert.ToDouble(tempWords[5]) / 1);

                            if (showVectors && !stopLines)
                            {
                                vectorName = "Lost Particle " + lineCounter.ToString() + " Vector";
                                LineSegment lineSeg = new LineSegment(currentPoint, vectorPoint);
                                ITrimmedCurve shape = CurveSegment.Create(lineSeg);
                                DesignCurve desCurve = DesignCurve.Create(vectorPart, shape);
                                desCurve.Name = vectorName;
                                desCurve.SetColor(null, Color.Blue);
                                lineCounter++;
                                particleLineCounter++;

                                if (particleLineCounter >= maxParicles && maxParticlesBool)
                                {
                                    stopLines = true;
                                }
                            }

                            //Line vectorLine = Line.CreateThroughPoints(currentPoint, origin);
                            Line vectorLine = Line.CreateThroughPoints(currentPoint, vectorPoint);



                            //Line vectorLine = Line.Create(currentPoint, direction);
                            lineList.Add(vectorLine);
                        }

                        #region Old Code
                        /*
                        else if (lostParticleBool)
                        {

                            if (line.Contains(endLostParticleIndicator))
                            {
                                //string patternOne = @"(\-|\d)\d*\.\d+";
                                string patternOne = @"(?<=\d)(\+|\-)";
                                string[] tempWords = prevLine.Split(delimeterChars, StringSplitOptions.RemoveEmptyEntries);
                                string[] xWords = Regex.Split(tempWords[2], patternOne);
                                string[] yWords = Regex.Split(tempWords[3], patternOne);
                                string[] zWords = Regex.Split(tempWords[4], patternOne);
                                //Debug.WriteLine(xWords[0]);
                                //Debug.WriteLine(yWords[0]);

                                string xFinal = xWords[0] + "E" + xWords[1] + xWords[2];
                                string yFinal = yWords[0] + "E" + yWords[1] + yWords[2];
                                string zFinal = zWords[0] + "E" + zWords[1] + zWords[2];

                                Debug.WriteLine(xFinal);
                                Debug.WriteLine(yFinal);
                                Debug.WriteLine(zFinal);

                                /*
                                foreach (string word in xWords)
                                {
                                    Debug.WriteLine(word);
                                }

                                Debug.WriteLine(" ");

                                foreach (string word in yWords)
                                {
                                    Debug.WriteLine(word);
                                }   


                                xValues.Add(Convert.ToDouble(xFinal));
                                yValues.Add(Convert.ToDouble(yFinal));
                                zValues.Add(Convert.ToDouble(zFinal));

                            }

                        //prevLine = line;
                        }     
                        */

                        #endregion
                    }

                    //Debug.WriteLine(pointList.Count);

                    // Use values and highlight the positions in SpaceClaim
                    //---------------------------------------------------------------
                    foreach (Point point in pointList)
                    {
                        pointName = "Lost Particle " + pointCounter.ToString();
                        window.ActiveTool.CreateIndicator(point);
                        //DatumPoint.Create(lostPart, pointName, point);
                        var pc = PointCurve.Create(point);
                        ITrimmedCurve shapey = CurveSegment.Create(pc);
                        DesignCurve dcurve = DesignCurve.Create(curvePart, shapey);
                        if (chosenColour == "Blue")
                        {
                            dcurve.SetColor(null, Color.Blue);
                        }
                        else if (chosenColour == "Red")
                        {
                            dcurve.SetColor(null, Color.Red);
                        }
                        else if (chosenColour == "Green")
                        {
                            dcurve.SetColor(null, Color.Green);
                        }
                        else if (chosenColour == "Orange")
                        {
                            dcurve.SetColor(null, Color.Orange);
                        }
                        else if (chosenColour == "Black")
                        {
                            dcurve.SetColor(null, Color.Black);
                        }
                        else if (chosenColour == "Yellow")
                        {
                            dcurve.SetColor(null, Color.Yellow);
                        }
                        else if (chosenColour == "Salmon Pink")
                        {
                            dcurve.SetColor(null, Color.Salmon);
                        }
                        pointCounter++;
                    }


                    if (showVectors)
                    {
                        /*
                        foreach (Line line in lineList)
                        {
                            vectorName = "Lost Particle " + lineCounter + " Vector";
                            DatumLine dLine = DatumLine.Create(vectorPart, vectorName, line);
                            lineCounter++;
                        }
                        */
                        Component vectorComponent = Component.Create(rootPart, vectorPart);
                    }

                    //Component lostParticleComponent = Component.Create(rootPart, lostPart);
                }
                else
                {
                    // Text file
                    /*
                    foreach (string line in lines)
                    {
                        Debug.Write(String.Format("{0}\n",line));
                    }
                    */
                    bool firstLine = true;
                    int length = 0;

                    foreach (string line in lines)
                    {
                        if (line.Length != 0)
                        {
                            string[] tempWords = line.Split(delimeterChars, StringSplitOptions.RemoveEmptyEntries);
                            if (firstLine)
                            {
                                firstLine = false;
                                length = tempWords.Length;
                            }

                            if (length > 3)
                            {
                                currentPoint = Point.Create(Convert.ToDouble(tempWords[0]) / 100, Convert.ToDouble(tempWords[1]) / 100, Convert.ToDouble(tempWords[2]) / 100);
                                pointList.Add(currentPoint);
                                particleCounter++;

                                Point origin = Point.Create(0, 0, 0);
                                Direction direction = Direction.Create(Convert.ToDouble(tempWords[3]), Convert.ToDouble(tempWords[4]), Convert.ToDouble(tempWords[5]));
                                vectorPoint = Point.Create(currentPoint.X + Convert.ToDouble(tempWords[3]) / 1, currentPoint.Y + Convert.ToDouble(tempWords[4]) / 1, currentPoint.Z + Convert.ToDouble(tempWords[5]) / 1);

                                if (showVectors)
                                {
                                    vectorName = "Lost Particle " + lineCounter.ToString() + " Vector";
                                    LineSegment lineSeg = new LineSegment(currentPoint, vectorPoint);
                                    ITrimmedCurve shape = CurveSegment.Create(lineSeg);
                                    DesignCurve desCurve = DesignCurve.Create(vectorPart, shape);
                                    desCurve.Name = vectorName;
                                    desCurve.SetColor(null, Color.Blue);
                                    lineCounter++;
                                    particleLineCounter++;                                   
                                }

                                //Line vectorLine = Line.CreateThroughPoints(currentPoint, origin);
                                Line vectorLine = Line.CreateThroughPoints(currentPoint, vectorPoint);



                                //Line vectorLine = Line.Create(currentPoint, direction);
                                lineList.Add(vectorLine);
                            }
                            else if (length <= 3)
                            {
                                try
                                {
                                    currentPoint = Point.Create(Convert.ToDouble(tempWords[0]) / 100, Convert.ToDouble(tempWords[1]) / 100, Convert.ToDouble(tempWords[2]) / 100);
                                    pointList.Add(currentPoint);                                    
                                    particleCounter++;
                                }
                                catch
                                {

                                }
                            }                            
                        }
                    }

                    foreach (Point point in pointList)
                    {
                        pointName = "Lost Particle " + pointCounter.ToString();
                        window.ActiveTool.CreateIndicator(point);
                        //DatumPoint.Create(lostPart, pointName, point);
                        var pc = PointCurve.Create(point);
                        ITrimmedCurve shapey = CurveSegment.Create(pc);
                        DesignCurve dcurve = DesignCurve.Create(curvePart, shapey);
                        if (chosenColour == "Blue")
                        {
                            dcurve.SetColor(null, Color.Blue);
                        }
                        else if (chosenColour == "Red")
                        {
                            dcurve.SetColor(null, Color.Red);
                        }
                        else if (chosenColour == "Green")
                        {
                            dcurve.SetColor(null, Color.Green);
                        }
                        else if (chosenColour == "Orange")
                        {
                            dcurve.SetColor(null, Color.Orange);
                        }
                        else if (chosenColour == "Black")
                        {
                            dcurve.SetColor(null, Color.Black);
                        }
                        else if (chosenColour == "Yellow")
                        {
                            dcurve.SetColor(null, Color.Yellow);
                        }
                        else if (chosenColour == "Salmon Pink")
                        {
                            dcurve.SetColor(null, Color.Salmon);
                        }
                        pointCounter++;
                    }

                    if (showVectors)
                    {                      
                        Component vectorComponent = Component.Create(rootPart, vectorPart);
                    }

                    //Component lostParticleComponent = Component.Create(rootPart, lostPart);                   
                }
            }
        }        
    }    
}
