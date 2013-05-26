namespace RVMS.Win.Dialogs
{
    partial class IzmenaRelacije
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IzmenaRelacije));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.medjustanicnoRastojanjeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.searchLookUpEdit2 = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.stajalisteDTOBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.searchLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNaziv1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOpstina1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.searchLookUpEdit1 = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNaziv = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOpstina = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.medjustanicnoRastojanjeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stajalisteDTOBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnOk);
            this.layoutControl1.Controls.Add(this.textEdit2);
            this.layoutControl1.Controls.Add(this.textEdit1);
            this.layoutControl1.Controls.Add(this.searchLookUpEdit2);
            this.layoutControl1.Controls.Add(this.searchLookUpEdit1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(358, 149);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(278, 108);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Zatvori";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(204, 108);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(70, 22);
            this.btnOk.StyleController = this.layoutControl1;
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "Prihvati";
            // 
            // textEdit2
            // 
            this.textEdit2.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.medjustanicnoRastojanjeBindingSource, "VremeVoznje", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textEdit2.Location = new System.Drawing.Point(107, 84);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Properties.Appearance.Options.UseTextOptions = true;
            this.textEdit2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textEdit2.Properties.Mask.EditMask = "n0";
            this.textEdit2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEdit2.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.textEdit2.Size = new System.Drawing.Size(239, 20);
            this.textEdit2.StyleController = this.layoutControl1;
            this.textEdit2.TabIndex = 7;
            // 
            // medjustanicnoRastojanjeBindingSource
            // 
            this.medjustanicnoRastojanjeBindingSource.DataSource = typeof(RVMS.Model.Entities.MedjustanicnoRastojanje);
            // 
            // textEdit1
            // 
            this.textEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.medjustanicnoRastojanjeBindingSource, "Rastojanje", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textEdit1.Location = new System.Drawing.Point(107, 60);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Appearance.Options.UseTextOptions = true;
            this.textEdit1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textEdit1.Properties.Mask.EditMask = "n2";
            this.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEdit1.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.textEdit1.Size = new System.Drawing.Size(239, 20);
            this.textEdit1.StyleController = this.layoutControl1;
            this.textEdit1.TabIndex = 6;
            // 
            // searchLookUpEdit2
            // 
            this.searchLookUpEdit2.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.medjustanicnoRastojanjeBindingSource, "DolaznoStajalisteId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.searchLookUpEdit2.EditValue = "";
            this.searchLookUpEdit2.Location = new System.Drawing.Point(107, 36);
            this.searchLookUpEdit2.Name = "searchLookUpEdit2";
            this.searchLookUpEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchLookUpEdit2.Properties.DataSource = this.stajalisteDTOBindingSource;
            this.searchLookUpEdit2.Properties.DisplayMember = "Naziv";
            this.searchLookUpEdit2.Properties.NullText = "";
            this.searchLookUpEdit2.Properties.ShowClearButton = false;
            this.searchLookUpEdit2.Properties.ValueMember = "Id";
            this.searchLookUpEdit2.Properties.View = this.searchLookUpEdit2View;
            this.searchLookUpEdit2.Size = new System.Drawing.Size(239, 20);
            this.searchLookUpEdit2.StyleController = this.layoutControl1;
            this.searchLookUpEdit2.TabIndex = 5;
            // 
            // stajalisteDTOBindingSource
            // 
            this.stajalisteDTOBindingSource.DataSource = typeof(RVMS.Model.DTO.StajalisteDTO);
            // 
            // searchLookUpEdit2View
            // 
            this.searchLookUpEdit2View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNaziv1,
            this.colOpstina1});
            this.searchLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit2View.Name = "searchLookUpEdit2View";
            this.searchLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            // 
            // colNaziv1
            // 
            this.colNaziv1.Caption = "Naziv stajališta";
            this.colNaziv1.FieldName = "Naziv";
            this.colNaziv1.Name = "colNaziv1";
            this.colNaziv1.Visible = true;
            this.colNaziv1.VisibleIndex = 0;
            // 
            // colOpstina1
            // 
            this.colOpstina1.Caption = "Opština";
            this.colOpstina1.FieldName = "Opstina";
            this.colOpstina1.Name = "colOpstina1";
            this.colOpstina1.Visible = true;
            this.colOpstina1.VisibleIndex = 1;
            // 
            // searchLookUpEdit1
            // 
            this.searchLookUpEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.medjustanicnoRastojanjeBindingSource, "PolaznoStajalisteId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.searchLookUpEdit1.Location = new System.Drawing.Point(107, 12);
            this.searchLookUpEdit1.Name = "searchLookUpEdit1";
            this.searchLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchLookUpEdit1.Properties.DataSource = this.stajalisteDTOBindingSource;
            this.searchLookUpEdit1.Properties.DisplayMember = "Naziv";
            this.searchLookUpEdit1.Properties.NullText = "";
            this.searchLookUpEdit1.Properties.ShowClearButton = false;
            this.searchLookUpEdit1.Properties.ValueMember = "Id";
            this.searchLookUpEdit1.Properties.View = this.searchLookUpEdit1View;
            this.searchLookUpEdit1.Size = new System.Drawing.Size(239, 20);
            this.searchLookUpEdit1.StyleController = this.layoutControl1;
            this.searchLookUpEdit1.TabIndex = 4;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNaziv,
            this.colOpstina});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // colNaziv
            // 
            this.colNaziv.Caption = "Stajalište";
            this.colNaziv.FieldName = "Naziv";
            this.colNaziv.Name = "colNaziv";
            this.colNaziv.Visible = true;
            this.colNaziv.VisibleIndex = 0;
            // 
            // colOpstina
            // 
            this.colOpstina.Caption = "Opština";
            this.colOpstina.FieldName = "Opstina";
            this.colOpstina.Name = "colOpstina";
            this.colOpstina.Visible = true;
            this.colOpstina.VisibleIndex = 1;
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
            this.layoutControlItem7,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(358, 149);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.searchLookUpEdit1;
            this.layoutControlItem1.CustomizationFormText = "Polazno stajalište";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(338, 24);
            this.layoutControlItem1.Text = "Polazno stajalište";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(92, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.searchLookUpEdit2;
            this.layoutControlItem2.CustomizationFormText = "Dolazno stajalište";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(338, 24);
            this.layoutControlItem2.Text = "Dolazno stajalište";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(92, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.textEdit1;
            this.layoutControlItem3.CustomizationFormText = "Rastojanje [km]";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(338, 24);
            this.layoutControlItem3.Text = "Rastojanje [km]";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(92, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.textEdit2;
            this.layoutControlItem4.CustomizationFormText = "Vreme vožnje [min]";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(338, 24);
            this.layoutControlItem4.Text = "Vreme vožnje [min]";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(92, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnOk;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(192, 96);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(74, 33);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnCancel;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(266, 96);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(72, 33);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 96);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(192, 33);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.medjustanicnoRastojanjeBindingSource;
            // 
            // IzmenaRelacije
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(358, 149);
            this.Controls.Add(this.layoutControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IzmenaRelacije";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Izmena relacije";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.medjustanicnoRastojanjeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stajalisteDTOBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.SearchLookUpEdit searchLookUpEdit2;
        private System.Windows.Forms.BindingSource stajalisteDTOBindingSource;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit2View;
        private DevExpress.XtraGrid.Columns.GridColumn colNaziv1;
        private DevExpress.XtraGrid.Columns.GridColumn colOpstina1;
        private DevExpress.XtraEditors.SearchLookUpEdit searchLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colNaziv;
        private DevExpress.XtraGrid.Columns.GridColumn colOpstina;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private System.Windows.Forms.BindingSource medjustanicnoRastojanjeBindingSource;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
    }
}