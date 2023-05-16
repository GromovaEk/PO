using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PO2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CtrlTPN = new TCtrl<TPNumber>();
            CtrlTF = new TCtrl<TFrac>();
            Buttons = new Button[16];

            Buttons[0] = button0;
            Buttons[1] = button1;
            Buttons[2] = button2;
            Buttons[3] = button3;
            Buttons[4] = button4;
            Buttons[5] = button5;
            Buttons[6] = button6;
            Buttons[7] = button7;
            Buttons[8] = button8;
            Buttons[9] = button9;
            Buttons[10] = button10;
            Buttons[11] = button11;
            Buttons[12] = button12;
            Buttons[13] = button13;
            Buttons[14] = button14;
            Buttons[15] = button15;

            checkedListBox1.SetItemChecked(0, true);
        }

        private TCtrl<TPNumber> CtrlTPN;
        private TCtrl<TFrac> CtrlTF;
        
        private enum ControlType {
            PController,            
            FractionController,     
            ComplexController
        }

        ControlType controller_type;

        private Button[] Buttons;

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            if(controller_type == ControlType.PController)
            {
                label1.Focus();
                int n = Convert.ToInt32(domainUpDown1.SelectedItem.ToString());
                for (int i = 1; i < 16; i++)
                    Buttons[i].Enabled = i < n ? true : false;
                richTextBox1.Text = CtrlTPN.Command(30 + n);
                MemoryValue.Text = CtrlTPN.Memory.GetStr();
            }
            if (controller_type == ControlType.FractionController)
            {
                label1.Focus();
                int n = Convert.ToInt32(domainUpDown1.SelectedItem.ToString());
                for (int i = 1; i < 16; i++)
                    Buttons[i].Enabled = i < n ? true : false;
                richTextBox1.Text = CtrlTF.Command(30 + n); 
                MemoryValue.Text = CtrlTF.Memory.GetStr();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D0:
                    button0.PerformClick();
                    break;
                case Keys.D1:
                    button1.PerformClick();
                    break;
                case Keys.D2:
                    button2.PerformClick();
                    break;
                case Keys.D3:
                    button3.PerformClick();
                    break;
                case Keys.D4:
                    button4.PerformClick();
                    break;
                case Keys.D5:
                    button5.PerformClick();
                    break;
                case Keys.D6:
                    button6.PerformClick();
                    break;
                case Keys.D7:
                    button7.PerformClick();
                    break;
                case Keys.D8:
                    if ((e.Modifiers & Keys.Shift) == Keys.Shift)
                        buttonMul.PerformClick();
                    else
                        button8.PerformClick();
                    break;
                case Keys.D9:
                    button9.PerformClick();
                    break;

                case Keys.A:
                    button10.PerformClick();
                    break;
                case Keys.B:
                    button11.PerformClick();
                    break;
                case Keys.C:
                    button12.PerformClick();
                    break;
                case Keys.D:
                    button13.PerformClick();
                    break;
                case Keys.E:
                    button14.PerformClick();
                    break;
                case Keys.F:
                    button15.PerformClick();
                    break;

                case Keys.OemMinus:
                    buttonMin.PerformClick();
                    break;
                case Keys.Oemcomma:
                    buttonComma.PerformClick();
                    break;
                
                case Keys.Oemplus:
                    if ((e.Modifiers & Keys.Shift) == Keys.Shift)
                        buttonPlus.PerformClick();
                    else
                        buttonEq.PerformClick();
                    break;
                case Keys.Oem2:
                    buttonDelim.PerformClick();
                    break;

                case Keys.Back:
                    buttonC.PerformClick();
                    break;
                case Keys.Enter:
                    buttonEq.PerformClick();
                    break;
            }
        }

        private void button0_Click(object sender, EventArgs e)
        {
            label1.Focus();

            switch(controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(0);
                    break;
                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(0);
                    break;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Focus();

            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(1);
                    break;
                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(1);
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Focus();

            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(2);
                    break;
                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(2);
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Focus();

            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(3);
                    break;
                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(3);
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Focus();

            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(4);
                    break;
                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(4);
                    break;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label1.Focus();

            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(5);
                    break;
                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(5);
                    break;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label1.Focus();

            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(6);
                    break;
                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(6);
                    break;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            label1.Focus();

            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(7);
                    break;
                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(7);
                    break;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            label1.Focus();

            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(8);
                    break;
                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(8);
                    break;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            label1.Focus();

            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(9);
                    break;
                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(9);
                    break;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            label1.Focus();

            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(10);
                    break;
                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(10);
                    break;
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            label1.Focus();

            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(11);
                    break;
                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(11);
                    break;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            label1.Focus();

            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(12);
                    break;
                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(12);
                    break;
            }

        }

        private void button13_Click(object sender, EventArgs e)
        {
            label1.Focus();

            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(13);
                    break;
                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(13);
                    break;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            label1.Focus();

            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(14);
                    break;
                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(14);
                    break;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            label1.Focus();

            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(15);
                    break;
                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(15);
                    break;
            }
        }
        // Добавить в память
        private void buttonMpl_Click(object sender, EventArgs e)
        {
            label1.Focus();

            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(16);
                    MemoryValue.Text = CtrlTPN.Memory.GetStr();
                    break;

                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(16);
                    MemoryValue.Text = CtrlTF.Memory.GetStr();
                    break;
            }

            
        }
        // Сохранить в память
        private void buttonMS_Click(object sender, EventArgs e)
        {
            label1.Focus();

            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(17);
                    MemoryValue.Text = CtrlTPN.Memory.GetStr();
                    break;

                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(17);
                    MemoryValue.Text = CtrlTF.Memory.GetStr();
                    break;
            }
        }
        // Взять из памяти
        private void buttonMR_Click(object sender, EventArgs e)
        {
            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(18);
                    MemoryValue.Text = CtrlTPN.Memory.GetStr();
                    break;

                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(18);
                    MemoryValue.Text = CtrlTF.Memory.GetStr();
                    break;
            }
        }
        // Очистить память
        private void buttonMC_Click(object sender, EventArgs e)
        {
            label1.Focus();

            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(19);
                    MemoryValue.Text = CtrlTPN.Memory.GetStr();
                    break;

                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(19);
                    MemoryValue.Text = CtrlTF.Memory.GetStr();
                    break;
            }

        }

        private void buttonBackspace_Click(object sender, EventArgs e)
        {
            label1.Focus();

            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(20);
                    break;

                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(20);
                    break;
            }
        }

        
        private void buttonCE_Click(object sender, EventArgs e)
        {
            label1.Focus();

            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(21);
                    break;

                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(21);
                    break;
            }

        }

        private void buttonC_Click(object sender, EventArgs e)
        {
            label1.Focus();

            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(22);
                    break;

                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(22);
                    break;
            }
        }

        private void buttonReverse_Click(object sender, EventArgs e)
        {
            label1.Focus();

            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(23);
                    break;

                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(23);
                    break;
            }
        }

        private void buttonComma_Click(object sender, EventArgs e)
        {
            label1.Focus();
            
            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(24);
                    break;

                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(24);
                    break;
            }
        }

        // Ввод знака равенства
        private void buttonEq_Click(object sender, EventArgs e)
        {
            label1.Focus();

            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(25);
                    break;

                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(25);
                    break;
            }
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            label1.Focus();

            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(26);
                    break;

                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(26);
                    break;
            }
        }


        private void buttonMin_Click(object sender, EventArgs e)
        {
            label1.Focus();

            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(27);
                    break;

                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(27);
                    break;
            }
        }

        private void buttonMul_Click(object sender, EventArgs e)
        {
            label1.Focus();
            
            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(28);
                    break;

                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(28);
                    break;
            }
        }

        private void buttonDelim_Click(object sender, EventArgs e)
        {
            label1.Focus();
            
            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(29);
                    break;

                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(29);
                    break;
            }
        }

        private void buttonSqr_Click(object sender, EventArgs e)
        {
            label1.Focus();
            
            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(30);
                    break;

                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(30);
                    break;
            }
        }

        private void buttonInverse_Click(object sender, EventArgs e)
        {
            label1.Focus();
            
            switch (controller_type)
            {
                case ControlType.PController:
                    richTextBox1.Text = CtrlTPN.Command(31);
                    break;

                case ControlType.FractionController:
                    richTextBox1.Text = CtrlTF.Command(31);
                    break;
            }
        }


        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //Смена режима работы (целые/дробные)
        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if(controller_type == ControlType.PController)
            {
                if (e.Index == 1)
                {
                    checkedListBox1.SetItemChecked(0, false);
                    buttonComma.Enabled = true;
                    richTextBox1.Text = CtrlTPN.Command(48);
                }
                else
                {
                    checkedListBox1.SetItemChecked(1, false);
                    buttonComma.Enabled = false;
                    richTextBox1.Text = CtrlTPN.Command(47);
                }
            }
        }
        // Изменение точности
        private void domainUpDown2_SelectedItemChanged(object sender, EventArgs e)
        {
            if (controller_type == ControlType.PController)
            {
                label1.Focus();
                int n = Convert.ToInt32(domainUpDown2.SelectedItem.ToString());
                richTextBox1.Text = CtrlTPN.Command(47 + n);
                MemoryValue.Text = CtrlTPN.Memory.GetStr();
            }
        }

        //включение памяти 
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (controller_type == ControlType.PController)
            {
                if (checkBox3.Checked)
                    richTextBox1.Text = CtrlTPN.Command(58);
                else
                    richTextBox1.Text = CtrlTPN.Command(57);
                MemoryValue.Text = CtrlTPN.Memory.GetStr();
            }
            else if (controller_type == ControlType.FractionController)
            {
                if(checkBox3.Checked)
                    richTextBox1.Text = CtrlTF.Command(58);
                else
                    richTextBox1.Text = CtrlTF.Command(57);
                MemoryValue.Text = CtrlTF.Memory.GetStr();
            }
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pичноеЧислоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int n;
            if (domainUpDown1.SelectedItem != null)
                n = Convert.ToInt32(domainUpDown1.SelectedItem.ToString());
            else n = 10;

            for (int i = 1; i < 16; i++)
                Buttons[i].Enabled = i < n ? true : false;

            domainUpDown1.Enabled = true;
            domainUpDown2.Enabled = true;
            checkedListBox1.Enabled = true;
            buttonComma.Text = ",";

            // возврат значения режима работы....
            if (checkedListBox1.GetSelected(0))
            {
                buttonComma.Enabled = true;
                richTextBox1.Text = CtrlTPN.Command(48);
            }
            else
            {
                buttonComma.Enabled = false;
                richTextBox1.Text = CtrlTPN.Command(47);
            }
            
            controller_type = ControlType.PController;
            richTextBox1.Text = CtrlTPN.Editor.Str;
            MemoryValue.Text = CtrlTPN.Memory.GetStr();
        }

        private void простаяДробьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 16; i++)
                Buttons[i].Enabled = i < 10 ? true : false;

            //domainUpDown1.Enabled = false;
            domainUpDown2.Enabled = false;
            checkedListBox1.Enabled = false;
            buttonComma.Text = "/";
            buttonComma.Enabled = true;

            controller_type = ControlType.FractionController;
            if(checkBox3.Checked)
                CtrlTF.Memory.FState = true;

            richTextBox1.Text = CtrlTF.Editor.Str;
            MemoryValue.Text = CtrlTF.Memory.GetStr();

        }

        private void комплексноеЧислоToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
