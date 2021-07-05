using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System.Threading;

namespace SBoxGamemodeTest.UI
{
    public class ProgressBar : Panel
    {
        public Panel Bar;
        private float mValue;
        private float TimeElapsed;
        private float StartValue;
        public float PercentValue
        {
            get { return mValue; }
            set { mValue = MathX.Clamp(value, 1, 100); }
        }

        public ProgressBar()
        {
            mValue = 100;
            Bar = Add.Panel("bar");
        }

        public override void Tick()
        {
            float val = mValue;
            if (Bar.Style.Width.Value.Value != mValue) {
                if (StartValue < 0) {
                    StartValue = Bar.Style.Width.Value.Value;
                    TimeElapsed = 0;
                }
                
                if (TimeElapsed > 3) {
                    TimeElapsed = 0;
                }

                float t = TimeElapsed / .5f;
                t = t * t * (3f - 1f * t);

                if (t > 1)
                    t = 1;

                val = MathX.LerpTo(StartValue, mValue, t);
                TimeElapsed += Time.Delta;
            } else {
                StartValue = -1;
            }

            Bar.Style.Width = Length.Percent(val);
            if (val < 22) {
                Bar.Style.Height = Length.Fraction(val / 22);
                Bar.Style.Top = Length.Fraction(((val * -0.5f) / 22) + 0.5f);
                Bar.Style.Left = Length.Fraction(((val * -0.12f) / 22) + 0.12f);
            } else {
                Bar.Style.Height = Length.Fraction(1);
                Bar.Style.Top = Length.Fraction(0);
                Bar.Style.Left = Length.Fraction(0);
            }
            Bar.Style.Dirty();
            base.Tick();
        }
    }
}
