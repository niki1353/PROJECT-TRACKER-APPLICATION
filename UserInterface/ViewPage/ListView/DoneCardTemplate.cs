﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using TeamTracker;
using System.Windows.Forms;

namespace UserInterface.ViewPage.ListView
{
    public enum CardMode
    {
        TeamLead,
        TeamMember
    }

    public partial class DoneCardTemplate : UserControl
    {
        public TeamTracker.Task SelectedTask
        {
            get
            {
                return selectedTask;
            }
            set
            {
                if (value != null)
                {
                    SetDoneTaskUI();
                }
            }
        }

        public CardMode ModeOfView
        {
            get; set;
        }

        private TeamTracker.Task selectedTask;
        public DoneCardTemplate()
        {
            InitializeComponent();
        }

        private void SetDoneTaskUI()
        {
            if (ModeOfView == CardMode.TeamLead)
            {
                profilePictureBox1.Image = Image.FromFile(EmployeeManager.FetchEmployeeFromID(selectedTask.AssignedBy).EmpProfileLocation);
            }
            else
            {
                profilePictureBox1.Image = Image.FromFile(EmployeeManager.FetchEmployeeFromID(selectedTask.AssignedTo).EmpProfileLocation);
            }
            projectName.Text = VersionManager.FetchProjectName(selectedTask.VersionID);
            taskNameLabel.Text = selectedTask.TaskName;
            dueDate.Text = selectedTask.EndDate.ToShortDateString();

            switch (selectedTask.TaskPriority)
            {
                case Priority.Critical:
                    pictureBox1.Image = UserInterface.Properties.Resources.flag_OnProcess;
                    break;
                case Priority.Hard:
                    pictureBox1.Image = UserInterface.Properties.Resources.flag_stuck;
                    break;
                case Priority.Medium:
                    pictureBox1.Image = UserInterface.Properties.Resources.flag_NotStarted;
                    break;
                case Priority.Easy:
                    pictureBox1.Image = UserInterface.Properties.Resources.flag_UnderReview;
                    break;
                default:
                    pictureBox1.Image = UserInterface.Properties.Resources.flag_empty;
                    break;
            }
        }
    }
}
