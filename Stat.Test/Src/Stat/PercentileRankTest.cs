using System;
using System.Linq;
using NUnit.Framework;
using Spare;
using Veho;
using Veho.Vector;
using static Aryth.Math;

namespace Stat.Test.Stat {
  public static partial class Util {


    public static double PRankWiki(this double[] vec, double v) {
      var total = vec.Length;
      var minor = vec.Count(x => x < v);
      var level = vec.Count(x => AlmostEqual(x, v, 0.0001));
      var pr = (minor + 0.5 * level) / total;
      return pr;
    }

    public static double PRankGM(this double[] vec, double v, bool check = true) {
      Array.Sort(vec);
      double? pr = null;
      for (var i = 0; i < vec.Length; i++) {
        if (!AlmostEqual(vec[i], v, 0.0001)) continue;
        pr = (double)i / (vec.Length - 1);
        if (check) return pr.Value;
      }
      if (pr.HasValue) return pr.Value;
      // calculate value using linear interpolation
      for (var i = 0; i < vec.Length - 1; i++) {
        if (v <= vec[i] || vec[i + 1] <= v) continue;
        double x = vec[i], y = vec[i + 1];
        double xr = PRankGM(vec, x, false), yr = PRankGM(vec, y, false);
        return ((y - v) * xr + (v - x) * yr) / (y - x);
      }
      throw new ArgumentOutOfRangeException("Out of bounds");
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
  public class PercentileRankTest {
    public static double[] alpha = { 13, 12, 11, 8, 4, 3, 2, 1, 1, 1 };
    public static double[] beta = { 0, 40, 50, 60, 80, 90, 100 };
    public static double[] gamma = { 80, 0, 40, 100, 60, 80, 90, };
    public static double[] delta = { 7, 5, 5, 4, 4, 3, 3, 3, 2, 1 };
    [Test]
    public void TestPercentileRank() {
      var vec = delta;
      var raw = vec.Slice();
      foreach (var v in raw) {
        Console.WriteLine($">> [{v}] percentile rank ({vec.PRankWiki(v):n2})");
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