![1](/Logos/Coua2.png)

# CouaCurves

Coua Curves is a basic C# library for for quickly generating curves and evaluating them. 

## About

I created this basic library for the purpose of having quick access to things like Bezier Curves when making animations for applications.  Sometimes all I need is to evaluate a simple curve rather than deal with a bulky animator.  

### The "CouaVector2" struct

A simple struct for holding X and Y values on a graph.  

### The "CouaBezier" class

A class that holds 4 control points that will form a cubic bezier curve.  The points can be initialized in the constructor via CouaVector2s or floats.  These points can also be set through the "SetPointA(), SetPointB() ..." methods. The parameter "position" refers to the point's value on the X axis.  Paramter "value" refers to the point's value on the Y axis.

#### EvaluateCoords()

Evaluates the curve at _t_ and returns both it's X and Y value as a "CouaVector2".

#### EvaluateY()

Evaluates the curve at _t_ and returns it's Y position.  This is used most often.

#### EvaluateX()

Evaluates the curve at _t_ and returns it's X position.  

## Build Instructions

1. Clone the repository.
2. Navigate to the repository using the Command Prompt.
3. Build for Debug or Release: 

```
dotnet build --configuration Release
```

5. Use the resulting .dll in any of your C# projects.