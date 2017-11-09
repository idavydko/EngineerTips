
using System;
using System.Collections.Generic;
using System.Linq;

namespace EngineerTips.Core.Soils.Calculators.ResidemationByLayers
{
    public class ResidemationByLayersCalculator
    {
        private readonly SoilLayerParameters[] layers;
        private readonly double PaddingDepth;
        private readonly double FoundationWidth;
        private readonly double ResidemationByRosenfeld;
        private readonly double Gamma11Above;

        public ResidemationByLayersCalculator(SoilLayerParameters[] layers,
                                              double paddingDepth,
                                              double foundationWidth,
                                              double residemationByRosenfeld,
                                              double gamma11Above)
        {
            this.layers = layers;
            PaddingDepth = paddingDepth;
            FoundationWidth = foundationWidth;
            ResidemationByRosenfeld = residemationByRosenfeld;
            Gamma11Above = gamma11Above;
        }

        public double Calculate()
        {
            var levels = new List<SoilLayerPartition>();
            var startLevel = CalculateFirstLevel();

            levels.Add(startLevel);

            foreach (var layer in layers)
            {
                var partitionsNumber = 10;
                var partitionH = layer.hLayer / partitionsNumber;

                while (partitionH > 2)
                {
                    partitionsNumber = partitionsNumber * 2;
                    partitionH = layer.hLayer / partitionsNumber;
                }

                for (int i = 0; i < partitionsNumber; i++)
                {
                    var previousLevel = levels.Last();
                    var level = CalculateLevel(partitionH, previousLevel, layer);

                    levels.Add(level);
                }
            }

            return levels.Sum(x => x.S);
        }

        private SoilLayerPartition CalculateFirstLevel()
        {
            var level = new SoilLayerPartition
            {
                Depth = -1 * PaddingDepth,
                AlphaK = 1,
                SigmaZP = ResidemationByRosenfeld - PaddingDepth * Gamma11Above
            };
            
            level.Z = level.Depth < 0 ? 0 : level.Depth;

            level.Zita = 2 * level.Z / FoundationWidth;
            
            return level;
        }

        private SoilLayerPartition CalculateLevel(double partitionH, SoilLayerPartition previousLevel, SoilLayerParameters layer)
        {
            var level = new SoilLayerPartition();

            level.Depth = previousLevel.Depth + partitionH;
            level.Depth1 = previousLevel.Depth1 + partitionH;

            level.Z = level.Depth < 0 ? 0 : level.Depth;

            level.SigmaZG = previousLevel.SigmaZG + partitionH * layer.Rou * 10;
            level.SigmaDiveded = level.SigmaZG * 0.2;
            level.Zita = Math.Round(2 * level.Z / FoundationWidth, 1);

            if (level.Zita > 12)
                level.AlphaK = 0.106;
            else
                level.AlphaK = 1; // TODO map table

            level.SigmaZP = level.AlphaK * previousLevel.SigmaZP;

            level.SigmaZpCp = (level.SigmaZP + previousLevel.SigmaZP) / 2;
            level.H = level.Z - previousLevel.Z;
            level.h = layer.hLayer / 10;
            level.E = layer.E;

            level.S = level.Z == default(double) || (level.SigmaDiveded > 14) || level.E == 0
                ? default(double)
                : 0.8 * level.SigmaZpCp / level.E / 1000 * level.h * 1000;

            return level;
        }
    }
}
