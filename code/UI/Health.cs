using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace RPBox.UI
{
    public partial class Health : Panel
    {
        public Label HealthText;
        public Panel Wrapper;
        public ProgressBar ProgressBar;

        public Health()
        {
			StyleSheet.Load("/UI/Styles/Health.scss");
            ProgressBar = AddChild<ProgressBar>("progressBar");
            Wrapper = Add.Panel("wrapper");
            Wrapper.Add.Label("🩸", "icon");
            HealthText = Wrapper.Add.Label("100", "value");
        }

        public override void Tick()
        {
            base.Tick();
            var player = Local.Pawn;
            if (player == null) return;

            HealthText.Text = $"{player.Health:n0}";

            ProgressBar.PercentValue = player.Health;
        }
    }
}
