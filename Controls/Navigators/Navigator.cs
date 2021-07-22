using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net4Sage.Controls.Navigators
{
    /// <summary>
    /// Base Class for Navigators
    /// </summary>
    public partial class Navigator : Component
    {
        private int _current = -1;
        /// <summary>
        /// Event occurs when the Navigator position Changes
        /// </summary>
        public event CancelEventHandler OnChange;
        /// <summary>
        /// Event occurs when the Navigator is manually update
        /// </summary>
        public event EventHandler OnManualUpdate;
        /// <summary>
        /// Get the Current Position of the Navigator
        /// </summary>
        public int Current
        {
            get { return _current; }
            internal set
            {
                if (value < -1) value = -1;
                _current = value;
            }
        }
        public bool LoadFull { get; set; }
        /// <summary>
        /// Create Instance of Navigator Base
        /// </summary>
        protected Navigator()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Create Instance of Navigator Base
        /// </summary>
        /// <param name="container">Container</param>
        protected Navigator(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }
        /// <summary>
        /// Initialize Navigator
        /// </summary>
        public virtual void Initializer()
        {

        }
        internal void TriggerManualUpdate(object sender)
        {
            OnManualUpdate?.Invoke(sender, null);
        }
        internal void RaiseLoad(object sender, CancelEventArgs e)
        {
            OnChange?.Invoke(sender, e);
        }
        /// <summary>
        /// Get the size of the data collection handled by the Navigator
        /// </summary>
        /// <returns>Nuber of Items</returns>
        public virtual int Count()
        {
            return 0;
        }
    }
}
