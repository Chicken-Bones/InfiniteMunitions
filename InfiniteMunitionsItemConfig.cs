using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace InfiniteMunitions
{
	public class InfiniteMunitionsItemConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ServerSide;

		[ReloadRequired]
		[DefaultValue(true)]
		public bool InfiniteHealingPotionEnabled;

		[ReloadRequired]
		[DefaultValue(true)]
		public bool InfiniteManaPotionEnabled;

		[ReloadRequired]
		[DefaultValue(true)]
		public bool WormholeMirrorEnabled;
	}
}
