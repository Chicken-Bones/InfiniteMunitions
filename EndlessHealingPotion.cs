using CodeChickenLib;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CodeChicken.InfiniteMunitions
{
	public class EndlessHealingPotion : CCItem
	{
		public override bool ConsumeItem(Player player) => false;
		
		public override void SetDefaults() {
			var baseItem = new Item();
			baseItem.SetDefaults(ItemID.SuperHealingPotion, true);
			
			item.width = 26;
			item.height = 26;
			item.consumable = true;
			item.potion = true;
			item.healLife = baseItem.healLife;
			item.UseSound = baseItem.UseSound;
			item.useStyle = baseItem.useStyle;
			item.useTurn = baseItem.useTurn;
			item.useAnimation = baseItem.useAnimation;
			item.useTime = baseItem.useTime;
			item.value = baseItem.value*200;
			item.rare = baseItem.rare + 1;
		}

		public override void AddRecipes() {
			var recipe = new ModRecipe(mod);
			recipe.AddTile(TileID.CrystalBall);
			recipe.AddIngredient(ItemID.SuperHealingPotion, 300);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
