
using EngineerTips.Core.Soils;
using EngineerTips.Core.Soils.Ratios;

namespace EngineerTips.Core.RibbonFoundations
{
    public class RibbonFoundationParameters
    {
        public SoilPropertiesCalculationMethods SoilPropertiesCalculationMethod { get; set; }

        public double Width { get; set; }

        public double PaddingDepth { get; set; }

        public double BasementDepth { get; set; }

        public bool BasementExists { get; set; }

        public double SoilLayerHeightHigherFoundationBottom { get; set; }

        public double BasementFloorHeight { get; set; }

        public double BasementFloorAverageWeight { get; set; }

        public double ConstructionHeight { get; set; }

        public double ConstructionLength { get; set; }

        public BuildingTypesSuValue.BuildingType BuildingType { get; set; }

        public double Fv { get; set; } // Навантаження на погонний метр стрічкового фундаменту та призначення будівлі
    }
}
