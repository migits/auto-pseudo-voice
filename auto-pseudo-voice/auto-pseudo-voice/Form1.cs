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

            chart1.ChartAreas["ChartArea1"].AxisX.Title = "時間 [秒]";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "振幅";
            chart1.ChartAreas["ChartArea1"].BackColor = Color.Black;

            float[] sintbl = Array.ConvertAll(
                Enumerable.Range(0, 256).ToArray(),
                i => Convert.ToSingle(Math.Sin(2.0*Math.PI*16*i/256.0)
                    + Math.Sin(2.0*Math.PI*100.0*i/256.0))
            );

            Series test = new Series();
            test.ChartType = SeriesChartType.Line;
            test.Color = Color.Lime;
            test.BorderWidth = 2;
            for(int i = 0; i < sintbl.Length; i++)
            {
                test.Points.AddXY(i, sintbl[i]);
            }

            chart1.Series.Add(test);
            chart1.Titles.Add(title1);


            chart2.Series.Clear();
            chart2.Legends.Clear();
            chart2.Titles.Clear();

            var title2 = new Title("スペクトル");

            chart2.ChartAreas["ChartArea1"].AxisX.Title = "周波数 [Hz]";
            chart2.ChartAreas["ChartArea1"].AxisY.Title = "パワー";
            chart2.ChartAreas["ChartArea1"].BackColor = Color.Black;

            Series spectrum = new Series();
            spectrum.ChartType = SeriesChartType.Column;
            spectrum.Color = Color.Lime;
            spectrum["PointWidth"] = "1.0";
            spectrum.BorderColor = Color.Green;

            var it = (new STFTIterator(sintbl, WindowFunction.hamming(256))).GetEnumerator();
            it.MoveNext();
            float[] power = Array.ConvertAll(it.Current, x => Convert.ToSingle(x.Norm()));
            for (int k = 0; k < power.Length; k++) {
                spectrum.Points.AddXY(k, power[k]);
            }
            spectrum.Name = "スペクトル";

            Series maximul = new Series();
            maximul.ChartType = SeriesChartType.Point;
            var args = Maximal.argrelmax(power);
            foreach(int i in args)
            {
                if (power[i] > 0.1) maximul.Points.AddXY(i, power[i]);
            }
            maximul.Name = "スペクトルのピーク";
            maximul.MarkerSize = 12;
            maximul.MarkerStyle = MarkerStyle.Cross;
            maximul.Color = Color.Yellow;

            var leg = new Legend();
            leg.DockedToChartArea = "ChartArea1";
            leg.Alignment = StringAlignment.Near;
            leg.BackColor = Color.FromArgb(0xA0, 0xA0, 0xA0);
            leg.ForeColor = Color.Cyan;
            leg.BorderColor = Color.Gray;

            chart2.Series.Clear();
            chart2.Series.Add(spectrum);
            chart2.Series.Add(maximul);
            chart2.Legends.Add(leg);
            chart2.Titles.Add(title2);
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void wavファイルを開くToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "wavファイル(*.wav)|*.wav|すべてのファイル(*.*)|*.*";
            ofd.Title = "wavファイルを開く";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(ofd.FileName);
            }
        }
    }
}
