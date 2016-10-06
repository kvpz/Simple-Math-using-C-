using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq; // Select
using System.Collections;

namespace MathSample_HW1
{
    public partial class FormMath : Form
    {
        //System.Collections.ArrayList numList;
        decimal[] numList;
        public FormMath()
        {
            Console.WriteLine("In FormMath() Constructor");
            InitializeComponent();
            // this.IsMdiContainer = true;  //https://msdn.microsoft.com/en-us/library/system.windows.forms.form.ismdicontainer(v=vs.110).aspx
        }

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

        private void GenerateMultTable_Click(object sender, System.EventArgs e)
        {

            uint n;
            uint.TryParse(this.Nsize_text.Text, out n);
            if (uint.TryParse(this.Nsize_text.Text, out n))
            {
                int largestDigit = (int)(n * n);

                for (int i = 1; i < n + 1; ++i)
                {
                    for (int j = 1; j < n + 1; ++j)
                    {
                        this.MultTableText.AppendText((i * j).ToString() + ' ');
                    }
                    this.MultTableText.AppendText("\n");
                }
            }
            else
            {
                this.MultTableText.Text = "Invalid input";
                this.MultTableText.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void Nsize_text_TextChanged(object sender, System.EventArgs e)
        {
            this.MultTableText.Text = "";
        }

        private void GetMaxValue(object sender, System.EventArgs e)
        {
            if(this.numListTextBox.Text != "")
            {
                try
                {
                    numList = this.numListTextBox.Text.Split(',', ' ').Select(n => Convert.ToDecimal(n)).ToArray();
                }
                catch(FormatException)
                {
                    this.maxTextBox.ForeColor = Color.Red;
                    this.maxTextBox.Text = "Error: Only input integers";
                    numList = null;
                    
                }
                
                if (numList != null)
                {
                    this.maxTextBox.ForeColor = Color.Black;
                    Array.Sort(numList);
                    this.maxTextBox.Text = numList[numList.Length-1].ToString();
                }
            }
        }

        private void GetMinValue_Click(object sender, System.EventArgs e)
        {
            if (this.numListTextBox.Text != "")
            {
                try
                {
                    numList = this.numListTextBox.Text.Split(',', ' ').Select(n => Convert.ToDecimal(n)).ToArray();
                }
                catch (FormatException)
                {
                    this.minTextBox.Text = "Error: Only input integers";
                    this.minTextBox.ForeColor = Color.Red;
                    numList = null;
                }

                if (numList != null)
                {
                    this.minTextBox.ForeColor = Color.Black;
                    Array.Sort(numList);
                    this.minTextBox.Text = numList[0].ToString(); //numList[0].ToString();
                }
            }
        }

        private void GetAverage_Click(object sender, System.EventArgs e)
        {
            if (this.numListTextBox.Text != "")
            {
                try
                {
                    numList = this.numListTextBox.Text.Split(',', ' ').Select(n => Convert.ToDecimal(n)).ToArray();
                }
                catch (FormatException)
                {
                    this.avgTextBox.Text = "Error: Only input integers";
                    this.avgTextBox.ForeColor = Color.Red;
                    numList = null;
                }

                if (numList != null)
                {
                    this.avgTextBox.ForeColor = Color.Black;
                    this.avgTextBox.Text = (numList.Sum() / (decimal)numList.Length).ToString();
                }
            }
        }

    }
}
