using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MazeGeneratorCSharp
{
    public class Line
    {
        public Point Point1 { get; set; }
        public Point Point2 { get; set; }
        public bool IsOpen { get; set; } = false;
    }
}
