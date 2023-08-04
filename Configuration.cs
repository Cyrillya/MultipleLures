using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace MultipleLures;

public class Configuration : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;

    public override void OnLoaded() => MultipleLuresGlobalProj.configuration = this;

    [Range(0, 200)]
    [Increment(1)]
    [DefaultValue(5)]
    [Slider]
    public int LuresAmount;
}