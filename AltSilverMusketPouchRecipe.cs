using Terraria.ID;
using Terraria.ModLoader;

namespace InfiniteMunitions
{
	public class AltSilverMusketPouchRecipe : ModSystem
	{
		public override void AddRecipes() {
			ModContent.Find<ModItem>(Mod.Name, "endless_" + ItemID.SilverBullet)
				.CreateRecipe()
					.AddTile(TileID.CrystalBall)
					.AddIngredient(ItemID.EndlessMusketPouch)
					.AddIngredient(ItemID.TungstenBullet, 3996)
					.Register();
		}
	}
}
