using Sandbox;
using Sandbox.Internal;
using System.Text.Json;
using System.Collections.Generic;

namespace RPBox
{
	[Library("job")]
	public partial class Job : Entity
	{
		[Net]
		public string Name { get; set; }
		[Net]
		public int Income { get; set; }
		[Net]
		public List<string> Loadout { get; set; }

		public Job()
		{
			Transmit = TransmitType.Always;

			Log.Info("Creating Job Instance...");

			if ( IsClient )
			{
				Log.Info( "Creating job on client!" );
			}

			// JobManager.Instance.Jobs.Add(this);
		}

	}

    public partial class JobManager : Entity
    {
		private static JobManager instance;
		public static JobManager Instance { get => instance; set => instance = value; }
		[Net, OnChangedCallback]
        public List<Job> Jobs { get; set; }

		public JobManager()
		{
			Transmit = TransmitType.Always;
			instance = this;
			RPBoxGame.Instance.JobManager = this;

			if (IsServer) {
				var jobsJson = FileSystem.Mounted.ReadAllText("/data/jobs.json");
				Jobs = new List<Job>();
				Jobs = JsonSerializer.Deserialize<List<Job>>(jobsJson);
			}
		}

		public Job GetJob(int id)
		{
			return Jobs[id];
		}

		public int GetJobIndex(Job job)
		{
			return Jobs.IndexOf(job);
		}

		private void OnJobsChanged()
		{
			Log.Info($"Jobs has changed! {Jobs} {Jobs.Count}");
			UI.JobSelectMenu.Instance.UpdateJobs();
		}

		[ServerCmd("change_job")]
		public static void ChangeJob(int id)
		{
			var job = JobManager.Instance.GetJob(id);
			if (job is null)
			{
				Sandbox.Log.Error($"job {id} does not exist!");
				return;
			}

			var owner = ConsoleSystem.Caller;

			if (owner == null)
				return;

			owner.Pawn.Delete();

			var player = new Pawns.SandboxPlayer();
			player.Job = job;
			owner.Pawn = player;

			player.Respawn();

			Log.Info($"Player is now playing as {player.Job.Name}");

			
		}
    }
}
