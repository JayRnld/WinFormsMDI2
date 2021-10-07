﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsMDI2
{
    public partial class MdiWin : UserControl
    {
        public MdiControI2 mdiControl;

        const string max = "[//]", min = "__", normal = @"[\\]", exit = "X";
        bool isMove = false, isMin = false, isResize = false;
        int mx, my, rx, ry;
        Point lastLocation;
        Size lastSize;

        public MdiWin(MdiControI2 mdiControl)
        {
            Init();
            this.mdiControl = mdiControl;
        }

        public MdiWin()
        {
            Init();
        }

        private void Init()
        {
            BackColor = Color.LightGray;
            InitializeComponent();
            labelTitle.MouseDown += panelTop_MouseDown;
            labelTitle.MouseUp += panelTop_MouseUp;
            labelTitle.MouseMove += panelTop_MouseMove;
            labelTitle.DoubleClick += panelTop_DoubleClick;
            foreach (Control cont in Controls)
            {
                cont.MouseDown += mdiwin_Down;
            }
            foreach (Control cont in panelTop.Controls)
            {
                cont.MouseDown += mdiwin_Down;
            }
            labelTitle.Select();
        }

        private void mdiwin_Down(object sender, MouseEventArgs e)
        {
            mdiControl.FocusMdiWin(this);
        }

        #region behaviors
        [DefaultValue("MdiWin")]
        [Description("Is MdiWin Title")]
        public string Title { get { return labelTitle.Text; } set { labelTitle.Text = value; } }
        #endregion

        #region panelTop
        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            isMove = true;
            if (Dock == DockStyle.Fill)
            {
                mx = MousePosition.X - Location.X;
                my = MousePosition.Y - Location.Y;
            }
            else
            {
                mx = MousePosition.X - Location.X;
                my = MousePosition.Y - Location.Y;
            }
        }

        private void panelTop_MouseUp(object sender, MouseEventArgs e)
        {
            isMove = false;
        }

        private void panelTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMove)
            {
                if (Dock == DockStyle.Fill)
                {
                    Dock = DockStyle.None;
                }
                Location = new Point(MousePosition.X - mx, MousePosition.Y - my);
            }
        }

        private void panelTop_DoubleClick(object sender, EventArgs e)
        {
            if(Dock == DockStyle.Fill)
            {
                Dock = DockStyle.None;
            }
            else
            {
                Dock = DockStyle.Fill;
            }
            labelTitle.Select();
        }
        #endregion

        #region panelFloor
        private void panelFloor_MouseDown(object sender, MouseEventArgs e)
        {
            if (Dock != DockStyle.Fill)
            {
                isResize = true;
                ry = MousePosition.Y - Size.Height;
            }
        }

        private void panelFloor_MouseUp(object sender, MouseEventArgs e)
        {
            isResize = false;
        }

        private void panelFloor_MouseMove(object sender, MouseEventArgs e)
        {
            if (isResize)
            {
                Size = new Size(Size.Width, MousePosition.Y - ry);
            }
        }
        #endregion

        #region panelLeft
        private void panelLeft_MouseDown(object sender, MouseEventArgs e)
        {
            if (Dock != DockStyle.Fill)
            {
                isResize = true;
                rx = -MousePosition.X - Size.Width;
                mx = MousePosition.X - Location.X;
            }
        }

        private void panelLeft_MouseUp(object sender, MouseEventArgs e)
        {
            isResize = false;
        }

        private void panelLeft_MouseMove(object sender, MouseEventArgs e)
        {
            if (isResize)
            {
                Size = new Size(-MousePosition.X - rx, Size.Height);
                if(Size.Width > MinimumSize.Width)
                    Location = new Point(MousePosition.X - mx, Location.Y);
            }
        }
        #endregion

        #region panelRight
        private void panelRight_MouseDown(object sender, MouseEventArgs e)
        {
            if (Dock != DockStyle.Fill)
            {
                isResize = true;
                rx = MousePosition.X - Size.Width;
            }
        }

        private void panelRight_MouseUp(object sender, MouseEventArgs e)
        {
            isResize = false;
        }

        private void panelRight_MouseMove(object sender, MouseEventArgs e)
        {
            if (isResize)
            {
                Size = new Size(MousePosition.X - rx, Size.Height);
            }
        }
        #endregion

        #region buttons
        private void bExit_Click(object sender, EventArgs e)
        {
            mdiControl.Controls.Remove(this);
            Dispose();
            labelTitle.Select();
        }

        private void bMax_Click(object sender, EventArgs e)
        {
            if (Dock == DockStyle.Fill)
            {
                Dock = DockStyle.None;
                bMax.Text = max;
            }
            else
            {
                if (isMin)
                {
                    Size = lastSize;
                    Location = lastLocation;
                    bMin.Text = min;
                    isMin = false;
                }
                Dock = DockStyle.Fill;
                bMax.Text = normal;
            }
            labelTitle.Select();
        }

        private void bMin_Click(object sender, EventArgs e)
        {
            if (!isMin)
            {
                if (Dock == DockStyle.Fill)
                {
                    Dock = DockStyle.None;
                    bMax.Text = max;
                }

                lastSize = Size;
                lastLocation = Location;

                Size = new Size(224, 32);
                Location = new Point(0, mdiControl.Height - 32);
                bMin.Text = normal;
                isMin = true;
            }
            else
            {
                Size = lastSize;
                Location = lastLocation;
                bMin.Text = min;
                isMin = false;
            }
            
            labelTitle.Select();
        }

        private void bExit_MouseLeave(object sender, EventArgs e)
        {
            labelTitle.Select();
        }

        private void bMax_MouseLeave(object sender, EventArgs e)
        {
            labelTitle.Select();

        }

        private void bMin_MouseLeave(object sender, EventArgs e)
        {
            labelTitle.Select();
        }
        #endregion
    }
}