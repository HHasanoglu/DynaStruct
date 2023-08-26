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

        #region Private Methods

        private void SubscribeToEvents()
        {
            barBtnTrussSolver.ItemClick += BarBtnTrussSolver_ItemClick;
            barBtnFrameSolver.ItemClick += BarBtnFrameSolver_ItemClick;
        }

        private void BarBtnFrameSolver_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var form = new FrameSolver();
            form.Show();
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
