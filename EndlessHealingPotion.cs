using CodeChickenLib;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfiniteMunitions
{
	public class EndlessHealingPotion : CCItem
	{
		public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<InfiniteMunitionsItemConfig>().InfiniteHealingPotionEnabled;

		public override bool ConsumeItem(Player player) => false;

		public override void SetDefaults() {
			var baseItem = new Item();
			baseItem.SetDefaults(ItemID.SuperHealingPotion, true);

			Item.width = 26;
			Item.height = 26;
			Item.consumable = true;
			Item.potion = true;
			Item.healLife = baseItem.healLife;
			Item.UseSound = baseItem.UseSound;
			Item.useStyle = baseItem.useStyle;
			Item.useTurn = baseItem.useTurn;
			Item.useAnimation = baseItem.useAnimation;
			Item.useTime = baseItem.useTime;
			Item.value = baseItem.value*200;
			Item.rare = baseItem.rare + 1;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddTile(TileID.CrystalBall)
				.AddIngredient(ItemID.SuperHealingPotion, 150)
				.Register();
		}
	}
}
