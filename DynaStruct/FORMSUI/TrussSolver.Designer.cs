
namespace FORMSUI
{
    partial class TrussSolver
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrussSolver));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMemberLabel = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.gcElements = new DevExpress.XtraGrid.GridControl();
            this.gvElements = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnAddElement = new DevExpress.XtraEditors.SimpleButton();
            this.txtSectionArea = new DevExpress.XtraEditors.TextEdit();
            this.txtModulusOfElasticity = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNodeJ = new DevExpress.XtraEditors.TextEdit();
            this.txtNodeI = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbResult = new System.Windows.Forms.ComboBox();
            this.BtnAnalyze = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gcNodes = new DevExpress.XtraGrid.GridControl();
            this.gvNodes = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnAddNode = new DevExpress.XtraEditors.SimpleButton();
            this.txtNodeY = new DevExpress.XtraEditors.TextEdit();
            this.txtNodeX = new DevExpress.XtraEditors.TextEdit();
            this.txtNodeIDForNodes = new DevExpress.XtraEditors.TextEdit();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chartDrawing = new DevExpress.XtraCharts.ChartControl();
            this.scaleSlider = new DevExpress.XtraEditors.TrackBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemberLabel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcElements)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvElements)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSectionArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtModulusOfElasticity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNodeJ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNodeI.Properties)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcNodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNodeY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNodeX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNodeIDForNodes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDrawing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scaleSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scaleSlider.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this.barButtonItem1});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 2;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.Size = new System.Drawing.Size(1288, 158);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Solve";
            this.barButtonItem1.Id = 1;
            this.barButtonItem1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.Image")));
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem1);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "ribbonPageGroup2";
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 780);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(1288, 24);
            // 
            // ribbonPage2
            // 
            this.ribbonPage2.Name = "ribbonPage2";
            this.ribbonPage2.Text = "ribbonPage2";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtMemberLabel);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.gcElements);
            this.groupBox2.Controls.Add(this.btnAddElement);
            this.groupBox2.Controls.Add(this.txtSectionArea);
            this.groupBox2.Controls.Add(this.txtModulusOfElasticity);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtNodeJ);
            this.groupBox2.Controls.Add(this.txtNodeI);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(580, 164);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(563, 292);
            this.groupBox2.TabIndex = 45;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Elements Information";
            // 
            // txtMemberLabel
            // 
            this.txtMemberLabel.EditValue = "1";
            this.txtMemberLabel.Location = new System.Drawing.Point(74, 39);
            this.txtMemberLabel.MenuManager = this.ribbonControl1;
            this.txtMemberLabel.Name = "txtMemberLabel";
            this.txtMemberLabel.Size = new System.Drawing.Size(67, 20);
            this.txtMemberLabel.TabIndex = 52;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "Member ID";
            // 
            // gcElements
            // 
            this.gcElements.Location = new System.Drawing.Point(6, 85);
            this.gcElements.MainView = this.gvElements;
            this.gcElements.MenuManager = this.ribbonControl1;
            this.gcElements.Name = "gcElements";
            this.gcElements.Size = new System.Drawing.Size(550, 200);
            this.gcElements.TabIndex = 50;
            this.gcElements.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvElements});
            // 
            // gvElements
            // 
            this.gvElements.GridControl = this.gcElements;
            this.gvElements.Name = "gvElements";
            // 
            // btnAddElement
            // 
            this.btnAddElement.Location = new System.Drawing.Point(410, 29);
            this.btnAddElement.Name = "btnAddElement";
            this.btnAddElement.Size = new System.Drawing.Size(98, 38);
            this.btnAddElement.TabIndex = 44;
            this.btnAddElement.Text = "Add Element";
            // 
            // txtSectionArea
            // 
            this.txtSectionArea.EditValue = "1000";
            this.txtSectionArea.Location = new System.Drawing.Point(304, 50);
            this.txtSectionArea.MenuManager = this.ribbonControl1;
            this.txtSectionArea.Name = "txtSectionArea";
            this.txtSectionArea.Size = new System.Drawing.Size(67, 20);
            this.txtSectionArea.TabIndex = 49;
            // 
            // txtModulusOfElasticity
            // 
            this.txtModulusOfElasticity.EditValue = "200000000000";
            this.txtModulusOfElasticity.Location = new System.Drawing.Point(304, 20);
            this.txtModulusOfElasticity.MenuManager = this.ribbonControl1;
            this.txtModulusOfElasticity.Name = "txtModulusOfElasticity";
            this.txtModulusOfElasticity.Size = new System.Drawing.Size(67, 20);
            this.txtModulusOfElasticity.TabIndex = 48;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(284, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 47;
            this.label3.Text = "A";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(285, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 46;
            this.label6.Text = "E";
            // 
            // txtNodeJ
            // 
            this.txtNodeJ.EditValue = "2";
            this.txtNodeJ.Location = new System.Drawing.Point(201, 50);
            this.txtNodeJ.MenuManager = this.ribbonControl1;
            this.txtNodeJ.Name = "txtNodeJ";
            this.txtNodeJ.Size = new System.Drawing.Size(67, 20);
            this.txtNodeJ.TabIndex = 45;
            // 
            // txtNodeI
            // 
            this.txtNodeI.EditValue = "1";
            this.txtNodeI.Location = new System.Drawing.Point(201, 19);
            this.txtNodeI.MenuManager = this.ribbonControl1;
            this.txtNodeI.Name = "txtNodeI";
            this.txtNodeI.Size = new System.Drawing.Size(67, 20);
            this.txtNodeI.TabIndex = 44;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(156, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Node I";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(155, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Node J";
            // 
            // cmbResult
            // 
            this.cmbResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbResult.FormattingEnabled = true;
            this.cmbResult.Location = new System.Drawing.Point(1146, 522);
            this.cmbResult.Name = "cmbResult";
            this.cmbResult.Size = new System.Drawing.Size(89, 21);
            this.cmbResult.TabIndex = 34;
            // 
            // BtnAnalyze
            // 
            this.BtnAnalyze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAnalyze.Location = new System.Drawing.Point(1146, 462);
            this.BtnAnalyze.Name = "BtnAnalyze";
            this.BtnAnalyze.Size = new System.Drawing.Size(130, 42);
            this.BtnAnalyze.TabIndex = 4;
            this.BtnAnalyze.Text = "Analyze";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.gcNodes);
            this.groupBox3.Controls.Add(this.btnAddNode);
            this.groupBox3.Controls.Add(this.txtNodeY);
            this.groupBox3.Controls.Add(this.txtNodeX);
            this.groupBox3.Controls.Add(this.txtNodeIDForNodes);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Location = new System.Drawing.Point(12, 164);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(562, 292);
            this.groupBox3.TabIndex = 44;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Nodes Information";
            // 
            // gcNodes
            // 
            this.gcNodes.Location = new System.Drawing.Point(6, 85);
            this.gcNodes.MainView = this.gvNodes;
            this.gcNodes.MenuManager = this.ribbonControl1;
            this.gcNodes.Name = "gcNodes";
            this.gcNodes.Size = new System.Drawing.Size(550, 200);
            this.gcNodes.TabIndex = 44;
            this.gcNodes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvNodes});
            // 
            // gvNodes
            // 
            this.gvNodes.GridControl = this.gcNodes;
            this.gvNodes.Name = "gvNodes";
            // 
            // btnAddNode
            // 
            this.btnAddNode.Location = new System.Drawing.Point(304, 29);
            this.btnAddNode.Name = "btnAddNode";
            this.btnAddNode.Size = new System.Drawing.Size(98, 38);
            this.btnAddNode.TabIndex = 17;
            this.btnAddNode.Text = "Add Node";
            // 
            // txtNodeY
            // 
            this.txtNodeY.EditValue = "0";
            this.txtNodeY.Location = new System.Drawing.Point(193, 52);
            this.txtNodeY.MenuManager = this.ribbonControl1;
            this.txtNodeY.Name = "txtNodeY";
            this.txtNodeY.Size = new System.Drawing.Size(67, 20);
            this.txtNodeY.TabIndex = 43;
            // 
            // txtNodeX
            // 
            this.txtNodeX.EditValue = "0";
            this.txtNodeX.Location = new System.Drawing.Point(193, 20);
            this.txtNodeX.MenuManager = this.ribbonControl1;
            this.txtNodeX.Name = "txtNodeX";
            this.txtNodeX.Size = new System.Drawing.Size(67, 20);
            this.txtNodeX.TabIndex = 42;
            // 
            // txtNodeIDForNodes
            // 
            this.txtNodeIDForNodes.EditValue = "1";
            this.txtNodeIDForNodes.Location = new System.Drawing.Point(64, 38);
            this.txtNodeIDForNodes.MenuManager = this.ribbonControl1;
            this.txtNodeIDForNodes.Name = "txtNodeIDForNodes";
            this.txtNodeIDForNodes.Size = new System.Drawing.Size(67, 20);
            this.txtNodeIDForNodes.TabIndex = 41;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 40;
            this.label9.Text = "Node ID";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(147, 54);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 13);
            this.label12.TabIndex = 38;
            this.label12.Text = "Ycoord";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(148, 23);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(40, 13);
            this.label13.TabIndex = 36;
            this.label13.Text = "Xcoord";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(1035, 462);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(101, 311);
            this.pictureBox1.TabIndex = 43;
            this.pictureBox1.TabStop = false;
            // 
            // chartDrawing
            // 
            this.chartDrawing.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartDrawing.Location = new System.Drawing.Point(13, 461);
            this.chartDrawing.Name = "chartDrawing";
            this.chartDrawing.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartDrawing.Size = new System.Drawing.Size(1016, 312);
            this.chartDrawing.TabIndex = 46;
            // 
            // scaleSlider
            // 
            this.scaleSlider.Location = new System.Drawing.Point(1146, 568);
            this.scaleSlider.MenuManager = this.ribbonControl1;
            this.scaleSlider.Name = "scaleSlider";
            this.scaleSlider.Properties.LabelAppearance.Options.UseTextOptions = true;
            this.scaleSlider.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.scaleSlider.Size = new System.Drawing.Size(111, 45);
            this.scaleSlider.TabIndex = 49;
            // 
            // TrussSolver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1288, 804);
            this.Controls.Add(this.scaleSlider);
            this.Controls.Add(this.chartDrawing);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cmbResult);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.BtnAnalyze);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "TrussSolver";
            this.Ribbon = this.ribbonControl1;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar1;
            this.Text = "TrussSolver";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemberLabel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcElements)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvElements)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSectionArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtModulusOfElasticity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNodeJ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNodeI.Properties)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcNodes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNodes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNodeY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNodeX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNodeIDForNodes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDrawing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scaleSlider.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scaleSlider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.SimpleButton btnAddElement;
        private System.Windows.Forms.ComboBox cmbResult;
        private DevExpress.XtraEditors.TextEdit txtSectionArea;
        private DevExpress.XtraEditors.TextEdit txtModulusOfElasticity;
        private DevExpress.XtraEditors.SimpleButton BtnAnalyze;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.TextEdit txtNodeJ;
        private DevExpress.XtraEditors.TextEdit txtNodeI;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraEditors.SimpleButton btnAddNode;
        private DevExpress.XtraEditors.TextEdit txtNodeY;
        private DevExpress.XtraEditors.TextEdit txtNodeX;
        private DevExpress.XtraEditors.TextEdit txtNodeIDForNodes;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraCharts.ChartControl chartDrawing;
        private DevExpress.XtraGrid.GridControl gcElements;
        private DevExpress.XtraGrid.Views.Grid.GridView gvElements;
        private DevExpress.XtraGrid.GridControl gcNodes;
        private DevExpress.XtraGrid.Views.Grid.GridView gvNodes;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraEditors.TextEdit txtMemberLabel;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TrackBarControl scaleSlider;
    }
}