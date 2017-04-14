﻿using System;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Web;

namespace YoutubeIdExtractor
{
	public partial class mainForm : Form
	{
		public mainForm()
		{
			InitializeComponent();
		}

		private void startButtonClick(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(playlistIdTextBox.Text))
			{
				return;
			}

			mainTableLayoutPanel.Enabled = false;

			var pId = playlistIdTextBox.Text;

			try
			{
				var url = new Uri(playlistIdTextBox.Text);
				if (url != null && url.Query.Contains("list"))
				{
					var parameters = HttpUtility.ParseQueryString(url.Query);
					if (parameters.AllKeys.Contains("list"))
					{
						pId = parameters["list"];
					}
				}
			}
			catch (UriFormatException)
			{

			}

			try
			{
				if (readStatisticsCheckbox.Checked)
				{
					videoIdsTextBox.Text = YoutubeCommunication.GetPlaylistStats(pId);
				}
				else
				{
					var videoIds = YoutubeCommunication.GetPlaylistItems(pId);

					StringBuilder sb = new StringBuilder();
					foreach (var id in videoIds)
					{
						sb.AppendLine(id);
					}

					videoIdsTextBox.Text = sb.ToString();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, $"Beim Heraussuchen der IDs ist ein Fehler aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Es ist ein Fehler aufgetreten!", MessageBoxButtons.OK, MessageBoxIcon.Error);
				videoIdsTextBox.Text = $"Beim Heraussuchen der IDs ist ein Fehler aufgetreten:{Environment.NewLine}{Environment.NewLine}{ex.Message}";
			}

			mainTableLayoutPanel.Enabled = true;
		}
	}
}
