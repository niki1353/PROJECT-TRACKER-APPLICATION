﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using TeamTracker;

namespace UserInterface.ViewProject.BoardView.Custom_Controls
{
    public partial class BoardViewTemplate : UserControl
    {
        private ProjectVersion boardVersion;
        private Milestone milestone;
        private string toolTipMessage;

        public BoardViewTemplate()
        {
            InitializeComponent();
            InitializeBorder();
        }

               
        public ProjectVersion BoardVersion
        {
            get { return boardVersion; }
            set
            {
                milestoneStatusPicBox.Visible = false;
                boardVersion = value;
                if (value.StatusOfVersion == ProjectStatus.OnProcess || value.StatusOfVersion == ProjectStatus.OnStage || value.StatusOfVersion == ProjectStatus.Deployment)
                {
                    milestoneStatusPicBox.Visible = true;
                    SetMilestoneStatus();
                }

                versionNameLabel.Text = VersionManager.FetchProjectName(value.VersionID)+"\n"+value.VersionName;
                versionDateLabel.Text = value.StartDate.ToShortDateString() + " - " + value.EndDate.ToShortDateString();
                int id = VersionManager.FetchTeamLeadIDFromProjectID(value.ProjectID);
                profilePictureBox1.Image = Image.FromFile(EmployeeManager.FetchEmployeeFromID(id).EmpProfileLocation);
                milestoneCountLabel.Text = MilestoneManager.FetchMilestones(value.VersionID).Count.ToString();
                taskCountLabel.Text = TaskManager.FetchTaskCount(value.VersionID)[0].ToString();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            InitializeBorder();
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        private void InitializeBorder()
        {
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 10, 10));
        }

        private void OnMilestoneLabelPaint(object sender, PaintEventArgs e)
        {
            int width = (sender as Label).Width, height = (sender as Label).Height;
            Brush brush1 = new SolidBrush(Color.Gold);
            Brush brush2 = new SolidBrush(Color.White);
            StringFormat sFormat = new StringFormat();
            sFormat.Alignment = sFormat.LineAlignment = StringAlignment.Center;
            Font font = new Font(new FontFamily("Ebrima"), 10, FontStyle.Bold);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.FillEllipse(brush1, new Rectangle(0, 0, width - 1, height - 1));
            e.Graphics.DrawString(milestoneCountLabel.Text, font, brush2, new Rectangle(-1, -1, width, height), sFormat);
            brush1.Dispose();
        }

        private void OnTaskLabelPaint(object sender, PaintEventArgs e)
        {
            int width = (sender as Label).Width, height = (sender as Label).Height;
            Brush brush1 = new SolidBrush(Color.FromArgb(32, 201, 151));
            Brush brush2 = new SolidBrush(Color.White);
            StringFormat sFormat = new StringFormat();
            sFormat.Alignment = sFormat.LineAlignment = StringAlignment.Center;
            Font font = new Font(new FontFamily("Ebrima"), 10, FontStyle.Bold);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.FillEllipse(brush1, new Rectangle(0, 0, width, height));
            e.Graphics.DrawString(taskCountLabel.Text, font, brush2, new Rectangle(0, 0, width, height), sFormat);
            brush1.Dispose();
        }

        private void SetMilestoneStatus()
        {
            milestone = MilestoneManager.FetchCurrentVersion(boardVersion);

            if(milestone != null)
            {
                if (milestoneStatusPicBox.Image != null)
                    milestoneStatusPicBox.Image.Dispose();

                if(milestone.StartDate > DateTime.Today)
                {
                    toolTipMessage = "Beyond the Milestone Deadline";
                    milestoneStatusPicBox.BackColor = Color.Green;
                    milestoneStatusPicBox.Image = UserInterface.Properties.Resources.Version_Milestone_Status_Up;
                }
                else if(milestone.EndDate < DateTime.Today)
                {
                    toolTipMessage = "Behind the Milestone Deadline";
                    milestoneStatusPicBox.BackColor = Color.Red;
                    milestoneStatusPicBox.Image = UserInterface.Properties.Resources.Version_Milestone_Status_Down;
                }
                else
                {
                    toolTipMessage = "Steady State";
                    milestoneStatusPicBox.BackColor = Color.Blue;
                    milestoneStatusPicBox.Image = UserInterface.Properties.Resources.Version_Milestone_Status_Equal;
                }
            }
        }

        private void OnBoardClick(object sender, EventArgs e)
        {
            ;
        }

        private void OnUserControlPaint(object sender, PaintEventArgs e)
        {
            //OnUserControlPaint
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(181, 190, 197);
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(201, 210, 217);
        }

        private void OnMilestoneStatusHover(object sender, EventArgs e)
        {
            ;
        }
    }
}
