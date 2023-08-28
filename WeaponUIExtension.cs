using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace InfiniteMunitions
{
	public class WeaponUIExtension : ModSystem
	{
		public abstract class Rotation : ILoadable
		{
			public void Load(Mod mod) => Register(this);

			public void Unload() { }

			public abstract bool CanRotate(Player player, Item weapon);
			public abstract Item GetUI(Player player, Item weapon);
			public abstract void Rotate(Player player, Item weapon, int offset, bool scroll);
		}

		private static List<Rotation> rotations = new List<Rotation>();
		public static void Register(Rotation rotation) => rotations.Add(rotation);

		private ModKeybind HotKey;

		private static ModKeybind RegisterKeybindAndBindOnFirstLoad(Mod mod, string name, string defaultKey) {
			var unloadedKeys = PlayerInput.CurrentProfile.InputModes[InputMode.Keyboard].UnloadedModKeyStatus;
			var uniqueKey = $"{mod.Name}/{name}";
			var autoBoundKey = uniqueKey + " AutoBound";

			if (!unloadedKeys.ContainsKey(autoBoundKey)) {
				unloadedKeys[uniqueKey] = new List<string>() { defaultKey };
				unloadedKeys[autoBoundKey] = new List<string>() { "Done" };
				PlayerInput.Save();
			}

			return KeybindLoader.RegisterKeybind(mod, name, defaultKey);
		}

		private static void RenameHotKey(Mod mod, string oldName, string newName) {
			var unloadedKeys = PlayerInput.CurrentProfile.InputModes[InputMode.Keyboard].UnloadedModKeyStatus;
			bool changed = false;
			changed |= TryRenameKey(unloadedKeys, $"{mod.Name}/{oldName}", $"{mod.Name}/{newName}");
			changed |= TryRenameKey(unloadedKeys, $"{mod.Name}/{oldName} AutoBound", $"{mod.Name}/{newName} AutoBound");
			if (changed)
				PlayerInput.Save();
		}

		private static bool TryRenameKey<K, V>(Dictionary<K, V> dict, K from, K to) {
			if (!dict.Remove(from, out var v))
				return false;

			dict.TryAdd(to, v);
			return true;
		}

		public override void Load() {
			RenameHotKey(Mod, "Rotate Ammo", "RotateAmmo");
			HotKey = RegisterKeybindAndBindOnFirstLoad(Mod, "RotateAmmo", "Q");
		}

		public override void Unload() {
			rotations.Clear();
		}

		private Rotation GetRotation(Player player, Item weapon) =>
			rotations.FirstOrDefault(r => r.CanRotate(player, weapon));

        public override void PreUpdatePlayers()
        {
            if (Main.netMode != NetmodeID.Server && HotKey.JustPressed)
				Rotate(Main.LocalPlayer, 1, false);
        }

		public override void PostDrawInterface(SpriteBatch spriteBatch)
		{
			var player = Main.player[Main.myPlayer];
			if (!Main.playerInventory && !player.ghost)
			{
				if (Main.hasFocus && !Main.inFancyUI && !Main.ingameOptionsWindow && player.controlTorch)
					Rotate(player, PlayerInput.ScrollWheelDelta / -120, true);

				Draw(player);
			}
		}

		private void Rotate(Player player, int offset, bool scroll) {
			var weapon = GetActiveWeapon(player);
			GetRotation(player, weapon)?.Rotate(player, weapon, offset, scroll);
		}

		private void Draw(Player player) {
			var weapon = GetActiveWeapon(player);
			var item = GetRotation(player, weapon)?.GetUI(player, weapon);
			if (item == null || item.IsAir)
				return;

			Main.inventoryScale = 0.65f;
			int x = 20;
			int y = (int) (42 - Main.inventoryScale*22);
			for (int i = 0; i < 10; i++)
				x += (int)(TextureAssets.InventoryBack.Width() * Main.hotbarScale[i]) + 4;

			if (player.selectedItem >= 10)
				x += TextureAssets.InventoryBack.Width() + 4;

			var rect = TextureAssets.InventoryBack.Frame();
			rect.Offset(x, y);

			if (!player.hbLocked && !PlayerInput.IgnoreMouseInterface && rect.Contains(Main.mouseX, Main.mouseY) && !player.channel)
			{
				player.mouseInterface = true;
				player.cursorItemIconEnabled = false;
				Main.hoverItemName = item.AffixName();
				if (item.stack > 1)
					Main.hoverItemName += $" ({item.stack})";

				Main.rare = item.rare;
			}

			ItemSlot.Draw(Main.spriteBatch, new Item[] {item}, ItemSlot.Context.InventoryAmmo,
				0, new Vector2(x, y), new Color(1f, 1f, 1f, 0.75f));
		}

		private static Item GetActiveWeapon(Player player) =>
			player.inventory[player.nonTorch >= 0 ? player.nonTorch : player.selectedItem];
	}
}
