﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using TeamTracker;
using System.Windows.Forms;
using System.Reflection;

namespace UserInterface.Task.Timeline
{
    public partial class CurrentTimelineContent : UserControl
    {
        private List<TeamTracker.Task> taskCollection;
        private List<TeamTracker.Task> viewTaskCollection;
        private bool isBackEnable, isNextEnable;
        private List<string> monthCollections;
        private List<Label> labelCollections;
        private TimeSpan dateDifference;
        private DateTime startViewDate, endViewDate, iterDate;
        private ProjectVersion version;
        private TimelineTask taskTimeline;
        private TeamTracker.Task edgedTask = null;

        public CurrentTimelineContent()
        {
            InitializeComponent();
            InitializeLabels();
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, timelineControlPanel, new object[] { true });
        }

        public ProjectVersion Version
        {
            set
            {
                version = value;
                iterDate = startViewDate = value.StartDate;
                dateDifference = value.EndDate - value.StartDate;
                isBackEnable = false;
                isNextEnable = true;
                endViewDate = startViewDate.AddDays(20);
                taskCollection = TaskManager.FetchTasksByVersionID(version.VersionID);
                InitializeTimeline();
                SetViewTaskCollection();
            }
        }

        private void TimelineControlPaint(object sender, PaintEventArgs e)
        {
            int width, x, y, stepWidth;
            width = tableLayoutPanel1.Width; x = 0; stepWidth = tableLayoutPanel1.Width / 20; y = timelineControlPanel.Height;
            Pen border = new Pen(Color.FromArgb(157, 178, 191), 2);
            border.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            while (x < width)
            {
                e.Graphics.DrawLine(border, x - 1, 0, x - 1, y);
                x += stepWidth;
            }
            border.Dispose();
        }

        private void OnNextDayClick(object sender, EventArgs e)
        {
            if (isNextEnable)
            {
                startViewDate = iterDate = startViewDate.AddDays(1);
                endViewDate = endViewDate.AddDays(1);
                InitializeTimeline();
                SetViewTaskCollection();
            }
        }

        private void OnPrevDayClick(object sender, EventArgs e)
        {
            if (isBackEnable)
            {
                startViewDate = iterDate = startViewDate.AddDays(-1);
                endViewDate = endViewDate.AddDays(-1);
                InitializeTimeline();
                SetViewTaskCollection();
            }
        }

        private void InitializeTimeline()
        {
            isBackEnable = iterDate == version.StartDate ? false : true;

            for (int ctr = 0; ctr < labelCollections.Count; ctr++)
            {
                labelCollections[ctr].Text = monthCollections[iterDate.Month - 1] + "\n" + iterDate.Day;
                iterDate = iterDate.AddDays(1);
            }

            isNextEnable = iterDate > version.EndDate ? false : true;

            if (backPictureBox.Image != null) { backPictureBox.Image.Dispose(); }
            if (nextPictureBox.Image != null) { nextPictureBox.Image.Dispose(); }

            backPictureBox.Image = isBackEnable ? UserInterface.Properties.Resources.Back : UserInterface.Properties.Resources.Back_Hover;
            nextPictureBox.Image = isNextEnable ? UserInterface.Properties.Resources.Next : UserInterface.Properties.Resources.Next_Hover;
        }

        private void InitializeLabels()
        {
            labelCollections = new List<Label>();
            for (int ctr = 0; ctr < 20; ctr++)
            {
                labelCollections.Add(tableLayoutPanel1.GetControlFromPosition(ctr, 0) as Label);
            }

            monthCollections = new List<string>() { "Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sept", "Oct", "Nov", "Dec" };
        }

        private void SetViewTaskCollection()
        {
            int ptr = 0;
            if (timelineControlPanel.Controls != null)
            {
                for (int idx = 0; idx < timelineControlPanel.Controls.Count; idx++)
                {
                    if ((edgedTask != null && (timelineControlPanel.Controls[idx] as TimelineTask).SelectedTask.TaskID != edgedTask.TaskID) || edgedTask == null)
                    {
                        timelineControlPanel.Controls.Remove(timelineControlPanel.Controls[idx] as TimelineTask);
                        idx--;
                    }
                }
            }

            viewTaskCollection = new List<TeamTracker.Task>();
            foreach (var Iter in taskCollection)
            {
                if ((startViewDate <= Iter.EndDate && Iter.EndDate <= endViewDate) || (startViewDate <= Iter.StartDate && Iter.StartDate <= endViewDate) || (Iter.StartDate <= startViewDate && endViewDate <= Iter.EndDate))
                {
                    viewTaskCollection.Add(Iter);
                }
            }

            if (edgedTask != null)
            {
                viewTaskCollection.Add(edgedTask);
            }

            viewTaskCollection.Sort((v1, v2) => v1.StartDate.CompareTo(v2.StartDate));

            DateTime prevStartDate = DateTime.MinValue, prevEndDate = DateTime.MinValue;
            int height = 50, width = tableLayoutPanel1.Width / 20, x = 0, y = 0, controlWidth = 0;

            int ctr = 0;
            foreach (var Iter in viewTaskCollection)
            {
                x = GetTimelinePosition(Iter.StartDate, width);
                controlWidth = GetTimelineWidth(Iter.StartDate, Iter.EndDate, width);
                if (prevStartDate <= Iter.StartDate && Iter.StartDate <= prevEndDate)
                {
                    prevEndDate = prevEndDate < Iter.EndDate ? Iter.EndDate : prevEndDate;
                    y = y + height;
                }
                else if (prevStartDate <= Iter.EndDate && Iter.EndDate <= prevEndDate)
                {
                    prevStartDate = Iter.StartDate < prevStartDate ? Iter.EndDate : prevStartDate;
                    y = y + height;
                }
                else if (Iter.StartDate <= prevStartDate && prevEndDate <= Iter.EndDate)
                {
                    prevStartDate = Iter.StartDate; prevEndDate = Iter.EndDate;
                    y = y + height;
                }
                else
                {
                    prevStartDate = Iter.StartDate; prevEndDate = Iter.EndDate;
                    y = 0;
                }

                if ((edgedTask != null && Iter.TaskID != edgedTask.TaskID) || edgedTask == null)
                {
                    taskTimeline = new TimelineTask()
                    {
                        Location = new Point(x, y),
                        Height = height,
                        Width = controlWidth,
                        SelectedTask = Iter,
                        StatusColor = ColorManager.FetchTaskStatusColor(Iter.StatusOfTask),
                        StepWidth = tableLayoutPanel1.Width / 20,
                        DisplayMode = SetDisplayMode(Iter.StartDate, Iter.EndDate)
                    };
                    taskTimeline.TimeLineMovement += OnTimeLineMovement;
                    taskTimeline.TaskTimelineCheck += OnTimelineDateChecked;
                    taskTimeline.TaskDateChange += OnTaskDateChanged;
                    timelineControlPanel.Controls.Add(taskTimeline);
                }
                ctr++;
            }
        }

        private bool OnTimelineDateChecked(TimelineTask control)
        {
            int start = control.Location.X / (tableLayoutPanel1.Width / 20), end = (control.Location.X + control.Width) / (tableLayoutPanel1.Width / 20); end--;
            DateTime startDate = startViewDate.AddDays(start);
            DateTime endDate = startViewDate.AddDays(end);

            if(startDate.Date == version.StartDate || endDate.Date.AddDays(1) == version.EndDate)
            {
                return true;
            }
            return false;
        }

        private int GetTimelinePosition(DateTime date, int width)
        {
            dateDifference = date - startViewDate;
            if (dateDifference.Days <= 0)
            {
                return 0;
            }
            else
            {
                return dateDifference.Days * width;
            }

        }

        private int GetTimelineWidth(DateTime startdate, DateTime endDate, int width)
        {
            dateDifference = startdate - startViewDate;
            if (dateDifference.Days <= 0)
            {
                startdate = startViewDate;
            }

            dateDifference = endViewDate - endDate;
            if (dateDifference.Days <= 0)
            {
                endDate = endViewDate.AddDays(-1);
            }

            return ((endDate - startdate).Days + 1) * width;
        }

        private void OnTimeLineMovement(TimelineTask control, TeamTracker.Task selectedTask, int direction)
        {
            foreach(var Iter in taskCollection)
            {
                if(selectedTask.TaskID == Iter.TaskID)
                {
                    if(direction == -1)
                    {
                        iterDate = startViewDate = startViewDate.AddDays(-1);
                        Iter.StartDate = startViewDate;
                        endViewDate = endViewDate.AddDays(-1);
                        edgedTask = Iter;
                    }
                    if(direction == 1)
                    {
                        iterDate = startViewDate = startViewDate.AddDays(1);
                        Iter.EndDate = endViewDate;
                        endViewDate = endViewDate.AddDays(1);
                        edgedTask = Iter;
                    }
                    InitializeTimeline();
                    SetViewTaskCollection();
                    control.DisplayMode = SetDisplayMode(Iter.StartDate, Iter.EndDate);
                    break;
                }
            }
            edgedTask = null;
        }

        private TimelineDisplayMode SetDisplayMode(DateTime taskStartDate, DateTime taskEndDate)
        {
            if(startViewDate < taskStartDate && taskEndDate < endViewDate)
            {
                return TimelineDisplayMode.Full;
            }
            else if(taskStartDate <= startViewDate)
            {
                return TimelineDisplayMode.LeftPartial;
            }
            else if(endViewDate <= taskEndDate)
            {
                return TimelineDisplayMode.RightPartial;
            }
            else
            {
                return TimelineDisplayMode.Outer;
            }
        }

        private void OnTaskDateChanged(TimelineTask control, TeamTracker.Task selectedTask, int direction)
        {
            int start = control.Location.X / (tableLayoutPanel1.Width / 20), end = (control.Location.X + control.Width) / (tableLayoutPanel1.Width / 20);   end--;
            foreach (var Iter in taskCollection)
            {
                if (selectedTask.TaskID == Iter.TaskID)
                {
                    iterDate = startViewDate;
                    Iter.StartDate = startViewDate.AddDays(start);
                    Iter.EndDate = startViewDate.AddDays(end);
                    InitializeTimeline();
                    SetViewTaskCollection();
                    control.DisplayMode = SetDisplayMode(Iter.StartDate, Iter.EndDate);
                    break;
                }
            }
        }
    }
}
