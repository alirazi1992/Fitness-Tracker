using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FitnessTracker
{
    public partial class MainForm : Form
    {
        private readonly FitnessDb _db;

        public MainForm()
        {
            InitializeComponent();

            // DB init
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appDir = Path.Combine(appData, "FitnessTracker");
            if (!Directory.Exists(appDir)) Directory.CreateDirectory(appDir);
            string dbPath = Path.Combine(appDir, "fitness.db");
            _db = new FitnessDb(dbPath);
            _db.EnsureCreated();

            // UI init
            cmbType.Items.AddRange(new object[] { "Push", "Pull", "Legs", "Cardio", "Core", "Other" });
            if (cmbType.Items.Count > 0) cmbType.SelectedIndex = 0;

            cmbRange.Items.AddRange(new object[] { "All", "This Week", "This Month", "Custom" });
            cmbRange.SelectedIndex = 2;

            cmbMetric.Items.AddRange(new object[] { "Calories", "Duration (min)" });
            cmbMetric.SelectedIndex = 0;

            cmbRangeProg.Items.AddRange(new object[] { "This Week", "This Month", "Custom" });
            cmbRangeProg.SelectedIndex = 1;

            ApplyRangeDefaults();
            ApplyRangeDefaultsProgress();

            RefreshHistory();
            RefreshChart();
        }

        // ===== LOG TAB =====
        private void btnSave_Click(object sender, EventArgs e)
        {
            string type = cmbType.SelectedItem != null ? cmbType.SelectedItem.ToString() : "Other";
            int reps = (int)numReps.Value;
            double duration = (double)numDuration.Value;
            double calories = (double)numCalories.Value;
            DateTime date = dtDate.Value.Date;

            _db.AddWorkout(date, type, reps, duration, calories);

            MessageBox.Show("Workout saved.", "Fitness", MessageBoxButtons.OK, MessageBoxIcon.Information);

            RefreshHistory();
            RefreshChart();

            numReps.Value = 0;
            numDuration.Value = 0;
            numCalories.Value = 0;
        }

        // ===== HISTORY TAB =====
        private void ApplyRangeDefaults()
        {
            DateTime today = DateTime.Today;
            if (cmbRange.SelectedItem == null) return;

            string chosen = cmbRange.SelectedItem.ToString();
            if (chosen == "All")
            {
                dtFrom.Value = today.AddYears(-5);
                dtTo.Value = today.AddDays(1);
            }
            else if (chosen == "This Week")
            {
                DateTime start = StartOfWeek(today, DayOfWeek.Monday);
                dtFrom.Value = start;
                dtTo.Value = start.AddDays(7);
            }
            else if (chosen == "This Month")
            {
                DateTime first = new DateTime(today.Year, today.Month, 1);
                dtFrom.Value = first;
                dtTo.Value = first.AddMonths(1);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ApplyRangeDefaults();
            RefreshHistory();
        }

        private void RefreshHistory()
        {
            DateTime from = dtFrom.Value.Date;
            DateTime to = dtTo.Value.Date;
            var table = _db.GetWorkoutsTable(from, to);

            grid.DataSource = table;
            if (grid.Columns.Contains("date")) grid.Columns["date"].HeaderText = "Date";
            if (grid.Columns.Contains("type")) grid.Columns["type"].HeaderText = "Type";
            if (grid.Columns.Contains("reps")) grid.Columns["reps"].HeaderText = "Reps";
            if (grid.Columns.Contains("duration_min")) grid.Columns["duration_min"].HeaderText = "Duration (min)";
            if (grid.Columns.Contains("calories")) grid.Columns["calories"].HeaderText = "Calories";
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            if (grid.DataSource == null)
            {
                MessageBox.Show("No data to export.", "Fitness", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV Files (*.csv)|*.csv";
            sfd.FileName = "workouts.csv";
            if (sfd.ShowDialog() != DialogResult.OK) return;

            DataTable table = (DataTable)grid.DataSource;
            using (var sw = new StreamWriter(sfd.FileName))
            {
                // header
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    if (i > 0) sw.Write(",");
                    sw.Write(table.Columns[i].ColumnName);
                }
                sw.WriteLine();

                foreach (DataRow row in table.Rows)
                {
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        if (i > 0) sw.Write(",");
                        string cell = row[i] != null ? Convert.ToString(row[i], CultureInfo.InvariantCulture) : "";
                        if (cell.IndexOf(',') >= 0 || cell.IndexOf('"') >= 0)
                            cell = "\"" + cell.Replace("\"", "\"\"") + "\"";
                        sw.Write(cell);
                    }
                    sw.WriteLine();
                }
            }

            MessageBox.Show("CSV exported.", "Fitness", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ===== PROGRESS TAB =====
        private void ApplyRangeDefaultsProgress()
        {
            DateTime today = DateTime.Today;
            if (cmbRangeProg.SelectedItem == null) return;

            string chosen = cmbRangeProg.SelectedItem.ToString();
            if (chosen == "This Week")
            {
                DateTime start = StartOfWeek(today, DayOfWeek.Monday);
                dtFromProg.Value = start;
                dtToProg.Value = start.AddDays(7);
            }
            else if (chosen == "This Month")
            {
                DateTime first = new DateTime(today.Year, today.Month, 1);
                dtFromProg.Value = first;
                dtToProg.Value = first.AddMonths(1);
            }
        }

        private void btnRefreshProg_Click(object sender, EventArgs e)
        {
            ApplyRangeDefaultsProgress();
            RefreshChart();
        }

        private void RefreshChart()
        {
            DateTime from = dtFromProg.Value.Date;
            DateTime to = dtToProg.Value.Date;
            string metric = cmbMetric.SelectedItem != null ? cmbMetric.SelectedItem.ToString() : "Calories";

            var daily = _db.GetDailyTotals(from, to); // List<DailyTotal>

            // Style area
            chart.Series.Clear();
            chart.ChartAreas.Clear();

            var area = new ChartArea("main");
            area.BackColor = System.Drawing.Color.AliceBlue;
            area.AxisX.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            area.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            area.AxisX.LabelStyle.Format = "MM-dd";
            area.AxisX.Title = "Date";
            area.AxisY.Title = metric;
            chart.ChartAreas.Add(area);

            // Series
            var series = new Series("Progress");
            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 3;
            series.Color = System.Drawing.Color.MediumBlue;
            series.MarkerStyle = MarkerStyle.Circle;
            series.MarkerSize = 7;
            series.MarkerColor = System.Drawing.Color.Red;
            series.XValueType = ChartValueType.Date;
            chart.Series.Add(series);

            foreach (var day in daily)
            {
                double y = metric.StartsWith("Cal") ? day.TotalCalories : day.TotalDurationMin;
                series.Points.AddXY(day.Date, y);
            }

            // Summary
            if (daily.Count == 0)
            {
                lblSummary.Text = "No data found for the selected range.";
                return;
            }

            double totalCalories = 0, totalDuration = 0;
            int daysWithData = daily.Count;
            DateTime bestDay = daily[0].Date;
            double bestValue = metric.StartsWith("Cal") ? daily[0].TotalCalories : daily[0].TotalDurationMin;

            foreach (var d in daily)
            {
                totalCalories += d.TotalCalories;
                totalDuration += d.TotalDurationMin;
                double v = metric.StartsWith("Cal") ? d.TotalCalories : d.TotalDurationMin;
                if (v > bestValue) { bestValue = v; bestDay = d.Date; }
            }

            lblSummary.Text =
                string.Format("Days: {0}   Total Calories: {1:0}   Total Duration: {2:0} min   Best Day: {3:MM-dd} ({4:0})",
                              daysWithData, totalCalories, totalDuration, bestDay, bestValue);
        }

        // utils
        private static DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.Date.AddDays(-1 * diff);
        }
    }
}
