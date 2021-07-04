using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace SBoxGamemodeTest.UI
{
    public class Health : Panel
    {
        public Label HealthText;
        public ProgressBar ProgressBar;

        public Health()
        {
            ProgressBar = new ProgressBar(100);
            Add.Label("🩸", "icon");
            HealthText = Add.Label("100", "value");
        }

        public override void Tick()
        {
            var player = Local.Pawn;
            if (player == null) return;

            HealthText.Text = $"{player.Health:n0}";
            ProgressBar.Style.Width = Length.Percent(player.Health);
        }
    }
}
