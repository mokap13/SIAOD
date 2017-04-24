using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private static decimal delay;
        private static bool isStop;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<string> alphabet = new List<string>() { "1","0"," " };
            List<string> states = new List<string>() { "q1", "q2", "q3", "q4" };
            List<string> tape = new List<string>() {
                alphabet[2], alphabet[2], alphabet[2],
                alphabet[1], alphabet[1], alphabet[1], alphabet[1],
                alphabet[2], alphabet[2], alphabet[2] };

            

            InitPassageTable(alphabet, states);
            InitTape(tape,alphabet);
        }

        private void InitTape(List<string> tape, List<string> alphabet)
        {
            dataGridView2.Rows.Clear();
            dataGridView2.RowCount = 1;
            dataGridView2.ColumnCount = 10;
            for (int i = 0; i < tape.Count; i++)
            {
                dataGridView2.Rows[0].Cells[i].Value = tape[i];
                InitTapeCellContextMenu(alphabet, dataGridView2.Rows[0].Cells[i]);
            }
        }
        void InitTapeCellContextMenu(List<string> alphabet, DataGridViewCell cell)
        {
            cell.ContextMenuStrip = new ContextMenuStrip();

            List<ToolStripItem> alphabetMenuItems = new List<ToolStripItem>();
            alphabetMenuItems.AddRange(alphabet.Select(x => new ToolStripMenuItem(x)));
            foreach (var item in alphabetMenuItems)
            {
                item.Click += tapeContextMenuHandler;
            }

            cell.ContextMenuStrip.Items.AddRange(alphabetMenuItems.ToArray());
        }
        private void tapeContextMenuHandler(object sender, EventArgs e)
        {
            var currentCell = dataGridView2.CurrentCell;
            currentCell.Value = (sender as ToolStripMenuItem).Text;
        }

        private void InitPassageTable(List<string> alphabet, List<string> states)
        {
            List<List<Tuple<Direction, string, string>>> passagesTable = new List<List<Tuple<Direction, string, string>>>();
            for (int i = 0; i < alphabet.Count; i++)
            {
                passagesTable.Add(new List<Tuple<Direction, string, string>>());
                for (int j = 0; j < states.Count; j++)
                {
                    passagesTable[i].Add(Tuple.Create<Direction, string, string>(Direction.N, alphabet[0], states[0]));
                }
            }

            dataGridView1.ColumnCount = passagesTable[0].Count;
            dataGridView1.RowCount = passagesTable.Count;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = alphabet[i].ToString();

                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = passagesTable[i][j].ToString();
                    dataGridView1.Columns[j].HeaderCell.Value = states[j].ToString();
                    InitPassageTableCellContextMenu(alphabet, states,dataGridView1.Rows[i].Cells[j]);
                }
            }
            
            dataGridView1.MultiSelect = false;
            dataGridView1.Height = (dataGridView1.RowCount + 1 ) * dataGridView1.Rows[0].Height+3;
            //dataGridView1.Width = (dataGridView1.ColumnCount) * dataGridView1.Columns[0].Width;
            dataGridView1.ScrollBars = ScrollBars.None;
            dataGridView1.Update();

        }
        private void InitPassageTableCellContextMenu(List<string> alphabet,List<string> states,DataGridViewCell cell)
        {
            cell.ContextMenuStrip = new ContextMenuStrip();
    
            ToolStripMenuItem directionMenu = new ToolStripMenuItem("Direction");
            List<ToolStripItem> directionMenuItems = new List<ToolStripItem>();

            foreach (var direction in Enum.GetValues(typeof(Direction)))
            {
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(direction.ToString());
                directionMenu.DropDownItems.Add(toolStripMenuItem);
                directionMenu.DropDownItems[directionMenu.DropDownItems.Count-1].Click += DirectionMenuHandler;
            }

            cell.ContextMenuStrip.Items.Add(directionMenu);

            /********************************************************************/

            ToolStripMenuItem symbolMenu = new ToolStripMenuItem("Symbol");
            List<ToolStripItem> symbolMenuItem = new List<ToolStripItem>();

            foreach (var symbol in alphabet)
            {
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(symbol.ToString());
                symbolMenu.DropDownItems.Add(toolStripMenuItem);
                symbolMenu.DropDownItems[symbolMenu.DropDownItems.Count - 1].Click += SymbolMenuHandler;
            }

            cell.ContextMenuStrip.Items.Add(symbolMenu);
            /*********************************************************************/
            ToolStripMenuItem stateMenu = new ToolStripMenuItem("State");
            List<ToolStripItem> stateMenuItem = new List<ToolStripItem>();

            foreach (var state in states)
            {
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(state.ToString());
                stateMenu.DropDownItems.Add(toolStripMenuItem);
                stateMenu.DropDownItems[stateMenu.DropDownItems.Count - 1].Click += StateMenuHandler;
            }

            cell.ContextMenuStrip.Items.Add(stateMenu);
        }
        private void DirectionMenuHandler(object sender, EventArgs e)
        {
            //var currentCell = dataGridView1.CurrentCell;
            //currentCell.Value = ReplaceCharInString((currentCell.Value as string), 1, (sender as ToolStripMenuItem).Text[0]);
            var currentCell = dataGridView1.CurrentCell;
            var newDirection = (sender as ToolStripMenuItem).Text;

            var cellValue = currentCell
                .Value
                .ToString();
            var trimmedCellValue = cellValue.Trim('(', ')', '\n', '\r');
            var splittedTrimmedCellValue = trimmedCellValue.Split(',');

            var newSymbol = new String(splittedTrimmedCellValue[1].Skip(1).ToArray());
            //var newDirection = splittedTrimmedCellValue[0];
            var newState = new String(splittedTrimmedCellValue[2].Skip(1).ToArray());

            currentCell.Value = Tuple.Create<string, string, string>(newDirection, newSymbol, newState);
        }
        private void SymbolMenuHandler(object sender, EventArgs e)
        {
            var currentCell = dataGridView1.CurrentCell;
            var newSymbol = (sender as ToolStripMenuItem).Text;

            var cellValue = currentCell
                .Value
                .ToString();
            var trimmedCellValue = cellValue.Trim('(', ')', '\n', '\r');
            var splittedTrimmedCellValue = trimmedCellValue.Split(',');

            //var newSymbol = new String(splittedTrimmedCellValue[1].Skip(1).ToArray());
            var direction = splittedTrimmedCellValue[0];
            var newState = new String(splittedTrimmedCellValue[2].Skip(1).ToArray());

            currentCell.Value = Tuple.Create<string, string, string>(direction, newSymbol, newState);
        }
        private void StateMenuHandler(object sender, EventArgs e)
        {
            var currentCell = dataGridView1.CurrentCell;
            var newState = (sender as ToolStripMenuItem).Text;

            var cellValue = currentCell
                .Value
                .ToString();
            var trimmedCellValue = cellValue.Trim('(', ')', '\n', '\r');
            var splittedTrimmedCellValue = trimmedCellValue.Split(',');

            var newSymbol = new String(splittedTrimmedCellValue[1].Skip(1).ToArray());
            var direction = splittedTrimmedCellValue[0];
            //var newState = new String(splittedTrimmedCellValue[2].Skip(1).ToArray());

            currentCell.Value = Tuple.Create<string, string, string>(direction, newSymbol, newState);
        }

        private List<List<Tuple<Direction, string, string>>> InitDefaultPassageTable(List<string> alphabet, List<string> states)
        {
            List<List<Tuple<Direction, string, string>>> passagesTable = new List<List<Tuple<Direction, string, string>>>();

            passagesTable.AddRange(new List<List<Tuple<Direction, string, string>>>() {
                new List<Tuple<Direction, string, string>>() {
                    Tuple.Create(Direction.R, alphabet[0], states[0]),
                    Tuple.Create(Direction.L, alphabet[1], states[1]),
                    Tuple.Create(Direction.L, alphabet[0], states[2]),
                    Tuple.Create(Direction.N, alphabet[0], states[3])
                    },
                new List<Tuple<Direction, string, string>>() {
                    Tuple.Create(Direction.R, alphabet[1], states[0]),
                    Tuple.Create(Direction.L, alphabet[0], states[2]),
                    Tuple.Create(Direction.L, alphabet[1], states[2]),
                    Tuple.Create(Direction.N, alphabet[0], states[3])
                    },
                 new List<Tuple<Direction, string, string>>() {
                    Tuple.Create(Direction.L, alphabet[2], states[1]),
                    Tuple.Create(Direction.N, alphabet[0], states[3]),
                    Tuple.Create(Direction.R, alphabet[2], states[0]),
                    Tuple.Create(Direction.N, alphabet[0], states[3])
                    },


                }
            );

            return passagesTable;
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var h = dataGridView1.HitTest(e.X, e.Y);
                if (h.Type == DataGridViewHitTestType.Cell)
                {
                    var currentCell = dataGridView1.CurrentCell;
                    dataGridView1[h.ColumnIndex,h.RowIndex].Selected = true;
                    currentCell = dataGridView1[h.ColumnIndex, h.RowIndex];
                }
            }
        }
        private void dataGridView2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var h = dataGridView2.HitTest(e.X, e.Y);
                if (h.Type == DataGridViewHitTestType.Cell)
                {
                    var currentCell = dataGridView2.CurrentCell;
                    dataGridView2[h.ColumnIndex, h.RowIndex].Selected = true;
                    currentCell = dataGridView2[h.ColumnIndex, h.RowIndex];
                }
            }
        }

        public String ReplaceCharInString(String source, int index, Char newSymb)
        {
            StringBuilder sb = new StringBuilder(source);
            sb[index] = newSymb;
            return sb.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((!String.IsNullOrEmpty(textBox1.Text)&&!String.IsNullOrEmpty(textBox2.Text)))
            {
                try
                {
                    List<string> alphabet = textBox1.Text.Split(',').Distinct().Where(x=>!String.IsNullOrEmpty(x)).ToList();
                    List<string> states = textBox2.Text.Split(',').Distinct().ToList();
                    List<string> tape = new List<string>() {
                        alphabet[0], alphabet[0], alphabet[0],
                        alphabet[1], alphabet[1], alphabet[1], alphabet[1],
                        alphabet[0], alphabet[0], alphabet[0] };
                    InitPassageTable(alphabet, states);
                    InitTape(tape, alphabet);
                }
                catch (Exception)
                {
                    
                }
            }
            
        }

        private void buttonMove_Click_1(object sender, EventArgs e)
        {
            TuringMove();
        }

        private void TuringMove()
        {          
            if(dataGridView2.SelectedCells.Count == 0)
            {
                MessageBox.Show("Выберите начальное положение \n пишущей головки на ленте");
                isStop = true;
                return;
            }
            var currentSymbol = dataGridView2
                .SelectedCells[0]
                .Value
                .ToString();
            var currentState = dataGridView1
                .SelectedCells[0]
                .Value
                .ToString()
                .Split(',','(',')')[3];


            var columnIndex = dataGridView1.SelectedCells[0].ColumnIndex;
            int rowIndex = 0;
            List<string[]> cells = new List<string[]>();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                //cells.Add(dataGridView1.Rows[i].Cells[columnIndex].Value.ToString().Split(',', '(', ')'));
                if (dataGridView1.Rows[i].HeaderCell.Value.ToString() == currentSymbol)
                    rowIndex = i;
            }

            var cellValue = dataGridView1
                .Rows[rowIndex]
                .Cells[columnIndex]
                .Value
                .ToString();
            var trimmedCellValue = cellValue.Trim( '(', ')','\n','\r');
            var splittedTrimmedCellValue = trimmedCellValue.Split(',');

            var newSymbol = new String(splittedTrimmedCellValue[1].Skip(1).ToArray());
            ExecuteSymbolCommand(newSymbol);

            var direction = splittedTrimmedCellValue[0];
            ExecuteDirectionCommand(direction);

            var newState = new String(splittedTrimmedCellValue[2].Skip(1).ToArray());
            ExecuteStateCommand(newState);
        }

        private void ExecuteDirectionCommand(string direction)
        {
            int columnIndexTape = 0;
            switch (direction)
            {
                case "N":
                    break;
                case "R":
                    columnIndexTape = dataGridView2.SelectedCells[0].ColumnIndex;
                    dataGridView2.SelectedCells[0].Selected = false;
                    if (columnIndexTape + 1 > dataGridView2.Rows[0].Cells.Count)
                    {
                        MessageBox.Show("Память ленты кончилась");
                        break;
                    }

                    dataGridView2.Rows[0].Cells[columnIndexTape + 1].Selected = true;
                    break;
                case "L":
                    columnIndexTape = dataGridView2.SelectedCells[0].ColumnIndex;
                    dataGridView2.SelectedCells[0].Selected = false;

                    if(columnIndexTape - 1 < 0)
                    {
                        MessageBox.Show("Память ленты кончилась");
                        break;
                    }

                    dataGridView2.Rows[0].Cells[columnIndexTape - 1].Selected = true;
                    break;
                default:
                    break;
            }
        }
        private void ExecuteSymbolCommand(string newSymbol)
        {
            dataGridView2.SelectedCells[0].Value = newSymbol;
        }
        private void ExecuteStateCommand(string newState)
        {
            int columnIndex = 0;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                //cells.Add(dataGridView1.Rows[i].Cells[columnIndex].Value.ToString().Split(',', '(', ')'));
                var a = (dataGridView1.Columns[i].HeaderCell.Value.ToString());
                if ((dataGridView1.Columns[i].HeaderCell.Value.ToString() == newState))
                    columnIndex = i;
            }
            
            dataGridView1.SelectedCells[0].Selected = false;
            dataGridView1.Rows[0].Cells[columnIndex].Selected = true;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            delay = numericUpDown1.Value;
            isStop = false;
            button_start.Enabled = false;
            while (!isStop)
            {
                TuringMove();
                Application.DoEvents();
                Thread.Sleep((int)delay);
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            isStop = true;
            button_start.Enabled = true;
        }

        private void buttonSetCounterTable_Click(object sender, EventArgs e)
        {
            List<string> alphabet = new List<string>() { "1", "0", " " };
            List<string> states = new List<string>() { "q1", "q2", "q3", "q4" };
            List<string> tape = new List<string>() {
                alphabet[2], alphabet[2], alphabet[2],
                alphabet[1], alphabet[1], alphabet[1], alphabet[1],
                alphabet[2], alphabet[2], alphabet[2] };
            InitTape(tape, alphabet);
            dataGridView2.SelectedCells[0].Selected = false;
            dataGridView2.Rows[0].Cells[3].Selected = true;

            List<List<Tuple<Direction, string, string>>> passagesTable = InitDefaultPassageTable(alphabet, states);

            dataGridView1.ColumnCount = passagesTable[0].Count;
            dataGridView1.RowCount = passagesTable.Count;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = alphabet[i].ToString();

                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = passagesTable[i][j].ToString();
                    dataGridView1.Columns[j].HeaderCell.Value = states[j].ToString();
                    InitPassageTableCellContextMenu(alphabet, states, dataGridView1.Rows[i].Cells[j]);
                }
            }

            dataGridView1.MultiSelect = false;
            dataGridView1.Height = (dataGridView1.RowCount + 1) * dataGridView1.Rows[0].Height + 3;
            //dataGridView1.Width = (dataGridView1.ColumnCount) * dataGridView1.Columns[0].Width;
            dataGridView1.ScrollBars = ScrollBars.None;
            dataGridView1.Rows[0].Cells[0].Selected = true;
            dataGridView1.Update();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                @" Здравствуй пользователь! Перед тобой эмулятор абстрактной машины 
    великого британского математика Алана Тьюринга
  С помощью полей - 
    Алфавит и Состояния можно задать 
    произволные параметры(через запятую <,>)
    после чего нажать <Применить>
  C помощью контекстного меню - 
    можно выбрать параметры Ленты и 
    Таблицы переходов(щелчок правой кнопкой мыши по ячейке
Кнопкой <Задать программу счетчик> можно установить пример работающей небольшой программы>","Машина Тьюринга");
        }
    }
}