using Sandbox;
using Sandbox.UI;

namespace RPBox.UI
{
	public partial class TestHud : HudEntity<RootPanel>
	{
		public TestHud()
		{
			Log.Info( "Loading TestHud" );
			if ( !IsClient )
				return;

			RootPanel.AddChild<Overhead>();
			RootPanel.AddChild<Health>();
			RootPanel.AddChild<Job>();
			RootPanel.AddChild<JobSelectMenu>();
			RootPanel.AddChild<ChatBox>();
		}
	}
}
