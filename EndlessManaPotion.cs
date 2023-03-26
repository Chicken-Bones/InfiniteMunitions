using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfiniteMunitions;

public class EndlessManaPotion : ModItem {
	public override bool ConsumeItem(Player player) => false;

	public override void SetDefaults() {
		var baseItem = new Item();
		baseItem.SetDefaults(ItemID.SuperManaPotion, true);

		Item.width = 26;
		Item.height = 26;
		Item.consumable = true;
		Item.healMana = baseItem.healMana;
		Item.UseSound = baseItem.UseSound;
		Item.useStyle = baseItem.useStyle;
		Item.useTurn = baseItem.useTurn;
		Item.useAnimation = baseItem.useAnimation;
		Item.useTime = baseItem.useTime;
		Item.value = baseItem.value * 200;
		Item.rare = baseItem.rare + 1;
	}

	public override string Texture => Mod.Name + "/assets/endless_mana_potion";

	public override void AddRecipes() {
		CreateRecipe()
			.AddTile(TileID.CrystalBall)
			.AddIngredient(ItemID.SuperManaPotion, 297)
			.Register();
	}
}
