using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeGeneratorCSharp
{
    enum Wall
    { 
        Top = 1,
        Bottom = 2,
        Left = 3,
        Right = 4
    };

    public partial class Form1 : Form
    {

        private int size = 256; //64, 128, 256, 512


        private Cell previousCell;
        private Stack<Cell> visitedCells = new Stack<Cell>();
        private List<Cell> cells = new List<Cell>();

        public Form1()
        {
            InitializeComponent();
        }

        private void GenerateMaze()
        {
            Graphics graphics = this.CreateGraphics();

  

            Cell cell = cells[0];
            visitedCells.Push(cell);

            Random rand = new Random();

            var lootCell = cells[rand.Next(0, (cells.Count/2)-1)];
            Pen purplePen = new Pen(Color.Purple, 5);
            this.CreateGraphics().DrawEllipse(purplePen, (float)lootCell.Top.Point1.X + 5, 
                (float)lootCell.Top.Point1.Y + 5, 30, 30);


            var startCell = cells[rand.Next((cells.Count/2) + 2, cells.Count - 1)];
            Pen orangePen = new Pen(Color.Orange, 5);
            this.CreateGraphics().DrawEllipse(orangePen, (float) startCell.Top.Point1.X + 5,
                (float) startCell.Top.Point1.Y + 5, 30, 30);

            while (visitedCells.Count > 0)
            {
                if (cell.HasBeenVisited)
                {
                    cell = visitedCells.Pop();
                }
         
                ProcessNextCell(cell, graphics);

                int nextCell = rand.Next(0, cells.Count - 1);
                cell = cells[nextCell];

                Thread.Sleep(50);
            }
        }

        private void ProcessNextCell(Cell cell, Graphics graphics)
        {
            
            cell.HasBeenVisited = true;

            SolidBrush blueBrush = new SolidBrush(Color.Aquamarine);

            Wall wall = (Wall) new Random().Next(1, 4);

            OpenASide(cell, wall, graphics);

            visitedCells.Push(cell);
            previousCell = cell;
        }

        private void OpenASide(Cell cell, Wall wall, Graphics graphics)
        {
            Pen grayPen = new Pen(Color.LightGray, 3);
            if (wall == Wall.Top)
            {
                graphics.DrawLine(grayPen, cell.Top.Point1, cell.Top.Point2);
               
            }
            if (wall == Wall.Left)
            {
                graphics.DrawLine(grayPen, cell.Left.Point1, cell.Left.Point2);
                if (cell.Left.Point1.X -3 > 0)
                {
                    Point point1 = new Point(cell.Left.Point1.X - 3, cell.Left.Point1.Y);
                    Point point2 = new Point(cell.Left.Point2.X - 3, cell.Left.Point1.Y);

                    graphics.DrawLine(grayPen, point1, point2);
                }
            }
            if (wall == Wall.Bottom)
            {
                graphics.DrawLine(grayPen, cell.Bottom.Point1, cell.Bottom.Point2);

            }
            if (wall == Wall.Right)
            {
                graphics.DrawLine(grayPen, cell.Right.Point1, cell.Right.Point2);
                if (cell.Left.Point1.X +3 < this.Left)
                {
                    Point point1 = new Point(cell.Left.Point1.X + 3, cell.Left.Point1.Y);
                    Point point2 = new Point(cell.Left.Point2.X + 3, cell.Left.Point1.Y);

                    graphics.DrawLine(grayPen, point1, point2);
                }
            }

        }


        private void GenerateGridUsingLines()
        {
            cells.Clear();
            Pen blackPen = new Pen(Color.Black, 3);

            int cellId = 0;

            for (int i = 0; i < Math.Sqrt(size); i++)
            {
                for (int j = 0; j < Math.Sqrt(size); j++)
                {
                    Cell cell = new Cell();

                    cell.CellId = ++cellId;

                    cell.Top =
                        new Line
                        {
                            Point1 = new Point(i * 50, j * 50),
                            Point2 = new Point((i * 50) + 50, j * 50)
                        };
                   
                    cell.Left =
                        new Line
                        {
                            Point1 = new Point(i * 50, j * 50),
                            Point2 = new Point(i * 50, (j * 50) + 50)
                        };
                    
                    cell.Bottom =
                        new Line
                        {
                            Point1 = new Point((i * 50), (j * 50) + 50),
                            Point2 = new Point((i * 50) +50, (j * 50) + 50)
                        };
                    
                    cell.Right =
                        new Line
                        {
                            Point1 = new Point((i * 50) + 50, j * 50),
                            Point2 = new Point((i * 50) + 50, (j * 50) + 50)
                        };

                    this.CreateGraphics().DrawLine(blackPen, cell.Top.Point1,  cell.Top.Point2);
                    this.CreateGraphics().DrawLine(blackPen, cell.Bottom.Point1,  cell.Bottom.Point2);
                    this.CreateGraphics().DrawLine(blackPen, cell.Left.Point1,  cell.Left.Point2);
                    this.CreateGraphics().DrawLine(blackPen, cell.Right.Point1,  cell.Right.Point2);
                    cells.Add(cell);
                }
            }

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
            GenerateGridUsingLines();
            Task.Run( () => GenerateMaze());
        }

    }
}
