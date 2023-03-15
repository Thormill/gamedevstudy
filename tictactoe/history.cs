using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace TicTacToe {
  internal class History {
    // string[] turns;
    List<string> turns;

    public History() {
      turns = new List<string>();
    }

    public void Remember(string figure, int field){
      string record = $"Turn #{turns.Count}: {figure} at grid #{field}";
      turns.Add(record);
    }

    public void Draw() {
      foreach (var turn in turns) {
        Console.WriteLine(turn);
      }
    }
  }
}
