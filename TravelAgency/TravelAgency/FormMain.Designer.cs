namespace TravelAgency
{
    partial class FormMain
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Menu = new System.Windows.Forms.MenuStrip();
            this.справочникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.компонентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изделияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.турыDocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.турыXlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.гидыPdfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заказыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заказатьГидыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.посмотретьДоступныеГидыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonCreateOrder = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.ButtonPayOrder = new System.Windows.Forms.Button();
            this.ButtonOrderReady = new System.Windows.Forms.Button();
            this.ButtonTakeOrderInWork = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.Menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView.Location = new System.Drawing.Point(0, 22);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.Size = new System.Drawing.Size(766, 297);
            this.dataGridView.TabIndex = 0;
            // 
            // Menu
            // 
            this.Menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникиToolStripMenuItem,
            this.отчетыToolStripMenuItem,
            this.заказыToolStripMenuItem});
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Name = "Menu";
            this.Menu.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.Menu.Size = new System.Drawing.Size(940, 24);
            this.Menu.TabIndex = 1;
            this.Menu.Text = "menuStrip1";
            // 
            // справочникиToolStripMenuItem
            // 
            this.справочникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.компонентыToolStripMenuItem,
            this.изделияToolStripMenuItem});
            this.справочникиToolStripMenuItem.Name = "справочникиToolStripMenuItem";
            this.справочникиToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.справочникиToolStripMenuItem.Text = "Справочники";
            // 
            // компонентыToolStripMenuItem
            // 
            this.компонентыToolStripMenuItem.Name = "компонентыToolStripMenuItem";
            this.компонентыToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.компонентыToolStripMenuItem.Text = "Гиды";
            this.компонентыToolStripMenuItem.Click += new System.EventHandler(this.GuidesToolStripMenuItem_Click);
            // 
            // изделияToolStripMenuItem
            // 
            this.изделияToolStripMenuItem.Name = "изделияToolStripMenuItem";
            this.изделияToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.изделияToolStripMenuItem.Text = "Туры";
            this.изделияToolStripMenuItem.Click += new System.EventHandler(this.ToursToolStripMenuItem_Click);
            // 
            // отчетыToolStripMenuItem
            // 
            this.отчетыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.турыDocToolStripMenuItem,
            this.турыXlsToolStripMenuItem,
            this.гидыPdfToolStripMenuItem});
            this.отчетыToolStripMenuItem.Name = "отчетыToolStripMenuItem";
            this.отчетыToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.отчетыToolStripMenuItem.Text = "Отчеты";
            // 
            // турыDocToolStripMenuItem
            // 
            this.турыDocToolStripMenuItem.Name = "турыDocToolStripMenuItem";
            this.турыDocToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.турыDocToolStripMenuItem.Text = "Блюда (doc)";
            this.турыDocToolStripMenuItem.Click += new System.EventHandler(this.блюдаDocToolStripMenuItem_Click);
            // 
            // турыXlsToolStripMenuItem
            // 
            this.турыXlsToolStripMenuItem.Name = "турыXlsToolStripMenuItem";
            this.турыXlsToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.турыXlsToolStripMenuItem.Text = "Блюда (xls)";
            this.турыXlsToolStripMenuItem.Click += new System.EventHandler(this.блюдаXlsToolStripMenuItem_Click);
            // 
            // гидыPdfToolStripMenuItem
            // 
            this.гидыPdfToolStripMenuItem.Name = "гидыPdfToolStripMenuItem";
            this.гидыPdfToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.гидыPdfToolStripMenuItem.Text = "Движение продуктов (pdf)";
            this.гидыPdfToolStripMenuItem.Click += new System.EventHandler(this.продуктыPdfToolStripMenuItem_Click);
            // 
            // заказыToolStripMenuItem
            // 
            this.заказыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.заказатьГидыToolStripMenuItem,
            this.посмотретьДоступныеГидыToolStripMenuItem});
            this.заказыToolStripMenuItem.Name = "заказыToolStripMenuItem";
            this.заказыToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.заказыToolStripMenuItem.Text = "Заказы";
            // 
            // заказатьГидыToolStripMenuItem
            // 
            this.заказатьГидыToolStripMenuItem.Name = "заказатьГидыToolStripMenuItem";
            this.заказатьГидыToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.заказатьГидыToolStripMenuItem.Text = "Заказать продукты";
            this.заказатьГидыToolStripMenuItem.Click += new System.EventHandler(this.заказатьПродуктыToolStripMenuItem_Click);
            // 
            // посмотретьДоступныеГидыToolStripMenuItem
            // 
            this.посмотретьДоступныеГидыToolStripMenuItem.Name = "посмотретьДоступныеГидыToolStripMenuItem";
            this.посмотретьДоступныеГидыToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.посмотретьДоступныеГидыToolStripMenuItem.Text = "Посмотреть доступных гидов";
            this.посмотретьДоступныеГидыToolStripMenuItem.Click += new System.EventHandler(this.посмотретьДоступныеПродуктыToolStripMenuItem_Click);
            // 
            // buttonCreateOrder
            // 
            this.buttonCreateOrder.Location = new System.Drawing.Point(772, 22);
            this.buttonCreateOrder.Name = "buttonCreateOrder";
            this.buttonCreateOrder.Size = new System.Drawing.Size(154, 26);
            this.buttonCreateOrder.TabIndex = 2;
            this.buttonCreateOrder.Text = "Создать заказ";
            this.buttonCreateOrder.UseVisualStyleBackColor = true;
            this.buttonCreateOrder.Click += new System.EventHandler(this.ButtonCreateOrder_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(772, 210);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(154, 26);
            this.buttonRefresh.TabIndex = 6;
            this.buttonRefresh.Text = "Обновить список";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.ButtonRef_Click);
            // 
            // ButtonPayOrder
            // 
            this.ButtonPayOrder.Location = new System.Drawing.Point(772, 119);
            this.ButtonPayOrder.Name = "ButtonPayOrder";
            this.ButtonPayOrder.Size = new System.Drawing.Size(154, 26);
            this.ButtonPayOrder.TabIndex = 7;
            this.ButtonPayOrder.Text = "Заказ оплачен";
            this.ButtonPayOrder.UseVisualStyleBackColor = true;
            this.ButtonPayOrder.Click += new System.EventHandler(this.ButtonPayOrder_Click);
            // 
            // ButtonOrderReady
            // 
            this.ButtonOrderReady.Location = new System.Drawing.Point(772, 87);
            this.ButtonOrderReady.Name = "ButtonOrderReady";
            this.ButtonOrderReady.Size = new System.Drawing.Size(154, 26);
            this.ButtonOrderReady.TabIndex = 8;
            this.ButtonOrderReady.Text = "Заказ готов";
            this.ButtonOrderReady.UseVisualStyleBackColor = true;
            this.ButtonOrderReady.Click += new System.EventHandler(this.ButtonOrderReady_Click);
            // 
            // ButtonTakeOrderInWork
            // 
            this.ButtonTakeOrderInWork.Location = new System.Drawing.Point(772, 54);
            this.ButtonTakeOrderInWork.Name = "ButtonTakeOrderInWork";
            this.ButtonTakeOrderInWork.Size = new System.Drawing.Size(154, 26);
            this.ButtonTakeOrderInWork.TabIndex = 9;
            this.ButtonTakeOrderInWork.Text = "Выполнить";
            this.ButtonTakeOrderInWork.UseVisualStyleBackColor = true;
            this.ButtonTakeOrderInWork.Click += new System.EventHandler(this.ButtonTakeOrderInWork_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 319);
            this.Controls.Add(this.ButtonTakeOrderInWork);
            this.Controls.Add(this.ButtonOrderReady);
            this.Controls.Add(this.ButtonPayOrder);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.buttonCreateOrder);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.Menu);
            this.MainMenuStrip = this.Menu;
            this.Name = "FormMain";
            this.Text = "Тур фирма";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private new System.Windows.Forms.MenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem справочникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem компонентыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изделияToolStripMenuItem;
        private System.Windows.Forms.Button buttonCreateOrder;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.ToolStripMenuItem отчетыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem турыDocToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem турыXlsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem гидыPdfToolStripMenuItem;
        private System.Windows.Forms.Button ButtonPayOrder;
        private System.Windows.Forms.Button ButtonOrderReady;
        private System.Windows.Forms.Button ButtonTakeOrderInWork;
        private System.Windows.Forms.ToolStripMenuItem заказыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem заказатьГидыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem посмотретьДоступныеГидыToolStripMenuItem;
    }
}