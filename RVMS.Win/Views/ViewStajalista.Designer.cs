namespace RVMS.Win.Views
{
    partial class ViewStajalista
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.stajalistaViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.stajalisteDTOBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNaziv = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOpstina = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMesto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStanica = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDodaj = new DevExpress.XtraEditors.SimpleButton();
            this.txtNaziv = new DevExpress.XtraEditors.TextEdit();
            this.mesta = new DevExpress.XtraEditors.LookUpEdit();
            this.mestoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.opstine = new DevExpress.XtraEditors.LookUpEdit();
            this.opstinaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stajalistaViewModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stajalisteDTOBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNaziv.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mesta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mestoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.opstine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.opstinaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.checkEdit1);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.btnDodaj);
            this.layoutControl1.Controls.Add(this.txtNaziv);
            this.layoutControl1.Controls.Add(this.mesta);
            this.layoutControl1.Controls.Add(this.opstine);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1024, 768);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // checkEdit1
            // 
            this.checkEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.stajalistaViewModelBindingSource, "Stanica", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkEdit1.Location = new System.Drawing.Point(12, 88);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "Stanica";
            this.checkEdit1.Size = new System.Drawing.Size(348, 19);
            this.checkEdit1.StyleController = this.layoutControl1;
            this.checkEdit1.TabIndex = 9;
            // 
            // stajalistaViewModelBindingSource
            // 
            this.stajalistaViewModelBindingSource.DataSource = typeof(RVMS.Win.ViewModels.StajalistaViewModel);
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.stajalisteDTOBindingSource;
            this.gridControl1.Location = new System.Drawing.Point(12, 137);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1000, 619);
            this.gridControl1.TabIndex = 8;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // stajalisteDTOBindingSource
            // 
            this.stajalisteDTOBindingSource.DataSource = typeof(RVMS.Model.DTO.StajalisteDTO);
            // 
            // gridView1
            // 
            this.gridView1.ColumnPanelRowHeight = 32;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colNaziv,
            this.colOpstina,
            this.colMesto,
            this.colStanica});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            this.colId.OptionsColumn.AllowEdit = false;
            this.colId.OptionsColumn.FixedWidth = true;
            this.colId.Visible = true;
            this.colId.VisibleIndex = 0;
            this.colId.Width = 58;
            // 
            // colNaziv
            // 
            this.colNaziv.FieldName = "Naziv";
            this.colNaziv.Name = "colNaziv";
            this.colNaziv.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Naziv", "Ukupno: {0:n0}")});
            this.colNaziv.Visible = true;
            this.colNaziv.VisibleIndex = 1;
            this.colNaziv.Width = 430;
            // 
            // colOpstina
            // 
            this.colOpstina.Caption = "Opština";
            this.colOpstina.FieldName = "Opstina";
            this.colOpstina.Name = "colOpstina";
            this.colOpstina.OptionsColumn.AllowEdit = false;
            this.colOpstina.OptionsColumn.FixedWidth = true;
            this.colOpstina.Visible = true;
            this.colOpstina.VisibleIndex = 2;
            this.colOpstina.Width = 239;
            // 
            // colMesto
            // 
            this.colMesto.FieldName = "Mesto";
            this.colMesto.Name = "colMesto";
            this.colMesto.OptionsColumn.AllowEdit = false;
            this.colMesto.OptionsColumn.FixedWidth = true;
            this.colMesto.Visible = true;
            this.colMesto.VisibleIndex = 3;
            this.colMesto.Width = 205;
            // 
            // colStanica
            // 
            this.colStanica.FieldName = "Stanica";
            this.colStanica.Name = "colStanica";
            this.colStanica.OptionsColumn.FixedWidth = true;
            this.colStanica.Visible = true;
            this.colStanica.VisibleIndex = 4;
            this.colStanica.Width = 50;
            // 
            // btnDodaj
            // 
            this.btnDodaj.Location = new System.Drawing.Point(301, 111);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(59, 22);
            this.btnDodaj.StyleController = this.layoutControl1;
            this.btnDodaj.TabIndex = 7;
            this.btnDodaj.Text = "Dodaj";
            // 
            // txtNaziv
            // 
            this.txtNaziv.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.stajalistaViewModelBindingSource, "NazivStajalista", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtNaziv.EnterMoveNextControl = true;
            this.txtNaziv.Location = new System.Drawing.Point(87, 64);
            this.txtNaziv.Name = "txtNaziv";
            this.txtNaziv.Size = new System.Drawing.Size(273, 20);
            this.txtNaziv.StyleController = this.layoutControl1;
            this.txtNaziv.TabIndex = 6;
            // 
            // mesta
            // 
            this.mesta.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.stajalistaViewModelBindingSource, "IdMesta", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.mesta.EnterMoveNextControl = true;
            this.mesta.Location = new System.Drawing.Point(87, 38);
            this.mesta.Name = "mesta";
            this.mesta.Properties.ActionButtonIndex = 2;
            this.mesta.Properties.AutoSearchColumnIndex = 2;
            this.mesta.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.mesta.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::RVMS.Win.Properties.Resources.isync, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.mesta.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Naziv", "Naziv", 36, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.mesta.Properties.DataSource = this.mestoBindingSource;
            this.mesta.Properties.DisplayMember = "Naziv";
            this.mesta.Properties.NullText = "";
            this.mesta.Properties.ValueMember = "Id";
            this.mesta.Size = new System.Drawing.Size(273, 22);
            this.mesta.StyleController = this.layoutControl1;
            this.mesta.TabIndex = 5;
            // 
            // mestoBindingSource
            // 
            this.mestoBindingSource.DataSource = typeof(RVMS.Model.Entities.Mesto);
            // 
            // opstine
            // 
            this.opstine.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.stajalistaViewModelBindingSource, "IdOpstine", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.opstine.EnterMoveNextControl = true;
            this.opstine.Location = new System.Drawing.Point(87, 12);
            this.opstine.Name = "opstine";
            this.opstine.Properties.ActionButtonIndex = 2;
            this.opstine.Properties.AutoSearchColumnIndex = 2;
            this.opstine.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.opstine.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::RVMS.Win.Properties.Resources.isync, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.opstine.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NazivOpstine", "Naziv Opstine", 76, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.opstine.Properties.DataSource = this.opstinaBindingSource;
            this.opstine.Properties.DisplayMember = "NazivOpstine";
            this.opstine.Properties.NullText = "";
            this.opstine.Properties.ValueMember = "Id";
            this.opstine.Size = new System.Drawing.Size(273, 22);
            this.opstine.StyleController = this.layoutControl1;
            this.opstine.TabIndex = 4;
            // 
            // opstinaBindingSource
            // 
            this.opstinaBindingSource.DataSource = typeof(RVMS.Model.Entities.Opstina);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.layoutControlItem6});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1024, 768);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.opstine;
            this.layoutControlItem1.CustomizationFormText = "Opština";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(352, 26);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(352, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(352, 26);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Opština";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(72, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.mesta;
            this.layoutControlItem2.CustomizationFormText = "Mesto";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(352, 26);
            this.layoutControlItem2.Text = "Mesto";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(72, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtNaziv;
            this.layoutControlItem3.CustomizationFormText = "Naziv stajališta";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(352, 24);
            this.layoutControlItem3.Text = "Naziv stajališta";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(72, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnDodaj;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(289, 99);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(63, 26);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(63, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(63, 26);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.gridControl1;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 125);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(1004, 623);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(352, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(652, 125);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 99);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(289, 26);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.checkEdit1;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 76);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(352, 23);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // ViewStajalista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ViewStajalista";
            this.Size = new System.Drawing.Size(1024, 768);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stajalistaViewModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stajalisteDTOBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNaziv.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mesta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mestoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.opstine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.opstinaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnDodaj;
        private DevExpress.XtraEditors.TextEdit txtNaziv;
        private DevExpress.XtraEditors.LookUpEdit mesta;
        private DevExpress.XtraEditors.LookUpEdit opstine;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private System.Windows.Forms.BindingSource opstinaBindingSource;
        private System.Windows.Forms.BindingSource stajalistaViewModelBindingSource;
        private System.Windows.Forms.BindingSource mestoBindingSource;
        private System.Windows.Forms.BindingSource stajalisteDTOBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colNaziv;
        private DevExpress.XtraGrid.Columns.GridColumn colOpstina;
        private DevExpress.XtraGrid.Columns.GridColumn colMesto;
        private DevExpress.XtraGrid.Columns.GridColumn colStanica;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}
