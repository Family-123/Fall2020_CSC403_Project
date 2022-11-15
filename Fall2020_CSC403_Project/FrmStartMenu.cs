using Fall2020_CSC403_Project.code;
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
    
    public partial class FrmStartMenu : Form {

        public FrmStartMenu() {
            InitializeComponent();
        }

        private void FrmStartMenu_Load(object sender, EventArgs e)
        {
            
        }
        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void btnStartLevel_Click(object sender, EventArgs e)
        {
            FrmLevel level = new FrmLevel();
            level.Show();
            this.Hide();
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmLevel level1 = new FrmLevel();
            level1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HardFrmLevel level2 = new HardFrmLevel();
            level2.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HarderFrmLevel level3 = new HarderFrmLevel();
            level3.Show();
            this.Hide();
        }
    }
}
