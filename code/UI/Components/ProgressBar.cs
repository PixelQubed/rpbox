using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System.Threading;

namespace RPGamemode.UI
{
    public class ProgressBar : Panel
    {
        public Panel Bar;
        private float mValue;
        private float TimeElapsed;
        private float StartValue;
        private readonly bool Testing = false;
        private float valTest = 100;

        private float duartion = .5f;
		public float PercentValue
		{
            get { return mValue; }
            set { mValue = value.Clamp(1, 100); }
        }

        public ProgressBar()
        {
			StyleSheet.Load("/UI/Styles/ProgressBar.scss");
            mValue = 100;
            valTest = 100;
            Bar = Add.Panel("bar");
            Bar.Style.Width = Length.Percent(1);
        }

        public override void Tick()
        {
            if (Testing) {
				if (valTest == 1 && valTest.CompareTo(Bar.Style.Width.Value.Value) == 0)
                    valTest = 100;

                if (valTest == 100 && valTest.CompareTo(Bar.Style.Width.Value.Value) == 0)
                    valTest = 1;

                duartion = 3f;
                mValue = valTest;
            }

            float val = mValue;
            if (Bar.Style.Width.Value.Value != mValue) {
                if (StartValue == -1) {
                    StartValue = Bar.Style.Width.Value.Value;
                    TimeElapsed = 0;
                }

                float t = TimeElapsed / duartion;
                t = t * t * (3f - (1f * t));

                if (t > 1) {
                    t = 1;
                    TimeElapsed = 0;
                } else if (t < 0) {
                    t = 0;
                    TimeElapsed = 0;
                }

                val = StartValue.LerpTo(mValue, t);
                TimeElapsed += Time.Delta;
                if (t == 1) {
                    StartValue = -1;
                }
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
