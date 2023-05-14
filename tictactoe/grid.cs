using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace TicTacToe {
  internal class Grid {
    public const int SIZE = 3 ;
    public Field[] grid;
    private History history;

    public Grid() {
      grid = new Field[9];
      history = new History();

      for(int i = 0; i < 9; i++) {
        string shown_number = (i + 1).ToString();
        grid[i] = new Field(shown_number);
      }
    }

    public void Draw() {
      Console.Clear();

      for(int i = 0; i < 9; i++) {
        grid[i].Draw();

        if ((i + 1) % 3 == 0 ) {
          Console.WriteLine();
        }
      }

      history.Draw();
    }

    public void Turn(int number, string figure) {
      grid[number].Mark(figure);
      history.Remember(figure, number);
    }

    public int[] AvailableFields() {
      List<int> result = new List<int>();

      for(int i = 0; i < 9; i++) {
        if(grid[i].busy == false) {
          result.Add(i);
        }
      }

      return result.ToArray();
    }

    public bool CheckField(int number) {
      return grid[number].busy == true;
    }
  }

}
