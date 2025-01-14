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

namespace UserInterface.Home_Page.Team_Lead
{
    public partial class TeamLeadHome : UserControl
    {
        public TeamLeadHome()
        {
            InitializeComponent();
        }

        public void InitializeHomePage()
        {
            reportTemplate1.InitializeReport();
            overview1.OverviewCollection = VersionManager.FetchOnProcessProjectVersion();
            notificationContent1.NotifyList = DataHandler.FetchNotification();
        }
    }
}
