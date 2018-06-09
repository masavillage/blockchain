using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Console.WriteLine("FormLoad");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var blockchain = new BlockChain();
            blockchain.CreateBlock();
            MessageBox.Show(blockchain.blockChain.Last().Nonce.ToString(""));
        }
    }

    class TestClass
    {
        static void Main(string[] args)
        {
            // Display the number of command line arguments:
            System.Console.WriteLine(args.Length);
            Form f = new Form1();
            f.ShowDialog();
        }
    }
}

