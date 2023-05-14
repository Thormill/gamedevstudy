using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe;
using System.Runtime.CompilerServices;

namespace Tictactoe {
  internal class Ai_logic {
    public Ai_logic() {
    }
    public int GetTargetField(Grid grid1, string figure) {
      int target_field = -1;
      int curr_max_weight = -1;
      int curr_weight = -1;
      for (int i = 0; i < grid1.grid.Length; i++) {         // обходим массив всех ячеек и для каждой вычисляем вес
        curr_weight = EvaluateField(grid1, i, figure);      
        if (curr_weight > curr_max_weight) {                // сравниваем вес текущей ячейки с текущим максимумом
          target_field = i;                                 // если нашлась ячейка с большим весом, запоминаем ее номер и вес
          curr_max_weight = curr_weight;
        }
      }
      return target_field;
    }
    // на самом деле вполне очевидно, что может быть несколько ячеек с одинаковыми (максимальными) весами.
    // бот выбирает наименьший номер ячейки
    // для большего разнообразия можно запоминать (сделать отдельный массив/список или дописать в Field свойство "вес")
    // ячейки с одинаковыми весами и выбирать по рандому. Но не хотелось менять Field по принципу "не трогай чужой код, если не особо надо"
    // а делать еще что-то было лень, хотелось поскорее запустить
    public int EvaluateField(Grid grid, int field_num, string figure) {
      int field_weight = 0;
      if (grid.grid[field_num].busy) { field_weight = -1; return field_weight; } 
      field_weight = field_weight + EvalDiagonal(grid, field_num, figure);        // брутальный китайский код
      field_weight = field_weight + EvalHorizontal(grid, field_num, figure);      // вес ячейки - сумма весов потенциальных линий
      field_weight = field_weight + EvalVertical(grid, field_num, figure);        // проверка входит ли ячейка в диагональ
                                                                                  // засунута в саму функцию веса диагонали
      field_weight = field_weight + EvalRevDiag(grid, field_num, figure);          
      return field_weight;
    }

    public int EvalHorizontal(Grid grid, int field_num, string figure) {
      const int grid_size = 3; // для масштабируемости поля утащить в какую-то более глобальную область
      int ycoord = field_num / grid_size;
      int own_count = 0;
      int opp_count = 0;
      int free_count = 0;
      int weight = 0;
      for (int xcoord = 0; xcoord < grid_size; xcoord++) {
        if (!(grid.grid[ycoord * grid_size + xcoord].busy)) free_count++;
          else if (grid.grid[ycoord * grid_size + xcoord].value == figure) own_count++;
      }
      opp_count = grid_size - own_count - free_count;
      if (own_count == grid_size - 1) weight += 1000;
      if (opp_count == grid_size - 1) weight += 500;
      if (opp_count == 0) {
        if (own_count > 0) weight += 2; 
          else weight += 1;
      }
      return weight;
    }

    public int EvalVertical(Grid grid, int field_num,  string figure) {
      const int grid_size = 3; // для масштабируемости поля утащить в какую-то более глобальную область
      int xcoord = field_num % grid_size;
      int own_count = 0;
      int opp_count = 0;
      int free_count = 0;
      int weight = 0;
      for (int ycoord = 0; ycoord < grid_size; ycoord++) {
        if (!(grid.grid[ycoord * grid_size + xcoord].busy)) free_count++;
          else if (grid.grid[ycoord * grid_size + xcoord].value == figure) own_count++;
      }
      opp_count = grid_size - own_count - free_count;
      if (own_count == grid_size - 1) weight += 1000;
      if (opp_count == grid_size - 1) weight += 500;
      if (opp_count == 0) {
        if (own_count > 0) weight += 2; else weight += 1;
      }
      return weight;
    }

    public int EvalDiagonal(Grid grid, int field_num, string figure) {
      const int grid_size = 3; // для масштабируемости поля утащить в какую-то более глобальную область
      int xcoord = field_num % grid_size;
      int ycoord = field_num / grid_size;
      int own_count = 0;
      int opp_count = 0;
      int free_count = 0;
      int weight = 0;
      if (xcoord != ycoord) return weight;
      else for (int i = 0; i < grid_size; i++) {
          if (!(grid.grid[i * grid_size + i].busy)) free_count++;
            else if (grid.grid[i * grid_size + i].value == figure) own_count++;
        }
      opp_count = grid_size - own_count - free_count;
      if (own_count == grid_size - 1) weight += 1000;
      if (opp_count == grid_size - 1) weight += 500;
      if (opp_count == 0) {
        if (own_count > 0) weight += 2; else weight += 1;
      }
      return weight;
    }

    public int EvalRevDiag(Grid grid, int field_num, string figure) {
      const int grid_size = 3; // для масштабируемости поля утащить в какую-то более глобальную область
      int xcoord = field_num % grid_size;
      int ycoord = field_num / grid_size;
      int own_count = 0;
      int opp_count = 0;
      int free_count = 0;
      int weight = 0;
      if (xcoord + ycoord + 1 != grid_size) return weight;  // ячейка принадлежит побочной диагонали, если сумма координат + 1
                                                            // равна стороне таблицы
      else for (int i = 0; i < grid_size; i++) {
          if (!(grid.grid[i * grid_size - i].busy)) free_count++;
          else if (grid.grid[i * grid_size - i].value == figure) own_count++;
        }
      opp_count = grid_size - own_count - free_count;
      if (own_count == grid_size - 1) weight += 1000;
      if (opp_count == grid_size - 1) weight += 500;
      if (opp_count == 0) {
        if (own_count > 0) weight += 2; else weight += 1;
      }
      return weight;
    }
  }
}
