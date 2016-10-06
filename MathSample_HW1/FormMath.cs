using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MathSample_HW1
{
    public partial class FormMath : Form
    {
        public FormMath()
        {
            Console.WriteLine("In FormMath() Constructor");
            InitializeComponent();
            // this.IsMdiContainer = true;  //https://msdn.microsoft.com/en-us/library/system.windows.forms.form.ismdicontainer(v=vs.110).aspx
        }
        /*
        private void FormMath_MdiChildActivate(object sender, System.EventArgs e)
        {
            if (this.ActiveMdiChild == null)
                tabForms.Visible = false; // If no child form, hide tabControl
            else
            {
                this.ActiveMdiChild.WindowState = FormWindowState.Maximized; // Child form always maximized

                // If child form is new and no has tabPage, create new tabPage
                if (this.ActiveMdiChild.Tag == null)
                {
                    // Add a tabPage to tabControl with child form caption
                    TabPage tp = new TabPage(this.ActiveMdiChild.Text);
                    tp.Tag = this.ActiveMdiChild;
                    tp.Parent = tabForms;
                    tabForms.SelectedTab = tp;

                    this.ActiveMdiChild.Tag = tp;
                    this.ActiveMdiChild.FormClosed += new FormClosedEventHandler(ActiveMdiChild_FormClosed);
                }

                if (!tabForms.Visible) tabForms.Visible = true;
            }
        }
        */
        private void ActiveMdiChild_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((sender as Form).Tag as TabPage).Dispose();
        }

        private void tabForms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if( (tabForms.SelectedTab != null) && (tabForms.SelectedTab.Tag != null) )
            {
                (tabForms.SelectedTab.Tag as Form).Select();
            }
        }

        private void newFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("newFormToolStringMenuItem_click()");
        }

        // Calculate the area of a square
        private void CalculateAreaBox_Click(object sender, System.EventArgs e)
        {
            Square<decimal> square = new Square<decimal>();
            this.areaResultBox.ForeColor = Color.Black;
            decimal width, height;
            bool wIsNumeric, hIsNumeric;
            wIsNumeric = decimal.TryParse(this.widthBox.Text, out width);
            hIsNumeric = decimal.TryParse(this.heightBox.Text, out height);
            if (width <= 0 || height <= 0 || !wIsNumeric || !hIsNumeric)
            {
                this.areaResultBox.Text = "Invalid input";
                this.areaResultBox.ForeColor = Color.Red;
            }
            else
            {
                square.width = width;
                square.height = height;
                this.areaResultBox.Text = square.area().ToString();
            }
        }

    }
}