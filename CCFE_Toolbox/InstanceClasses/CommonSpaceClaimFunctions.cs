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
using Point = SpaceClaim.Api.V18.Geometry.Point;

namespace CCFE_Toolbox.InstanceClasses
{
    class CommonSpaceClaimFunctions
    {
        public List<IDesignBody> GatherAllBodies(IPart part)
        {
            var allBodies = new List<IDesignBody>();
            allBodies.AddRange(part.GetDescendants<IDesignBody>());
            return allBodies;
        }

        public List<IDesignBody> GatherAllVisibleBodies(IPart part, Window window)
        {
            List<IDesignBody> allBodies = new List<IDesignBody>();
            var tempList = new List<IDesignBody>();
            tempList.AddRange(part.GetDescendants<IDesignBody>());
            foreach (IDesignBody body in tempList)
            {
                if (CheckIfVisible(body, window))
                {
                    allBodies.Add(body);
                }
            }
            return allBodies;
        }

        public List<IDesignBody> GatherSelectionBodies(InteractionContext context )
        {
            var iBodies = new List<IDesignBody>();
            var selection = context.Selection;
            foreach (IDesignBody body in selection)
            {
                iBodies.Add(body);
            }
            return iBodies;
        }

        public List<Body> CopyIDesign(List<IDesignBody> iDesBods)
        {
            var bodies = new List<Body>();
            foreach (IDesignBody bod in iDesBods)
            {
                DesignBody master = bod.Master;
                Matrix masterTrans = bod.TransformToMaster;
                Matrix reverseTrans = masterTrans.Inverse;
                Body copy = master.Shape.Copy();
                copy.Transform(reverseTrans);
                bodies.Add(copy);
            }

            return bodies;
        }

        public List<Body> CopyIDesignAndTransforms(List<IDesignBody> iDesBods, out List<Matrix> transforms)
        {
            transforms = new List<Matrix>();
            var bodies = new List<Body>();
            foreach (IDesignBody bod in iDesBods)
            {
                DesignBody master = bod.Master;
                Matrix masterTrans = bod.TransformToMaster;
                transforms.Add(masterTrans);
                Matrix reverseTrans = masterTrans.Inverse;
                Body copy = master.Shape.Copy();
                copy.Transform(reverseTrans);
                bodies.Add(copy);
            }

            return bodies;
        }

        public Body CreateRectBody(Part part, double length, double width, double height, PointUV UVPoint, Plane plane)
        {
            Debug.Assert(part != null, "part != null");
            Body body = Body.ExtrudeProfile(new RectangleProfile(plane, length, width, UVPoint), height);
            return body;
        }

        public DesignBody CreateDesignBody(Part part, Body body)
        {
            DesignBody desBodyMaster = DesignBody.Create(part, Resources.Block, body);

            return desBodyMaster;
        }

        public void FileWriter(string path, string words)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(words);
            }
        }

        public void ColourFace(Face face, DesignBody desBody)
        {
            var desFace = desBody.GetDesignFace(face);
            desFace.SetColor(null, Color.Magenta);
        }

        public void SetFaceTranslucent (Face face, DesignBody desBody)
        {
            // Get face current colour
            var bodyColour = desBody.GetColor(null);
            var otherColour = Color.FromArgb(173, bodyColour.Value.R, bodyColour.Value.G, bodyColour.Value.B);
            var desFace = desBody.GetDesignFace(face);
            desFace.SetColor(null, otherColour);
        }

        public bool CheckIfVisible (IHasVisibility obj, Window window)
        {
            var context = window.Scene as IAppearanceContext;
            return obj.IsVisible(context);
        }

        public List<Moniker<IDesignBody>> ReturnMonikers (List<IDesignBody> iBodies)
        {
            List<Moniker<IDesignBody>> monikers = new List<Moniker<IDesignBody>>();
            foreach(IDesignBody iBody in iBodies)
            {
                monikers.Add(iBody.Moniker);
            }
            return monikers;
        }


    }
}
