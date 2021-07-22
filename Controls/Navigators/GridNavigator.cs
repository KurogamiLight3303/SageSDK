using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Net4Sage.Controls.Navigators
{
    public class GridNavigator : Navigator
    {
        private int _pageSize = 25;
        private BindingSource _binding;
        private IEnumerable<object> _collection;
        private int _count = 0;
        public int PageSize
        {
            get => _pageSize; set
            {
                if (value < 5)
                    value = 5;
                _pageSize = value;
            }
        }
        public BindingSource BindingSource { get => _binding; set { _binding = value; } }
        public GridNavigator() : base()
        {
            OnChange += RenderGrid; ;
        }
        public GridNavigator(IContainer container) : base(container)
        {
            OnChange += RenderGrid;
        }
        public override int Count()
        {
            return _count;
        }
        public override void Initializer()
        {
            base.Initializer();
        }
        public void SetData(IEnumerable<object> data)
        {
            _collection = data;
            Current = 0;
            RenderGrid(this, null);
            TriggerManualUpdate(this);
        }
        private void RenderGrid(object sender, CancelEventArgs e)
        {
            List<object> answer;
            if (Current >= 0)
            {
                answer = _collection.Skip(this.Current * PageSize).Take(PageSize).ToList();
                if (answer.Any())
                {
                    if (Current >= _count)
                        _count = Current + 1;
                    BindingSource.DataSource = answer;
                }
                else
                {
                    LoadFull = true;
                    Current--;
                    RenderGrid(sender, e);
                }
            }
        }
    }
}
