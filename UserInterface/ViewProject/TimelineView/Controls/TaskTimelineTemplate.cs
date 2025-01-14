﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TeamTracker;

namespace TeamTracker
{
    public partial class TaskTimelineTemplate : UserControl
    {

        private Task timelineTask;

        public TaskTimelineTemplate()
        {
            InitializeComponent();
        }

        public event EventHandler<Task> TaskSelect;

        public Task TimelineTask
        {
            get
            {
                return timelineTask;
            }

            set
            {
                timelineTask = value;
                taskLabel.Text = value.TaskName;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Pen pen = new Pen(Color.Gray,2);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.DrawRectangle(pen, new Rectangle(-1,-1,Width, Height));
            pen.Dispose();
        }

        private void OnClicked(object sender, EventArgs e)
        {
            TaskSelect?.Invoke(this, timelineTask);
        }
    }
}
