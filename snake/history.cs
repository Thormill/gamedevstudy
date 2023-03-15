using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Snake {
  internal class History {
    // string[] turns;
    List<int> results;
    public int record;

    public History() {
      results = new List<int>();
      record = 0;
    }

    public void Remember(int score){
      results.Add(score);
      record = record > score ? record : score;
    }

    public void Draw() {
      int i = 1;
      foreach (var result in results) {
        Console.WriteLine(@"{0}: {1}", i, result);
      }

      Console.WriteLine();
      Console.WriteLine(@"Record is {0}", record);
    }
  }
}
