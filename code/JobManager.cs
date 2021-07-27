using Sandbox;
using Sandbox.Internal;
using System.Text.Json;
using System.Collections.Generic;

namespace RPGamemode
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
			if (IsClient)
				Log.Info("Creating job on client!");
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
			RPGame.Instance.JobManager = this;

			if (IsServer) {
				var jobsJson = FileSystem.OrganizationData.ReadAllText("jobs.json");
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
    }
}
