using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MathNet.Numerics;

namespace auto_pseudo_voice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            chart1.Series.Clear();
            chart1.Legends.Clear();
            chart1.Titles.Clear();

            Title title1 = new Title("波形");

            chart1.ChartAreas["ChartArea1"].AxisX.Title = "時間[秒]";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "振幅";

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
            test.Name = "16Hz正弦波";
            
            Legend leg = new Legend();
            leg.DockedToChartArea = "ChartArea1";
            leg.Alignment = StringAlignment.Near;

            chart1.Series.Add(test);
            chart1.Legends.Add(leg);
            chart1.Titles.Add(title1);


            Series spectrum = new Series();
            spectrum.ChartType = SeriesChartType.Column;

            var it = (new STFTIterator(sintbl, hamming(256))).GetEnumerator();
            it.MoveNext();
            float[] power = Array.ConvertAll(it.Current, x => Convert.ToSingle(x.Norm()));
            for (int k = 0; k < power.Length; k++) {
                spectrum.Points.AddXY(k, power[k]);
            }
            spectrum.Name = "スペクトラム";


            Series maximul = new Series();
            maximul.ChartType = SeriesChartType.Point;
            var args = Maximal.argrelmax(power);
            foreach(int i in args)
            {
                if (power[i] > 0.1) maximul.Points.AddXY(i, power[i]);
            }
            maximul.Name = "スペクトラムのピーク";
            maximul.MarkerSize = 8;
            maximul.MarkerStyle = MarkerStyle.Circle;


            chart2.Series.Clear();
            chart2.Series.Add(spectrum);
            chart2.Series.Add(maximul);
        }

        private static float[] hamming(int width)
        {
            return Array.ConvertAll(
                Enumerable.Range(0, width).ToArray(),
                n => Convert.ToSingle(0.54 - 0.46*Math.Cos(2.0*Math.PI*n/width))
            );
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
