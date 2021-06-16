using CRUD.Net.Domain.Apps;
using CRUD.Net.UI.WF.Forms;
using System;
using System.Windows.Forms;

namespace CRUD.Net.UI.WF
{
    public partial class MainForm : Form
    {
        private readonly IProdutoApp _produtoApp;
        private readonly IFornecedorApp _fornecedorApp;
        
        public MainForm(string login)
        {
            _produtoApp =    (IProdutoApp)Program.ServiceProvider.GetService(typeof(IProdutoApp));
            _fornecedorApp = (IFornecedorApp)Program.ServiceProvider.GetService(typeof(IFornecedorApp));

            InitializeComponent();
            this.statusStrip.Items[0].Text = $"Usuario: {login ?? "Administrador"}";

            toolStripFornecedores.Click += new EventHandler(toolStripFornecedores_Click);
            toolStripProdutos.Click += new EventHandler(toolStripProdutos_Click);
            toolStripSair.Click += new EventHandler(toolStripSair_Click);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void toolStripFornecedores_Click(object sender, EventArgs e)
        {
            var fornecedoresForm = new FornecedoresForm();
            fornecedoresForm.ShowDialog();
        }

        private void toolStripProdutos_Click(object sender, EventArgs e)
        {
            var produtosForm = new ProdutosForm();
            produtosForm.ShowDialog();
        }
        private void toolStripSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        
    }
}
