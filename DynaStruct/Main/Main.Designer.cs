
namespace FESolver
{
    partial class Main
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
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.gcNodes = new DevExpress.XtraGrid.GridControl();
            this.gvNodes = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.chartDrawing = new DevExpress.XtraCharts.ChartControl();
            this.BtnAnalyze = new DevExpress.XtraEditors.SimpleButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gcLoads = new DevExpress.XtraGrid.GridControl();
            this.gvLoads = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNodeY = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtNodeX = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnAddNode = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtNodeIdLoading = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtYComponent = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtXComponent = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnAddLoad = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbSupportType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBCNodeId = new System.Windows.Forms.TextBox();
            this.N = new System.Windows.Forms.Label();
            this.btnAddRestrain = new System.Windows.Forms.Button();
            this.gcboundaryCondition = new DevExpress.XtraGrid.GridControl();
            this.gvBoundaryCondition = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Elements = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtStiffness = new System.Windows.Forms.TextBox();
            this.txtSectionArea = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddElement = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtNodeJ = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNodeI = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gcElements = new DevExpress.XtraGrid.GridControl();
            this.gvElements = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDrawing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLoads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLoads)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcboundaryCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBoundaryCondition)).BeginInit();
            this.Elements.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcElements)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvElements)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 1;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.Size = new System.Drawing.Size(1332, 158);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // gcNodes
            // 
            this.gcNodes.Location = new System.Drawing.Point(6, 114);
            this.gcNodes.MainView = this.gvNodes;
            this.gcNodes.MenuManager = this.ribbonControl1;
            this.gcNodes.Name = "gcNodes";
            this.gcNodes.Size = new System.Drawing.Size(342, 192);
            this.gcNodes.TabIndex = 1;
            this.gcNodes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvNodes});
            // 
            // gvNodes
            // 
            this.gvNodes.GridControl = this.gcNodes;
            this.gvNodes.Name = "gvNodes";
            // 
            // chartDrawing
            // 
            this.chartDrawing.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartDrawing.Location = new System.Drawing.Point(738, 495);
            this.chartDrawing.Name = "chartDrawing";
            this.chartDrawing.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartDrawing.Size = new System.Drawing.Size(494, 331);
            this.chartDrawing.TabIndex = 3;
            // 
            // BtnAnalyze
            // 
            this.BtnAnalyze.Location = new System.Drawing.Point(1145, 298);
            this.BtnAnalyze.Name = "BtnAnalyze";
            this.BtnAnalyze.Size = new System.Drawing.Size(130, 42);
            this.BtnAnalyze.TabIndex = 4;
            this.BtnAnalyze.Text = "Analyze";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(1240, 495);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 331);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // gcLoads
            // 
            this.gcLoads.Location = new System.Drawing.Point(6, 114);
            this.gcLoads.MainView = this.gvLoads;
            this.gcLoads.MenuManager = this.ribbonControl1;
            this.gcLoads.Name = "gcLoads";
            this.gcLoads.Size = new System.Drawing.Size(345, 192);
            this.gcLoads.TabIndex = 7;
            this.gcLoads.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLoads});
            // 
            // gvLoads
            // 
            this.gvLoads.GridControl = this.gcLoads;
            this.gvLoads.Name = "gvLoads";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNodeY);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtNodeX);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.btnAddNode);
            this.groupBox1.Controls.Add(this.gcNodes);
            this.groupBox1.Location = new System.Drawing.Point(14, 171);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 312);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nodes Info";
            // 
            // txtNodeY
            // 
            this.txtNodeY.Location = new System.Drawing.Point(58, 56);
            this.txtNodeY.Name = "txtNodeY";
            this.txtNodeY.Size = new System.Drawing.Size(67, 21);
            this.txtNodeY.TabIndex = 26;
            this.txtNodeY.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 59);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "Ycoord";
            // 
            // txtNodeX
            // 
            this.txtNodeX.Location = new System.Drawing.Point(58, 29);
            this.txtNodeX.Name = "txtNodeX";
            this.txtNodeX.Size = new System.Drawing.Size(67, 21);
            this.txtNodeX.TabIndex = 24;
            this.txtNodeX.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 32);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(40, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "Xcoord";
            // 
            // btnAddNode
            // 
            this.btnAddNode.Location = new System.Drawing.Point(151, 38);
            this.btnAddNode.Name = "btnAddNode";
            this.btnAddNode.Size = new System.Drawing.Size(66, 34);
            this.btnAddNode.TabIndex = 22;
            this.btnAddNode.Text = "Add Node";
            this.btnAddNode.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtNodeIdLoading);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.gcLoads);
            this.groupBox2.Controls.Add(this.txtYComponent);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtXComponent);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.btnAddLoad);
            this.groupBox2.Location = new System.Drawing.Point(375, 171);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(357, 312);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Loading Info";
            // 
            // txtNodeIdLoading
            // 
            this.txtNodeIdLoading.Location = new System.Drawing.Point(64, 29);
            this.txtNodeIdLoading.Name = "txtNodeIdLoading";
            this.txtNodeIdLoading.Size = new System.Drawing.Size(67, 21);
            this.txtNodeIdLoading.TabIndex = 30;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Node ID";
            // 
            // txtYComponent
            // 
            this.txtYComponent.Location = new System.Drawing.Point(64, 83);
            this.txtYComponent.Name = "txtYComponent";
            this.txtYComponent.Size = new System.Drawing.Size(67, 21);
            this.txtYComponent.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "Y";
            // 
            // txtXComponent
            // 
            this.txtXComponent.Location = new System.Drawing.Point(64, 56);
            this.txtXComponent.Name = "txtXComponent";
            this.txtXComponent.Size = new System.Drawing.Size(67, 21);
            this.txtXComponent.TabIndex = 26;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(45, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "X";
            // 
            // btnAddLoad
            // 
            this.btnAddLoad.Location = new System.Drawing.Point(159, 37);
            this.btnAddLoad.Name = "btnAddLoad";
            this.btnAddLoad.Size = new System.Drawing.Size(66, 36);
            this.btnAddLoad.TabIndex = 24;
            this.btnAddLoad.Text = "Add Load";
            this.btnAddLoad.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbSupportType);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtBCNodeId);
            this.groupBox3.Controls.Add(this.N);
            this.groupBox3.Controls.Add(this.btnAddRestrain);
            this.groupBox3.Controls.Add(this.gcboundaryCondition);
            this.groupBox3.Location = new System.Drawing.Point(738, 171);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(357, 312);
            this.groupBox3.TabIndex = 31;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Boundary Conditions";
            // 
            // cmbSupportType
            // 
            this.cmbSupportType.FormattingEnabled = true;
            this.cmbSupportType.Location = new System.Drawing.Point(94, 55);
            this.cmbSupportType.Name = "cmbSupportType";
            this.cmbSupportType.Size = new System.Drawing.Size(67, 21);
            this.cmbSupportType.TabIndex = 26;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Restrained Dir";
            // 
            // txtBCNodeId
            // 
            this.txtBCNodeId.Location = new System.Drawing.Point(94, 28);
            this.txtBCNodeId.Name = "txtBCNodeId";
            this.txtBCNodeId.Size = new System.Drawing.Size(67, 21);
            this.txtBCNodeId.TabIndex = 24;
            // 
            // N
            // 
            this.N.AutoSize = true;
            this.N.Location = new System.Drawing.Point(42, 31);
            this.N.Name = "N";
            this.N.Size = new System.Drawing.Size(46, 13);
            this.N.TabIndex = 23;
            this.N.Text = "Node ID";
            // 
            // btnAddRestrain
            // 
            this.btnAddRestrain.Location = new System.Drawing.Point(181, 38);
            this.btnAddRestrain.Name = "btnAddRestrain";
            this.btnAddRestrain.Size = new System.Drawing.Size(66, 34);
            this.btnAddRestrain.TabIndex = 22;
            this.btnAddRestrain.Text = "Add Constraint";
            this.btnAddRestrain.UseVisualStyleBackColor = true;
            // 
            // gcboundaryCondition
            // 
            this.gcboundaryCondition.Location = new System.Drawing.Point(6, 114);
            this.gcboundaryCondition.MainView = this.gvBoundaryCondition;
            this.gcboundaryCondition.MenuManager = this.ribbonControl1;
            this.gcboundaryCondition.Name = "gcboundaryCondition";
            this.gcboundaryCondition.Size = new System.Drawing.Size(345, 192);
            this.gcboundaryCondition.TabIndex = 7;
            this.gcboundaryCondition.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBoundaryCondition});
            // 
            // gvBoundaryCondition
            // 
            this.gvBoundaryCondition.GridControl = this.gcboundaryCondition;
            this.gvBoundaryCondition.Name = "gvBoundaryCondition";
            // 
            // Elements
            // 
            this.Elements.Controls.Add(this.groupBox5);
            this.Elements.Controls.Add(this.btnAddElement);
            this.Elements.Controls.Add(this.groupBox6);
            this.Elements.Controls.Add(this.gcElements);
            this.Elements.Location = new System.Drawing.Point(12, 489);
            this.Elements.Name = "Elements";
            this.Elements.Size = new System.Drawing.Size(718, 337);
            this.Elements.TabIndex = 32;
            this.Elements.TabStop = false;
            this.Elements.Text = "Nodes Info";
            // 
            // groupBox5
            // 
            this.groupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuPopup;
            this.groupBox5.Controls.Add(this.txtStiffness);
            this.groupBox5.Controls.Add(this.txtSectionArea);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Location = new System.Drawing.Point(6, 21);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(171, 89);
            this.groupBox5.TabIndex = 34;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Section Info";
            // 
            // txtStiffness
            // 
            this.txtStiffness.Location = new System.Drawing.Point(58, 29);
            this.txtStiffness.Name = "txtStiffness";
            this.txtStiffness.Size = new System.Drawing.Size(85, 21);
            this.txtStiffness.TabIndex = 1;
            // 
            // txtSectionArea
            // 
            this.txtSectionArea.Location = new System.Drawing.Point(58, 56);
            this.txtSectionArea.Name = "txtSectionArea";
            this.txtSectionArea.Size = new System.Drawing.Size(85, 21);
            this.txtSectionArea.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "E";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "A";
            // 
            // btnAddElement
            // 
            this.btnAddElement.Location = new System.Drawing.Point(396, 44);
            this.btnAddElement.Name = "btnAddElement";
            this.btnAddElement.Size = new System.Drawing.Size(86, 49);
            this.btnAddElement.TabIndex = 33;
            this.btnAddElement.Text = "Add";
            this.btnAddElement.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtNodeJ);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.txtNodeI);
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Location = new System.Drawing.Point(195, 21);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(183, 89);
            this.groupBox6.TabIndex = 32;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Nodes Info";
            // 
            // txtNodeJ
            // 
            this.txtNodeJ.Location = new System.Drawing.Point(72, 56);
            this.txtNodeJ.Name = "txtNodeJ";
            this.txtNodeJ.Size = new System.Drawing.Size(85, 21);
            this.txtNodeJ.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Node J-ID";
            // 
            // txtNodeI
            // 
            this.txtNodeI.Location = new System.Drawing.Point(72, 29);
            this.txtNodeI.Name = "txtNodeI";
            this.txtNodeI.Size = new System.Drawing.Size(85, 21);
            this.txtNodeI.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Node I-ID";
            // 
            // gcElements
            // 
            this.gcElements.Location = new System.Drawing.Point(6, 116);
            this.gcElements.MainView = this.gvElements;
            this.gcElements.MenuManager = this.ribbonControl1;
            this.gcElements.Name = "gcElements";
            this.gcElements.Size = new System.Drawing.Size(706, 221);
            this.gcElements.TabIndex = 31;
            this.gcElements.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvElements});
            // 
            // gvElements
            // 
            this.gvElements.GridControl = this.gcElements;
            this.gvElements.Name = "gvElements";
            // 
            // FESolver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1332, 831);
            this.Controls.Add(this.Elements);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.BtnAnalyze);
            this.Controls.Add(this.chartDrawing);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "FESolver";
            this.Ribbon = this.ribbonControl1;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNodes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNodes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDrawing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLoads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLoads)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcboundaryCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBoundaryCondition)).EndInit();
            this.Elements.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcElements)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvElements)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraGrid.GridControl gcNodes;
        private DevExpress.XtraGrid.Views.Grid.GridView gvNodes;
        private DevExpress.XtraCharts.ChartControl chartDrawing;
        private DevExpress.XtraEditors.SimpleButton BtnAnalyze;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraGrid.GridControl gcLoads;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLoads;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNodeY;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtNodeX;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnAddNode;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtNodeIdLoading;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtYComponent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtXComponent;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnAddLoad;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbSupportType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBCNodeId;
        private System.Windows.Forms.Label N;
        private System.Windows.Forms.Button btnAddRestrain;
        private DevExpress.XtraGrid.GridControl gcboundaryCondition;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBoundaryCondition;
        private System.Windows.Forms.GroupBox Elements;
        private DevExpress.XtraGrid.GridControl gcElements;
        private DevExpress.XtraGrid.Views.Grid.GridView gvElements;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtStiffness;
        private System.Windows.Forms.TextBox txtSectionArea;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddElement;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtNodeJ;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNodeI;
        private System.Windows.Forms.Label label4;
    }
}

