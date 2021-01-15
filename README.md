# SpaceClaim API Neutronics Tools

This repository contains a set of tools desgined the increase the efficiency of the preparation of CAD models for neutronics analysis. The tools
automate many of the tasks tpyically undertaken to simply a detailed CAD model to a level suitable for particle transport. Install instructions 
for inclusion of the tools as an addtional tab in the SpaceClaim GUI is provided below. The tools are desgined to aid.the user produce a CAD
model suitable for conversion in softwre such as SuperMC or McCAD.

It is expected that user of these tools has some level of competency using SpaceClaim. The tools do not fix broken CAD models nor is that their 
intended purpose. In any case, the tools will likely not work with poorly defined geometry. 

More extensive documentation can be found here: ![]{github_doc}

## Development

The tools have been produced using a combination of the C# API and python scripting available in ANSYS SpaceClaim. 

Feedback on existing tools welcome to: Alex.Valentine@ukaea.uk

## Included Tools and Purpose

| Tool | Purpose |
| ------ | ------ |
| Body Select | Highlights cones or tori, identifies small volumes (user input) and small rounds (user input) |
| Export | Saves each different colour in geometry as separate .sat file |
| Lost Particles | Plots MCNP lost particles on geometry|
| Max Surfaces | Highlights all bodies with a number of surfaces exceeding user input |
| Pipe Simplification | Automates simplification of pipe networks  |
| Tori Simplificaiton | Simplifies complex tori to cylindrical bodies |
| Cylinder Plane edge checker | Identifies a shared edge between and plane and cylinder in a single body |
| Cylinder plane splitter | Splits a body along cylinder-plane edge |
| Geometry assessor | Selects tori, cones, bodies with shared edges between higher order surfaces |
| Mesh tally writer | Writes the MCNP definition for a mesh tally based on geometry |
| Mesh tally checker | Plots simple mesh tallies from MCNP |
| Cylinder Cylinder splitter | Splits along edge between two different cylinders |
| Void Generator | Creates void cells for geometry to define reciprocal space |
| Volume Assessor | Calculates volume of all solids in structure tree |

## Installation

For installation instructions see. 


License
----

[MIT](../master/LICENSE)







