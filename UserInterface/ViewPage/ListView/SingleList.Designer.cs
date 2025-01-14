﻿using UserInterface.ViewProject;

namespace TeamTracker
{
    partial class SingleList
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SingleList));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.taskName = new System.Windows.Forms.Label();
            this.dueDateLabel = new System.Windows.Forms.Label();
            this.assignedBy = new UserInterface.ViewProject.EmployeeProfilePicAndName();
            this.statusLabel = new TeamTracker.AnimatedLabel();
            this.priorityLabel = new TeamTracker.AnimatedLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Controls.Add(this.taskName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dueDateLabel, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.assignedBy, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.statusLabel, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.priorityLabel, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 5, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1437, 58);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // taskName
            // 
            this.taskName.AutoSize = true;
            this.taskName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskName.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taskName.Location = new System.Drawing.Point(3, 0);
            this.taskName.Name = "taskName";
            this.taskName.Size = new System.Drawing.Size(233, 58);
            this.taskName.TabIndex = 0;
            this.taskName.Text = "label1";
            this.taskName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dueDateLabel
            // 
            this.dueDateLabel.AutoSize = true;
            this.dueDateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dueDateLabel.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dueDateLabel.Location = new System.Drawing.Point(720, 0);
            this.dueDateLabel.Name = "dueDateLabel";
            this.dueDateLabel.Size = new System.Drawing.Size(233, 58);
            this.dueDateLabel.TabIndex = 1;
            this.dueDateLabel.Text = "label2";
            this.dueDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // assignedBy
            // 
            this.assignedBy.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.assignedBy.HoverColor = System.Drawing.Color.Empty;
            this.assignedBy.Location = new System.Drawing.Point(242, 13);
            this.assignedBy.Name = "assignedBy";
            this.assignedBy.NormalColor = System.Drawing.Color.Empty;
            this.assignedBy.Size = new System.Drawing.Size(233, 32);
            this.assignedBy.TabIndex = 2;
            // 
            // statusLabel
            // 
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusLabel.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.LabelCornerColor = System.Drawing.Color.Empty;
            this.statusLabel.Location = new System.Drawing.Point(481, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(233, 58);
            this.statusLabel.TabIndex = 3;
            this.statusLabel.Text = "animatedLabel1";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.statusLabel.Click += new System.EventHandler(this.OnStatusClicked);
            // 
            // priorityLabel
            // 
            this.priorityLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.priorityLabel.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.priorityLabel.LabelCornerColor = System.Drawing.Color.Empty;
            this.priorityLabel.Location = new System.Drawing.Point(959, 0);
            this.priorityLabel.Name = "priorityLabel";
            this.priorityLabel.Size = new System.Drawing.Size(233, 58);
            this.priorityLabel.TabIndex = 4;
            this.priorityLabel.Text = "animatedLabel2";
            this.priorityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1195, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(242, 58);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // SingleList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SingleList";
            this.Size = new System.Drawing.Size(1437, 58);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label taskName;
        private System.Windows.Forms.Label dueDateLabel;
        private EmployeeProfilePicAndName assignedBy;
        private TeamTracker.AnimatedLabel statusLabel;
        private TeamTracker.AnimatedLabel priorityLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
