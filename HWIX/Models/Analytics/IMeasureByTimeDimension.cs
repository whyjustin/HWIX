using System;

namespace HWIX.Models.Analytics {
    public interface IMeasureByTimeDimension : IMeasureByDimension {
        DateTime Time { get; }
    }
}