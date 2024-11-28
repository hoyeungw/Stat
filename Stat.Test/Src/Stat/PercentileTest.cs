using System;
using Aryth;
using NUnit.Framework;
using Spare;
using Veho;
using Veho.Vector;
using static System.Math;
using static Aryth.Math;

namespace Stat.Test.Stat {
  public static partial class Util {
    public static double Percentile(double[] vec, double percent) {
      Array.Sort(vec);
      var len = vec.Length;
      var ti = (len - 1) * percent + 1;
      if (AlmostEqual(ti, 1d, 0.0001)) return vec[0];
      if (AlmostEqual(ti, len, 0.0001)) return vec[len - 1];
      var k = (int)ti;
      var d = ti - k;
      return vec[k - 1] + d * (vec[k] - vec[k - 1]);
    }

    // public static double PRankBeta(this double[] vec, double value) { }

    public static double PercentileFB(this double[] vec, double k) {
      var len = vec.Length;
      var ind = (int)Round(k * len / 100); // "kth percentile position"
      Array.Sort(vec);
      return vec[ind];
    }

    public static double PercentileNR(this double[] vec, double k) {
      var len = vec.Length;
      var ind = (int)Round(k * len / 100); // "kth percentile position"
      Array.Sort(vec);
      return vec[ind];
    }

    public static double PercentileInc(this double[] vec, double perc) {
      var len = vec.Length;
      var id = perc * (len - 1);
      int lo = (int)Floor(id), hi = (int)Ceiling(id);
      double scoLo = vec[lo], scoHi = vec[hi];
      double mod = id - lo;
      return scoLo + mod * (scoHi - scoLo);
    }

    public static int Size<T>(this T[] vec, out int lo, out int hi) {
      lo = vec.GetLowerBound(0);
      hi = vec.GetUpperBound(0);
      return vec.Length;
    }

    public static (int, int) Vol<T>(this T[] vec) => (vec.GetLowerBound(0), vec.GetUpperBound(0));
    public static (int, int) Leap<T>(this T[] vec) => (vec.GetLowerBound(0), vec.Length);

    public static double PercentileExc(this double[] vec, double perc) {
      var len = vec.Size(out var lo, out var hi);
      var id = perc * len;
      int iLo = (int)Floor(id), iHi = (int)Ceiling(id);

      double scoLo = vec[iLo], scoHi = vec[iHi];
      double mod = id - iLo;
      return scoLo + mod * (scoHi - scoLo);
    }


    // public static (string, (double min, double max))[] PercentileIntervals(this double[] values, double[] percentiles) {
    //   percentiles = percentiles.Filter(x => (0d, 1d).Allow(x)).ToArray();
    //   if (percentiles.Length <= 0 || values.Length <= 0) return Vec.Iso(1, (string.Empty, (double.NaN, double.NaN)));
    //   if (percentiles.Length == 1) {
    //     
    //   }
    // }
  }

  [TestFixture]
  public class PercentileTest {
    public static double[] alpha = { 13, 12, 11, 8, 4, 3, 2, 1, 1, 1 };
    public static double[] beta = { 0, 40, 50, 60, 80, 90, 100 };
    public static double[] gamma = { 80, 0, 40, 100, 60, 80, 90, };
    public static double[] delta = { 7, 5, 5, 4, 4, 3, 3, 3, 2, 1 };
    [Test]
    public void TestPercentile() {
      var vec = delta;
      double[] percents = { 0.1, 0.25, 0.5, 0.75, 0.9, 0.96 };
      var raw = vec.Slice();
      var sorted = raw.Sort(Comparer.NumAsc);
      sorted.Deco().Says("sorted");
      foreach (var perc in percents) {
        Console.WriteLine($">> [{perc * 100}] percentile rank ({sorted.PercentileExc(perc):n2})");
      }
    }
    [Test]
    public void Test() {
      var vec = gamma;
      var percentiles = Vec.From(0, 0.5, 0.8, 0.9, 1);
      var indexes = percentiles.Map(percentile => {
        var c = (vec.Length - 1) * percentile;
        Console.WriteLine($">> [c] {c}");
        for (var i = vec.Length - 1; i >= 0; i--)
          if (i <= c)
            return i;
        return -1;
      });
      Console.WriteLine($">> [indexes] {indexes.Deco()}");
      var percentileList = Vec.From(0, 3, 6);

      // foreach (var percentile in percentileList) {
      //   Console.WriteLine($">> [{percentile}] th smallest element is ({vec.Smalldian(percentile)})");
      // }
    }
  }
}