using DevExpress.XtraCharts;
using Solver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace FESolver
{
    public enum eResultToShow
    {
        Dispx,
        Dispy,

    }
    public partial class Main : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        #region Ctor

        public Main()
        {
            InitializeComponent();
            SubscribeToEvents();
            _nodesList = new List<Node>();
            _TrussElementsList = new List<TrussElement>();
            //_restrainedNodes = new List<RestrainedNode>();
            //_nodalForces = new List<PointLoad>();

            CreateExample1();

            prepareUI();
            SetNodeTableColumns();
            SetElementsTableColumns();
            SetBCTableColumns();
            SetLoadTableColumns();
            EditNodeTableGridView();
            EditElementsTableGridView();
            EditBCTableGridView();
            EditLoadTableGridView();
            drawChart();
        }

        #endregion

        #region Private Fields

        private List<Node> _nodesList;
        private List<TrussElement> _TrussElementsList;
        private DataTable _dataNodeTable;
        private DataTable _dataTrussElementsTable;
        private DataTable _dataBoundaryConditionsTable;
        private DataTable _dataLoadTable;
        //private List<RestrainedNode> _restrainedNodes;
        //private List<PointLoad> _nodalForces;

        #endregion

        #region Private Methods

        private void SubscribeToEvents()
        {
            BtnAnalyze.Click += BtnAnalyze_Click;
            //btnAddNode.Click += BtnAddNode_Click;
            //btnAddElement.Click += BtnAddElement_Click;
            //btnAddLoad.Click += BtnAddLoad_Click;
            //btnAddRestrain.Click += BtnAddRestrain_Click;
            //btnSolveTruss.Click += BtnSolveTruss_Click;
            pictureBox1.Paint += canvasPictureBox_Paint;
        }

        private void BtnAnalyze_Click(object sender, System.EventArgs e)
        {
            Assembler assembler = new Assembler(_TrussElementsList, _nodesList);
            AddDisplacementColorMapXDirection(eResultToShow.Dispy);
        }

        private void CreateExample1()
        {
            AddNode(1, 0, 0);
            AddNode(2, 5, 8.66);
            AddNode(3, 15, 8.66);
            AddNode(4, 20, 0);
            AddNode(5, 10, 0);
            AddNode(6, 10, -5);
            var E = 200 * Math.Pow(10, 9);
            var A = 5000;
            AddMember("A", 1, 2, E, A);
            AddMember("B", 2, 3, E, A);
            AddMember("C", 3, 4, E, A);
            AddMember("D", 4, 5, E, A);
            AddMember("E", 1, 5, E, A);
            AddMember("F", 2, 5, E, A);
            AddMember("G", 3, 5, E, A);
            AddMember("H", 5, 6, E, A);
            AddRestrainedNode(4, true, true);
            AddRestrainedNode(6, true, true);
            AddLoad(1, 0, -200000);

        }

        private void CreateExample2()
        {
            AddNode(1, 0, 0);
            AddNode(2, 1, 0);
            AddNode(3, 0.5, 1);
            var E = 1 * Math.Pow(10, 6);
            var A = 10000;
            AddMember("A", 1, 2, E, A);
            AddMember("B", 2, 3, E, A);
            AddMember("C", 3, 1, E, A);
            AddRestrainedNode(1, true, true);
            AddRestrainedNode(2, false, true);
            AddLoad(3, 0, -20);


        }

        private void AddLoad(int nodeId, double fx, double fy)
        {
            var node = (TrussNode)GetNodeById(nodeId);
            node.Fx = fx;
            node.Fy = fy;

        }

        private void AddRestrainedNode(int nodeId, bool isXRestrained, bool isYRestrained)
        {
            var node = (TrussNode)GetNodeById(nodeId);
            node.XDirection = isXRestrained ? eRestraintCondition.restrained : eRestraintCondition.free;
            node.YDirection = isYRestrained ? eRestraintCondition.restrained : eRestraintCondition.free;
        }

        private Node GetNodeById(int NodeId)
        {
            return _nodesList.FirstOrDefault(x => x.ID == NodeId);
        }

        private void AddNode(int ID, double X, double Y)
        {
            _nodesList.Add(new TrussNode(ID, X, Y));
        }

        public void AddMember(string memberLabel, int nodeI, int nodeJ, double Area, double E)
        {
            var nodei = GetNodeById(nodeI);
            var nodej = GetNodeById(nodeJ);
            _TrussElementsList.Add(new TrussElement(memberLabel, nodei, nodej, E, Area));
        }

        private void prepareUI()
        {
            //cmbSupportType.Items.Add(eRestraintCondition.X);
            //cmbSupportType.Items.Add(eRestraintCondition.Y);
            //cmbSupportType.Items.Add(eRestraintCondition.XY);
            // Add a title to the chart (if necessary).
            //chElements.Titles.Add(new ChartTitle());
            //chElements.Titles[0].Text = "A Line Chart";
            //drawingControl.OptionsView.ShowRulers = false;
            //drawingControl.OptionsView.ShowPageBreaks = false;
            //drawingControl.OptionsView.ShowGrid= false;
        }

        private void BtnSolveTruss_Click(object sender, EventArgs e)
        {
           
        }

        private void AddDisplacementColorMapXDirection(eResultToShow type, int magnificationFactor = 150000000, int numPoints = 200)
        {
            var xcoordList = new List<double>();
            var ycoordList = new List<double>();
            var intensities = new List<double>();

            double minValue;
            double maxValue;
            switch (type)
            {
                case eResultToShow.Dispx:
                    minValue = _nodesList.Cast<TrussNode>().Min(x => Math.Abs(x.Dispx));
                    maxValue = _nodesList.Cast<TrussNode>().Max(x => Math.Abs(x.Dispx));
                    break;
                case eResultToShow.Dispy:
                    minValue = _nodesList.Cast<TrussNode>().Min(x => Math.Abs(x.Dispy));
                    maxValue = _nodesList.Cast<TrussNode>().Max(x => Math.Abs(x.Dispy));
                    break;

                default:
                    minValue = _nodesList.Cast<TrussNode>().Min(x => Math.Abs(x.Dispy));
                    maxValue = _nodesList.Cast<TrussNode>().Max(x => Math.Abs(x.Dispy));
                    break;
            }

            // Create a scatter chart series
            var series = new Series("Intensity", ViewType.Point);
            PointSeriesView seriesView = (PointSeriesView)series.View;

            foreach (var element in _TrussElementsList)
            {
                //var element = _TrussElementsList.First();
                var NodeI = (TrussNode)element.NodeI;
                var NodeJ = (TrussNode)element.NodeJ;
                var x1 = NodeI.getXcoordFinal(magnificationFactor);
                var y1 = NodeI.getYcoordFinal(magnificationFactor);
                var x2 = NodeJ.getXcoordFinal(magnificationFactor);
                var y2 = NodeJ.getYcoordFinal(magnificationFactor);
                //var series1 = new Series("Intensity", ViewType.Line);
                //series1.Points.Add(new SeriesPoint(x1, y1));
                //series1.Points.Add(new SeriesPoint(x2, y2));
                //chartDrawing.Series.Add(series1);

                xcoordList.AddRange(Enumerable.Range(0, numPoints).Select(i => x1 + i * (x2 - x1) / (numPoints - 1.0)));
                ycoordList.AddRange(Enumerable.Range(0, numPoints).Select(i => y1 + i * (y2 - y1) / (numPoints - 1.0)));
                intensities.AddRange(Enumerable.Range(0, numPoints).Select(i => Math.Abs(NodeI.Dispy + i * (NodeJ.Dispy - NodeI.Dispy) / (numPoints - 1.0))));
            }

            // Create a ColorMap object with the jet colormap and the range of intensities
            ColorMap colorMap = new ColorMap(minValue, maxValue);
            for (int i = 0; i < intensities.Count; i++)
            {
                var pointColor = colorMap.GetColor(intensities[i]);
                var seriesPoint = new SeriesPoint(xcoordList[i], ycoordList[i]) { Color = pointColor };
                series.ShowInLegend = false;
                series.Points.Add(seriesPoint);
            }
            seriesView.PointMarkerOptions.Kind = MarkerKind.Circle;
            seriesView.PointMarkerOptions.Size = 3;

            chartDrawing.Series.Add(series);
        }

        private void BtnAddRestrain_Click(object sender, EventArgs e)
        {
            //eRestraintCondition restraniedType = (eRestraintCondition)cmbSupportType.SelectedIndex;
            //int Id = Convert.ToInt32(txtBCNodeId.Text);
            //_restrainedNodes.Add(new RestrainedNode(Id, restraniedType));
            AddRestrainedDataRows();
        }

        private void BtnAddElement_Click(object sender, EventArgs e)
        {
            //var startnodeId = Convert.ToInt32(txtNodeI.Text);
            //var EndnodeId = Convert.ToInt32(txtNodeJ.Text);
            //NodesInfo startNode = _nodesList.Find(obj => obj.ID == startnodeId);
            //NodesInfo endNode = _nodesList.Find(obj => obj.ID == EndnodeId);
            //var E = Convert.ToDouble(txtStiffness.Text);
            //var A = Convert.ToDouble(txtSectionArea.Text);

            //_TrussElementsList.Add(new TrussElement(startNode, endNode, E, A));
            AddElementsDataRows();
        }

        private void EditNodeTableGridView()
        {
            gvNodes.OptionsView.ShowGroupPanel = false;
            gvNodes.OptionsCustomization.AllowColumnMoving = false;
            gvNodes.OptionsCustomization.AllowFilter = false;
            gvNodes.OptionsMenu.EnableColumnMenu = false;
            gvNodes.OptionsView.ShowIndicator = false;
            gvNodes.OptionsView.AllowHtmlDrawHeaders = true;
            gvNodes.OptionsView.ColumnAutoWidth = true;
            gvNodes.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            gvNodes.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvNodes.Columns[2].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvNodes.Columns[0].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            gvNodes.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvNodes.Columns[2].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }

        private void EditElementsTableGridView()
        {
            gvElements.OptionsView.ShowGroupPanel = false;
            gvElements.OptionsCustomization.AllowColumnMoving = false;
            gvElements.OptionsCustomization.AllowFilter = false;
            gvElements.OptionsMenu.EnableColumnMenu = false;
            gvElements.OptionsView.ShowIndicator = false;
            gvElements.OptionsView.AllowHtmlDrawHeaders = true;
            gvElements.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            gvElements.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvElements.Columns[2].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvElements.Columns[0].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            gvElements.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvElements.Columns[2].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }

        private void EditBCTableGridView()
        {
            gvBoundaryCondition.OptionsView.ShowGroupPanel = false;
            gvBoundaryCondition.OptionsCustomization.AllowColumnMoving = false;
            gvBoundaryCondition.OptionsCustomization.AllowFilter = false;
            gvBoundaryCondition.OptionsMenu.EnableColumnMenu = false;
            gvBoundaryCondition.OptionsView.ShowIndicator = false;
            gvBoundaryCondition.OptionsView.AllowHtmlDrawHeaders = true;
            gvBoundaryCondition.OptionsView.ColumnAutoWidth = true;
            gvBoundaryCondition.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            gvBoundaryCondition.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvBoundaryCondition.Columns[0].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            gvBoundaryCondition.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }

        private void EditLoadTableGridView()
        {
            gvLoads.OptionsView.ShowGroupPanel = false;
            gvLoads.OptionsCustomization.AllowColumnMoving = false;
            gvLoads.OptionsCustomization.AllowFilter = false;
            gvLoads.OptionsMenu.EnableColumnMenu = false;
            gvLoads.OptionsView.ShowIndicator = false;
            gvLoads.OptionsView.AllowHtmlDrawHeaders = true;
            gvLoads.OptionsView.ColumnAutoWidth = true;
            gvLoads.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            gvLoads.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvLoads.Columns[0].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            gvLoads.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }

        private void AddNodesDataRows()
        {
            DataRow row;
            _dataNodeTable.Rows.Clear();
            foreach (Node node in _nodesList)
            {
                row = _dataNodeTable.NewRow();
                row[0] = node.ID;
                row[1] = node.Xcoord;
                row[2] = node.Ycoord;
                _dataNodeTable.Rows.Add(row);
            }
        }

        private void AddElementsDataRows()
        {
            DataRow row;
            _dataTrussElementsTable.Rows.Clear();
            foreach (TrussElement elemnent in _TrussElementsList)
            {
                row = _dataTrussElementsTable.NewRow();
                row[0] = elemnent.NodeI.ID;
                row[1] = elemnent.NodeJ.ID;
                row[1] = elemnent.NodeJ.ID;
                row[2] = elemnent.L;
                row[3] = elemnent.Theta;
                _dataTrussElementsTable.Rows.Add(row);
            }
        }

        private void AddLoadsDataRows()
        {
            //DataRow row;
            //_dataLoadTable.Rows.Clear();
            //foreach (PointLoad force in _nodalForces)
            //{
            //    row = _dataLoadTable.NewRow();
            //    row[0] = force.NodeID;
            //    row[1] = force.Load.XComponent;
            //    row[2] = force.Load.YComponent;
            //    _dataLoadTable.Rows.Add(row);
            //}
        }
        private void AddRestrainedDataRows()
        {
            //DataRow row;
            //_dataBoundaryConditionsTable.Rows.Clear();
            //foreach (RestrainedNode restrain in _restrainedNodes)
            //{
            //    row = _dataBoundaryConditionsTable.NewRow();
            //    row[0] = restrain.NodeID;
            //    row[1] = restrain.Direction;
            //    _dataBoundaryConditionsTable.Rows.Add(row);
            //}
        }

        private void SetNodeTableColumns()
        {
            _dataNodeTable = new DataTable();
            _dataNodeTable.Columns.Add("NodeID", typeof(int));
            _dataNodeTable.Columns.Add("Xcoord", typeof(double)); ;
            _dataNodeTable.Columns.Add("Ycoord", typeof(double));
            gcNodes.DataSource = _dataNodeTable;
        }

        private void SetElementsTableColumns()
        {
            _dataTrussElementsTable = new DataTable();
            _dataTrussElementsTable.Columns.Add("Start NodeID", typeof(int));
            _dataTrussElementsTable.Columns.Add("End NodeID", typeof(int));
            _dataTrussElementsTable.Columns.Add("Length", typeof(double));
            _dataTrussElementsTable.Columns.Add("Angle", typeof(double));
            gcElements.DataSource = _dataTrussElementsTable;
        }
        private void SetBCTableColumns()
        {
            _dataBoundaryConditionsTable = new DataTable();
            _dataBoundaryConditionsTable.Columns.Add(" Node ID", typeof(int));
            _dataBoundaryConditionsTable.Columns.Add("Restrained Direction", typeof(eRestraintCondition));
            gcboundaryCondition.DataSource = _dataBoundaryConditionsTable;
        }

        private void SetLoadTableColumns()
        {
            _dataLoadTable = new DataTable();
            _dataLoadTable.Columns.Add(" Node ID", typeof(int));
            _dataLoadTable.Columns.Add("X Component", typeof(double));
            _dataLoadTable.Columns.Add("Y Component", typeof(double));
            gcLoads.DataSource = _dataLoadTable;
        }

        private void drawChart()
        {
            // Add elements to the series
            AddElementsToSeries();

            // Add nodes to the series
            AddNodesToSeries();

        }

        private void GenerateColorMap(double x1, double y1, double x2, double y2, double minvalue, double maxvalue, int numPoints = 300)
        {
            double[] x = Enumerable.Range(0, numPoints).Select(i => x1 + i * (x2 - x1) / (numPoints - 1.0)).ToArray();
            double[] y = Enumerable.Range(0, numPoints).Select(i => y1 + i * (y2 - y1) / (numPoints - 1.0)).ToArray();

            // Evaluate intensity for each point using interpolation
            double[] intensities = new double[numPoints];
            intensities[0] = minvalue;
            intensities[intensities.Length - 1] = maxvalue;

            for (int i = 1; i < intensities.Length - 1; i++)
            {
                double t = (double)i / (intensities.Length - 1);
                intensities[i] = (1 - t) * intensities[0] + t * intensities[intensities.Length - 1];
            }

            // Create a ColorMap object with the jet colormap and the range of intensities
            ColorMap colorMap = new ColorMap(intensities.Min(), intensities.Max());

            // Create a scatter chart series
            var series = new Series("Intensity", ViewType.Point);
            for (int i = 0; i < numPoints; i++)
            {
                var intensity = intensities[i];
                var pointColor = colorMap.GetColor(intensity);
                var seriesPoint = new SeriesPoint(x[i], y[i]) { Color = pointColor };
                series.ShowInLegend = false;
                series.Points.Add(seriesPoint);
            }
            chartDrawing.Series.Add(series);
        }

        private void AddNodesToSeries()
        {
            foreach (TrussNode nodes in _nodesList)
            {
                Series series1 = new Series("Nodes", ViewType.Point);
                PointSeriesView seriesView = (PointSeriesView)series1.View;
                if (nodes.XDirection == eRestraintCondition.free)
                {
                    seriesView.PointMarkerOptions.Kind = MarkerKind.Circle;
                    seriesView.Color = Color.Blue;
                    series1.Points.Add(new SeriesPoint(nodes.Xcoord, nodes.Ycoord));
                }
                else
                {
                    seriesView.Color = Color.Red;
                    seriesView.PointMarkerOptions.Kind = MarkerKind.Triangle;
                    series1.Points.Add(new SeriesPoint(nodes.Xcoord, nodes.Ycoord));

                    seriesView.PointMarkerOptions.Size = 15;
                }
                series1.ShowInLegend = false;
                chartDrawing.Series.Add(series1);
            }
        }

        private void AddElementsToSeries()
        {
            foreach (var element in _TrussElementsList)
            {
                Series series = new Series(element.Memberlabel, ViewType.Line);
                series.Points.Add(new SeriesPoint(element.NodeI.Xcoord, element.NodeI.Ycoord));
                series.Points.Add(new SeriesPoint(element.NodeJ.Xcoord, element.NodeJ.Ycoord));
                LineSeriesView lineView = (LineSeriesView)series.View;
                lineView.MarkerVisibility = DevExpress.Utils.DefaultBoolean.False;
                //lineView.LineMarkerOptions.Size = 1;
                //lineView.LineMarkerOptions.Kind = MarkerKind.Circle;
                lineView.LineStyle.Thickness = 1;
                lineView.LineMarkerOptions.BorderColor = Color.Black;
                lineView.LineMarkerOptions.BorderVisible = true;
                series.ShowInLegend = false;
                chartDrawing.Series.Add(series);
                series.View.Color = Color.LightGray;
            }
        }

        private void AdddisplacementToSeries()
        {
            var magnificationFactor = 150000000;
            foreach (var element in _TrussElementsList)
            {
                var NodeI = (TrussNode)element.NodeI;
                var NodeJ = (TrussNode)element.NodeJ;
                Series series = new Series(element.Memberlabel, ViewType.Line);
                series.Points.Add(new SeriesPoint(NodeI.getXcoordFinal(magnificationFactor), NodeI.getYcoordFinal(magnificationFactor)));
                series.Points.Add(new SeriesPoint(NodeJ.getXcoordFinal(magnificationFactor), NodeJ.getYcoordFinal(magnificationFactor)));
                LineSeriesView lineView = (LineSeriesView)series.View;
                lineView.MarkerVisibility = DevExpress.Utils.DefaultBoolean.False;
                //lineView.LineMarkerOptions.Size = 1;
                //lineView.LineMarkerOptions.Kind = MarkerKind.Circle;
                lineView.LineStyle.Thickness = 2;
                lineView.LineMarkerOptions.BorderColor = Color.Black;
                lineView.LineMarkerOptions.BorderVisible = true;
                series.ShowInLegend = false;
                chartDrawing.Series.Add(series);
                series.View.Color = Color.Cyan;
            }
        }

        #endregion

        #region Events

        private void BtnAddNode_Click(object sender, EventArgs e)
        {
            //var Xcoord = Convert.ToDouble(txtNodeX.Text);
            //var Ycoord = Convert.ToDouble(txtNodeY.Text);

            //_nodesList.Add(new NodesInfo(Xcoord, Ycoord, ++_nodeCount));
            AddNodesDataRows();
            drawChart();
            //CreateChart();
            //RefreshGraphics();
            //drawingControl.Refresh();
        }

        private void BtnAddLoad_Click(object sender, EventArgs e)
        {
            //int node = Convert.ToInt32(txtNodeIdLoading.Text);
            //double xComponent = Convert.ToDouble(txtXComponent.Text);
            //double YComponent = Convert.ToDouble(txtYComponent.Text);
            //_nodalForces.Add(new PointLoad(node, new Load(xComponent, YComponent)));
            AddLoadsDataRows();

        }

        private void canvasPictureBox_Paint(object sender, PaintEventArgs e)
        {
            // Define the min and max values and the number of pieces
            int minVal = 0;
            int maxVal = 100;
            int pieces = 10;

            // Define the width and height of the color bar
            int colorBarWidth = 30;
            int colorBarHeight = chartDrawing.Height;

            // Create a new LinearGradientBrush to paint the color bar with jet colors
            //Color[] colors = { Color.Blue, Color.Cyan, Color.Lime, Color.Yellow, Color.Orange, Color.Red };
            Color[] colors = { Color.Red, Color.Orange, Color.Yellow, Color.Lime, Color.Cyan, Color.Blue };
            float[] positions = { 0.0f, 0.2f, 0.4f, 0.6f, 0.8f, 1.0f };
            LinearGradientBrush brush = new LinearGradientBrush(
                new Point(colorBarWidth, 0), new Point(colorBarWidth, colorBarHeight),
                Color.Red, Color.Blue);
            ColorBlend blend = new ColorBlend();
            blend.Colors = colors;
            blend.Positions = positions;
            brush.InterpolationColors = blend;

            // Create a new Graphics object from the PictureBox's image
            Graphics g = e.Graphics;

            // Fill the rectangle for the color bar with the LinearGradientBrush
            g.FillRectangle(brush, 0, 0, colorBarWidth, colorBarHeight);

            // Draw the text for the minimum and maximum values on both sides of the color bar
            Font font = new Font("Arial", 8);
            Brush textBrush = new SolidBrush(Color.Black);
            for (int i = 1; i <= pieces - 1; i++)
            {
                int val = (int)Math.Round((double)i * maxVal / pieces);
                float y = colorBarHeight - (float)i * colorBarHeight / pieces - 6;
                g.DrawString(val.ToString(), font, textBrush, colorBarWidth + 4, y - 3);
                g.DrawLine(Pens.Black, colorBarWidth - 2, y + 4, colorBarWidth, y + 4);
                //g.DrawLine(Pens.Black, 0, y + 4, colorBarWidth - 2, y + 4);
            }
            g.DrawString(minVal.ToString(), font, textBrush, colorBarWidth + 4, colorBarHeight - 12);
            g.DrawString(maxVal.ToString(), font, textBrush, colorBarWidth + 4, 0);

        }

        #endregion


    }
    
}
