using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using RPBox;

namespace RPBox.UI
{
    public class BaseOverhead : BaseNameTag
    {
		private ProgressBar ProgressBar;
        public BaseOverhead( Player player ) : base(player)
        {
            ProgressBar = AddChild<ProgressBar>("progressBar");

			if (player.GetType() == typeof(Pawns.SelectJob))
				return;
			Pawns.SandboxPlayer gamePlayer = (Pawns.SandboxPlayer)player;
			var jobName = Add.Label($"{gamePlayer.Job.Name}", "job");
        }

		public override void UpdateFromPlayer( Player player )
		{
			ProgressBar.PercentValue = player.Health;
		}
		
    }

	public partial class Overhead : NameTags
	{
		public Overhead()
		{
			StyleSheet.Load( "/UI/Styles/Overhead.scss" );
		}
		public override BaseOverhead CreateNameTag( Player player )
		{
			if ( player.GetClientOwner() == null )
				return null;

			var tag = new BaseOverhead( player );
			tag.Parent = this;
			return tag;
		}

		public override void Tick()
		{
			base.Tick();

			if (Local.Pawn.GetType() == typeof(Pawns.SelectJob) && Style.Display != DisplayMode.None) {
				Style.Display = DisplayMode.None;
				Style.Dirty();
			} else if (Style.Display == DisplayMode.None) {
				Style.Display = DisplayMode.Flex;
				Style.Dirty();
			}
		}

		public void UpdateFromPlayer(Player player)
		{
			if (Local.Pawn != player)
				return;
		}
	}
}
