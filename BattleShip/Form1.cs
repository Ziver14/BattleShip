namespace BattleShip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            model = new Model();
            //model.PlayerShips[0, 0] = CoordStatus.Ship;
            //model.PlayerShips[5, 2] = CoordStatus.Ship;
            //model.PlayerShips[5, 3] = CoordStatus.Ship;
            //model.PlayerShips[5, 4] = CoordStatus.Ship;
            //model.PlayerShips[7, 3] = CoordStatus.Ship;
            //model.PlayerShips[8, 3] = CoordStatus.Ship;
            for (int i = 0; i < 10; i++)
            {
                dataGridView1.Rows.Add(row);
            }
        }

        Model model;

        string[] row = { "", "", "", "", "", "", "", "", "", "" };
        private void button1_Click(object sender, EventArgs e)
        {
            //model.LastShootCord = textBox1.Text;

            model.LastShot = model.Shot(textBox1.Text);
            int x = int.Parse(textBox1.Text.Substring(0, 1));
            int y = int.Parse(textBox1.Text.Substring(1));
            switch (model.LastShot)
            {
                case ShotStatus.Miss:
                    model.EnemyShips[x, y] = CoordStatus.Shot; break;
                case ShotStatus.Wounded:
                    model.EnemyShips[x, y] = CoordStatus.Got; break;
                case ShotStatus.Kill:
                    model.EnemyShips[x, y] = CoordStatus.Got; break;
            }
            if (model.LastShot == ShotStatus.Wounded) { model.LastShootCord = textBox1.Text; model.WoundedStatus = true; }
            MessageBox.Show(model.Shot(textBox1.Text).ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s;
            int x, y;
            do
            {

                s = model.ShotGen();
                x = int.Parse(s.Substring(0, 1));
                y = int.Parse(s.Substring(1));
            }
            while (model.EnemyShips[x, y] != CoordStatus.None);
            {
                textBox1.Text = s;
            }


        }

        private void button204_Click(object sender, EventArgs e)
        {

            //for (int x = 0; x < 10; x++)
            //    for (int y = 0; y < 10; y++)
            //    {
            //        string name = "b" + x.ToString() + y.ToString();
            //        var b = this.Controls.Find(name, true);

            //        if (b.Count() > 0)
            //        {
            //            var btn = b[0];
            //            switch (model.PlayerShips[x, y])
            //            {
            //                case CoordStatus.Ship:
            //                    btn.Text = "x";
            //                    break;
            //                case CoordStatus.None: btn.Text = ""; break;
            //            }
            //        }


            //    }

            for (int x = 0; x < 10; x++)
                for (int y = 0; y < 10; y++)
                {
                    switch (model.PlayerShips[x, y])
                    {
                        case CoordStatus.Ship:
                            dataGridView1[x, y].Value = "x";
                            break;
                        case CoordStatus.None:
                            dataGridView1[x, y].Value = " ";
                            break;
                    }




                }

        }

        private void button30_Click(object sender, EventArgs e)
        {

        }

        private void button67_Click(object sender, EventArgs e)
        {

        }

        private void button203_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 1)
            {
                for (int i = 0; i < dataGridView1.SelectedCells.Count; i++)
                {
                    int x = dataGridView1.SelectedCells[i].ColumnIndex;
                    int y = dataGridView1.SelectedCells[i].RowIndex;
                    CoordStatus coordStatus;
                    if (!checkBox2.Checked) coordStatus = CoordStatus.Ship;
                    else coordStatus = CoordStatus.None;
                    model.PlayerShips[x, y] = coordStatus;

                }
            }
            
            Direction direction;
            ShipType shipType = ShipType.x1;
            if (checkBox1.Checked) direction = Direction.Vertical; else direction = Direction.Horizontal;
            if (checkBox2.Checked)
            {
                model.AddDelShip(textBox1.Text, shipType, direction, true);
                button204_Click(sender, e);
                return;
            };
            if (radioButton1.Checked) shipType = ShipType.x1;
            if (radioButton2.Checked) shipType = ShipType.x2;
            if (radioButton3.Checked) shipType = ShipType.x3;
            if (radioButton4.Checked) shipType = ShipType.x4;



            model.AddDelShip(textBox1.Text, shipType, direction, checkBox2.Checked);
            button204_Click(sender, e);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int y = dataGridView1.SelectedCells[0].RowIndex;
            int x = dataGridView1.SelectedCells[0].ColumnIndex;

            textBox1.Text = x.ToString() + y.ToString();
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked) { button203.Text = "Удалить"; }
            else { button203.Text = "Поставить"; }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int cnt = dataGridView1.SelectedCells.Count;
            textBox1.Text = cnt.ToString();
            if (cnt > 4) 
            {
                MessageBox.Show("Превышено колличество клеток");
                int x = dataGridView1.SelectedCells[cnt -1].ColumnIndex;
                int y = dataGridView1.SelectedCells[0].RowIndex;
                dataGridView1.Rows[y].Cells[x].Selected = false;
                dataGridView1.ClearSelection();
            }
        }
    }


}

       

      

