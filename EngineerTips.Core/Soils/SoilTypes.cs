
namespace EngineerTips.Core.Soils
{
    public enum SoilTypes
    {
        BulkSoilLightDust,
        BulkSoilLightSandyLoam,
        BulkSoilHeavyDust,
        BulkSoilHeavySandyLoam,
        ClayLightDust,
        ClayLightSandyLoam,
        LoamLightDust,
        LoamLightSandyLoam,
        LoamHeavyDust,
        LoamHeavySandyLoam,
        SandyLoamDust,
        SandyLoamSandyLoam,
        SandGravel,
        SandBig,
        SandMiddleBig,
        SandSmall,
        SandDust,
    }

    public enum BulkSoil // Насипний грунт
    {
        LightDust,     // легкий пилуватий
        LightSandyLoam, // легкий піщанистий
        HeavyDust,     // важкий пилуватий
        HeavySandyLoam // важкий піщанистий
    }
    public enum Clay // Глина
    {
        LightDust,     // легка пилувата
        LightSandyLoam // легка піщаниста
    }
    public enum Loam // Суглинок
    {
        LightDust,     // легкий пилуватий
        LightSandyLoam, // легкий піщанистий
        HeavyDust,     // важкий пилуватий
        HeavySandyLoam // важкий піщанистий
    }
    public enum SandyLoam // Супісок
    {
        Dust,       // пилуватий
        SandyLoam   // піщанистий
    }
    public enum Sand // Пісок
    {
        Gravel,     // гравіюватий
        Big,        // крупний
        MiddleBig,  // середньої крупності
        Small,      // мілкий
        Dust,       // пилуватий
    }
}
