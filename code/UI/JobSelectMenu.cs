using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using Sandbox.UI.Tests;
using System.Collections.Generic;

namespace RPBox.UI
{
	[Library]
	public partial class JobSelectMenu : Panel
	{
		private readonly VirtualScrollPanel Overview;
		private static JobSelectMenu instance;
		public static JobSelectMenu Instance { get => instance; set => instance = value; }
		private bool Open { get; set; }

		private RPBox.Job Job { get; set; }

		public JobSelectMenu()
		{
			instance = this;

			Open = false;

			StyleSheet.Load("/UI/Styles/JobSelectMenu.scss");

			var left = Add.Panel("left");
			var right = Add.Panel("right");
			{
				var jobsPage = right.Add.Panel("jobspage");
				{
					jobsPage.AddClass("page");
					jobsPage.SetClass("active", true);



					jobsPage.AddChild(out Overview, "overview");
					{
						Overview.Layout.AutoColumns = true;
						Overview.Layout.ItemSize = new Vector2(100, 100);
						Overview.OnCreateCell = (cell, data) => 
						{
							var entry = (RPBox.Job)data;
							var icon = cell.Add.Button(entry.Name, "icon");

							// This is probably trival and bad code... oh well...
							icon.AddEventListener("onclick", () => Job = entry);
							icon.Style.Background = new PanelBackground
							{
								Texture = Texture.Load( FileSystem.Data.FindFile( "images", $"{entry.Name}.png" ).GetEnumerator().Current )
							};
							var overlay = cell.Add.Panel("overlay");
						};

						if (!(JobManager.Instance is null)) {
							UpdateJobs();
						}
					}

					var information = jobsPage.Add.Panel("job-info");
					{
						information.Add.Button( "X", "close-btn", () => {
								information.Style.Display = DisplayMode.None;
								information.Style.Dirty();
							});
					}
				}

				var header = left.Add.Panel("header");
				{
					header.Add.Label("RP Gamemode", "title");
				}

				var body = left.Add.Panel("body");
				{
					var tabs = body.AddChild<ButtonGroup>();
					tabs.AddClass("tabs");
					tabs.SelectedButton = tabs.AddButtonActive("Jobs", (b) => jobsPage.SetClass("active", b));
				}
			}
		}

		public override void Tick()
		{
			base.Tick();

            var pawn = Local.Pawn.GetType();
            if (pawn.Equals(typeof(Pawns.SelectJob)) && !Parent.HasClass("menuOpen") || Open)
				Parent.AddClass("menuOpen");
			else if (!pawn.Equals(typeof(Pawns.SelectJob)) && Parent.HasClass("menuOpen") && !Open)
				Parent.RemoveClass("menuOpen");
		}

		public override void OnKeyTyped( char key )
		{
			Log.Info(key);
			if (key == 'f') {
				Parent.SetClass("menuOpen", !Open);
				Open = !Open;
			}
		}

		public void UpdateJobs()
		{
			if (JobManager.Instance is null || JobManager.Instance.Jobs is null || Overview.ChildCount == JobManager.Instance.Jobs.Count)
				return;
			
			Log.Info($"Updating Jobs List... Old Job Count: {Overview.ChildCount}, New Job Count: {JobManager.Instance.Jobs.Count}");

			Overview.Clear();

			foreach (var job in JobManager.Instance.Jobs)
			{
				if (job is null) {
					continue;
				}
				Log.Info($"Adding Job: {job.Name}");
				Overview.AddItem(job);
			}
		}
	}
}
