using Terraria.ModLoader.Config;

namespace InfiniteMunitions
{
	public class InfiniteMunitionsPersonalConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		public bool OnlyRotateAmmoSlots;
		public bool OnlyRotateFavoritedItems;
	}
}
