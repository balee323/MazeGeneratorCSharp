using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MazeGeneratorCSharp
{
    public class Cell
    {
        //public Rectangle Rectangle { get; set; }
        public bool HasBeenVisited { get; set; } = false;
        public Line Top { get; set; }
        public Line Bottom { get; set; }
        public Line Left { get; set; }
        public Line Right { get; set; }
        public int CellId { get; set; }

    }
}
