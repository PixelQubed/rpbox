using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using Sandbox.UI.Tests;
using System.Collections.Generic;

namespace RPGamemode.UI
{
	[Library]
	public partial class JobSelectMenu : Panel
	{
		private readonly VirtualScrollPanel Overview;
		public JobSelectMenu()
		{
			Instance = this;

			StyleSheet.Load("/UI/Styles/JobSelectMenu.scss");

			var left = Add.Panel("left");
			var right = Add.Panel("right");
			{
				var jobsPage = right.Add.Panel("jobspage");
				{
					jobsPage.AddClass("page");
					jobsPage.SetClass("active", true);

					List<Jobs.Base> jobs = new();

					if (RPGame.Instance.Jobs != null)
					{
						jobs = RPGame.Instance.Jobs;
					}

					jobsPage.AddChild(out Overview, "overview");
					{
						Overview.Layout.AutoColumns = true;
						Overview.Layout.ItemSize = new Vector2(100, 100);
						Overview.OnCreateCell = (cell, data) =>
						{
							var entry = (Jobs.Base)data;
							Log.Info(entry);
							var icon = cell.Add.Button(entry.Name, "icon");
							icon.AddEventListener("onclick", () => ConsoleSystem.Run("change_job", entry.Name));
							icon.Style.Background = new PanelBackground
							{
								Texture = Texture.Load($"/Jobs/Imgs/{entry.Name}.png", true)
							};
							var overlay = cell.Add.Panel("overlay");
						};

						foreach (var job in jobs)
						{
							Overview.AddItem(job);
						}
					}

					var information = jobsPage.Add.Panel("jobs-overview");
					{
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

		public static JobSelectMenu Instance { get; set; }

		public override void Tick()
		{
			base.Tick();

            var pawn = Local.Pawn.GetType();
            if (pawn.Equals(typeof(Pawns.SelectJob)) && !Parent.HasClass("menuOpen"))
				Parent.AddClass("menuOpen");
			else if (!pawn.Equals(typeof(Pawns.SelectJob)) && Parent.HasClass("menuOpen"))
				Parent.RemoveClass("menuOpen");

			UpdateJobs();
		}

		public void UpdateJobs()
		{
			if (Overview.ChildCount != 0)
				return;
			if (RPGame.Instance.Jobs == null)
			{
				//RPGame.GetJobs();
				return;
			}

			foreach (var job in RPGame.Instance.Jobs)
			{
				Overview.AddItem(job);
			}
		}
	}
}
