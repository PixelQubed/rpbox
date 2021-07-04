using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace SBoxGamemodeTest.UI
{
    public class ProgressBar : Panel
    {
        private Label Bar;
        public float percentage;

        public ProgressBar(float perc)
        {
            percentage = perc;
            Bar = Add.Label(null, "progressBar");
        }

        public override void Tick()
        {
            Bar.Style.Width = Length.Percent(percentage);
        }
    }
}
