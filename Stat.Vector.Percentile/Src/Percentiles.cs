using Veho.Vector;
using static System.Math;

namespace Stat.Vector {
  public static class Percentiles {
    public static double PercentileInc(this double[] vec, double perc) {
      var (min, max) = vec.Bin();
      var len = vec.Length;
      var id = perc * (len - 1);
      int lo = (int)Floor(id), hi = (int)Ceiling(id);
      if (!(min <= lo && hi <= max)) return double.NaN;
      double scoLo = vec[lo], scoHi = vec[hi], mod = id - lo;
      return scoLo + mod * (scoHi - scoLo);
    }

    public static double PercentileExc(this double[] vec, double perc) {
      var (min, max) = vec.Bin();
      var len = vec.Length;
      var id = perc * len;
      int lo = (int)Floor(id), hi = (int)Ceiling(id);
      if (!(min <= lo && hi <= max)) return double.NaN;
      double scoLo = vec[lo], scoHi = vec[hi], mod = id - lo;
      return scoLo + mod * (scoHi - scoLo);
    }
  }
}