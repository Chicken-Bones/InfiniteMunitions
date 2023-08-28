using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfiniteMunitions
{
	[Autoload(false)]
	public class EndlessFallenStar : EndlessAmmoItem
	{
		internal EndlessFallenStar() : base(ItemID.FallenStar) { }

		public override void SetDefaults() {
			base.SetDefaults();
			Item.value = baseItem.value * 400;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddTile(TileID.CrystalBall)
				.AddIngredient(baseItem.type, 400)
				.Register();
		}

		public override void Update(ref float gravity, ref float maxFallSpeed) {
			if (Main.dayTime) {
				var vel = Item.velocity * 1.5f;
				var dustRect = new Rectangle((int)(Item.position.X - 25), (int)(Item.position.Y - 25), 50, 50);
				for (int j = 0; j < 200; j++) { // a lot of dust! Vanilla is 10
					Dust.NewDust(Main.rand.NextVector2FromRectangle(dustRect), Item.width, Item.height, DustID.MagicMirror, vel.X, vel.Y, 150, default, 1.2f);
				}

				for (int k = 0; k < 20; k++) { // larger and slightly longer lasting yellow particles
					var pos = Item.position + Main.rand.NextVector2FromRectangle(new Rectangle(-25, -25, 50, 50));
					Gore.NewGore(null, Main.rand.NextVector2FromRectangle(dustRect), vel, Main.rand.Next(16, 18));
				}

				Item.active = false;
				Item.type = ItemID.None;
				Item.stack = 0;
				if (Main.netMode == NetmodeID.Server)
					NetMessage.SendData(MessageID.SyncItem, -1, -1, null, Item.whoAmI);
			}
		}
	}
}
