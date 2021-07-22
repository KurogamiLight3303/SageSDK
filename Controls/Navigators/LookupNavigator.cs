using Net4Sage.Controls.Lookup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Net4Sage.Controls.Navigators
{
    /// <summary>
    /// Lookup Navigator Component
    /// </summary>
    public class LookupNavigator : Navigator
    {
        private int _count;
        private object _currentObj = null;
        private bool _working = false;
        private LookupButton _source;
        /// <summary>
        /// Create instance of the component
        /// </summary>
        public LookupNavigator() : base()
        {
            OnChange += LoadFromLookup;
        }
        /// <summary>
        /// Create instance of the component
        /// </summary>
        /// <param name="container">Control Container</param>
        public LookupNavigator(IContainer container) : base(container)
        {
            OnChange += LoadFromLookup;
        }
        private void LoadFromLookup(object sender, CancelEventArgs e)
        {
            _working = true;
            if (Source != null && !Source.MoveTo(Current)) e.Cancel = true;
            _working = false;
        }
        private void UpdateObj(object sender, LookupReturnEventArgs eventArgs)
        {
            if (_working)
                _currentObj = eventArgs.ReturnValue;
            else
                Update(sender, eventArgs.ReturnValue);
        }
        /// <summary>
        /// The Lookup button linked to the navigator
        /// </summary>
        public LookupButton Source { get => _source; set
            {
                if(_source != value)
                {
                    _source = value;
                    _source.OnLookupReturn += UpdateObj;
                    _source.OnDataSourceChange += delegate
                    {
                        TriggerManualUpdate(this);
                    };
                }
            }
        }
        /// <summary>
        /// Get the size of the data collection handled by the Navigator
        /// </summary>
        /// <returns>Nuber of Items</returns>
        public override int Count()
        {
            return _count;
        }
        /// <summary>
        /// Manualy updates the current object
        /// </summary>
        /// <param name="sender">Sender control</param>
        /// <param name="obj">Object</param>
        public void Update(object sender, object obj)
        {
            if (Source == null) return;
            int _tempIndex = Source.IndexOf(obj);
            if (_tempIndex > _count)
                _count = _tempIndex + 1;
            if (_tempIndex != Current)
            {
                Current = _tempIndex;
                TriggerManualUpdate(sender);
            }
        }
    }
}
