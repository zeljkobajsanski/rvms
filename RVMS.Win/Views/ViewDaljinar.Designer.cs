namespace RVMS.Win.Views
{
    partial class ViewDaljinar
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.relacijaDTOBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNaziv = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDuzinaRelacije = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVremeVoznje = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSrednjaSaobracajnaBrzina = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.relacijaDTOBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(606, 531);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(606, 531);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.relacijaDTOBindingSource;
            this.gridControl1.Location = new System.Drawing.Point(12, 12);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(582, 507);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colNaziv,
            this.colDuzinaRelacije,
            this.colVremeVoznje,
            this.colSrednjaSaobracajnaBrzina});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(586, 511);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // relacijaDTOBindingSource
            // 
            this.relacijaDTOBindingSource.DataSource = typeof(RVMS.Model.DTO.RelacijaDTO);
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            this.colId.Width = 42;
            // 
            // colNaziv
            // 
            this.colNaziv.FieldName = "Naziv";
            this.colNaziv.Name = "colNaziv";
            this.colNaziv.Visible = true;
            this.colNaziv.VisibleIndex = 0;
            this.colNaziv.Width = 129;
            // 
            // colDuzinaRelacije
            // 
            this.colDuzinaRelacije.FieldName = "DuzinaRelacije";
            this.colDuzinaRelacije.Name = "colDuzinaRelacije";
            this.colDuzinaRelacije.Visible = true;
            this.colDuzinaRelacije.VisibleIndex = 1;
            this.colDuzinaRelacije.Width = 129;
            // 
            // colVremeVoznje
            // 
            this.colVremeVoznje.FieldName = "VremeVoznje";
            this.colVremeVoznje.Name = "colVremeVoznje";
            this.colVremeVoznje.Visible = true;
            this.colVremeVoznje.VisibleIndex = 2;
            this.colVremeVoznje.Width = 129;
            // 
            // colSrednjaSaobracajnaBrzina
            // 
            this.colSrednjaSaobracajnaBrzina.FieldName = "SrednjaSaobracajnaBrzina";
            this.colSrednjaSaobracajnaBrzina.Name = "colSrednjaSaobracajnaBrzina";
            this.colSrednjaSaobracajnaBrzina.Visible = true;
            this.colSrednjaSaobracajnaBrzina.VisibleIndex = 3;
            this.colSrednjaSaobracajnaBrzina.Width = 135;
            // 
            // ViewDaljinar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ViewDaljinar";
            this.Size = new System.Drawing.Size(606, 531);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.relacijaDTOBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.BindingSource relacijaDTOBindingSource;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colNaziv;
        private DevExpress.XtraGrid.Columns.GridColumn colDuzinaRelacije;
        private DevExpress.XtraGrid.Columns.GridColumn colVremeVoznje;
        private DevExpress.XtraGrid.Columns.GridColumn colSrednjaSaobracajnaBrzina;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}
