# SpaceClaim API Neutronics Tools

This repository contains a set of tools desgined the increase the efficiency of the preparation of CAD models for neutronics analysis. The tools
automate many of the tasks typically undertaken to simply a detailed CAD model to a level suitable for particle transport. Install instructions 
for inclusion of the tools as an addtional tab in the SpaceClaim GUI is provided below. The tools are desgined to aid.the user produce a CAD
model suitable for conversion in softwre such as SuperMC or McCAD.

It is expected that user of these tools has some level of competency using SpaceClaim. The tools do not fix broken CAD models nor is that their 
intended purpose. In any case, the tools will likely not work with poorly defined geometry. 

[More extensive documentation including installation instructions can be found here](docs)

## Development

The tools have been produced using a combination of the C# API and python scripting available in ANSYS SpaceClaim. All tools exist in a single ribbon which is added when the tools are compiled. 

Feedback on existing tools or proposals for new tools welcome.

## Included Tools and Purpose

| Tool | Purpose |
| ------ | ------ |
| Body Select | Highlights cones or tori, identifies small volumes (user input) and small rounds (user input) |
| Export | Saves bodies of different colour in geometry as separate .sat files |
| Lost particles | Plots MCNP lost particles on geometry|
| Max surfaces | Highlights all bodies with a number of surfaces exceeding user input |
| Pipe tools | Automates simplification of pipe networks  |
| Tori tools | Simplifies complex tori to cylindrical bodies |
| Cylinder-plane locator | Identifies a shared edge between and plane and cylinder in a single body |
| Cylinder-plane splitter | Splits a body along cylinder-plane edge |
| Geometry assessor | Selects tori, cones, bodies with shared edges between higher order surfaces |
| Mesh tally writer | Writes the MCNP definition for a mesh tally based on geometry |
| Mesh tally checker | Plots simple mesh tallies from MCNP |
| Cylinder Cylinder splitter | Splits along edge between two different cylinders |
| Void generator | Creates void cells for geometry to define reciprocal space |
| Volume assessor | Calculates volume of all solids in structure tree |

## Installation

To compile and install the CCFE SpaceClaim tools it is recommended that Visual Studios Community edition (which is free to use on opensource projects) is installed. This provides a relatively quick and easy way to compile and install the tools into SpaceClaim.

These instructions use Visual Studios 2019, but should be relatively similar for all versions of Visual Studios. It should be noted that .NET Framework version 4 or later is required.

First you will need to clone the github repository by clicking on clone repository in the File menu:

![install_1](https://user-images.githubusercontent.com/40658938/104763547-f80b5780-575d-11eb-8cef-fe3f8b0d444d.png)

Enter the github address of the repository in the first box and the local location of the folder you want to store the repository in the second box: 

![install_2](https://user-images.githubusercontent.com/40658938/104892446-1ba2ed80-596a-11eb-8812-73d32f987af5.png)

Once cloned you will need to re-point the project to the SpaceClaim API Library. This can be done by deleting **SpaceClaim.Api.V16** and **SpaceClaim.Api.V16.Scripting** from the **‘References’** in the **‘Solution Explorer’**.

![install_3](https://user-images.githubusercontent.com/40658938/104892455-1e9dde00-596a-11eb-8980-8d2d70dd811a.png)

And then re-adding them by right-clicking on References and selecting **‘Add Reference’**:

![install_4](https://user-images.githubusercontent.com/40658938/104892467-2198ce80-596a-11eb-924b-188c73db64e9.png)

Click browse and navigate to *C:\Program Files\ANSYS Inc\v192\scdm\SpaceClaim.Api.V16*  (or the location SpaceClaim is installed if it is not installed as part of ANSYS):

![install_5](https://user-images.githubusercontent.com/40658938/104892474-23fb2880-596a-11eb-9c49-5e07a9888bce.png)

Add **SpaceClaim.Api.V16.dll**:

![install_6](https://user-images.githubusercontent.com/40658938/104892486-28274600-596a-11eb-94ea-00aab57643fb.png)

Add **SpaceClaim.Api.V16.Scripting.dll** (found in *C:Program Files\ANSYS Inc\v192\scdm*).

![install_7](https://user-images.githubusercontent.com/40658938/104892505-2c536380-596a-11eb-923e-643ce6f5fea1.png)

Repeat the above steps to also add **SpaceClaim.Api.V18.dll** and **SpaceClaim.Api.V18.Scripting.dll**.

The next step is to make sure the output is placed into the correct folder, which can then be loaded by SpaceClaim when it opens. To do this the projects **‘Properties’** should be opened in the Solution Explorer. 

![install_8](https://user-images.githubusercontent.com/40658938/104892516-2f4e5400-596a-11eb-870a-dafe35f9ab76.png)

Check that the Output path is set to:
*C:ProgramData\SpaceClaim\AddIns\Samples\V18\CCFE\Toolkit* (Or the location of your ProgramData directory if this is not on your C drive).

![install_9](https://user-images.githubusercontent.com/40658938/104892526-32494480-596a-11eb-881d-a8456f2b80a8.png)

You can now build the tools in Visual Studio. **Please ensure that the SpaceClaim application is closed when you do this**.

License
----

[MIT](LICENSE)







