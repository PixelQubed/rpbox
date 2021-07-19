using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace RPGamemode.UI
{
    public partial class Job : Panel
    {
		private static Job instance;
		public static Job Instance { get => instance; set => instance = value; }
        public Label JobText;
        public Panel Wrapper;
        public ProgressBar ProgressBar;

        public Job()
        {
			instance = this;
			StyleSheet.Load("/UI/Styles/Job.scss");
            Wrapper = Add.Panel("wrapper");
            Wrapper.Add.Label("💼", "icon");
            JobText = Wrapper.Add.Label("Civilian", "value");
        }

		public void UpdateJobText(string jobName)
		{
            JobText.Text = jobName;
		}
    }
}
