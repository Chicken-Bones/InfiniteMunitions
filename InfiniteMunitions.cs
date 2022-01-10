using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfiniteMunitions
{
    public class InfiniteMunitions : Mod
	{
		public override void Load() {
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

		private ModItem AddEndless(int type) {
			var item = new EndlessAmmoItem(type);
			AddContent(item);
			return item;
		}

		public override void AddRecipes() {
			ModContent.Find<ModItem>(Name, "endless_" + ItemID.SilverBullet)
				.CreateRecipe()
					.AddTile(TileID.CrystalBall)
					.AddIngredient(ItemID.EndlessMusketPouch)
					.AddIngredient(ItemID.TungstenBullet, 3996)
					.Register();
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