﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace YoutubeIdExtractor
{
	public partial class loadForm : Form
	{
		public loadForm(bool showStatistics)
		{
			InitializeComponent();

			string file = "playlists.json";

			if (File.Exists(file))
			{
				using (var reader = new StreamReader(file))
				{
					var content = reader.ReadToEnd();
					playlists = JsonConvert.DeserializeObject<PlaylistSave[]>(content);
					playlistComboBox.Items.AddRange(playlists.Select(p => p.Name).ToArray());

					if (playlists.Length > 0)
					{
						playlistComboBox.SelectedIndex = 0;
					}
				}
			}

			showStatisticsCheckBox.Checked = showStatistics;
		}

		public string Id { get; private set; }
		public bool ReadStatistics { get; private set; }

		private PlaylistSave[] playlists = new PlaylistSave[0];

		private void loadButton_Click(object sender, EventArgs e)
		{
			Id = playlists.FirstOrDefault(p => p.Name == (string)playlistComboBox.SelectedItem)?.Id;
			ReadStatistics = showStatisticsCheckBox.Checked;
		}
	}
}
