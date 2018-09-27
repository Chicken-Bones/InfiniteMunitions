using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CodeChicken.InfiniteMunitions
{
	public class EndlessAmmoItem : ModItem
	{
		public readonly Item baseItem;

		public override string Texture => mod.Name + "/assets/endless_" + baseItem.type;
		public override bool CloneNewInstances => true;

		internal EndlessAmmoItem(Item baseItem) {
			this.baseItem = baseItem;
		}

		internal EndlessAmmoItem(int type) {
			baseItem = new Item();
			baseItem.SetDefaults(type, true);
		}

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Endless " + baseItem.Name + " " + (baseItem.ammo == AmmoID.Arrow ? "Quiver" : "Pouch"));
		}

		public override void SetDefaults() {
			item.width = 26;
			item.height = 26;
			item.ranged = true;
			item.ammo = baseItem.ammo;
			item.shoot = baseItem.shoot;
			item.shootSpeed = baseItem.shootSpeed;
			item.damage = baseItem.damage;
			item.knockBack = baseItem.knockBack;
			item.value = baseItem.value*1000;
			item.rare = baseItem.rare + 1;
		}

		public override void AddRecipes() {
			var recipe = new ModRecipe(mod);
			recipe.AddTile(TileID.CrystalBall);
			recipe.AddIngredient(baseItem.ammo == AmmoID.Arrow ? ItemID.EndlessQuiver : ItemID.EndlessMusketPouch);
			recipe.AddIngredient(baseItem.type, 3996);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
