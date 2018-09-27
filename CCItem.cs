using System.Text.RegularExpressions;
using Terraria.ModLoader;

namespace CodeChickenLib
{
	public abstract class CCItem : ModItem
	{
		public override string Texture => mod.Name + "/assets/" +
			Regex.Replace(GetType().Name, "([A-Z])", "_$1").Trim('_').ToLower();
	}
}
