using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Net4Sage.Controls
{
    /// <summary>
    /// Delegate for the Validations Events
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">Event Arguments</param>
    /// <returns>result</returns>
    [Obsolete]
    public delegate bool ValidationEvenHandler(object sender, EventArgs e);

    /// <summary>
    /// Modes of the MenuBar
    /// </summary>
    public enum MenuBarMode
    {
        /// <summary>
        /// Containces the Save, Cancel, Finish and Delete Buttons
        /// </summary>
        Mantaince = 0,
        /// <summary>
        /// Containces the Next Number, Save, Cancel, Finish and Delete Buttons
        /// </summary>
        Transaction = 1,
        /// <summary>
        /// Containces the Preview Button
        /// </summary>
        Report = 2,
        /// <summary>
        /// Containces the Finish and Cancel Buttons
        /// </summary>
        Operation = 3,
        /// <summary>
        /// Containces the Save, Cancel and Finish Buttons
        /// </summary>
        SemiMantaince = 4,
        /// <summary>
        /// The menu starts empty
        /// </summary>
        None = 5
    }
    /// <summary>
    /// MenuBar Control
    /// </summary>
    public class MenuBar : MenuStrip
    {
        private MenuBarMode _mode;
        /// <summary>
        /// Sage Session
        /// </summary>
        public SageSession SysSession { get; set; }
        /// <summary>
        /// The mode identify the buttons of the Menu
        /// </summary>
        public MenuBarMode Mode
        {
            get => _mode;
            set
            {
                if (_mode != value)
                {
                    _mode = value;
                    DrawButtons(this, null);
                }
            }
        }
        /// <summary>
        /// The event triger when the Save button is Clicked
        /// </summary>
        public event EventHandler OnSave;
        /// <summary>
        /// The event triger when the Cancel button is Clicked
        /// </summary>
        public event EventHandler OnCancel;
        /// <summary>
        /// The event triger when the Delete button is Clicked
        /// </summary>
        [Obsolete]
        public event ValidationEvenHandler OnDelete;
        /// <summary>
        /// The event triger before the Save button is Clicked and handled validations
        /// </summary>
        [Obsolete]
        public event ValidationEvenHandler OnValidation;
        /// <summary>
        /// The event triger before the Save button is Clicked and handled validations
        /// </summary>
        public event CancelEventHandler OnValidate;
        /// <summary>
        /// The event triger when the Next Number button is Clicked
        /// </summary>
        public event EventHandler OnNextNumber;
        /// <summary>
        /// The event triger when the Preview button is Clicked
        /// </summary>
        public event EventHandler OnPreview;

        private ToolStripMenuItem mibFinish;
        private ToolStripMenuItem mibSave;
        private ToolStripMenuItem mibCancel;
        private ToolStripMenuItem mibDelete;
        private ToolStripMenuItem mibNextNumber;
        private ToolStripMenuItem mibPreview;
        private ToolStripMenuItem mibFinishExit;
        private ToolStripMenuItem mibCancelExit;
        private List<ToolStripMenuItem> mibOtherItems;

        /// <summary>
        /// Create Instance of Control
        /// </summary>
        public MenuBar() : base()
        {

            InitializeComponent();
            this.ShowItemToolTips = true;
            mibOtherItems = new List<ToolStripMenuItem>();

            mibNextNumber = new ToolStripMenuItem()
            {
                Image = global::Net4Sage.Properties.Resources.Linkaddvars.ToBitmap(),
                Name = "mibNextNumber",
                Size = new System.Drawing.Size(28, 20),
                ToolTipText = "Próximo Numero",
            };

            mibNextNumber.Click += delegate
            {
                OnCancel?.Invoke(this, new EventArgs());
                OnNextNumber?.Invoke(this, new EventArgs());
            };

            mibFinish = new ToolStripMenuItem()
            {
                Image = global::Net4Sage.Properties.Resources.OK_16,
                Name = "mibFinish",
                Size = new System.Drawing.Size(28, 20),
                ToolTipText = "Finalizar",
            };
            mibFinish.Click += delegate
            {
                if (OnValidation == null || OnValidation.Invoke(this, new EventArgs()))
                {
                    OnSave?.Invoke(this, new EventArgs());
                    OnCancel?.Invoke(this, new EventArgs());
                }
            };

            mibFinishExit = new ToolStripMenuItem()
            {
                Image = global::Net4Sage.Properties.Resources.OK_16,
                Name = "mibFinishExit",
                Size = new System.Drawing.Size(28, 20),
                ToolTipText = "Finalizar y Salir",
            };
            mibFinishExit.Click += delegate
            {
                if (OnValidation != null && !OnValidation.Invoke(this, new EventArgs()))
                    return;
                CancelEventArgs e = new CancelEventArgs();
                OnValidate?.Invoke(this, e);
                if (!e.Cancel)
                {
                    OnSave?.Invoke(this, new EventArgs());
                    OnCancel?.Invoke(this, new EventArgs());
                }
            };

            mibSave = new ToolStripMenuItem()
            {
                Image = global::Net4Sage.Properties.Resources.save_16.ToBitmap(),
                Name = "mibSave",
                Size = new System.Drawing.Size(28, 20),
                ToolTipText = "Salvar",
            };

            mibSave.Click += delegate
            {
                if (OnValidation != null && !OnValidation.Invoke(this, new EventArgs()))
                    return;
                CancelEventArgs e = new CancelEventArgs();
                OnValidate?.Invoke(this, e);
                if (!e.Cancel)
                    OnSave?.Invoke(this, new EventArgs());
            };

            mibCancel = new ToolStripMenuItem()
            {
                Image = global::Net4Sage.Properties.Resources.cancl_16,
                Name = "mibCancel",
                Size = new System.Drawing.Size(28, 20),
                ToolTipText = "Cancelar",
            };
            mibCancel.Click += delegate
            {
                OnCancel?.Invoke(this, new EventArgs());
            };

            mibCancelExit = new ToolStripMenuItem()
            {
                Image = global::Net4Sage.Properties.Resources.cancl_16,
                Name = "mibCancelExit",
                Size = new System.Drawing.Size(28, 20),
                ToolTipText = "Cancelar y Salir",
            };
            mibCancelExit.Click += delegate
            {
                OnCancel?.Invoke(this, new EventArgs());
            };

            mibDelete = new ToolStripMenuItem()
            {
                Image = global::Net4Sage.Properties.Resources.Del_24.ToBitmap(),
                Name = "mibDelete",
                Size = new System.Drawing.Size(28, 20),
                ToolTipText = "Eliminar",
            };
            mibDelete.Click += delegate
            {
                if (OnDelete == null || OnDelete.Invoke(this, new EventArgs()))
                    OnCancel?.Invoke(this, new EventArgs());
            };

            mibPreview = new ToolStripMenuItem()
            {
                Image = global::Net4Sage.Properties.Resources.Preview.ToBitmap(),
                Name = "mibPreview",
                Size = new System.Drawing.Size(28, 20),
                ToolTipText = "Previsualizar",
            };
            mibPreview.Click += delegate
            {
                OnPreview?.Invoke(this, new EventArgs());
            };
        }
        /// <summary>
        /// Add new MenuItem to the Menu
        /// </summary>
        /// <param name="item">MenuItem</param>
        public void AddMenuItem(ToolStripMenuItem item)
        {
            mibOtherItems.Add(item);
            DrawButtons(this, null);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MenuBar
            // 
            this.Layout += new System.Windows.Forms.LayoutEventHandler(DrawButtons);
            this.ResumeLayout(false);
        }

        private void DrawButtons(object sender, LayoutEventArgs e)
        {
            this.Items.Clear();
            if (Mode == MenuBarMode.Report)
                this.Items.Add(mibPreview);
            if (Mode == MenuBarMode.Transaction || Mode == MenuBarMode.Mantaince || Mode == MenuBarMode.SemiMantaince)
            {
                this.Items.Add(mibFinish);
                this.Items.Add(mibSave);
                this.Items.Add(mibCancel);
                if (Mode != MenuBarMode.SemiMantaince)
                    this.Items.Add(mibDelete);
            }
            if (Mode == MenuBarMode.Transaction)
                this.Items.Add(mibNextNumber);
            if (Mode == MenuBarMode.Operation)
            {
                this.Items.Add(mibFinishExit);
                this.Items.Add(mibCancelExit);
            }

            foreach (ToolStripMenuItem i in mibOtherItems)
                this.Items.Add(i);
        }
    }
}
