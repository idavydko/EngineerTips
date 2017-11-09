using System.Collections.Generic;

namespace EngineerTips.Core.Soils.Ratios
{
    // Su, мм
    public class BuildingTypesSuValue
    {
        public enum BuildingType
        {
            FerroConcreteFrame,                     // Виробничі і цивільні одноповерхові і багатоповерхові будинки з повним ЗАЛІЗОБЕТОННИМ каркасом
            FerroConcreteFrameMonilithicOverlap,    // Виробничі і цивільні одноповерхові і багатоповерхові будинки з повним ЗАЛІЗОБЕТОННИМ каркасом 
                                                    // з улаштуванням залізобетонних поясів або монолітних перекриттів, а також будівель монолітної конструкції
            SteelFrame,                             // Виробничі і цивільні одноповерхові і багатоповерхові будинки з повним СТАЛЕВИМ каркасом
            SteelFrameMonilithicOverlap,            // Виробничі і цивільні одноповерхові і багатоповерхові будинки з повним СТАЛЕВИМ каркасом 
                                                    // з улаштуванням залізобетонних поясів або монолітних перекриттів, а також будівель монолітної конструкції
            WithoutUnevenSedimentationSoilEfforts,  // Будинки і споруди, у конструкціях яких не виникають зусилля від нерівномірних осідань
            LargePanelWalls,                        // Багатоповерхові безкаркасні будинки з несучими стінами з крупних панелей
            LargeBlockOrBrickWithoutSteel,          // Багатоповерхові безкаркасні будинки з несучими стінами з крупних блоків чи цегляної кладки БЕЗ армування 
            LargeBlockOrBrickWithSteel,             // Багатоповерхові безкаркасні будинки з несучими стінами з крупних блоків чи цегляної кладки З армуванням, 
                                                    // у тому числі з влаштуванням залізобетонних поясів або монолітних перекриттів, а також будівель монолітної конструкції 
        }

        private readonly IDictionary<BuildingType, double> _values;
        private static readonly object _locker = new object();
        private static volatile BuildingTypesSuValue _instance;

        public static BuildingTypesSuValue Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null)
                            _instance = new BuildingTypesSuValue();
                    }
                }

                return _instance;
            }
        }

        public double GetSuByBuildingType(BuildingType type)
        {
            double value;
            return _values.TryGetValue(type, out value) ? value : default(double);
        }

        private BuildingTypesSuValue()
        {
            _values = new Dictionary<BuildingType, double>
            {
                {BuildingType.FerroConcreteFrame, 100 },
                {BuildingType.FerroConcreteFrameMonilithicOverlap, 150 },
                {BuildingType.SteelFrame, 150 },
                {BuildingType.SteelFrameMonilithicOverlap, 180 },
                {BuildingType.WithoutUnevenSedimentationSoilEfforts, 200 },
                {BuildingType.LargePanelWalls, 120 },
                {BuildingType.LargeBlockOrBrickWithoutSteel, 120 },
                {BuildingType.LargeBlockOrBrickWithSteel, 180 },
            };
        }
    }
}
