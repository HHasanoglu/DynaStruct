
namespace FORMSUI
{
    partial class FrameSolver
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
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.chartDrawing = new DevExpress.XtraCharts.ChartControl();
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
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDrawing)).BeginInit();
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
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.barButtonItem1});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 2;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(1288, 158);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Analyze";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem1);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 780);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1288, 24);
            // 
            // chartDrawing
            // 
            this.chartDrawing.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartDrawing.Location = new System.Drawing.Point(13, 461);
            this.chartDrawing.Name = "chartDrawing";
            this.chartDrawing.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartDrawing.Size = new System.Drawing.Size(1156, 312);
            this.chartDrawing.TabIndex = 50;
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
            this.groupBox2.Location = new System.Drawing.Point(640, 164);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(635, 292);
            this.groupBox2.TabIndex = 49;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Elements Information";
            // 
            // txtMemberLabel
            // 
            this.txtMemberLabel.EditValue = "1";
            this.txtMemberLabel.Location = new System.Drawing.Point(74, 39);
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
            this.gcElements.Name = "gcElements";
            this.gcElements.Size = new System.Drawing.Size(623, 201);
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
            this.txtSectionArea.Name = "txtSectionArea";
            this.txtSectionArea.Size = new System.Drawing.Size(67, 20);
            this.txtSectionArea.TabIndex = 49;
            // 
            // txtModulusOfElasticity
            // 
            this.txtModulusOfElasticity.EditValue = "200000000000";
            this.txtModulusOfElasticity.Location = new System.Drawing.Point(304, 20);
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
            this.txtNodeJ.Name = "txtNodeJ";
            this.txtNodeJ.Size = new System.Drawing.Size(67, 20);
            this.txtNodeJ.TabIndex = 45;
            // 
            // txtNodeI
            // 
            this.txtNodeI.EditValue = "1";
            this.txtNodeI.Location = new System.Drawing.Point(201, 19);
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
            this.groupBox3.Size = new System.Drawing.Size(622, 292);
            this.groupBox3.TabIndex = 48;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Nodes Information";
            // 
            // gcNodes
            // 
            this.gcNodes.Location = new System.Drawing.Point(6, 85);
            this.gcNodes.MainView = this.gvNodes;
            this.gcNodes.Name = "gcNodes";
            this.gcNodes.Size = new System.Drawing.Size(610, 201);
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
            this.txtNodeY.Name = "txtNodeY";
            this.txtNodeY.Size = new System.Drawing.Size(67, 20);
            this.txtNodeY.TabIndex = 43;
            // 
            // txtNodeX
            // 
            this.txtNodeX.EditValue = "0";
            this.txtNodeX.Location = new System.Drawing.Point(193, 20);
            this.txtNodeX.Name = "txtNodeX";
            this.txtNodeX.Size = new System.Drawing.Size(67, 20);
            this.txtNodeX.TabIndex = 42;
            // 
            // txtNodeIDForNodes
            // 
            this.txtNodeIDForNodes.EditValue = "1";
            this.txtNodeIDForNodes.Location = new System.Drawing.Point(64, 38);
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
            this.pictureBox1.Location = new System.Drawing.Point(1175, 461);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(101, 311);
            this.pictureBox1.TabIndex = 47;
            this.pictureBox1.TabStop = false;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Solve";
            this.barButtonItem1.Id = 1;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // FrameSolver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1288, 804);
            this.Controls.Add(this.chartDrawing);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Name = "FrameSolver";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "FrameSolver";
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDrawing)).EndInit();
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraCharts.ChartControl chartDrawing;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.TextEdit txtMemberLabel;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl gcElements;
        private DevExpress.XtraGrid.Views.Grid.GridView gvElements;
        private DevExpress.XtraEditors.SimpleButton btnAddElement;
        private DevExpress.XtraEditors.TextEdit txtSectionArea;
        private DevExpress.XtraEditors.TextEdit txtModulusOfElasticity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.TextEdit txtNodeJ;
        private DevExpress.XtraEditors.TextEdit txtNodeI;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraGrid.GridControl gcNodes;
        private DevExpress.XtraGrid.Views.Grid.GridView gvNodes;
        private DevExpress.XtraEditors.SimpleButton btnAddNode;
        private DevExpress.XtraEditors.TextEdit txtNodeY;
        private DevExpress.XtraEditors.TextEdit txtNodeX;
        private DevExpress.XtraEditors.TextEdit txtNodeIDForNodes;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}