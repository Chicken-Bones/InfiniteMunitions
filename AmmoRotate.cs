using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace InfiniteMunitions
{
	internal class AmmoRotate : WeaponUIExtension.Rotation
	{
		private static int GetAmmoType(Item weapon) => weapon.useAmmo;

		private static List<int> GetAmmoSlots(Player player, int ammoType) =>
			WeaponUIExtension.AmmoOrder.Where(i => player.inventory[i].ammo == ammoType).ToList();

		public override bool CanRotate(Player player, Item weapon) => GetAmmoType(weapon) > 0;

		public override Item GetUI(Player player, Item weapon) {
			int ammoType = GetAmmoType(weapon);
			if (ammoType <= 0)
				return null;

			var slots = GetAmmoSlots(player, ammoType);
			return slots.Count > 0 ? player.inventory[slots[0]] : null;
		}

		public override void Rotate(Player player, Item weapon, int offset, bool scroll) {
			int ammoType = GetAmmoType(weapon);
			if (ammoType <= 0)
				return;

			var slots = GetAmmoSlots(player, ammoType);
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
