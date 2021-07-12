using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace RPGamemode.UI
{
    public partial class Job : Panel
    {
        public Label JobText;
        public Panel Wrapper;
        public ProgressBar ProgressBar;

        public Job()
        {
			StyleSheet.Load("/UI/Styles/Job.scss");
            Wrapper = Add.Panel("wrapper");
            Wrapper.Add.Label("💼", "icon");
            JobText = Wrapper.Add.Label("Civilian", "value");
        }

        public override void Tick()
        {
            base.Tick();
            if (Local.Pawn.GetType().Equals(typeof(Pawns.SelectJob))) return;
            var player = (Pawns.GamePlayer)Local.Pawn;
			if (!(player.Job is null) && player.Job.Name != JobText.Text)
            	JobText.Text = $"{player.Job.Name}";
        }
    }
}
