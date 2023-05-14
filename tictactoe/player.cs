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
      const int grid_size = 3; // make this global in case of scalable grid
      bool win = false;
      /* List<int> marked = new List<int>();
      // grid indexes of marked elements
      //for(int i = 0; i < grid.grid.Length; i++) {
      //  if(grid.grid[i].value == figure) {
      //   marked.Add(i);
      //  }
      //} */
      for(int i = 0; i < grid_size; i++) {
        int hor_count = 0;
        int ver_count = 0;
        for(int j = 0; j < grid_size; j++)
        {
          if (grid.grid[grid_size * i + j].value == figure) hor_count++;
          if (grid.grid[grid_size * j + i].value == figure) ver_count++;
        }
        if (hor_count == grid_size) win = true;     // используем презумпцию что выигрышная линия всегда от конца до конца поля
        if (ver_count == grid_size) win = true;     // на больших полях вроде играют до какого-то числа подряд идущих
                                                    // для такого случая естественно надо будет переделывать
      }
      int diag_count_str = 0;
      int diag_count_rev = 0;
      for (int i = 0; i < grid_size; i++)
      {
        if (grid.grid[grid_size * i + i].value == figure) diag_count_str++;
        if (grid.grid[grid_size * i - i + 2].value == figure) diag_count_rev++;
      }
      if (diag_count_str == grid_size) win = true;
      if (diag_count_rev == grid_size) win = true;
      /*/ TODO: rewrite with loops? TBD
      // horizontal lines
      //if (marked.Contains(0) && marked.Contains(1) && marked.Contains(2)) win = true;
      //if (marked.Contains(3) && marked.Contains(4) && marked.Contains(5)) win = true;
      //if (marked.Contains(6) && marked.Contains(7) && marked.Contains(8)) win = true;

      //// vertical lines
      //if (marked.Contains(0) && marked.Contains(3) && marked.Contains(6)) win = true;
      //if (marked.Contains(1) && marked.Contains(4) && marked.Contains(7)) win = true;
      //if (marked.Contains(2) && marked.Contains(5) && marked.Contains(8)) win = true;

      //// diagonal lines
      //if (marked.Contains(0) && marked.Contains(4) && marked.Contains(8)) win = true;
      //if (marked.Contains(2) && marked.Contains(4) && marked.Contains(6)) win = true;
      */
      return win;
    }
  }
}
