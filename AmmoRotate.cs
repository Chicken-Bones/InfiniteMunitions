using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfiniteMunitions
{
	internal class AmmoRotate : WeaponUIExtension.Rotation
	{
		private static int GetAmmoType(Item weapon) => weapon.useAmmo;

		public override bool CanRotate(Player player, Item weapon) => GetAmmoType(weapon) > 0;

		public override Item GetUI(Player player, Item weapon) => player.ChooseAmmo(weapon);

		private static readonly List<int> AmmoSlotOrder = Enumerable.Range(54, 4).Concat(Enumerable.Range(0, 54)).ToList();

		private bool CanRotateAmmo(int slot, Item item) {
			if (slot < 54 && ModContent.GetInstance<InfiniteMunitionsPersonalConfig>().OnlyRotateAmmoSlots)
				return false;

			if (!item.favorited && ModContent.GetInstance<InfiniteMunitionsPersonalConfig>().OnlyRotateFavoritedItems)
				return false;

			return true;
		}

		public override void Rotate(Player player, Item weapon, int offset, bool scroll) {
			var slots = AmmoSlotOrder.Where(slot => ItemLoader.CanChooseAmmo(weapon, player.inventory[slot], player) && CanRotateAmmo(slot, player.inventory[slot])).ToList();
			if (slots.Count <= 1)
				return;

			while (offset >= slots.Count)
				offset -= slots.Count;
			while (offset < 0)
				offset += slots.Count;
			if (offset == 0)
				return;

			var items = slots.Skip(offset).Concat(slots.Take(offset))
				.Select(i => player.inventory[i].Clone()).ToList();
			for (int i = 0; i < slots.Count; i++)
				player.inventory[slots[i]] = items[i];

			SoundEngine.PlaySound(SoundID.MenuTick);
		}
	}
}
