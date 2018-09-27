using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace CodeChicken.InfiniteMunitions
{
	public class InfiniteMunitions : Mod
	{
		public const string HotkeyName = "Rotate Ammo";

		public override void Load() {
			RegisterHotKey(HotkeyName, "Q");
			WeaponUIExtension.Register(new AmmoRotate());
			WeaponUIExtension.Register(new MageHoodRotate());
			
			AddEndless(ItemID.FlamingArrow);
			AddEndless(ItemID.UnholyArrow);
			AddEndless(ItemID.JestersArrow);
			AddEndless(ItemID.HellfireArrow);
			AddEndless(ItemID.HolyArrow);
			AddEndless(ItemID.CursedArrow);
			AddEndless(ItemID.FrostburnArrow);
			AddEndless(ItemID.ChlorophyteArrow);
			AddEndless(ItemID.IchorArrow);
			AddEndless(ItemID.VenomArrow);
			AddEndless(ItemID.BoneArrow);
			AddEndless(ItemID.MoonlordArrow);
			
			AddEndless(ItemID.MeteorShot);
			AddEndless(ItemID.SilverBullet);
			AddEndless(ItemID.CrystalBullet);
			AddEndless(ItemID.CursedBullet);
			AddEndless(ItemID.ChlorophyteBullet);
			AddEndless(ItemID.HighVelocityBullet);
			AddEndless(ItemID.IchorBullet);
			AddEndless(ItemID.VenomBullet);
			AddEndless(ItemID.PartyBullet);
			AddEndless(ItemID.NanoBullet);
			AddEndless(ItemID.ExplodingBullet);
			AddEndless(ItemID.GoldenBullet);
			AddEndless(ItemID.MoonlordBullet);
		}

		private void AddEndless(int type) {
			AddItem("endless_" + type, new EndlessAmmoItem(type));
		}

		public override void HotKeyPressed(string name) {
			if (name == HotkeyName && PlayerInput.Triggers.JustPressed.KeyStatus[Name + ": " + name])
				WeaponUIExtension.Rotate(Main.player[Main.myPlayer], 1, false);
		}

		public override void PostDrawInterface(SpriteBatch spriteBatch) {
			var player = Main.player[Main.myPlayer];
			if (!Main.playerInventory && !player.ghost) {
				if (Main.hasFocus && !Main.inFancyUI && !Main.ingameOptionsWindow && player.controlTorch)
					WeaponUIExtension.Rotate(player, PlayerInput.ScrollWheelDelta / -120, true);

				WeaponUIExtension.Draw(player);
			}
		}

		public override void Unload() {
			WeaponUIExtension.Unload();
		}

		public override object Call(params object[] args) {
			if (args.Length < 2 || !(args[0] is string)) return null;

			switch ((string) args[0]) {
				case "EndlessAmmoItem":
					var item = args[1] as Item;
					if (item != null)
						return new EndlessAmmoItem(item);
						break;

			}
			return null;
		}
	}
}