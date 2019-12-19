using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MLCModpackLauncher
{
    public partial class UpdaterForm : Form
    {
        private int TotalItemCount;

        public UpdaterForm(string version)
        {
            InitializeComponent();
            lblTitle.Text = "Updating to " + version;
            ControlBox = false;
        }
        public void SetTotalItems(int totalItems)
        {
            TotalItemCount = totalItems;
        }
        public void SetCurrentItemProgress(int value)
        {
            progressCurrentItem.Value = value;
            progressCurrentItem.PerformStep();
        }
        public void SetOverallProgress(int value, int currentItemCount)
        {
            progressTotal.Value = value;
            progressTotal.PerformStep();
            lblOverallProgress.Text = "Overall Progress: " + currentItemCount + " of " + TotalItemCount + " files";
        }
        public void SetCurrentItem(string currentItem)
        {
            lblCurrentItem.Text = currentItem;
            lblCurrentItem.Refresh();
        }
        public void UpdateStatus(string status)
        {
            lblStatusText.Text = status;
            lblStatusText.Refresh();
        }
    }
}
