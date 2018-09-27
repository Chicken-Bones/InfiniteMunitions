using Terraria;
using Terraria.ID;

namespace CodeChicken.InfiniteMunitions
{
	internal class MageHoodRotate : WeaponUIExtension.Rotation
	{
		private static bool IsHood(Item item) => item.type == ItemID.SpectreHood || item.type == ItemID.SpectreMask;

		private static int GetInvHoodSlot(Player player) {
			for (int i = 0; i < 54; i++)
				if (IsHood(player.inventory[i]) && player.inventory[i].type != player.armor[0].type)
					return i;

			return -1;
		}

		public bool CanRotate(Player player, Item weapon) => 
			weapon.magic && IsHood(player.armor[0]) && GetInvHoodSlot(player) >= 0;

		public Item GetUI(Player player, Item weapon) => player.armor[0];

		public void Rotate(Player player, Item weapon, int offset, bool scroll) {
			if (!scroll) {
				Utils.Swap(ref player.armor[0], ref player.inventory[GetInvHoodSlot(player)]);
				player.armor[0].favorited = false;
			}
		}
	}
}
