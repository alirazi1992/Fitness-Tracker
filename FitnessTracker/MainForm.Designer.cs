namespace FitnessTracker
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.TabPage tabHistory;
        private System.Windows.Forms.TabPage tabProgress;

        // Log tab
        private System.Windows.Forms.DateTimePicker dtDate;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.NumericUpDown numReps;
        private System.Windows.Forms.NumericUpDown numDuration;
        private System.Windows.Forms.NumericUpDown numCalories;
        private System.Windows.Forms.Button btnSave;

        // History tab
        private System.Windows.Forms.ComboBox cmbRange;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExportCsv;
        private System.Windows.Forms.DataGridView grid;

        // Progress tab
        private System.Windows.Forms.ComboBox cmbMetric;
        private System.Windows.Forms.ComboBox cmbRangeProg;
        private System.Windows.Forms.DateTimePicker dtFromProg;
        private System.Windows.Forms.DateTimePicker dtToProg;
        private System.Windows.Forms.Button btnRefreshProg;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.tabProgress = new System.Windows.Forms.TabPage();

            // --- Log tab controls
            this.dtDate = new System.Windows.Forms.DateTimePicker();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.numReps = new System.Windows.Forms.NumericUpDown();
            this.numDuration = new System.Windows.Forms.NumericUpDown();
            this.numCalories = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();

            // --- History tab controls
            this.cmbRange = new System.Windows.Forms.ComboBox();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExportCsv = new System.Windows.Forms.Button();
            this.grid = new System.Windows.Forms.DataGridView();

            // --- Progress tab controls
            this.cmbMetric = new System.Windows.Forms.ComboBox();
            this.cmbRangeProg = new System.Windows.Forms.ComboBox();
            this.dtFromProg = new System.Windows.Forms.DateTimePicker();
            this.dtToProg = new System.Windows.Forms.DateTimePicker();
            this.btnRefreshProg = new System.Windows.Forms.Button();
            this.lblSummary = new System.Windows.Forms.Label();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();

            // ===== Form =====
            this.SuspendLayout();
            this.Text = "🏋️ Fitness Tracker";
            this.ClientSize = new System.Drawing.Size(980, 640);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            // ===== Tabs =====
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Controls.Add(this.tabLog);
            this.tabs.Controls.Add(this.tabHistory);
            this.tabs.Controls.Add(this.tabProgress);

            // =======================
            //       LOG TAB
            // =======================
            this.tabLog.Text = "Log Workout";
            this.tabLog.Padding = new System.Windows.Forms.Padding(12);

            this.dtDate.Location = new System.Drawing.Point(24, 24);
            this.dtDate.Width = 220;

            this.cmbType.Location = new System.Drawing.Point(24, 64);
            this.cmbType.Width = 220;
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.numReps.Location = new System.Drawing.Point(24, 104);
            this.numReps.Maximum = 10000;

            this.numDuration.Location = new System.Drawing.Point(24, 144);
            this.numDuration.Maximum = 10000;
            this.numDuration.DecimalPlaces = 1;

            this.numCalories.Location = new System.Drawing.Point(24, 184);
            this.numCalories.Maximum = 50000;
            this.numCalories.DecimalPlaces = 1;
            this.numCalories.Increment = 10;

            this.btnSave.Location = new System.Drawing.Point(24, 232);
            this.btnSave.Text = "Save Workout";
            this.btnSave.AutoSize = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            var lbl1 = new System.Windows.Forms.Label(); lbl1.Text = "Date"; lbl1.AutoSize = true; lbl1.Location = new System.Drawing.Point(260, 28);
            var lbl2 = new System.Windows.Forms.Label(); lbl2.Text = "Type"; lbl2.AutoSize = true; lbl2.Location = new System.Drawing.Point(260, 68);
            var lbl3 = new System.Windows.Forms.Label(); lbl3.Text = "Reps"; lbl3.AutoSize = true; lbl3.Location = new System.Drawing.Point(260, 108);
            var lbl4 = new System.Windows.Forms.Label(); lbl4.Text = "Duration (min)"; lbl4.AutoSize = true; lbl4.Location = new System.Drawing.Point(260, 148);
            var lbl5 = new System.Windows.Forms.Label(); lbl5.Text = "Calories"; lbl5.AutoSize = true; lbl5.Location = new System.Drawing.Point(260, 188);

            this.tabLog.Controls.Add(lbl1);
            this.tabLog.Controls.Add(lbl2);
            this.tabLog.Controls.Add(lbl3);
            this.tabLog.Controls.Add(lbl4);
            this.tabLog.Controls.Add(lbl5);
            this.tabLog.Controls.Add(this.dtDate);
            this.tabLog.Controls.Add(this.cmbType);
            this.tabLog.Controls.Add(this.numReps);
            this.tabLog.Controls.Add(this.numDuration);
            this.tabLog.Controls.Add(this.numCalories);
            this.tabLog.Controls.Add(this.btnSave);

            // =======================
            //      HISTORY TAB
            // =======================
            this.tabHistory.Text = "History";
            this.tabHistory.Padding = new System.Windows.Forms.Padding(12);

            this.cmbRange.Location = new System.Drawing.Point(24, 16);
            this.cmbRange.Width = 140;
            this.cmbRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.dtFrom.Location = new System.Drawing.Point(180, 16);
            this.dtFrom.Width = 180;
            this.dtTo.Location = new System.Drawing.Point(370, 16);
            this.dtTo.Width = 180;

            this.btnRefresh.Location = new System.Drawing.Point(560, 14);
            this.btnRefresh.Text = "Apply";
            this.btnRefresh.AutoSize = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            this.btnExportCsv.Location = new System.Drawing.Point(640, 14);
            this.btnExportCsv.Text = "Export CSV";
            this.btnExportCsv.AutoSize = true;
            this.btnExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);

            this.grid.Location = new System.Drawing.Point(24, 56);
            this.grid.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            this.grid.Size = new System.Drawing.Size(910, 520);
            this.grid.ReadOnly = true;
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.tabHistory.Controls.Add(this.cmbRange);
            this.tabHistory.Controls.Add(this.dtFrom);
            this.tabHistory.Controls.Add(this.dtTo);
            this.tabHistory.Controls.Add(this.btnRefresh);
            this.tabHistory.Controls.Add(this.btnExportCsv);
            this.tabHistory.Controls.Add(this.grid);

            // =======================
            //      PROGRESS TAB
            // =======================
            this.tabProgress.Text = "Progress";
            this.tabProgress.Padding = new System.Windows.Forms.Padding(12);

            // Row of filters
            this.cmbMetric.Location = new System.Drawing.Point(24, 16);
            this.cmbMetric.Width = 160;
            this.cmbMetric.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.cmbRangeProg.Location = new System.Drawing.Point(200, 16);
            this.cmbRangeProg.Width = 140;
            this.cmbRangeProg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.dtFromProg.Location = new System.Drawing.Point(350, 16);
            this.dtFromProg.Width = 160;
            this.dtToProg.Location = new System.Drawing.Point(520, 16);
            this.dtToProg.Width = 160;

            this.btnRefreshProg.Location = new System.Drawing.Point(700, 14);
            this.btnRefreshProg.Text = "Update Chart";
            this.btnRefreshProg.AutoSize = true;
            this.btnRefreshProg.Click += new System.EventHandler(this.btnRefreshProg_Click);

            // Summary label
            this.lblSummary.Location = new System.Drawing.Point(24, 44);
            this.lblSummary.AutoSize = true;
            this.lblSummary.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);

            // Scrollable panel for chart
            var panelChart = new System.Windows.Forms.Panel();
            panelChart.Location = new System.Drawing.Point(24, 68);
            panelChart.Size = new System.Drawing.Size(910, 500);
            panelChart.Anchor = (System.Windows.Forms.AnchorStyles.Top
                                | System.Windows.Forms.AnchorStyles.Bottom
                                | System.Windows.Forms.AnchorStyles.Left
                                | System.Windows.Forms.AnchorStyles.Right);
            panelChart.AutoScroll = true;

            // Chart (smaller)
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.chart.Location = new System.Drawing.Point(0, 0);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(600, 350); // smaller chart
            this.chart.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();

            // Add chart to the scrollable panel
            panelChart.Controls.Add(this.chart);

            // Add controls to Progress tab
            this.tabProgress.Controls.Add(this.cmbMetric);
            this.tabProgress.Controls.Add(this.cmbRangeProg);
            this.tabProgress.Controls.Add(this.dtFromProg);
            this.tabProgress.Controls.Add(this.dtToProg);
            this.tabProgress.Controls.Add(this.btnRefreshProg);
            this.tabProgress.Controls.Add(this.lblSummary);
            this.tabProgress.Controls.Add(panelChart);

            // Add tabs to form
            this.Controls.Add(this.tabs);
            this.ResumeLayout(false);
        }
    }
}
