using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using FORMSUI;
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
        stress,
        strain
    }

    public partial class Main : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        #region Ctor

        public Main()
        {
            InitializeComponent();
            SubscribeToEvents();
            this.WindowState = FormWindowState.Maximized;
        }
        #endregion

        #region Private Fields

        private List<Node> _nodesList;
        private List<TrussElement> _TrussElementsList;
        private DataTable _dataNodeTable;
        private DataTable _dataTrussElementsTable;
        private DataTable _dataBoundaryConditionsTable;
        private DataTable _dataLoadTable;
        private int _nodeId;
        //private List<RestrainedNode> _restrainedNodes;
        //private List<PointLoad> _nodalForces;

        private string _columnNameNodeId = "nodeId";
        private string _columnNameXcoord = "xCoord";
        private string _columnNameYcoord = "yCoord";
        private string _columnNameXRestaint = "xRestraint";
        private string _columnNameYRestaint = "yRestraint";
        private string _columnNameFx = "fx";
        private string _columnNameFy = "fy";
        private bool _disableXrestraint = false;
        private bool _disableYrestraint = false;

        private RepositoryItemComboBox _combo= new RepositoryItemComboBox();
        private int _selectedRowHandle;
        private GridColumn _selectedGridColumn;
        #endregion

        #region Private Methods

        private void SubscribeToEvents()
        {
            barBtnTrussSolver.ItemClick += BarBtnTrussSolver_ItemClick;
        }
        #endregion

        #region Events
        private void BarBtnTrussSolver_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var form = new TrussSolver();
            form.Show();
        }

        #endregion

    }

}
