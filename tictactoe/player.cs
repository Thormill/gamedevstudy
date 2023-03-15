using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace TicTacToe {

  internal class Player {
    public string figure;
    public bool current_turn;

    public Player(string fig) {
      figure = fig;
      if (figure == Field.FIGURE_CROSS) {
        current_turn = true;
      } else {
        current_turn = false;
      }
    }

    public string Draw() {
      return figure;
    }

    public void Turn() {
      current_turn = false;
    }

    public void Ready() {
      current_turn = true;
    }

    public bool CheckWin(Grid grid) {
      bool win = false;
      List<int> marked = new List<int>();

      // grid indexes of marked elements
      for(int i = 0; i < grid.grid.Length; i++) {
        if(grid.grid[i].value == figure) {
          marked.Add(i);
        }
      }

      // TODO: rewrite with loops? TBD
      // horizontal lines
      if (marked.Contains(0) && marked.Contains(1) && marked.Contains(2)) win = true;
      if (marked.Contains(3) && marked.Contains(4) && marked.Contains(5)) win = true;
      if (marked.Contains(6) && marked.Contains(7) && marked.Contains(8)) win = true;

      // vertical lines
      if (marked.Contains(0) && marked.Contains(3) && marked.Contains(6)) win = true;
      if (marked.Contains(1) && marked.Contains(4) && marked.Contains(7)) win = true;
      if (marked.Contains(2) && marked.Contains(5) && marked.Contains(8)) win = true;

      // diagonal lines
      if (marked.Contains(0) && marked.Contains(4) && marked.Contains(8)) win = true;
      if (marked.Contains(2) && marked.Contains(4) && marked.Contains(6)) win = true;

      return win;
    }
  }
}
