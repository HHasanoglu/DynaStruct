﻿using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using FORMSUI;
using Solver;
using Solver.Truss;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace FORMSUI
{
    public partial class TrussSolver : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        #region Ctor

        public TrussSolver()
        {
            InitializeComponent();
            _solverHelper = new TrussSolverHelper();
            setComboBoxItems();
            prepareUI();
            SubscribeToEvents();
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

        private TrussSolverHelper _solverHelper;
        private DataTable _dataNodeTable;
        private DataTable _dataTrussElementsTable;
        private string _strFormat = "#0.#";
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
            chartDrawing.CustomDrawCrosshair += ChartDrawing_CustomDrawCrosshair;
            pictureBox1.Paint += canvasPictureBox_Paint;
            cmbResults.SelectedIndexChanged += cmbResults_SelectedIndexChanged;
            this.Resize += TrussSolver_Resize;
            scaleTrackbar.ValueChanged += scaleTrackbar_ValueChanged;
            TestsComboBox.SelectedIndexChanged += TestsComboBox_SelectedIndexChanged;
        }

        private void TestsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _solverHelper.ClearNodeAndElements();
            var selectedValue = (eBenchmarkTests)((ComboBoxEdit)sender).SelectedIndex;
            _solverHelper.SetExample(selectedValue);
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
            _solverHelper.AddMember(memberLabel, nodeI, nodeJ, A, E);
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

        private void Main_Load(object sender, EventArgs e)
        {
            chartDrawing.ToolTipController = new ToolTipController();
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
                var node = (TrussNode)_solverHelper.NodeList[gvNodes.FocusedRowHandle];
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
                var node = (TrussNode)_solverHelper.NodeList[_selectedRowHandle];
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
            // Create a scatter chart series
            var series = new Series("Intensity", ViewType.Point);
            var seriesToClear = chartDrawing.GetSeriesByName("Intensity");
            chartDrawing.Series.Remove(seriesToClear);

            PointSeriesView seriesView = (PointSeriesView)series.View;

            GetCoordinates(type, magnificationFactor, numPoints, out List<double> xcoordList, out List<double> ycoordList);

            List<double> intensities = GetIntesities(numPoints, type);

            // Create a ColorMap object with the jet colormap and the range of intensities
            _minVal = _solverHelper.GetMinValueForColorMap(_cmbResultsIndex);
            _maxVal = _solverHelper.GetMaxValueForColorMap(_cmbResultsIndex);
            SetColorMap(xcoordList, ycoordList, series, seriesView, intensities, _minVal, _maxVal);
            chartDrawing.Series.Add(series);
        }

        private void GetCoordinates(eResultToShow type, int magnificationFactor, int numPoints, out List<double> xcoordList, out List<double> ycoordList)
        {
            xcoordList = new List<double>();
            ycoordList = new List<double>();
            foreach (var element in _solverHelper.ElementList)
            {
                //var element = _TrussElementsList.First();
                var NodeI = ((TrussElement)element).NodeI;
                var NodeJ = ((TrussElement)element).NodeJ;
                double x1 = NodeI.Xcoord;
                double x2 = NodeJ.Xcoord;
                double y1 = NodeI.Ycoord;
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

            foreach (TrussElement element in _solverHelper.ElementList)
            {
                var NodeI = element.NodeI;
                var NodeJ = element.NodeJ;
                SetStartAndEndValues(type, NodeI, NodeJ, out double start, out double End);
                intensities.AddRange(Enumerable.Range(0, numPoints).Select(i => Math.Abs(start + i * (End - start) / (numPoints - 1.0))));
            }

            return intensities;
        }

        private static void SetStartAndEndValues(eResultToShow type, TrussNode NodeI, TrussNode NodeJ, out double start, out double End)
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
            foreach (TrussNode node in _solverHelper.NodeList)
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
            foreach (TrussElement elemnent in _solverHelper.ElementList)
            {
                row = _dataTrussElementsTable.NewRow();
                row[_columnNameElementId] = elemnent.Id;
                row[_columnNameElementNodeI] = elemnent.NodeI.ID;
                row[_columnNameElementNodeJ] = elemnent.NodeJ.ID;
                row[_columnNameElementSectionLength] = elemnent.L.ToString(_strFormat);
                row[_columnNameElementSectionAngle] = elemnent.Angle.ToString(_strFormat);
                row[_columnNameElementModulus] = elemnent.E.ToString("E2");
                row[_columnNameElementSectionArea] = elemnent.A.ToString(_strFormat);
                row[_columnNameAxialLoad] = elemnent.IEndForce.ToString(_strFormat);
                if (_isAnalyzed)
                {

                    row[_columnNameAxialLoad] = elemnent.IEndForce.ToString(_strFormat);
                }
                else
                {
                    row[_columnNameAxialLoad] = "?";
                }
                _dataTrussElementsTable.Rows.Add(row);
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
            chartDrawing.Series.Clear();
            // Add elements to the series
            AddElementsToSeries();

            // Add nodes to the series
            AddNodesToSeries();

            // Add Loading Info to the chart
            DrawLoadingInfo();

        }

        private void DrawLoadingInfo()
        {
            foreach (var node in _solverHelper.NodeList)
            {
                var castedNode = node as TrussNode;
                if (castedNode != null)
                {

                    Series series = new Series(castedNode.ID.ToString(), ViewType.Line);
                    Series seriespoint = new Series("", ViewType.Point);

                    if (castedNode.Fx != 0)
                    {
                        if (castedNode.Fx < 0)
                        {
                            drawArrowLeft(castedNode.Xcoord, castedNode.Ycoord);
                        }
                        else
                        {
                            drawArrowRight(castedNode.Xcoord, castedNode.Ycoord);
                        }
                    }
                    if (castedNode.Fy != 0)
                    {
                        if (castedNode.Fy < 0)
                        {
                            drawArrowDown(castedNode.Xcoord, castedNode.Ycoord);
                        }
                        else
                        {
                            drawArrowUp(castedNode.Xcoord, castedNode.Ycoord);
                        }
                    }


                    LineSeriesView lineView = (LineSeriesView)series.View;
                    //lineView.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                    //lineView.LineMarkerOptions.Size = 10;
                    //lineView.LineMarkerOptions.Kind = MarkerKind.InvertedTriangle;
                    lineView.LineStyle.Thickness = 2;
                    lineView.LineMarkerOptions.BorderColor = Color.Black;
                    lineView.LineMarkerOptions.BorderVisible = true;
                    series.ShowInLegend = false;
                    series.CrosshairEnabled = DefaultBoolean.False;
                    chartDrawing.Series.Add(series);
                    series.View.Color = Color.Orange;
                }
            }
        }

        private void drawArrowRight(double x, double y)
        {
            var scale = 0.5;
            var shiftedX = x + 1.5 * scale;
            Series seriestriangle = new Series("", ViewType.Line);
            seriestriangle.Points.Add(new SeriesPoint(shiftedX, y + 0.5 * scale));
            seriestriangle.Points.Add(new SeriesPoint(shiftedX, y - 0.5 * scale));
            seriestriangle.Points.Add(new SeriesPoint(shiftedX + 0.5 * scale, y));
            seriestriangle.View.Color = Color.Orange;
            chartDrawing.Series.Add(seriestriangle);

            Series seriestriangle2 = new Series("", ViewType.Line);
            seriestriangle2.Points.Add(new SeriesPoint(shiftedX, y - 0.5 * scale));
            seriestriangle2.Points.Add(new SeriesPoint(shiftedX, y + 0.5 * scale));
            seriestriangle2.Points.Add(new SeriesPoint(shiftedX + 0.5 * scale, y));
            seriestriangle2.View.Color = Color.Orange;
            chartDrawing.Series.Add(seriestriangle2);


            Series seriestriangle3 = new Series("", ViewType.Line);
            seriestriangle3.Points.Add(new SeriesPoint(x, y));
            seriestriangle3.Points.Add(new SeriesPoint(shiftedX, y));
            seriestriangle3.View.Color = Color.Orange;
            chartDrawing.Series.Add(seriestriangle3);
        }
        private void drawArrowUp(double x, double y)
        {
            var scale = 1.0;
            var shiftedy = y + 1.5 * scale;
            Series seriestriangle = new Series("", ViewType.Line);
            seriestriangle.Points.Add(new SeriesPoint(x, shiftedy));
            seriestriangle.Points.Add(new SeriesPoint(x, y));
            seriestriangle.View.Color = Color.Orange;
            chartDrawing.Series.Add(seriestriangle);

            scale = 0.25;
            seriestriangle = new Series("", ViewType.Line);
            seriestriangle.Points.Add(new SeriesPoint(x + 0.5 * scale, shiftedy));
            seriestriangle.Points.Add(new SeriesPoint(x - 0.5 * scale, shiftedy));
            seriestriangle.View.Color = Color.Orange;
            chartDrawing.Series.Add(seriestriangle);

            seriestriangle = new Series("", ViewType.Line);
            seriestriangle.Points.Add(new SeriesPoint(x - 0.5 * scale, shiftedy));
            seriestriangle.Points.Add(new SeriesPoint(x, shiftedy + 2 * scale));
            seriestriangle.View.Color = Color.Orange;
            chartDrawing.Series.Add(seriestriangle);

            seriestriangle = new Series("", ViewType.Line);
            seriestriangle.Points.Add(new SeriesPoint(x + 0.5 * scale, shiftedy));
            seriestriangle.Points.Add(new SeriesPoint(x, shiftedy + 2 * scale));
            seriestriangle.View.Color = Color.Orange;
            chartDrawing.Series.Add(seriestriangle);
        }

        private void drawArrowDown(double x, double y)
        {
            var scale = 1.0;
            var shiftedy = y - 1.5 * scale;
            Series seriestriangle = new Series("", ViewType.Line);
            seriestriangle.Points.Add(new SeriesPoint(x, shiftedy));
            seriestriangle.Points.Add(new SeriesPoint(x, y));
            seriestriangle.View.Color = Color.Orange;
            chartDrawing.Series.Add(seriestriangle);

            scale = 0.25;
            seriestriangle = new Series("", ViewType.Line);
            seriestriangle.Points.Add(new SeriesPoint(x + 0.5 * scale, shiftedy));
            seriestriangle.Points.Add(new SeriesPoint(x - 0.5 * scale, shiftedy));
            seriestriangle.View.Color = Color.Orange;
            chartDrawing.Series.Add(seriestriangle);

            seriestriangle = new Series("", ViewType.Line);
            seriestriangle.Points.Add(new SeriesPoint(x - 0.5 * scale, shiftedy));
            seriestriangle.Points.Add(new SeriesPoint(x, shiftedy - 2 * scale));
            seriestriangle.View.Color = Color.Orange;
            chartDrawing.Series.Add(seriestriangle);

            seriestriangle = new Series("", ViewType.Line);
            seriestriangle.Points.Add(new SeriesPoint(x + 0.5 * scale, shiftedy));
            seriestriangle.Points.Add(new SeriesPoint(x, shiftedy - 2 * scale));
            seriestriangle.View.Color = Color.Orange;
            chartDrawing.Series.Add(seriestriangle);
        }
        private void drawArrowLeft(double x, double y)
        {
            var scale = 0.5;
            var shiftedX = x - 1.5 * scale;
            Series seriestriangle = new Series("", ViewType.Line);
            seriestriangle.Points.Add(new SeriesPoint(shiftedX, y + 0.5 * scale));
            seriestriangle.Points.Add(new SeriesPoint(shiftedX, y - 0.5 * scale));
            seriestriangle.Points.Add(new SeriesPoint(shiftedX - 0.5 * scale, y));
            seriestriangle.View.Color = Color.Orange;
            chartDrawing.Series.Add(seriestriangle);

            Series seriestriangle2 = new Series("", ViewType.Line);
            seriestriangle2.Points.Add(new SeriesPoint(shiftedX, y - 0.5 * scale));
            seriestriangle2.Points.Add(new SeriesPoint(shiftedX, y + 0.5 * scale));
            seriestriangle2.Points.Add(new SeriesPoint(shiftedX - 0.5 * scale, y));
            seriestriangle2.View.Color = Color.Orange;
            chartDrawing.Series.Add(seriestriangle2);


            Series seriestriangle3 = new Series("", ViewType.Line);
            seriestriangle3.Points.Add(new SeriesPoint(x, y));
            seriestriangle3.Points.Add(new SeriesPoint(shiftedX, y));
            seriestriangle3.View.Color = Color.Orange;
            chartDrawing.Series.Add(seriestriangle3);
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
            foreach (TrussNode nodes in _solverHelper.NodeList)
            {
                Series series1 = new Series("Nodes", ViewType.Point);
                PointSeriesView seriesView = (PointSeriesView)series1.View;
                if (nodes.XRestraint == eRestraint.Free)
                {
                    seriesView.PointMarkerOptions.Kind = MarkerKind.Circle;
                    seriesView.Color = Color.Blue;
                    var point = new SeriesPoint(nodes.Xcoord, nodes.Ycoord);
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
                chartDrawing.CrosshairOptions.ShowArgumentLine = false;
                chartDrawing.CrosshairOptions.ShowValueLine = false;
                chartDrawing.CrosshairOptions.ShowOnlyInFocusedPane = true;
                chartDrawing.ToolTipEnabled = DefaultBoolean.True;
                chartDrawing.CrosshairEnabled = DefaultBoolean.True;
            }
        }

        private void AddElementsToSeries()
        {
            foreach (TrussElement element in _solverHelper.ElementList)
            {
                Series series = new Series(element.Id.ToString(), ViewType.Line);
                series.Points.Add(new SeriesPoint(element.NodeI.Xcoord, element.NodeI.Ycoord));
                series.Points.Add(new SeriesPoint(element.NodeJ.Xcoord, element.NodeJ.Ycoord));
                LineSeriesView lineView = (LineSeriesView)series.View;
                lineView.MarkerVisibility = DevExpress.Utils.DefaultBoolean.False;
                //lineView.LineMarkerOptions.Size = 1;
                //lineView.LineMarkerOptions.Kind = MarkerKind.Circle;
                lineView.LineStyle.Thickness = 2;
                lineView.LineMarkerOptions.BorderColor = Color.Black;
                lineView.LineMarkerOptions.BorderVisible = true;
                series.ShowInLegend = false;
                series.CrosshairEnabled = DefaultBoolean.False;
                chartDrawing.Series.Add(series);
                series.View.Color = Color.LightGray;
            }
        }

        private void AdddisplacementToSeries()
        {
            var magnificationFactor = 150000000;
            foreach (TrussElement element in _solverHelper.ElementList)
            {
                var NodeI = element.NodeI;
                var NodeJ = element.NodeJ;
                Series series = new Series(element.Id.ToString(), ViewType.Line);
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
            var nodeId = Convert.ToInt32(txtNodeIDForNodes.Text);
            var xCoord = Convert.ToDouble(txtNodeX.Text);
            var yCoord = Convert.ToDouble(txtNodeY.Text);
            _solverHelper.AddNode(nodeId, xCoord, yCoord);
            txtNodeIDForNodes.EditValue = _solverHelper.NodeList.Count + 1;
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

                AddDisplacementColorMapXDirection(_cmbResultsIndex, _scale);
                drawcolorbar();
            }
        }

        private void setDispXLimits()
        {
        }

        private void drawcolorbar()
        {
            // Define the width and height of the color bar
            int colorBarWidth = 30;
            int colorBarHeight = chartDrawing.Height;
            int paddingTop = 10;
            int paddingBottom = 10;

            // Calculate the height of the color bar without padding
            int colorBarHeightWithoutPadding = colorBarHeight - paddingTop - paddingBottom;

            // Create a new LinearGradientBrush to paint the color bar with jet colors
            Color[] colors = { Color.Red, Color.Orange, Color.Yellow, Color.Lime, Color.Cyan, Color.Blue };
            float[] positions = { 0.0f, 0.2f, 0.4f, 0.6f, 0.8f, 1.0f };
            LinearGradientBrush brush = new LinearGradientBrush(
                new PointF(colorBarWidth, paddingTop), new PointF(colorBarWidth, paddingTop + colorBarHeightWithoutPadding),
                Color.Red, Color.Blue);
            ColorBlend blend = new ColorBlend();
            blend.Colors = colors;
            blend.Positions = positions;
            brush.InterpolationColors = blend;

            // Create a new Graphics object from the PictureBox's image
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);

            // Fill the rectangle for the color bar with the LinearGradientBrush
            g.FillRectangle(brush, 0, paddingTop, colorBarWidth, colorBarHeightWithoutPadding);

            // Draw the text for the minimum and maximum values on both sides of the color bar
            Font font = new Font("Arial", 8);
            Brush textBrush = new SolidBrush(Color.Black);
            var step = (_maxVal - _minVal) / _pieces;
            for (int i = 0; i <= _pieces; i++)
            {
                double val = _minVal + i * step;
                float y = colorBarHeightWithoutPadding - (float)i * colorBarHeightWithoutPadding / _pieces + paddingTop;
                g.DrawString(val.ToString("E3"), font, textBrush, colorBarWidth + 4, y - 7.5f);
                g.DrawLine(Pens.Black, colorBarWidth, y, colorBarWidth + 5, y);
            }
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

    public class ColorMap
    {
        private double min;
        private double max;
        private string name;
        private Color[] colorMap;
        public ColorMap(double min, double max)
        {
            this.min = min;
            this.max = max;
            this.name = "jet";
        }
        public Color GetColor(double value)
        {
            if (value == max)
            {
                return Color.Red;
            }
            else if (value == min)
            {
                return Color.Blue;
            }
            else
            {
                double ratio = (value - min) / (max - min);
                Color[] colors = {Color.Blue , Color.Cyan, Color.Lime, Color.Yellow, Color.Orange,Color.Red
                              //Color.FromArgb(0, 0, 204),
                              //Color.FromArgb(0, 51, 204),
                              //Color.FromArgb(0, 102, 204),
                              //Color.FromArgb(0, 153, 204),
                              //Color.FromArgb(0, 204, 204),
                              //Color.FromArgb(0, 204, 153),
                              //Color.FromArgb(0, 204, 102),
                              //Color.FromArgb(0, 204, 51),
                              //Color.FromArgb(0, 204, 0),
                              //Color.FromArgb(51, 204, 0),
                              //Color.FromArgb(102, 204, 0),
                              //Color.FromArgb(153, 204, 0),
                              //Color.FromArgb(204, 204, 0),
                              //Color.FromArgb(204, 153, 0),
                              //Color.FromArgb(204, 102, 0),
                              //Color.FromArgb(204, 51, 0),
                              //Color.FromArgb(204, 0, 0),
            
            };
                double segmentRatio = 1.0 / (colors.Length - 1);
                // Map the ratio to a segment and calculate the segment-specific ratio
                int segmentIndex = (int)(ratio / segmentRatio);
                double segmentRatioValue = (ratio - segmentIndex * segmentRatio) / segmentRatio;

                // Interpolate between the colors of the current segment and the next segment
                Color startColor = colors[segmentIndex];
                Color endColor = colors[segmentIndex + 1];
                int r = (int)(startColor.R + segmentRatioValue * (endColor.R - startColor.R));
                int g = (int)(startColor.G + segmentRatioValue * (endColor.G - startColor.G));
                int b = (int)(startColor.B + segmentRatioValue * (endColor.B - startColor.B));
                Color interpolatedColor = Color.FromArgb(r, g, b);
                return interpolatedColor;
            }

        }
    }
}