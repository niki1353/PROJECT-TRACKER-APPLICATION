﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeamTracker
{
    public partial class UcTaskDescription : UserControl
    {

        private string topLabelText = "Task Name";
        private string centerLabelText;
        private Color borderColor = Color.Black;
        private Color topLabelColor = Color.White;

        public UcTaskDescription()
        {
            InitializeComponent();
        }

        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }

        public Color TopLabelColor
        {
            get { return topLabelColor; }
            set
            {
                topLabelColor = value;
                SetTopText();
            }
        }
        public string TopLabelText
        {
            get { return topLabelText; }
            set
            {
                topLabelText = value;
                SetTopText();
            }
        }

        public string CenterLabelText
        {
            get { return centerLabelText; }
            set
            {
                centerLabelText = value;
                SetCenterText();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Point pt1 = new Point(4, 4);
            Point pt2 = new Point(Width - 6, 4);
            Point pt3 = new Point(Width - 6, Height - 6);
            Point pt4 = new Point(4, Height - 6);
            Pen border = new Pen(borderColor, 2);
            e.Graphics.DrawPolygon(border, new Point[] { pt1, pt2, pt3, pt4 });
        }


        private void SetTopText()
        {
            labelTop.BackColor = TopLabelColor;
            labelTop.Text = topLabelText;

        }
        private void SetCenterText()
        {
            labelCenter.Text = centerLabelText;
        }

        
    }
}
