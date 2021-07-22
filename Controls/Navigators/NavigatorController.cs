using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Net4Sage.Controls.Navigators
{
    public partial class NavigatorController : UserControl
    {
        private Navigator _navigator;
        public Navigator Navigator { get => _navigator; set
            {
                _navigator = value;
                if(_navigator != null)
                    _navigator.OnManualUpdate += OnNavigatorManualUpdate;
            }
        }
        public NavigatorController()
        {
            InitializeComponent();
        }

        #region "Navigator Handling"
        private void btnFirst_Click(object sender, EventArgs e)
        {
            Firts();
        }
        private void btnEnd_Click(object sender, EventArgs e)
        {
            Last();
        }
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            Previous();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            Next();
        }
        public void Firts()
        {
            MoveIndex(0);
        }
        public void Previous()
        {
            if (Navigator != null)
                MoveIndex(Navigator.Current - 1);
        }
        public void Next()
        {
            if (Navigator != null)
                MoveIndex(Navigator.Current + 1);
        }
        public void Last()
        {
            if (Navigator != null)
                MoveIndex(Navigator.Count() - 1);
        }
        private void MoveIndex(int index)
        {
            if (Navigator != null)
            {
                int tag = Navigator.Current;
                if (tag != index && !Navigator.LoadFull || index < Navigator.Count() && index >= 0)
                {
                    CancelEventArgs _e = new CancelEventArgs();
                    Navigator.Current = index;
                    Navigator.RaiseLoad(this, _e);
                    if (_e.Cancel)
                        Navigator.Current = tag;
                    else
                        Navigator.TriggerManualUpdate(this);
                }
            }
        }
        internal void UpdateNavigator()
        {
            if (Navigator != null)
            {
                if (Navigator.Current >= 0)
                    lblNavigator.Text = (Navigator.Current + 1) + " de " + Navigator.Count();
                else
                    lblNavigator.Text = "-- / --";
                btnFirst.Enabled = btnPrevious.Enabled = Navigator.Current > 0;
                btnEnd.Enabled = btnNext.Enabled = !Navigator.LoadFull || Navigator.Current + 1 < Navigator.Count();
            }
        }
        private void OnNavigatorManualUpdate(object sender, EventArgs e)
        {
            UpdateNavigator();
        }
        #endregion
    }
}
