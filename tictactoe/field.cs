using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace TicTacToe {
  internal class Field {
    public string value;
    public bool busy;

    public const string FIGURE_DEFAULT = "-";
    public const string FIGURE_CROSS = "X";
    public const string FIGURE_CIRCLE = "O";

    public Field(string val = FIGURE_DEFAULT) {
      // TODO: check input for constant matching
      value = val;
      busy = false;
    }

    public void Mark(string figure) {
      value = figure;
      busy = true;
    }

    public void Draw() {
      switch(value) {
        case FIGURE_CROSS:
          Console.BackgroundColor = ConsoleColor.Blue;
          Console.ForegroundColor = ConsoleColor.Red;

          break;
        case FIGURE_CIRCLE:
          Console.BackgroundColor = ConsoleColor.Red;
          Console.ForegroundColor = ConsoleColor.Blue;

          break;
      }

      Console.Write(@"|{0}|", value);
      Console.ResetColor();
    }
  }

}
