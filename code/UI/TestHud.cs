using Sandbox;
using Sandbox.UI;

namespace RPGamemode.UI
{
	public partial class TestHud : HudEntity<RootPanel>
	{
		public TestHud()
		{
			Log.Info( "Loading TestHud" );
			if ( !IsClient )
				return;

			RootPanel.StyleSheet.Load( "/UI/Styles/TestHud.scss" );

			RootPanel.AddChild<NameTags>();
			RootPanel.AddChild<Health>();
		}
	}
}
