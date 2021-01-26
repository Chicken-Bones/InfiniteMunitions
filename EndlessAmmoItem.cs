﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InfiniteMunitions
{
	public class EndlessAmmoItem : ModItem
	{
		public readonly Item baseItem;

		public override string Name => "endless_" + baseItem.type;
		public override string Texture => Mod.Name + "/assets/" + Name;

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
			Item.width = 26;
			Item.height = 26;
			Item.DamageType = DamageClass.Ranged;
			Item.ammo = baseItem.ammo;
			Item.shoot = baseItem.shoot;
			Item.shootSpeed = baseItem.shootSpeed;
			Item.damage = baseItem.damage;
			Item.knockBack = baseItem.knockBack;
			Item.value = baseItem.value*1000;
			Item.rare = baseItem.rare + 1;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddTile(TileID.CrystalBall)
				.AddIngredient(baseItem.ammo == AmmoID.Arrow ? ItemID.EndlessQuiver : ItemID.EndlessMusketPouch)
				.AddIngredient(baseItem.type, 3996)
				.Register();
		}
	}
}
