using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project
{
    public partial class PauseMenu : Form
    {
        public PauseMenu()
        {
            InitializeComponent();
        }

        private void ResumeGame(object sender, EventArgs e)
        {
            FrmLevel.IsPaused = false;
            Close();
        }

        private void QuiteGame(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}