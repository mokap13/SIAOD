using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SIAOD_Routing
{
    
    public partial class Form1 : Form
    {
        private static List<Town> srcTowns;
        private static List<Road> srcRoads;
        private static List<Road> shortRoads;
        List<Town> checkedTowns;
        DekstraAlgoritm da;
        float[][] matrix;
        private static void InitializeTownsAndRoads()
        {
            srcTowns = new List<Town>()
            {
                new Town("1"),
                new Town("2"),
                new Town("3"),
                new Town("4"),
                new Town("5"),
                new Town("6"),
                new Town("7"),
                new Town("8"),
                new Town("9"),
                new Town("10"),
                new Town("11"),
            };

            srcRoads = new List<Road>() {
            new Road(srcTowns[0],srcTowns[1],12),
            new Road(srcTowns[0],srcTowns[3],28),
            new Road(srcTowns[0],srcTowns[8],15),

            new Road(srcTowns[1],srcTowns[0],12),
            new Road(srcTowns[1],srcTowns[2],10),
            new Road(srcTowns[1],srcTowns[3],43),

            new Road(srcTowns[2],srcTowns[1],10),
            new Road(srcTowns[2],srcTowns[4],10),
            new Road(srcTowns[2],srcTowns[7],20),

            new Road(srcTowns[3],srcTowns[0],28),
            new Road(srcTowns[3],srcTowns[1],43),
            new Road(srcTowns[3],srcTowns[2],17),

            new Road(srcTowns[4],srcTowns[1],31),
            new Road(srcTowns[4],srcTowns[2],10),
            new Road(srcTowns[4],srcTowns[5],8),

            new Road(srcTowns[5],srcTowns[4],14),
            new Road(srcTowns[5],srcTowns[6],6),

            new Road(srcTowns[6],srcTowns[5],6),
            new Road(srcTowns[6],srcTowns[7],21),

            new Road(srcTowns[7],srcTowns[2],20),
            new Road(srcTowns[7],srcTowns[6],21),

            new Road(srcTowns[8],srcTowns[0],15),
            new Road(srcTowns[8],srcTowns[9],5),
            new Road(srcTowns[8],srcTowns[10],6),

            new Road(srcTowns[9],srcTowns[8],5),

            new Road(srcTowns[10],srcTowns[8],10)
            };

            shortRoads = new List<Road>();
        }
        public Form1()
        {
            InitializeComponent();

            InitializeTownsAndRoads();
                  
            da = new DekstraAlgoritm(srcTowns, srcRoads);
            foreach (var town in srcTowns)
            {
                checkedListBox1.Items.Add(town);
            }
        }
        private void selectIndexChanhedHandler(object sender, EventArgs e)
        {
            checkedTowns = new List<Town>();
            
            foreach (var index in checkedListBox1.CheckedIndices)
            {
                checkedTowns.Add(srcTowns[(int)index]);
            }

            if (checkedTowns.Count == 0)
                return;
            matrix = da.GetFullMatrix(checkedTowns);

            dataGridView1.RowCount = matrix[0].Length;
            dataGridView1.ColumnCount = matrix.Length;
            for (int i = 0; i < matrix.Length; i++)
                for (int j = 0; j < matrix[i].Length; j++)
                    dataGridView1.Rows[i].Cells[j].Value = matrix[i][j];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<int> path = null;
            if(matrix == null 
                || matrix.Count() < 2){

                textBox1.Clear();
                textBox1.Text = "Нужно выбрать как минимум 2 пункта";
                return;
            }

            path = BranchAndBoundAlgorytm.getFullMatrix(matrix);
            
            List<Road> finishPath = new List<Road>();
            for (int i = 0; i < path.Count; i++)
            {
                finishPath.AddRange(da.newRoads.
                    Where(x => x.FirstTown.Name == checkedListBox1.CheckedItems[i].ToString() 
                        && x.SecondTown.Name == checkedListBox1.CheckedItems[path[i]].ToString()));
            }
            float pathLenght = finishPath.Sum(x => x.Weight);
            if((checkedListBox1.SelectedItem as Town) == null)
            {
                textBox1.Clear();
                textBox1.Text = "Стартовый город не выбран";

                return;
            }
            if(finishPath.Where(x=>x.FirstTown.Name == (checkedListBox1.SelectedItem as Town).Name).Count() == 0)
            {
                textBox1.Clear();
                textBox1.Text = "Стартовый город должен входить в маршрут";

                return;
            }

            string start = (checkedListBox1.SelectedItem as Town).Name;

            List<Road> sortPath = new List<Road>();
            sortPath.Add(finishPath.First(x => x.FirstTown.Name == start));

            for (int i = 0; i < finishPath.Count-1; i++)
            {
                sortPath.Add(finishPath.First(x => x.FirstTown == sortPath.Last().SecondTown));
            }

            textBox1.Clear();
            textBox1.Text += sortPath.First().FirstTown.Name;
            foreach (var road in sortPath)
            {
                foreach (var compoziteRoad in road.compoziteRoad)
                {
                    textBox1.Text += " -> " + compoziteRoad.SecondTown.Name;
                }
            }
            textBox1.Text += "   ***   " + pathLenght;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
            selectIndexChanhedHandler(checkedListBox1, e);
        }
    }
}
