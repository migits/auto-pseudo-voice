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
using NAudio.Wave;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace auto_pseudo_voice
{
    public partial class Form1 : Form
    {
        Dictionary<string, AudioFileReader> readers = new Dictionary<string, AudioFileReader>();
        WaveOut wo = new WaveOut();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void LoadSoundFile(string path) {
            readers.Add(path, new AudioFileReader(path));
            soundFileList.Items.Add(path);
        }

        private void soundFileList_DragDrop(object sender, DragEventArgs e) {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string path in files) {
                if (String.Compare(Path.GetExtension(path), ".wav", true) == 0) {
                    LoadSoundFile(path);
                }
            }
        }
        private void soundFileList_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private async void ConvertButton_Click(object sender, EventArgs e) {
            foreach (var pair in readers) {
            }
        }
    }
}
