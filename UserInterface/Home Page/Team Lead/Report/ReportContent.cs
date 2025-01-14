﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeamTracker;
//using System.Windows.Forms.DataVisualization.Charting;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Media;

namespace UserInterface.Home_Page.Team_Lead.Report
{
    public partial class ReportContent : UserControl
    {

        public bool isOpened = false;
        private int month, year, priority;
        private List<System.Drawing.Color> colorList = new List<System.Drawing.Color>
        {
            System.Drawing.Color.FromArgb(3, 4, 94), System.Drawing.Color.FromArgb(2, 62, 138), System.Drawing.Color.FromArgb(0, 119, 182), System.Drawing.Color.FromArgb(0, 150, 199), System.Drawing.Color.FromArgb(0, 180, 216),System.Drawing.Color.FromArgb(72, 202, 228)
        };
        private int colorIndex = 0;
        private Random rnd = new Random();

        public ReportContent()
        {
            InitializeComponent();
        }

        public int Month
        {
            get { return month; }
            set
            {
                if (value != 0)
                {
                    month = value;
                    SetReport();
                }
            }
        }
        public int Year
        {
            get { return year; }
            set
            {
                if (value != 0)
                {
                    year = value;
                    SetReport();
                }
            }
        }
        public int Priority
        {
            get { return priority; }
            set
            {
                priority = value;
                SetReport();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void TablePanelPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(221, 230, 237));
            e.Graphics.DrawLine(pen, new Point(tableLayoutPanel4.Width / 20, tableLayoutPanel4.Height / 2), new Point(tableLayoutPanel4.Width * 19 / 20, tableLayoutPanel4.Height / 2));
            pen.Dispose();
        }

        private void OnFilterClick(object sender, EventArgs e)
        {
            FilterForm form = new FilterForm();
            form.Month = month; form.Year = year; form.Priority = priority;
            form.Location = filterPicBox.PointToScreen(new Point(-225, 0));
            form.Filter += OnFiltered;
            form.Show();
        }

        private void OnFiltered(int month, int year, int priority)
        {
            Month = month; Year = year; Priority = priority;
        }

        private void SetReport()
        {
            if (isOpened)
            {
                totalTaskCount.Text = TaskManager.FilterTaskCount(month, year, priority).ToString();
                totalmilestoneCount.Text = MilestoneManager.FilterMilestoneCount(month, year).ToString();
                Dictionary<string, int> result1 = TaskManager.FilterTeamMemberTaskCount(month, year, priority);
                Dictionary<string, Dictionary<DateTime, int>> result2 = TaskManager.FilterTaskCountByDate(month, year);

                int total = 0;
                foreach (var Iter in result1) total += Iter.Value;
                teammatesTaskCount.Text = total.ToString();

                SeriesCollection seriesCollection = new SeriesCollection();
                foreach(var Iter in result1)
                {
                    seriesCollection.Add(new PieSeries { Title = Iter.Key, Values = new ChartValues<double> { Iter.Value }, Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(colorList[colorIndex].A, colorList[colorIndex].R, colorList[colorIndex].G, colorList[colorIndex].B)) });
                    colorIndex = (colorIndex + 1) % colorList.Count;
                }
                pieChart1.Series = seriesCollection;

                //donutChart.Series[0] = new Series();
                //donutChart.Series[0].ChartType = SeriesChartType.Doughnut;
                //foreach (var Iter in result1)
                //{
                //    donutChart.Series[0].Points.AddXY(Iter.Key, Iter.Value);
                //}

                //donutChart.Legends["Legend1"].ForeColor = Color.Black;


                cartesianChart1.Series.Clear();

                cartesianChart1.AxisX.Add(new Axis { Title = "Date" });
                cartesianChart1.AxisY.Add(new Axis { Title = "Task Solved" });

                foreach (var employeeData in result2)
                {
                    var lineSeries = new LineSeries
                    {
                        Title = employeeData.Key,
                        Values = new ChartValues<int>(employeeData.Value.OrderBy(kv => kv.Key).Select(kv => kv.Value)),
                        Stroke = new SolidColorBrush(System.Windows.Media.Color.FromArgb(colorList[colorIndex].A, colorList[colorIndex].R, colorList[colorIndex].G, colorList[colorIndex].B)),
                        Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(colorList[colorIndex].A, colorList[colorIndex].R, colorList[colorIndex].G, colorList[colorIndex].B))
                    };
                    cartesianChart1.Series.Add(lineSeries);
                }

                //barchart.Series.Clear();

                //int ctr = 0;
                //foreach (var Iter in result2)
                //{
                //    Series series = new Series();
                //    series.Name = Iter.Key;
                //    series.ChartType = SeriesChartType.Line;
                //    series.MarkerSize = 5;

                //    foreach (var seriesIter in Iter.Value)
                //    {
                //        series.Points.AddXY(seriesIter.Key, seriesIter.Value);
                //    }
                //    barchart.Series.Add(series);
                //}

                //barchart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                //barchart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            }
        }

        
    }
}
