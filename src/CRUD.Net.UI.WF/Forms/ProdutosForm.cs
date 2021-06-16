using CRUD.Net.Domain.Apps;
using CRUD.Net.Domain.Entities;
using CRUD.Net.UI.WF.Helpers;
using System;
using System.Windows.Forms;

namespace CRUD.Net.UI.WF.Forms
{
    public partial class ProdutosForm : Form
    {
        private readonly IProdutoApp _produtoApp;
        private readonly IFornecedorApp _fornecedorApp;
        private ValidateResponse _validateResponse;
        private BindingSource _bindingSourceGrid = new BindingSource();
        private BindingSource _bindingSourceComboFornecedores = new BindingSource();
        private Guid idProduto;

        public ProdutosForm()
        {
            _produtoApp = (IProdutoApp)Program.ServiceProvider.GetService(typeof(IProdutoApp));
            _fornecedorApp = (IFornecedorApp)Program.ServiceProvider.GetService(typeof(IFornecedorApp));
            _validateResponse = new ValidateResponse();
            InitializeComponent();
            buttonExcluir.Click += new EventHandler(buttonExcluir_Click);
            buttonNovo.Click += new EventHandler(buttonNovo_Click);
            textBoxPesquisar.KeyUp += textBoxPesquisar_KeyUp;
            dataGridViewProdutos.CellDoubleClick += new DataGridViewCellEventHandler(dataGridViewProdutos_CellDoubleClick);
            buttonCancelar.Click += new EventHandler(buttonCancelar_Click);
            buttonSalvar.Click += new EventHandler(buttonSalvar_Click);

            _bindingSourceComboFornecedores.DataSource = _fornecedorApp.GetAllAtivos();
            comboBoxFornecedoresAtivos.DataSource = _bindingSourceComboFornecedores.DataSource;
            comboBoxFornecedoresAtivos.DisplayMember = "Nome";
            comboBoxFornecedoresAtivos.ValueMember = "Id";
            ClearFields();
        }

        private void dataGridViewProdutos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            idProduto = (Guid)dataGridViewProdutos.CurrentRow.Cells["Id"].Value;
            textBoxNome.Text = (string)dataGridViewProdutos.CurrentRow.Cells["Nome"].Value;
            comboBoxFornecedoresAtivos.SelectedValue = (Guid)dataGridViewProdutos.CurrentRow.Cells["FornecedorId"].Value;
            numericUpDownQuantidade.Value = (Int32)dataGridViewProdutos.CurrentRow.Cells["Quantidade"].Value;
            tabControl.SelectedTab = tabPageCadastro;
        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            _produtoApp.CreateOrUpdate(
                new Produto
                {
                    Id = idProduto,
                    Nome = textBoxNome.Text,
                    Fornecedor = _fornecedorApp.GetById((Guid)comboBoxFornecedoresAtivos.SelectedValue),
                    Quantidade = (Int32)numericUpDownQuantidade.Value,
                });
            string msgOk = idProduto == Guid.Empty ? "criado" : "alterado";
            if (_validateResponse.CustomResponse($"Produto {msgOk} com sucesso."))
            {
                ClearFields();
                GetData();
            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void textBoxPesquisar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetData();
                e.Handled = true;
            }
        }

        private void GetData()
        {
            dataGridViewProdutos.DataSource = _bindingSourceGrid;
            _bindingSourceGrid.DataSource = _produtoApp.GetByNomeADO(textBoxPesquisar.Text);
        }

        private void buttonNovo_Click(object sender, EventArgs e)
        {
            ClearFields();
            tabControl.SelectedTab = tabPageCadastro;
        }

        private void buttonExcluir_Click(object sender, EventArgs e)
        {
            _produtoApp.Delete((Guid)dataGridViewProdutos.CurrentRow.Cells["Id"].Value);
            _validateResponse.CustomResponse("Produto excluído com sucesso.");
            GetData();
        }

        private void ClearFields()
        {
            idProduto = Guid.Empty;
            textBoxNome.Text = "";
            comboBoxFornecedoresAtivos.SelectedIndex = -1;
            numericUpDownQuantidade.Value = 0;
        }
    }
}
