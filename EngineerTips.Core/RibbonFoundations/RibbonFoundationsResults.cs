
namespace EngineerTips.Core.RibbonFoundations
{
    public class RibbonFoundationsResults
    {
        public double EstimatedSoilResistance { get; set; } // Розрахунковий опір грунту, кПа
        public double AverageFoundationPressure { get; set; } // Середній тиск на фундаменти, кПа
        public double ResidemationByRosenfeld { get; set; } // Осідання за Розенфельдом, мм
        public double ResidemationByLayersMethod { get; set; } // Осідання пошаровим методом, мм
    }
}
