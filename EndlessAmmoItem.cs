using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace InfiniteMunitions
{
	public class EndlessAmmoItem : ModItem
	{
		[CloneByReference]
		public readonly Item baseItem;

		public override string Name => "endless_" + baseItem.type;
		public override string Texture => Mod.Name + "/assets/" + Name;
		public override LocalizedText Tooltip => LocalizedText.Empty;

		internal EndlessAmmoItem(Item baseItem) {
			this.baseItem = baseItem;
		}

		internal EndlessAmmoItem(int type) : this(new Item(type)) { }

		protected override bool CloneNewInstances => true;

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
				.AddIngredient(baseItem.type, 3996)
				.Register();
		}
	}
}
