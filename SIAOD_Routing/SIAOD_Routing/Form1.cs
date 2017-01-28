using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIAOD_Routing
{
    
    public partial class Form1 : Form
    {
        private static List<Town> srcTowns;
        private static List<Road> srcRoads;
        List<Town> checkedTowns;
        DekstraAlgorim da;
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
            new Town("7")
        };

            srcRoads = new List<Road>() {
            new Road(srcTowns[0],srcTowns[1],12),
            new Road(srcTowns[0],srcTowns[3],28),

            new Road(srcTowns[1],srcTowns[0],12),
            new Road(srcTowns[1],srcTowns[2],10),
            new Road(srcTowns[1],srcTowns[3],43),

            new Road(srcTowns[2],srcTowns[1],10),
            new Road(srcTowns[2],srcTowns[4],10),

            new Road(srcTowns[3],srcTowns[0],28),
            new Road(srcTowns[3],srcTowns[1],43),
            new Road(srcTowns[3],srcTowns[2],17),

            new Road(srcTowns[4],srcTowns[1],31),
            new Road(srcTowns[4],srcTowns[2],10),
            new Road(srcTowns[4],srcTowns[5],8),

            new Road(srcTowns[5],srcTowns[4],14),
            new Road(srcTowns[5],srcTowns[6],6),

            new Road(srcTowns[6],srcTowns[5],6)
        };
        }
        public Form1()
        {
            InitializeComponent();

            InitializeTownsAndRoads();
                  
            da = new DekstraAlgorim(srcTowns, srcRoads);
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
            List<int> path;
            if (matrix != null)
                path = BranchAndBoundAlgorytm.getFullMatrix(matrix);
        }

        private void checkedListBoxDoubleClickHandler(object sender, EventArgs e)
        {
            for (int i = 0; i < (sender as CheckedListBox).Items.Count; i++)
            {
                (sender as CheckedListBox).SetItemChecked(i, true);
            }
            selectIndexChanhedHandler(sender, e);
        }   
    }
}
