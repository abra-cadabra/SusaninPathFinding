using System.Windows.Forms;
using SusaninPathFinding;

namespace SusaninPathFindingForm
{
    public partial class Form1 : Form
    {
        private GraphManager Map;

        public Form1()
        {
            InitializeComponent();
            Map = new GraphManager();


            ////Map.NodeCosts[Map.FindNode(0 + 99, 0 + 99, 0)] = CellType.Impassable;
            ////Map.NodeCosts[Map.FindNode(1 + 99, 0 + 99, 0)] = CellType.Impassable;
            ////Map.NodeCosts[Map.FindNode(2 + 99, 0 + 99, 0)] = CellType.Impassable;
            ////Map.NodeCosts[Map.FindNode(4 + 99, 0 + 99, 0)] = CellType.Impassable;
            ////Map.NodeCosts[Map.FindNode(5 + 99, 0 + 99, 0)] = CellType.Impassable;
            ////Map.NodeCosts[Map.FindNode(6 + 99, 0 + 99, 0)] = CellType.Impassable;
            ////Map.NodeCosts[Map.FindNode(7 + 99, 0 + 99, 0)] = CellType.Impassable;

            ////Map.NodeCosts[Map.FindNode(0 + 99, 1 + 99, 0)] = CellType.Impassable;
            ////Map.NodeCosts[Map.FindNode(7 + 99, 1 + 99, 0)] = CellType.Impassable;

            ////Map.NodeCosts[Map.FindNode(0 + 99, 2 + 99, 0)] = CellType.Impassable;
            ////Map.NodeCosts[Map.FindNode(7 + 99, 2 + 99, 0)] = CellType.Impassable;

            ////Map.NodeCosts[Map.FindNode(7 + 99, 3 + 99, 0)] = CellType.Impassable;

            ////Map.NodeCosts[Map.FindNode(0 + 99, 4 + 99, 0)] = CellType.Impassable;
            ////Map.NodeCosts[Map.FindNode(7 + 99, 4 + 99, 0)] = CellType.Impassable;

            ////Map.NodeCosts[Map.FindNode(0 + 99, 5 + 99, 0)] = CellType.Impassable;
            ////Map.NodeCosts[Map.FindNode(2 + 99, 5 + 99, 0)] = CellType.Impassable;
            ////Map.NodeCosts[Map.FindNode(3 + 99, 5 + 99, 0)] = CellType.Impassable;
            ////Map.NodeCosts[Map.FindNode(4 + 99, 5 + 99, 0)] = CellType.Impassable;
            ////Map.NodeCosts[Map.FindNode(5 + 99, 5 + 99, 0)] = CellType.Impassable;
            ////Map.NodeCosts[Map.FindNode(6 + 99, 5 + 99, 0)] = CellType.Impassable;
            ////Map.NodeCosts[Map.FindNode(7 + 99, 5 + 99, 0)] = CellType.Impassable;

            ////IList<Point3D> locations = Map.FindPath(new MovingAgent(Map.NodeCosts), Map.FindNode(123, 289, 0), Map.FindNode(5 + 99, 2 + 99, 0));

            //Map.NodeCosts[Map.FindNode(3, 1, 0)].Type = CellType.Impassable;
            //Map.NodeCosts[Map.FindNode(4, 1, 0)].Type = CellType.Impassable;
            //Map.NodeCosts[Map.FindNode(5, 1, 0)].Type = CellType.Impassable;

            //Map.NodeCosts[Map.FindNode(3, 2, 0)].Type = CellType.Impassable;
            //Map.NodeCosts[Map.FindNode(5, 2, 0)].Type = CellType.Impassable;

            //Map.NodeCosts[Map.FindNode(1, 3, 0)] = new LadderCellInfo(Direction.East);
            //Map.NodeCosts[Map.FindNode(2, 3, 0)].Type = CellType.Impassable;
            //Map.NodeCosts[Map.FindNode(3, 3, 0)].Type = CellType.Impassable;
            //Map.NodeCosts[Map.FindNode(5, 3, 0)].Type = CellType.Impassable;

            ////===============================================================================

            //Map.NodeCosts[Map.FindNode(3, 1, 1)].Type = CellType.Passable;
            //Map.NodeCosts[Map.FindNode(4, 1, 1)].Type = CellType.Passable;
            //Map.NodeCosts[Map.FindNode(5, 1, 1)].Type = CellType.Passable;

            //Map.NodeCosts[Map.FindNode(3, 2, 1)].Type = CellType.Passable;
            //Map.NodeCosts[Map.FindNode(5, 2, 1)].Type = CellType.Passable;

            //Map.NodeCosts[Map.FindNode(2, 3, 1)] = new LadderCellInfo(Direction.East);
            //Map.NodeCosts[Map.FindNode(3, 3, 1)].Type = CellType.Passable;
            //Map.NodeCosts[Map.FindNode(5, 3, 1)].Type = CellType.Passable;

            //IList<Point3D> locations = Map.FindPath(new MovingAgent(Map.NodeCosts), Map.FindNode(0, 0, 0), Map.FindNode(4, 1, 1));
        }
    }
}
