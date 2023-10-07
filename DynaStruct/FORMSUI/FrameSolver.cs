using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Solver;
using Solver.Frame;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace FORMSUI
{
    public partial class FrameSolver : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        #region Ctor

        public FrameSolver()
        {
            InitializeComponent();
            _solverHelper = new FrameSolverHelper();
            _solverHelper.CreateFrameExample1();
            setComboBoxItems();
            prepareUI();
            SubscribeToEvents();

            _drawingHelper = new DrawignHelper();
            // Calculate the grid spacings for X and Y directions
            float totalGridSpacing = 100000.0f; // Total grid spacing in centimeters
            gridSpacingX = totalGridSpacing / 10000; // Total grid spacing in X direction
            gridSpacingY = totalGridSpacing / 10000; // Total grid spacing in Y direction
            this.zoomFactor = CalculateInitialZoomFactor();
            //lblZoomSize.Text = string.Format("Zoom: {0,0:F4}", zoomFactor);


        }

        private void PrepareDataSource()
        {

            AddNodesDataRows();
            AddElementsDataRows();
            drawChart();
        }

        private void setComboBoxItems()
        {
            cmbResults.Items.Add(eResultToShow.Dispx);
            cmbResults.Items.Add(eResultToShow.Dispy);
            cmbResults.Items.Add(eResultToShow.Dispxy);
            cmbResults.Items.Add(eResultToShow.strain);
            cmbResults.Items.Add(eResultToShow.stress);
            TestsComboBox.Items.Add("Test 1");
            TestsComboBox.Items.Add("Test 2");
            TestsComboBox.Items.Add("Test 3");
            TestsComboBox.Items.Add("Test 4");
            TestsComboBox.Items.Add("Test 5");
        }

        #endregion

        #region Private Fields

        private DataTable _dataNodeTable;
        private FrameSolverHelper _solverHelper;
        private DataTable _dataTrussElementsTable;
        private string _strFormat = "#0.#";
        private Graphics _panelGraphic;
        //private List<RestrainedNode> _restrainedNodes;
        //private List<PointLoad> _nodalForces;

        private string _columnNameNodeId = "nodeId";
        private string _columnNameXcoord = "xCoord";
        private string _columnNameYcoord = "yCoord";
        private string _columnNameXRestaint = "xRestraint";
        private string _columnNameYRestaint = "yRestraint";
        private string _columnNameFx = "fx";
        private string _columnNameFy = "fy";
        private string _columnNameDispX = "Dispx";
        private string _columnNameDispY = "Dispy";
        private bool _disableXrestraint = false;
        private bool _disableYrestraint = false;

        private string _columnNameElementId = "ElementId";
        private string _columnNameElementNodeI = "ElementNodeI";
        private string _columnNameElementNodeJ = "ElementNodeJ";
        private string _columnNameElementModulus = "ElementModulus";
        private string _columnNameElementSectionArea = "ElementSecrionArea";
        private string _columnNameAxialLoad = "ElementAxialLoad";
        private string _columnNameElementSectionLength = "ElementSecrionLength";
        private string _columnNameElementSectionAngle = "ElementSecrionAngle";

        private double _minVal;
        private double _maxVal;
        private int _pieces;
        private int _scale;
        private bool _isAnalyzed;
        private eResultToShow _cmbResultsIndex;
        private bool _IscmbResultsIndexChanged;

        private RepositoryItemComboBox _combo = new RepositoryItemComboBox();
        private int _selectedRowHandle;
        private int _initialScale;


        private Vector _currentPosition;
        private Vector _firstPosition;
        private int _drawIndex = -1;
        private bool _activeDrawing = false;
        private int _clickNum = 1;
        private bool _enableExtendedLine = false;
        private DrawignHelper _drawingHelper;
        const int _gridSize = 100; // Adjust this value for the grid spacing
        //int _gridSize = 1; // grid size in Cm;
        //int gridspacingcm = 1; // adjust this value for the desired grid spacing in centimeters
        private float zoomFactor; // Initial zoom factor
        private float pixelsPerCentimeter = 37.795276f; // Typical DPI for most screens
        private float gridSpacingX; // Grid spacing in centimeters for the X direction
        private float gridSpacingY; // Grid spacing in centimeters for the Y direction
        private PointF translation = PointF.Empty; // Translation vector
                                                   //private float gridSpacing = 100000.0f; // Default grid spacing in centimeters 

        private float DrawingPanelHeightInCm
        {
            get { return Pixel_To_Cm(drawingPanel.Height); }
        }
        private float DrawingPanelWidthInCm
        {
            get { return Pixel_To_Cm(drawingPanel.Width); }
        }

        private void UpdateValues()
        {
            // Update the values based on user input
            //if (int.TryParse(columnsXTextBox.Text, out int newColumnsX) && newColumnsX > 0)
            //{
            //    columnsX = newColumnsX;
            //}

            //if (int.TryParse(rowsYTextBox.Text, out int newRowsY) && newRowsY > 0)
            //{
            //    rowsY = newRowsY;
            //}


            // Redraw the panel when values change to adjust the grid
            drawingPanel.Invalidate();
        }


        private float CalculateInitialZoomFactor()
        {
            // Calculate the required zoom factor to fit the grid within the panel
            float panelWidthInCm = drawingPanel.Width / pixelsPerCentimeter;
            float panelHeightInCm = drawingPanel.Height / pixelsPerCentimeter;

            float zoomFactorX = panelWidthInCm / (gridSpacingX);
            float zoomFactorY = panelHeightInCm / (gridSpacingY);

            return Math.Min(zoomFactorX, zoomFactorY);
        }
        private void DrawGrid()
        {

            #region Drawing Grid in Pixel with grid size of 100;
            //using (_panelGraphic = drawingPanel.CreateGraphics())
            //{

            //    Pen gridpen = new Pen(Color.White, 1); // pen for grid lines
            //    drawingPanel.BackColor = Color.FromArgb(229, 236, 246);
            //    // draw the horizontal lines
            //    for (int y = drawingPanel.Height; y > 0; y -= _gridSize)
            //    {
            //        _panelGraphic.DrawLine(gridpen, 0, y, drawingPanel.Width, y);
            //    }

            //    //draw the vertical lines
            //    for (int x = 0; x < drawingPanel.Width; x += _gridSize)
            //    {
            //        _panelGraphic.DrawLine(gridpen, x, 0, x, drawingPanel.Height);
            //    }

            //    // dispose of the pen object to release resources
            //    gridpen.Dispose();
            //}

            #endregion


            #region Drawing Grid in cm with a dynamic size

            //using (_panelGraphic = drawingPanel.CreateGraphics())
            //{

            //    //// Calculate the actual grid spacing in centimeters
            //    //float actualGridSpacingX = drawingPanel.Width / (float)columnsX / pixelsPerCentimeter;
            //    //float actualGridSpacingY = drawingPanel.Height / (float)rowsY / pixelsPerCentimeter;

            //    // Calculate the actual grid spacing in pixels for X and Y directions
            //    float actualGridSpacingXInPixels = gridSpacingX * pixelsPerCentimeter;
            //    float actualGridSpacingYInPixels = gridSpacingY * pixelsPerCentimeter;

            //    Pen gridPen = new Pen(Color.Gray, 1);

            //    // Calculate the width and height of each grid cell in pixels
            //    int cellWidth = (int)(actualGridSpacingXInPixels);
            //    int cellHeight = (int)(actualGridSpacingYInPixels);

            //    // Calculate the scaled grid spacing based on the zoom factor
            //    float scaledGridSpacingX = actualGridSpacingXInPixels * zoomFactor;
            //    float scaledGridSpacingY = actualGridSpacingYInPixels * zoomFactor;

            //    // Calculate the number of columns and rows based on the panel size
            //    int numColumns = (int)(drawingPanel.Width / scaledGridSpacingX);
            //    int numRows = (int)(drawingPanel.Height / scaledGridSpacingY);

            //    // Calculate the adjusted width and height of each grid cell
            //    int adjustedCellWidth = drawingPanel.Width / numColumns;
            //    int adjustedCellHeight = drawingPanel.Height / numRows;



            //    for (int column = 0; column <= columnsX; column++)
            //    {
            //        int x = column * adjustedCellWidth;
            //        _panelGraphic.DrawLine(gridPen, x, 0, x, drawingPanel.Height);
            //    }

            //    // Draw the grid in the Y direction
            //    for (int row = 0; row <= rowsY; row++)
            //    {
            //        int y = row * adjustedCellHeight;
            //        _panelGraphic.DrawLine(gridPen, 0, y, drawingPanel.Width, y);
            //    }
            //    gridPen.Dispose();
            //}

            #endregion

            #region AutoCad

            // Calculate the actual grid spacing in pixels based on zoom factor
            float actualGridSpacingX = gridSpacingX * 10.0f * zoomFactor;
            float actualGridSpacingY = gridSpacingY * 10.0f * zoomFactor;

            //// Apply the scale and translation transforms
            //g.ScaleTransform(zoomFactor, zoomFactor);
            //g.TranslateTransform(translation.X, translation.Y);

            using (_panelGraphic = drawingPanel.CreateGraphics())
            {
                // Draw the grid
                Pen gridPen = new Pen(Color.Gray, 1);

                for (float x = 0; x < drawingPanel.Width; x += actualGridSpacingX)
                {
                    _panelGraphic.DrawLine(gridPen, x, 0, x, drawingPanel.Height);
                }

                for (float y = 0; y < drawingPanel.Height; y += actualGridSpacingY)
                {
                    _panelGraphic.DrawLine(gridPen, 0, y, drawingPanel.Width, y);
                }

                gridPen.Dispose();


                // Draw lines from the origin
                Pen axisPen = new Pen(Color.Black, 1.5f);

                _panelGraphic.DrawLine(axisPen, translation.X, 0, translation.X, drawingPanel.Height); // Vertical line from X origin
                _panelGraphic.DrawLine(axisPen, 0, translation.Y, drawingPanel.Width, translation.Y); // Horizontal line from Y origin

                //// Draw origin as a colored point
                //float xOrigin = (translation.X + translation.X) * 10.0f;
                //float yOrigin = (translation.Y + translation.Y) * 10.0f;
                //_panelGraphic.FillEllipse(new SolidBrush(Color.Blue), xOrigin - 2, drawingPanel.Height - (yOrigin - 2), 4, 4); // Adjust for point size and flip Y coordinate


            }
            #endregion




        }

        private void DrawingPanel_MouseMove(object sender, MouseEventArgs e)
        {
            //var originalXCoordinate = e.Location.X;
            //var originalYCoordinate = e.Location.Y;

            //PointF p = new PointF(originalXCoordinate - _gridSize, drawingPanel.Height - originalYCoordinate - _gridSize);
            //_currentPosition = new Vector(ConvertCoordinate(e.Location.X - _gridSize), ConvertCoordinate(e.Location.Y, true));


            // X and Y coordinates in Pixel
            // lblX.Text = string.Format("X: {0,0:f2}, Y: {1,0:f2}", originalXCoordinate, drawingPanel.Height -  originalYCoordinate);

            //X and Y Coordinate in cm  
            //lblX.Text = string.Format("X: {0,0:f2}, Y: {1,0:f2}", Pixel_To_Cm(originalXCoordinate), DrawingPanelHeightInCm - Pixel_To_Cm(originalYCoordinate));

            //float xCm = (e.X - panOffset.X) / (pixelsPerCentimeter * zoomFactor);
            //float yCm = drawingPanel.Height -  ((e.Y - panOffset.Y) / (pixelsPerCentimeter * zoomFactor));

            // Calculate the x and y coordinates in centimeters
            //float xCm = e.X / (pixelsPerCentimeter * zoomFactor);
            //float yCm = (drawingPanel.Height - e.Y) / (pixelsPerCentimeter * zoomFactor);

            // Calculate the x and y coordinates in centimeters
            //float xCm = e.X / (10.0f * zoomFactor); // 1 cm = 10 mm
            //float yCm = (drawingPanel.Height - e.Y) / (10.0f * zoomFactor); // 1 cm = 10 mm

            #region Calculate the x and y coordinate in centimeters

            // Calculate the x and y coordinates in centimeters
            float xCm = (e.X / (10.0f * zoomFactor)) - (translation.X / (10.0f * zoomFactor));
            float yCm = ((drawingPanel.Height - e.Y) / (10.0f * zoomFactor)) - (translation.Y / (10.0f * zoomFactor));

            // Calculate the clicked point coordinates in centimeters
            float xCm1 = (e.X / (10.0f * zoomFactor)) - (translation.X / (10.0f * zoomFactor));
            float yCm1 = (e.Y / (10.0f * zoomFactor)) - (translation.Y / (10.0f * zoomFactor));
            #endregion



            // Current Clicked Coordinate
            _currentPosition = new Vector(xCm1, yCm1);

            //lblZoomSize.Text = _currentPosition.X.ToString() + "   " + _currentPosition.Y.ToString();

            //lblX.Text = $"Mouse Coordinates (cm): X = {xCm:F2}, Y = {yCm:F2}";

            if (_enableExtendedLine)
            {
                drawingPanel.Refresh();
            }

        }
        #endregion

        #region Private Methods

        private void SubscribeToEvents()
        {
            BarBtnSolve.ItemClick += BtnAnalyze_Click;
            btnAddNode.Click += BtnAddNode_Click;
            btnAddElement.Click += BtnAddElement_Click;
            gvNodes.CellValueChanged += GvNodes_CellValueChanged;
            gvNodes.FocusedColumnChanged += GvNodes_FocusedColumnChanged;
            gvNodes.RowCellStyle += GvNodes_RowCellStyle;
            _combo.SelectedValueChanged += _combo_SelectedValueChanged;
            pictureBox1.Paint += canvasPictureBox_Paint;
            cmbResults.SelectedIndexChanged += cmbResults_SelectedIndexChanged;
            this.Resize += TrussSolver_Resize;
            scaleTrackbar.ValueChanged += scaleTrackbar_ValueChanged;
            TestsComboBox.SelectedIndexChanged += TestsComboBox_SelectedIndexChanged;

            //btnDrawLine.Click += BtnDrawLine_Click;
            //drawingPanel.Resize += DrawingPanel_Resize;
            //drawingPanel.Paint += DrawingPanel_Paint;
            //btnDeleteLine.Click += BtnDeleteLines_Click;
            //btnDrawDiagram.Click += BtnDrawDiagram_Click;
            //drawingPanel.MouseMove += DrawingPanel_MouseMove;
            //drawingPanel.MouseDown += DrawingPanel_MouseDown;
            //btnDrawWithMouse.Click += BtnDrawWithMouse_Click;
            //btnDrawLineWithMouse.Click += BtnDrawLineWithMouse_Click;
            //btnDisableExtendedLine.Click += BtnDisableExtendedLine_Click;
            //btnInsertGrid.Click += BtnInsertGrid_Click;
            //drawingPanel.MouseWheel += DrawingPanel_MouseWheel;
            //btnZoomIn.Click += BtnZoomIn_Click;
            //btnZoomOut.Click += BtnZoomOut_Click;
            //btnResetZoom.Click += BtnResetZoom_Click;
            //btnSubmitGrid.Click += BtnSubmitGrid_Click;
        }

        private void BtnSubmitGrid_Click(object sender, EventArgs e)
        {
            UpdateValues();
        }

        private void BtnResetZoom_Click(object sender, EventArgs e)
        {
            // Reset Zoom

            zoomFactor = 1.0f; // Increase zoom factor by 10%
            // Ensure that zoom factor stays within reasonable limits
            drawingPanel.Invalidate();
            //lblZoomSize.Text = string.Format("Zoom: {0,0:F4}", zoomFactor);

        }


        private void BtnZoomOut_Click(object sender, EventArgs e)
        {
            // Zoom out


            zoomFactor /= 1.1f; // Decrease zoom factor by 10%

            // Ensure that zoom factor stays within reasonable limits

            //lblZoomSize.Text = string.Format("Zoom: {0,0:F4}", zoomFactor);

            drawingPanel.Invalidate();
        }

        private void BtnZoomIn_Click(object sender, EventArgs e)
        {
            // Zoom in
            zoomFactor *= 1.1f; // Increase zoom factor by 10%

            // Ensure that zoom factor stays within reasonable limits

            drawingPanel.Invalidate();
            //lblZoomSize.Text = string.Format("Zoom: {0,0:F4}", zoomFactor);


        }

        private void DrawingPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            // Zoom in or out based on the mouse wheel direction
            if (e.Delta > 0)
            {
                // Zoom in
                zoomFactor *= 1.1f; // Increase zoom factor by 10%
            }
            else
            {
                // Zoom out
                zoomFactor /= 1.1f; // Decrease zoom factor by 10%
            }

            //lblZoomSize.Text = string.Format("Zoom: {0,0:F4}", zoomFactor);

            // Redraw the PictureBox with the updated zoom
            drawingPanel.Invalidate();

        }

        private float Pixel_To_Cm(float pixel)
        {
            return pixel * 25.4f * 0.1f ;
        }

        private float ConvertCoordinate(float value, bool isY = false)
        {

            return isY ? drawingPanel.Height - value - _gridSize : value + _gridSize;

            //return isY ? drawingPanel.Height - value  : value ; 



        }

        private void DrawMagnitude(float x1, float y1, float magnitude1, float x2, float y2, float magnitude2)
        {

            x1 = ConvertCoordinate(x1);
            x2 = ConvertCoordinate(x2);
            y1 = ConvertCoordinate(y1, true);
            y2 = ConvertCoordinate(y2, true);

            var points = new List<PointF>();


            var maxMagnitude = Math.Max(magnitude1, magnitude2);
            var minMagnitude = Math.Min(magnitude1, magnitude2);

            float length = (float)Math.Sqrt(Math.Pow(y2 - y1, 2) + Math.Pow(x2 - x1, 2));

            var maxLength = length * 0.2f;
            var x3 = x2 - magnitude2;
            var y3 = y2;

            var x4 = x1 - magnitude1;
            var y4 = y1;

            //if (x1 > x4)
            //{
            //    x1 = x1 - 1.5f;
            //    x2 = x2 - 1.5f;
            //}
            //else
            //{
            //    x1 = x1 + 1.5f;
            //    x2 = x2 + 1.5f;
            //}

            points.Add(new PointF(x1, y1));
            points.Add(new PointF(x2, y2));
            points.Add(new PointF(x3, y3));
            points.Add(new PointF(x4, y4));

            DrawPolygon(points.ToArray());


            //using (_panelGraphic = drawingPanel.CreateGraphics())
            //{

            //    // Define the points for the polygon

            //    PointF[] polygonPoints = new PointF[]
            //{
            //    new PointF(ConvertCoordinate( 0),ConvertCoordinate(0  ,true)),
            //    new PointF(ConvertCoordinate(0), ConvertCoordinate(100,true)),
            //    new PointF(ConvertCoordinate(50),ConvertCoordinate(100,true)),
            //    new PointF(ConvertCoordinate(-50), ConvertCoordinate(0  ,true))
            //};

            //    // Create a Pen for the polygon outline
            //    Pen outlinePen = new Pen(Color.Blue, 2);
            //    Brush fillBrush = new SolidBrush(Color.Blue);

            //    //Draw the polygon
            //    _panelGraphic.FillPolygon(fillBrush, polygonPoints);

            //    // Dispose of the Pen object to release resources
            //    outlinePen.Dispose();
            //}

            //using (_panelGraphic = drawingPanel.CreateGraphics())
            //{
            //    Pen gridPen = new Pen(Color.Black, 3);
            //    var finalX1 = x1;
            //    var finalY1 = drawingPanel.Height - y1;
            //    var finalX2 = x1;
            //    var finalY2 = drawingPanel.Height - y1 - maxLength;

            //    //var finalMagnitude = (magnitude * 25) / 100;


            //    _panelGraphic.DrawLine(gridPen, finalX1, finalY1, finalX2, finalY2);
            //    _listOfPointsForMagnitude.Add(new PointF(finalX2, finalY2));
            //    gridPen.Dispose();
            //}

        }

        private void DrawPolygon(PointF[] polygonPoints)
        {
            using (_panelGraphic = drawingPanel.CreateGraphics())
            {
                // Create a Pen for the polygon outline
                Pen outlinePen = new Pen(Color.Blue, 2);
                Brush fillBrush = new SolidBrush(Color.LightBlue);

                //Draw the polygon
                _panelGraphic.FillPolygon(fillBrush, polygonPoints);

                // Dispose of the Pen object to release resources
                outlinePen.Dispose();
            }
        }

        private void DrawLine(float x1, float y1, float x2, float y2)
        {
            using (_panelGraphic = drawingPanel.CreateGraphics())
            {
                Pen Pen = new Pen(Color.Blue, 3);

                var finalX1 = x1 + _gridSize;
                var finalY1 = drawingPanel.Height - y1 - _gridSize;
                var finalX2 = x2 + _gridSize;
                var finalY2 = drawingPanel.Height - y2 - _gridSize;

                _panelGraphic.DrawLine(Pen, finalX1, finalY1, finalX2, finalY2);

                //if (_drawingHelper.ListOfPoints.Count == 0)
                //{
                //    _panelGraphic.DrawLine(Pen, finalX1, finalY1, finalX2, finalY2);
                //}
                //else
                //{
                //    //var count = _drawingHelper.ListOfPoints.Count;
                //    //PointF Point = _drawingHelper.ListOfPoints[_drawingHelper.ListOfPoints.Last().];
                //    finalX1 = _drawingHelper.ListOfPoints.Last().X + _gridSize;
                //    finalY1 = drawingPanel.Height - _drawingHelper.ListOfPoints.Last().Y - _gridSize;
                //    finalX2 = x1 + _gridSize;
                //    finalY2 = drawingPanel.Height - y1 - _gridSize;

                //    _panelGraphic.DrawLine(Pen, finalX1, finalY1, finalX2, finalY2);
                //    //_listOfCoordinatesForFillBrush.Add()
                //}


                //DrawCircle(finalX1, finalY1);
                //DrawCircle(finalX2, finalY2);

                _drawingHelper.ListOfPoints.Add(new PointF(x1, y1));


                ClearInputs();

                Pen.Dispose();
            }
        }

        private void ClearInputs()
        {
            //txtX1.Clear();
            //txtX2.Clear();
            //txtMagnitude.Clear();
            //txtY1.Text = "";
            //txtY2.Text = "";
        }

        private void DrawCircle(float x, float y)
        {
            using (_panelGraphic = drawingPanel.CreateGraphics())
            {

                int diameter = 4; // Diameter of the smaller rounded circle
                int cornerRadius = 2; // Radius of the rounded corners

                // Create a Brush for filling the smaller rounded circle (e.g., solid green)
                Brush fillBrush = new SolidBrush(Color.Black);

                // Create a rounded rectangle for the smaller rounded circle
                RectangleF roundedRectangle = new RectangleF(x - 3, y - 3, diameter, diameter);
                GraphicsPath roundedPath = GetRoundedRectangle(roundedRectangle, cornerRadius);

                // Fill the smaller rounded circle
                _panelGraphic.FillPath(fillBrush, roundedPath);

                // Dispose of the Brush and GraphicsPath objects to release resources
                fillBrush.Dispose();
                roundedPath.Dispose();


            }

        }

        // Helper method to create a rounded rectangle
        private GraphicsPath GetRoundedRectangle(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90); // Top-left corner
            path.AddArc(rect.Right - 2 * radius, rect.Y, radius * 2, radius * 2, 270, 90); // Top-right corner
            path.AddArc(rect.Right - 2 * radius, rect.Bottom - 2 * radius, radius * 2, radius * 2, 0, 90); // Bottom-right corner
            path.AddArc(rect.X, rect.Bottom - 2 * radius, radius * 2, radius * 2, 90, 90); // Bottom-left corner
            path.CloseFigure(); // Close the path

            return path;
        }




        private void FitGridInPanel()
        {
            // Calculate the zoom factor to fit the grid within the fixed Panel size
            //float horizontalZoom = drawingPanel.Width / (columnsX * gridSpacingX * pixelsPerCentimeter);
            //float verticalZoom = drawingPanel.Height / (rowsY * gridSpacingY * pixelsPerCentimeter);

            // Update the zoom factor of the Panel
            drawingPanel.AutoScroll = true; // Enable scrolling if the content exceeds the Panel size
            drawingPanel.SuspendLayout(); // Suspend layout updates while setting zoom
            //drawingPanel.Scale(new SizeF(horizontalZoom, verticalZoom)); // Apply the zoom factor
            drawingPanel.ResumeLayout(); // Resume layout updates

            // Force the Panel to redraw
            drawingPanel.Invalidate();
        }
        private void TestsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _solverHelper.ClearNodeAndElements();

            prepareUI();
        }

        private void scaleTrackbar_ValueChanged(object sender, EventArgs e)
        {
            _scale = _initialScale;
            var zoomTrackBar = (TrackBarControl)sender;

            var value = (int)zoomTrackBar.EditValue * 10000000;

            _scale += (int)(value);
            updateColorBarValues();
        }

        private void TrussSolver_Resize(object sender, EventArgs e)
        {
            AddNodesDataRows();
            AddElementsDataRows();
            updateColorBarValues();
        }

        private void BtnAddElement_Click(object sender, EventArgs e)
        {
            int memberLabel = Convert.ToInt32(txtMemberLabel.EditValue);
            int nodeI = Convert.ToInt32(txtNodeI.EditValue);
            int nodeJ = Convert.ToInt32(txtNodeJ.EditValue);
            double E = Convert.ToDouble(txtModulusOfElasticity.EditValue);
            double A = Convert.ToDouble(txtSectionArea.EditValue);
            double I = Convert.ToDouble(txtMomentOfInertia.EditValue);
            _solverHelper.AddFrameMember(memberLabel, nodeI, nodeJ, A, E, I);
            AddElementsDataRows();
            drawChart();
        }

        private void BarBtnTrussSolver_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var form = new TrussSolver();
            form.Show();
        }

        private void ChartDrawing_CustomDrawCrosshair(object sender, CustomDrawCrosshairEventArgs e)
        {
            foreach (CrosshairElement element in e.CrosshairElements)
            {
                //if (element.SeriesPoint != null)
                //{
                //    // get the value you want to display in the tooltip
                //    string customTooltip = $"Node : {_nodesList.FirstOrDefault(x=>x.Xcoord == element.SeriesPoint.NumericalArgument && x.Ycoord== element.SeriesPoint.Values[0]).ID}";
                //    // set the tooltip text
                //    element.LabelElement.Text = customTooltip;
                //}
            }
        }
        private void ChartDrawing_MouseMove(object sender, MouseEventArgs e)
        {
            //ChartHitInfo hitInfo = chartDrawing.CalcHitInfo(e.X, e.Y);
            //if (hitInfo.SeriesPoint != null)
            //{
            //    chartDrawing.CrosshairEnabled = DefaultBoolean.True;
            //    chartDrawing.CrosshairOptions.ShowOnlyInFocusedPane = true;
            //    chartDrawing.CrosshairOptions.ShowArgumentLine = false;
            //    chartDrawing.CrosshairOptions.ShowValueLine = false;
            //    chartDrawing.CrosshairOptions.ShowValueLabels = true;
            //    chartDrawing.CrosshairOptions.ShowArgumentLabels = false;
            //    chartDrawing.Series[0].CrosshairLabelPattern = "Custom Text: {V:F2}";
            //    chartDrawing.CrosshairOptions.GroupHeaderPattern = "Custom Group Header";
            //    chartDrawing.CrosshairOptions.HighlightPoints=default;
            //}
            //else
            //{
            //    chartDrawing.CrosshairEnabled = false;
            //}

        }

        private void GvNodes_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            //if (e.PrevFocusedColumn.FieldName == _columnNameXRestaint)
            //{
            //    if (_disableXrestraint)
            //    {
            //        e.RepositoryItem = new RepositoryItemTextEdit();
            //        e.RepositoryItem.ReadOnly = true;
            //    }
            //    else
            //    {
            //        e.RepositoryItem = new RepositoryItemTextEdit();
            //        e.RepositoryItem.ReadOnly = false;
            //    }
            //}

            //if (e.Column.FieldName == _columnNameFy)
            //{
            //    if (_disableYrestraint)
            //    {
            //        e.RepositoryItem = new RepositoryItemTextEdit();
            //        e.RepositoryItem.ReadOnly = true;
            //    }
            //    else
            //    {
            //        e.RepositoryItem = new RepositoryItemTextEdit();
            //        e.RepositoryItem.ReadOnly = false;
            //    }

            //}
        }

        private void _combo_SelectedValueChanged(object sender, EventArgs e)
        {
            string selectedValue = ((ComboBoxEdit)sender).EditValue?.ToString();
            if (!string.IsNullOrEmpty(selectedValue))
            {
                eRestraint myEnumValue = (eRestraint)Enum.Parse(typeof(eRestraint), selectedValue);
                var node = _solverHelper.GetNodeById(gvNodes.FocusedRowHandle);
                if (gvNodes.FocusedColumn.FieldName == _columnNameXRestaint)
                {
                    node.XRestraint = myEnumValue;
                    if (myEnumValue == eRestraint.Free)
                    {
                        gvNodes.SetRowCellValue(gvNodes.FocusedRowHandle, _columnNameFx, node.Fx);
                    }
                    else
                    {
                        gvNodes.SetRowCellValue(gvNodes.FocusedRowHandle, _columnNameFx, "?");
                        _disableXrestraint = true;
                    }
                }
                if (gvNodes.FocusedColumn.FieldName == _columnNameYRestaint)
                {
                    node.YRestraint = myEnumValue;
                    if (myEnumValue == eRestraint.Free)
                    {
                        gvNodes.SetRowCellValue(gvNodes.FocusedRowHandle, _columnNameFy, node.Fy);
                    }
                    else
                    {
                        gvNodes.SetRowCellValue(gvNodes.FocusedRowHandle, _columnNameFy, "?");
                        _disableYrestraint = true;
                    }
                }

            }

        }

        private void GvNodes_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (_disableXrestraint && e.Column.FieldName == _columnNameFx)
            {
                //e.Appearance.BackColor = Color.LightGray;
            }
            else
            {
                //e.Appearance.ba;
            }
            if (_disableYrestraint && e.Column.FieldName == _columnNameFy)
            {
                //e.Appearance.BackColor = Color.LightGray;
            }
        }

        private void GvNodes_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            _selectedRowHandle = e.RowHandle;
            if (_selectedRowHandle != GridControl.InvalidRowHandle)
            {
                var node = _solverHelper.GetNodeById(_selectedRowHandle) as TrussNode;
                node.Ycoord = (double)gvNodes.GetRowCellValue(_selectedRowHandle, _columnNameYcoord);
                node.Xcoord = (double)gvNodes.GetRowCellValue(_selectedRowHandle, _columnNameXcoord);
                //node.XRestraint = (eRestraint)Enum.Parse(typeof(eRestraint), (string)gvNodes.GetRowCellValue(_selectedRowHandle, _columnNameXRestaint));
                //node.YRestraint = (eRestraint)Enum.Parse(typeof(eRestraint), (string)gvNodes.GetRowCellValue(_selectedRowHandle, _columnNameYRestaint));
                if (node.XRestraint == eRestraint.Free)
                {
                    _disableXrestraint = false;
                    node.Fx = Convert.ToDouble(gvNodes.GetRowCellValue(_selectedRowHandle, _columnNameFx));
                }
                else
                {
                    _disableXrestraint = true;
                }

                //node.YRestraint = (eRestraint)Enum.Parse(typeof(eRestraint), (string)gvNodes.GetRowCellValue(_selectedRowHandle, _columnNameYRestaint));
                if (node.YRestraint == eRestraint.Free)
                {
                    _disableYrestraint = false;
                    node.Fy = Convert.ToDouble(gvNodes.GetRowCellValue(_selectedRowHandle, _columnNameFy));
                }
                else
                {
                    _disableYrestraint = true;
                }
            }

            PrepareDataSource();
        }

        private void cmbResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            _IscmbResultsIndexChanged = true;
            _cmbResultsIndex = (eResultToShow)((ComboBoxEdit)sender).SelectedIndex;
            updateColorBarValues();
        }

        private void BtnAnalyze_Click(object sender, System.EventArgs e)
        {
            if (_solverHelper.AnalyzeModel())
            {
                updateColorBarValues();
                AddNodesDataRows();
                AddElementsDataRows();
            }

        }
        private void prepareUI()
        {

            SetNodeTableColumns();
            SetElementsTableColumns();
            SetBCTableColumns();

            txtNodeIDForNodes.EditValue = 0;
            PrepareDataSource();
            scaleTrackbar.Minimum = 0;
            scaleTrackbar.Maximum = 10;
            _scale = _initialScale;
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


        private void AddDisplacementColorMapXDirection(eResultToShow type, int magnificationFactor = 100000000, int numPoints = 300)
        {

        }

        private void GetCoordinates(eResultToShow type, int magnificationFactor, int numPoints, out List<double> xcoordList, out List<double> ycoordList)
        {
            xcoordList = new List<double>();
            ycoordList = new List<double>();
            foreach (var element in _solverHelper.ElementList)
            {
                //var element = _TrussElementsList.First();
                var NodeI = element.Nodes[0];
                var NodeJ = element.Nodes[1];
                double x1 = NodeI.Xcoord;
                double y1 = NodeI.Ycoord;
                double x2 = NodeJ.Xcoord;
                double y2 = NodeJ.Ycoord;
                if (type == eResultToShow.Dispx || type == eResultToShow.Dispxy)
                {
                    x1 = NodeI.getXcoordFinal(magnificationFactor);
                    x2 = NodeJ.getXcoordFinal(magnificationFactor);
                }

                if (type == eResultToShow.Dispy || type == eResultToShow.Dispxy)
                {
                    y1 = NodeI.getYcoordFinal(magnificationFactor);
                    y2 = NodeJ.getYcoordFinal(magnificationFactor);
                }

                xcoordList.AddRange(Enumerable.Range(0, numPoints).Select(i => x1 + i * (x2 - x1) / (numPoints - 1.0)));
                ycoordList.AddRange(Enumerable.Range(0, numPoints).Select(i => y1 + i * (y2 - y1) / (numPoints - 1.0)));
            }
        }

        private static void SetColorMap(List<double> xcoordList, List<double> ycoordList, Series series, PointSeriesView seriesView, List<double> intensities, double minValue, double maxValue)
        {
            ColorMap colorMap = new ColorMap(minValue, maxValue);
            for (int i = 0; i < intensities.Count; i++)
            {
                var pointColor = colorMap.GetColor(intensities[i]);
                var seriesPoint = new SeriesPoint(xcoordList[i], ycoordList[i]) { Color = pointColor };
                series.ShowInLegend = false;
                series.Points.Add(seriesPoint);
            }
            seriesView.PointMarkerOptions.Kind = MarkerKind.Circle;
            seriesView.PointMarkerOptions.Size = 4;
        }


        private List<double> GetIntesities(int numPoints, eResultToShow type)
        {
            var intensities = new List<double>();

            foreach (var element in _solverHelper.ElementList)
            {
                var NodeI = element.Nodes[0];
                var NodeJ = element.Nodes[1];
                SetStartAndEndValues(type, NodeI, NodeJ, out double start, out double End);
                intensities.AddRange(Enumerable.Range(0, numPoints).Select(i => Math.Abs(start + i * (End - start) / (numPoints - 1.0))));
            }

            return intensities;
        }

        private static void SetStartAndEndValues(eResultToShow type, ANode NodeI, ANode NodeJ, out double start, out double End)
        {
            switch (type)
            {
                case eResultToShow.Dispx:
                    start = NodeI.Dispx;
                    End = NodeJ.Dispx;
                    break;
                case eResultToShow.Dispy:
                    start = NodeI.Dispy;
                    End = NodeJ.Dispy;
                    break;
                case eResultToShow.Dispxy:
                    start = NodeI.Dispxy;
                    End = NodeJ.Dispxy;
                    break;
                case eResultToShow.strain:
                    start = NodeI.Dispx;
                    End = NodeJ.Dispx;
                    break;
                default:
                    start = NodeI.Dispx;
                    End = NodeJ.Dispx;
                    break;
            }
        }

        private void BtnAddRestrain_Click(object sender, EventArgs e)
        {
            //eRestraintCondition restraniedType = (eRestraintCondition)cmbSupportType.SelectedIndex;
            //int Id = Convert.ToInt32(txtBCNodeId.Text);
            //_restrainedNodes.Add(new RestrainedNode(Id, restraniedType));
            AddRestrainedDataRows();
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
            gvNodes.Columns[_columnNameNodeId].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            gvNodes.Columns[_columnNameNodeId].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            for (int i = 1; i < gvNodes.Columns.Count; i++)
            {
                gvNodes.Columns[i].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gvNodes.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }
            gvNodes.BestFitColumns();
            _combo.Items.Clear();
            _combo.Items.AddRange(Enum.GetValues(typeof(eRestraint)).Cast<eRestraint>().Select(e => e.ToString()).ToArray());
            gvNodes.Columns[_columnNameXRestaint].ColumnEdit = _combo;
            gvNodes.Columns[_columnNameYRestaint].ColumnEdit = _combo;
        }

        private void EditElementsTableGridView()
        {
            gvElements.OptionsView.ShowGroupPanel = false;
            gvElements.OptionsCustomization.AllowColumnMoving = false;
            gvElements.OptionsCustomization.AllowFilter = false;
            gvElements.OptionsMenu.EnableColumnMenu = false;
            gvElements.OptionsView.ShowIndicator = false;
            gvElements.OptionsView.AllowHtmlDrawHeaders = true;
            foreach (GridColumn column in gvElements.Columns)
            {
                column.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }
            gvElements.Columns[0].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gvElements.Columns[1].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gvElements.Columns[2].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gvElements.Columns[0].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gvElements.Columns[1].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gvElements.Columns[2].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
        }

        private void EditBCTableGridView()
        {
            //gvBoundaryCondition.OptionsView.ShowGroupPanel = false;
            //gvBoundaryCondition.OptionsCustomization.AllowColumnMoving = false;
            //gvBoundaryCondition.OptionsCustomization.AllowFilter = false;
            //gvBoundaryCondition.OptionsMenu.EnableColumnMenu = false;
            //gvBoundaryCondition.OptionsView.ShowIndicator = false;
            //gvBoundaryCondition.OptionsView.AllowHtmlDrawHeaders = true;
            //gvBoundaryCondition.OptionsView.ColumnAutoWidth = true;
            //gvBoundaryCondition.Columns[_columnNameNodeId].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            //gvBoundaryCondition.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //gvBoundaryCondition.Columns[0].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            //gvBoundaryCondition.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
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
            _dataNodeTable.Columns.Add(new DataColumn() { ColumnName = _columnNameNodeId, DataType = typeof(int), Caption = "Node ID" });
            _dataNodeTable.Columns.Add(new DataColumn() { ColumnName = _columnNameXcoord, DataType = typeof(double), Caption = "X-Coord" });
            _dataNodeTable.Columns.Add(new DataColumn() { ColumnName = _columnNameYcoord, DataType = typeof(double), Caption = "Y-Coord" });
            _dataNodeTable.Columns.Add(new DataColumn() { ColumnName = _columnNameXRestaint, DataType = typeof(string), Caption = "X-Restraint" });
            _dataNodeTable.Columns.Add(new DataColumn() { ColumnName = _columnNameYRestaint, DataType = typeof(string), Caption = "Y-Restraint" });
            _dataNodeTable.Columns.Add(new DataColumn() { ColumnName = _columnNameFx, DataType = typeof(string), Caption = "Fx (N)" });
            _dataNodeTable.Columns.Add(new DataColumn() { ColumnName = _columnNameFy, DataType = typeof(string), Caption = "Fy (N)" });
            _dataNodeTable.Columns.Add(new DataColumn() { ColumnName = _columnNameDispX, DataType = typeof(string), Caption = "Disp-X (mm)" });
            _dataNodeTable.Columns.Add(new DataColumn() { ColumnName = _columnNameDispY, DataType = typeof(string), Caption = "Disp-Y (mm)" });
            gcNodes.DataSource = _dataNodeTable;
        }
        private void AddNodesDataRows()
        {
            DataRow row;
            _dataNodeTable.Rows.Clear();
            foreach (var node in _solverHelper.NodeList)
            {
                row = _dataNodeTable.NewRow();
                row[_columnNameNodeId] = node.ID;
                row[_columnNameXcoord] = node.Xcoord;
                row[_columnNameYcoord] = node.Ycoord;
                row[_columnNameXRestaint] = node.XRestraint;
                row[_columnNameYRestaint] = node.YRestraint;
                if (node.XRestraint == eRestraint.Restrained && !_isAnalyzed)//TODO make it inline if
                {
                    row[_columnNameFx] = "?";
                }
                else
                {
                    row[_columnNameFx] = node.Fx;
                }

                if (node.YRestraint == eRestraint.Restrained)//TODO make it inline if
                {
                    row[_columnNameFy] = "?";
                }
                else
                {
                    row[_columnNameFy] = node.Fy;
                }
                if (_isAnalyzed)
                {
                    if (node.XRestraint == eRestraint.Restrained)
                    {
                        row[_columnNameDispX] = node.Dispx.ToString(_strFormat);
                    }
                    else
                    {
                        row[_columnNameDispX] = node.Dispx.ToString("E2");
                    }

                    if (node.YRestraint == eRestraint.Restrained)
                    {
                        row[_columnNameDispY] = node.Dispx.ToString(_strFormat);
                    }
                    else
                    {
                        row[_columnNameDispY] = node.Dispy.ToString("E2");
                    }
                }
                else
                {

                    row[_columnNameDispX] = "?";
                    row[_columnNameDispY] = "?";
                }


                _dataNodeTable.Rows.Add(row);
            }
        }

        private void SetElementsTableColumns()
        {
            _dataTrussElementsTable = new DataTable();
            _dataTrussElementsTable.Columns.Add(new DataColumn() { ColumnName = _columnNameElementId, DataType = typeof(int), Caption = "Element ID" });
            _dataTrussElementsTable.Columns.Add(new DataColumn() { ColumnName = _columnNameElementNodeI, DataType = typeof(int), Caption = "Start NodeID" });
            _dataTrussElementsTable.Columns.Add(new DataColumn() { ColumnName = _columnNameElementNodeJ, DataType = typeof(int), Caption = "End NodeID" });
            _dataTrussElementsTable.Columns.Add(new DataColumn() { ColumnName = _columnNameElementSectionLength, DataType = typeof(double), Caption = $"Length (mm)" });
            _dataTrussElementsTable.Columns.Add(new DataColumn() { ColumnName = _columnNameElementSectionAngle, DataType = typeof(double), Caption = "Angle" });
            _dataTrussElementsTable.Columns.Add(new DataColumn() { ColumnName = _columnNameElementModulus, DataType = typeof(string), Caption = "E (N/mm^2)" });
            _dataTrussElementsTable.Columns.Add(new DataColumn() { ColumnName = _columnNameElementSectionArea, DataType = typeof(double), Caption = "Area (mm^2)" });
            _dataTrussElementsTable.Columns.Add(new DataColumn() { ColumnName = _columnNameAxialLoad, DataType = typeof(string), Caption = "AxialForce (N)" });
            gcElements.DataSource = _dataTrussElementsTable;
        }

        private void AddElementsDataRows()
        {
            DataRow row;
            _dataTrussElementsTable.Rows.Clear();
            foreach (var elemnent in _solverHelper.ElementList)
            {
                FrameElement frameElement = elemnent as FrameElement;
                if (frameElement != null)
                {
                    row = _dataTrussElementsTable.NewRow();
                    row[_columnNameElementId] = frameElement.Id;
                    row[_columnNameElementNodeI] = frameElement.NodeI.ID;
                    row[_columnNameElementNodeJ] = frameElement.NodeJ.ID;
                    row[_columnNameElementSectionLength] = frameElement.L.ToString(_strFormat);
                    row[_columnNameElementSectionAngle] = frameElement.MemberAngle.ToString(_strFormat);
                    row[_columnNameElementModulus] = frameElement.E.ToString("E2");
                    row[_columnNameElementSectionArea] = frameElement.A.ToString(_strFormat);
                    //row[_columnNameAxialLoad] = frameElement.IEndForce.ToString(_strFormat);
                    if (_isAnalyzed)
                    {

                        //row[_columnNameAxialLoad] = elemnent.IEndForce.ToString(_strFormat);
                    }
                    else
                    {
                        row[_columnNameAxialLoad] = "?";
                    }
                    _dataTrussElementsTable.Rows.Add(row);
                }
            }
        }

        private void SetBCTableColumns()
        {
            //_dataBoundaryConditionsTable = new DataTable();
            //_dataBoundaryConditionsTable.Columns.Add(" Node ID", typeof(int));
            //_dataBoundaryConditionsTable.Columns.Add("Restrained Direction", typeof(eRestraint));
            //gcboundaryCondition.DataSource = _dataBoundaryConditionsTable;
        }

        private void drawChart()
        {

        }

        private void DrawMomentDiagram()
        {

        }

        #endregion

        #region Events


        private void BtnInsertGrid_Click(object sender, EventArgs e)
        {


        }

        private void BtnDisableExtendedLine_Click(object sender, EventArgs e)
        {
        }
        // Button Draw With Mouse

        private void BtnDrawLineWithMouse_Click(object sender, EventArgs e)
        {
            if (_activeDrawing)
            {
                _drawIndex = -1;
                drawingPanel.Cursor = Cursors.Arrow;
                _activeDrawing = false;
            }
            else
            {
                _drawIndex = 1;
                _activeDrawing = true;
                drawingPanel.Cursor = Cursors.Cross;
            }
        }

        private void BtnDrawWithMouse_Click(object sender, EventArgs e)
        {
            if (_activeDrawing)
            {
                _drawIndex = -1;
                drawingPanel.Cursor = Cursors.Arrow;
                _activeDrawing = false;
            }
            else
            {
                _drawIndex = 0;
                _activeDrawing = true;
                drawingPanel.Cursor = Cursors.Cross;
            }
        }

        //Button Draw Diagram
        private void BtnDrawDiagram_Click(object sender, EventArgs e)
        {
        }

        //  Mouse Down on panel to draw a point
        private void DrawingPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_activeDrawing)
                {
                    switch (_drawIndex)
                    {
                        case 0: // Point
                            {
                                AddCurrentPositionToListOfPoints();
                                break;
                            }

                        case 1: // Line
                            {
                                switch (_clickNum)
                                {
                                    case 1:
                                        {
                                            _firstPosition = _currentPosition;

                                            AddCurrentPositionToListOfPoints();

                                            _clickNum++;
                                            break;
                                        }

                                    case 2:
                                        {
                                            LineElement line = new LineElement(_firstPosition, _currentPosition);
                                            // MessageBox.Show($"{_firstPosition.X.ToString()} , {_firstPosition.Y.ToString()} --- {_currentPosition.X.ToString()} , {_currentPosition.Y.ToString()} ");
                                            AddCurrentPositionToListOfPoints();
                                            //MessageBox.Show(string.Format("{0}, {1}", line.StartPoint.X + " " + line.StartPoint.Y, line.EndPoint.X + " " + line.EndPoint.Y));
                                            AddLineToListOfLines(line);
                                            _firstPosition = _currentPosition;

                                            break;
                                        }
                                }
                                break;
                            }
                    }
                    drawingPanel.Refresh();
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                if (_drawIndex == 1)
                {
                    _drawIndex = -1;
                    drawingPanel.Cursor = Cursors.Arrow;
                    _activeDrawing = false;
                }
            }
        }

        private void AddLineToListOfLines(LineElement line)
        {
            _drawingHelper.LineElements.Add(line.Id, line);
        }

        private void AddCurrentPositionToListOfPoints()
        {
            _drawingHelper.ListOfPointsCoordinate.Add(new Point(_currentPosition));
        }

        // Button to clear to the panel
        private void BtnDeleteLines_Click(object sender, EventArgs e)
        {
            using (_panelGraphic = drawingPanel.CreateGraphics())
            {
                //_panelGraphic.Clear(Color.White);

                _drawingHelper.ListOfPoints.Clear();
                _drawingHelper.ListOfPointsForMagnitude.Clear();


            }
            _drawingHelper.ListOfPointsCoordinate.Clear();
            _drawingHelper.LineElements.Clear();

            _clickNum = 1;
            _currentPosition = null;
            _firstPosition = null;
            drawingPanel.Refresh();
            drawingPanel.Invalidate();
        }

        private void DrawingPanel_Resize(object sender, EventArgs e)
        {
            // Redraw the panel when its size changes
            drawingPanel.Invalidate();
        }
        private void BtnDrawLine_Click(object sender, EventArgs e)
        {
            //float.TryParse(txtX1.Text, out float x1);
            //float.TryParse(txtY1.Text, out float y1);
            //float.TryParse(txtX2.Text, out float x2);
            //float.TryParse(txtY2.Text, out float y2);


            //DrawLine(x1, y1, x2, y2);

            //float.TryParse(txtMagnitude.Text, out float magnitude1);
            //float.TryParse(txtMagnitude2.Text, out float magnitude2);

            //DrawMagnitude(x1, y1, magnitude1);

            //if (_listOfPointsForMagnitude.Count >= 2)
            //{
            //    ConnectMagnitudes(_listOfPointsForMagnitude[0].X, _listOfPointsForMagnitude[0].Y, _listOfPointsForMagnitude[1].X, _listOfPointsForMagnitude[1].Y);

            //    _listOfPointsForMagnitude.RemoveAt(0);
            //}

            //DrawMagnitude(x2, y2, magnitude2);
        }

        private void ConnectMagnitudes(float x1, float y1, float x2, float y2)
        {
            using (_panelGraphic = drawingPanel.CreateGraphics())
            {
                Pen gridPen = new Pen(Color.Red, 3);

                //var finalX1 = x1 + _gridSize;
                //var finalY1 = drawingPanel.Height - y1 - _gridSize;
                //var finalX2 = x2 + _gridSize;
                //var finalY2 = drawingPanel.Height - y2 - _gridSize;

                _panelGraphic.DrawLine(gridPen, x1, y1, x2, y2);

                gridPen.Dispose();
            }
        }

        private void DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            using (_panelGraphic = drawingPanel.CreateGraphics())
            {

                //Draw All lines

                var Pen = new Pen(Color.LightSkyBlue, 5);

                if (_drawingHelper.LineElements.Count > 0)
                {
                    foreach (var line in _drawingHelper.LineElements)
                    {
                        _panelGraphic.DrawLineInCm(Pen, line.Value);
                    }

                    //Draw Line Extended

                    switch (_drawIndex)
                    {
                        case 1:
                            {
                                if (_clickNum == 2)
                                {
                                    Pen = new Pen(Color.LightGreen, 2f);

                                    Vector startPoint = new Vector(_firstPosition.X, _firstPosition.Y);
                                    Vector endPoint = new Vector(_currentPosition.X, _currentPosition.Y);

                                    LineElement line = new LineElement(startPoint, endPoint);
                                    _panelGraphic.DrawLineInCm(Pen, line);


                                }
                                break;
                            }
                    }



                }

                Pen.Dispose();
            }
        }

        private void BtnAddNode_Click(object sender, EventArgs e)
        {
            var nodeId = Convert.ToInt32(txtNodeIDForNodes.EditValue);
            var xCoord = Convert.ToDouble(txtNodeX.EditValue);
            var yCoord = Convert.ToDouble(txtNodeY.EditValue);
            _solverHelper.AddFrameNode(nodeId, xCoord, yCoord);
            txtNodeIDForNodes.EditValue = 0;
            AddNodesDataRows();
            drawChart();
        }



        private void canvasPictureBox_Paint(object sender, PaintEventArgs e)
        {
            updateColorBarValues();

        }

        private void updateColorBarValues()
        {
            if (_IscmbResultsIndexChanged)
            {
                _pieces = 10;

                AddDisplacementColorMapXDirection(_cmbResultsIndex);

            }
        }

        private void setDispXLimits()
        {
        }

        //private void drawcolorbar()
        //{
        //    // Define the width and height of the color bar
        //    int colorBarWidth = 30;
        //    int colorBarHeight = chartDrawing.Height;
        //    int paddingTop = 10;
        //    int paddingBottom = 10;

        //    // Calculate the height of the color bar without padding
        //    int colorBarHeightWithoutPadding = colorBarHeight - paddingTop - paddingBottom;

        //    // Create a new LinearGradientBrush to paint the color bar with jet colors
        //    Color[] colors = { Color.Red, Color.Orange, Color.Yellow, Color.Lime, Color.Cyan, Color.Blue };
        //    float[] positions = { 0.0f, 0.2f, 0.4f, 0.6f, 0.8f, 1.0f };
        //    LinearGradientBrush brush = new LinearGradientBrush(
        //        new Point(colorBarWidth, paddingTop), new Point(colorBarWidth, paddingTop + colorBarHeightWithoutPadding),
        //        Color.Red, Color.Blue);
        //    ColorBlend blend = new ColorBlend();
        //    blend.Colors = colors;
        //    blend.Positions = positions;
        //    brush.InterpolationColors = blend;

        //    // Create a new Graphics object from the PictureBox's image
        //    Graphics g = pictureBox1.CreateGraphics();
        //    g.Clear(Color.White);

        //    // Fill the rectangle for the color bar with the LinearGradientBrush
        //    g.FillRectangle(brush, 0, paddingTop, colorBarWidth, colorBarHeightWithoutPadding);

        //    // Draw the text for the minimum and maximum values on both sides of the color bar
        //    Font font = new Font("Arial", 8);
        //    Brush textBrush = new SolidBrush(Color.Black);
        //    var step = (_maxVal - _minVal) / _pieces;
        //    for (int i = 0; i <= _pieces; i++)
        //    {
        //        double val = _minVal + i * step;
        //        float y = colorBarHeightWithoutPadding - (float)i * colorBarHeightWithoutPadding / _pieces - 6 + paddingTop;
        //        g.DrawString(val.ToString("E3"), font, textBrush, colorBarWidth + 4, y-5);
        //        g.DrawLine(Pens.Black, colorBarWidth - 2, y + 4, colorBarWidth, y + 4);
        //    }


        //}


        #endregion
    }

    public class DrawignHelper
    {
        #region Ctor
        public DrawignHelper()
        {
            _lineElements = new Dictionary<Guid, LineElement>();
            _listOfPointsCoordinate = new List<Point>();
            _listOfPoints = new List<PointF>();
            _listOfPointsForMagnitude = new List<PointF>();

        }

        #endregion


        #region Private Fields

        private Dictionary<Guid, LineElement> _lineElements;
        private List<Point> _listOfPointsCoordinate;
        private List<PointF> _listOfPoints;
        private List<PointF> _listOfPointsForMagnitude;

        #endregion

        #region Public Properties
        public Dictionary<Guid, LineElement> LineElements { get => _lineElements; set => _lineElements = value; }
        public List<PointF> ListOfPoints { get => _listOfPoints; set => _listOfPoints = value; }
        public List<PointF> ListOfPointsForMagnitude { get => _listOfPointsForMagnitude; set => _listOfPointsForMagnitude = value; }
        public List<Point> ListOfPointsCoordinate { get => _listOfPointsCoordinate; set => _listOfPointsCoordinate = value; }

        #endregion
    }

    public static class GraphicsExtenstion
    {
        private static float Height;

        public static void SetParameters(this Graphics g, float height)
        {
            Height = height;

        }

        public static void SetTransform(this Graphics g)
        {

            //g.PageUnit = GraphicsUnit.Millimeter;
            //g.TranslateTransform(0, Height);
            //g.ScaleTransform(1.0f, -1.0f);

        }

        public static void DrawPoint(this Graphics g, Pen pen, Point point)
        {


            g.DrawEllipse(pen, point.Position.X, point.Position.Y, 1, 1);


        }

        public static void DrawLine(this Graphics g, Pen pen, LineElement line)
        {

            g.DrawLine(pen, line.StartPoint.X, line.StartPoint.Y, line.EndPoint.X, line.EndPoint.Y);

        }
        public static void DrawLineInCm(this Graphics g, Pen pen, LineElement line)
        {
            g.DrawLine(pen, line.StartPoint.X * 10f, line.StartPoint.Y * 10f, line.EndPoint.X * 10f, line.EndPoint.Y * 10f);
        }

        public static void DrawPointInCm(this Graphics g, Brush brush, Point point)
        {
            g.FillEllipse(brush, point.Position.X * 10f - 2, point.Position.Y * 10f - 2, 5, 5);
        }
    }

    public class Vector
    {

        #region Ctor
        public Vector(float x, float y, float z = 0f)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        #endregion

        #region Private Fields

        private float _x;
        private float _y;
        private float _z;

        #endregion

        #region Public Properties

        //public PointF ToPointF
        //{
        //    get
        //    {
        //        return new PointF(X, Y);
        //    }
        //}

        public static Vector Zero
        {
            get { return new Vector(0.0f, 0.0f, 0.0f); }
        }

        public float X { get => _x; set => _x = value; }
        public float Y { get => _y; set => _y = value; }
        public float Z { get => _z; set => _z = value; }


        #endregion

    }

    public class LineElement
    {
        #region Ctor

        public LineElement()
        {
            StartPoint = Vector.Zero;
            EndPoint = Vector.Zero;
            Id = Guid.NewGuid();
            Thickness = 0.0;
        }

        public LineElement(Vector startPoint, Vector endPoint)
        {
            Id = Guid.NewGuid();
            StartPoint = startPoint;
            EndPoint = endPoint;
            Thickness = 0.0;

        }


        #endregion

        #region private Fields

        private Guid _id;

        private Vector _startPoint;

        private Vector _endPoint;

        private double _thickness;

        public double Thickness
        {
            get { return _thickness; }
            set { _thickness = value; }
        }



        #endregion

        #region Public Properties
        public Guid Id { get => _id; set => _id = value; }
        public Vector EndPoint
        {
            get { return _endPoint; }
            set { _endPoint = value; }
        }


        public Vector StartPoint
        {
            get { return _startPoint; }
            set { _startPoint = value; }
        }

        #endregion
    }

    public class Point
    {
        #region Ctor

        public Point()
        {
            this.Position = Vector.Zero;
            this.Thickness = 0f;
        }
        public Point(Vector position)
        {
            this.Position = position;
            this.Thickness = 0f;
        }

        #endregion

        #region Private Fields

        private Vector _position;
        private double _thickness;


        #endregion

        #region Public Properties
        public Vector Position { get => _position; set => _position = value; }
        public double Thickness { get => _thickness; set => _thickness = value; }

        #endregion
    }
}