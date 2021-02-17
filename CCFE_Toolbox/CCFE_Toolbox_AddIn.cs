using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using SpaceClaim.Api.V18.Extensibility;
using System.Windows.Forms;
using SpaceClaim.Api.V18.Geometry;


namespace CCFE_Toolbox
{
    class CCFE_Toolbox_AddIn : SpaceClaim.Api.V18.Extensibility.AddIn, SpaceClaim.Api.V18.Extensibility.IExtensibility, SpaceClaim.Api.V18.Extensibility.ICommandExtensibility, SpaceClaim.Api.V18.Extensibility.IRibbonExtensibility
    {

        readonly SpaceClaim.Api.V18.Extensibility.CommandCapsule[] capsules = new[]
        {
            new SpaceClaim.Api.V18.Extensibility.CommandCapsule("CCFE_Toolbox.C#.V18.RibbonTab", Properties.Resources.RibbonTabText),
            new SpaceClaim.Api.V18.Extensibility.CommandCapsule("CCFE_Toolbox.C#.V18.PartGroup", Properties.Resources.PartGroupText), 
            new CCFE_Commands.Export(),
            new CCFE_Commands.Pipes_simp(),
            new CCFE_Commands.Tori_simp(),
            new CCFE_Commands.BodySelect(),
            new CCFE_Commands.LostParticles(),
            new CCFE_Commands.MaxSurfaces(),
            new CCFE_Commands.CylinderPlaneSeperator(),
            new CCFE_Commands.CylinderPlaneEdgeChecker(),
            new CCFE_Commands.PipeSeperator(),
            new CCFE_Commands.VoidGenerator(),
            new CCFE_Commands.MeshTallyWriter(),
            new CCFE_Commands.GeometryAssessor(),
            new CCFE_Commands.VolumeAssessor(),
            new CCFE_Commands.MeshTallyChecker()

        };

        #region IExtensibility members
        public bool Connect()
        {
            // Initilization for add-in
            SpaceClaim.Api.V18.Unsupported.JournalMethods.RecordAutoLoadAddIn("SampleAddIn.C#.V18.RibbonTab", Properties.Resources.AddInManifestInfo);

            return true;
        }

        public void Disconnect()
        {

        }

        #endregion

        #region ICommandExtensibility members

        public void Initialize()
        {
            foreach (SpaceClaim.Api.V18.Extensibility.CommandCapsule capsule in capsules)
                capsule.Initialize();


            // Insert commands here for the context menu
        }

        #endregion

        #region IRibbonExtensibility members

        public string GetCustomUI()
        {
            return Properties.Resources.Ribbon;
        }

        #endregion
    }


}

