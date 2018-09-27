using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace CodeChicken.InfiniteMunitions
{
	public static class WeaponUIExtension
	{
		public interface Rotation
		{
			bool CanRotate(Player player, Item weapon);
			Item GetUI(Player player, Item weapon);
			void Rotate(Player player, Item weapon, int offset, bool scroll);
		}
		
		public static readonly List<int> AmmoOrder = Enumerable.Range(54, 4).Concat(Enumerable.Range(0, 54)).ToList();

		private static List<Rotation> rotations = new List<Rotation>();
		public static void Register(Rotation rotation) => rotations.Add(rotation);
		internal static void Unload() { rotations.Clear(); }

		public static void Rotate(Player player, int offset, bool scroll) {
			var weapon = GetActiveWeapon(player);
			GetRotation(player, weapon)?.Rotate(player, weapon, offset, scroll);
		}

		public static void Draw(Player player) {
			var weapon = GetActiveWeapon(player);
			var item = GetRotation(player, weapon)?.GetUI(player, weapon);
			if (item == null || item.type <= 0)
				return;

			Main.inventoryScale = 0.65f;
			int x = 20;
			int y = (int) (42 - Main.inventoryScale*22);
			for (int i = 0; i < 10; i++)
				x += (int)(Main.inventoryBackTexture.Width * Main.hotbarScale[i]) + 4;

			if (player.selectedItem >= 10)
				x += Main.inventoryBackTexture.Width + 4;

			var rect = Main.inventoryBackTexture.Frame();
			rect.Offset(x, y);

			if (!player.hbLocked && !PlayerInput.IgnoreMouseInterface && rect.Contains(Main.mouseX, Main.mouseY) && !player.channel)
			{
				player.mouseInterface = true;
				player.showItemIcon = false;
				Main.hoverItemName = item.AffixName();
				if (item.stack > 1)
				{
					object obj = Main.hoverItemName;
					Main.hoverItemName = string.Concat(obj, " (", item.stack, ")");
				}
				Main.rare = item.rare;
			}

			ItemSlot.Draw(Main.spriteBatch, new Item[] {item}, ItemSlot.Context.InventoryAmmo, 
				0, new Vector2(x, y), new Color(1f, 1f, 1f, 0.75f));
		}

		private static Item GetActiveWeapon(Player player) => 
			player.inventory[player.nonTorch >= 0 ? player.nonTorch : player.selectedItem];

		private static Rotation GetRotation(Player player, Item weapon) =>
			rotations.FirstOrDefault(r => r.CanRotate(player, weapon));
	}
}
