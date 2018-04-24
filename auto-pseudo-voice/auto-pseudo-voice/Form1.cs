using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MathNet.Numerics.IntegralTransforms;

namespace auto_pseudo_voice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            float[] sintbl = Array.ConvertAll(
                Enumerable.Range(0, 256).ToArray(),
                i => Convert.ToSingle(Math.Sin(2.0*Math.PI*16*i/256.0))
            );

            Series test = new Series();
            test.ChartType = SeriesChartType.Line;
            for(int i = 0; i < sintbl.Length; i++)
            {
                test.Points.AddXY(i, sintbl[i]);
            }
            
            Series maximul = new Series();
            maximul.ChartType = SeriesChartType.Point;
            var args = Maximal.argrelmax(sintbl);
            foreach(int i in args)
            {
                maximul.Points.AddXY(i, sintbl[i]);
            }
            
            chart1.Series.Add(test);
            chart1.Series.Add(maximul);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
