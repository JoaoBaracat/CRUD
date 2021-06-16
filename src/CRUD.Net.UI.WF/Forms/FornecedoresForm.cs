using CRUD.Net.Domain.Apps;
using CRUD.Net.Domain.Entities;
using CRUD.Net.UI.WF.Helpers;
using System;
using System.Windows.Forms;

namespace CRUD.Net.UI.WF.Forms
{
    public partial class FornecedoresForm : Form
    {
        private readonly IFornecedorApp _fornecedorApp;
        private ValidateResponse _validateResponse;
        private BindingSource _bindingSource = new BindingSource();
        private Guid idFornecedor;

        public FornecedoresForm()
        {
            _fornecedorApp = (IFornecedorApp)Program.ServiceProvider.GetService(typeof(IFornecedorApp));
            _validateResponse = new ValidateResponse();
            InitializeComponent();
            buttonExcluir.Click += new EventHandler(buttonExcluir_Click);
            buttonNovo.Click += new EventHandler(buttonNovo_Click);
            textBoxPesquisar.KeyUp += textBoxPesquisar_KeyUp;

            dataGridViewFornecedores.DoubleClick += new EventHandler(dataGridViewFornecedores_DoubleClick);
            buttonCancelar.Click += new EventHandler(buttonCancelar_Click);
            buttonSalvar.Click += new EventHandler(buttonSalvar_Click);
            ClearFields();
        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            _fornecedorApp.CreateOrUpdate(
                new Fornecedor
                {
                    Id = idFornecedor,
                    Nome = textBoxNome.Text,
                    Endereco = textBoxEndereco.Text,
                    CNPJ = textBoxCNPJ.Text,
                    Ativo = checkBoxAtivo.Checked,
                });
            string msgOk = idFornecedor == Guid.Empty ? "criado" : "alterado";
            if (_validateResponse.CustomResponse($"Fornecedor {msgOk} com sucesso."))
            {
                ClearFields();
                GetData();
            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void dataGridViewFornecedores_DoubleClick(object sender, EventArgs e)
        {
            idFornecedor = (Guid)dataGridViewFornecedores.CurrentRow.Cells["Id"].Value;
            textBoxNome.Text = (string)dataGridViewFornecedores.CurrentRow.Cells["Nome"].Value;
            textBoxCNPJ.Text = (string)dataGridViewFornecedores.CurrentRow.Cells["CNPJ"].Value;
            textBoxEndereco.Text = (string)dataGridViewFornecedores.CurrentRow.Cells["Endereco"].Value;
            checkBoxAtivo.Checked = (bool)dataGridViewFornecedores.CurrentRow.Cells["Ativo"].Value;
            tabControl.SelectedTab = tabPageCadastro;
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
            dataGridViewFornecedores.DataSource = _bindingSource;
            _bindingSource.DataSource = _fornecedorApp.GetByNomeADO(textBoxPesquisar.Text);
        }

        private void buttonNovo_Click(object sender, EventArgs e)
        {
            ClearFields();
            tabControl.SelectedTab = tabPageCadastro;
        }

        private void buttonExcluir_Click(object sender, EventArgs e)
        {
            _fornecedorApp.Delete((Guid)dataGridViewFornecedores.CurrentRow.Cells["Id"].Value);
            _validateResponse.CustomResponse("Fornecedor excluído com sucesso.");
            GetData();
        }

        private void ClearFields()
        {
            idFornecedor = Guid.Empty;
            textBoxNome.Text = "";
            textBoxCNPJ.Text = "";
            textBoxEndereco.Text = "";
            checkBoxAtivo.Checked = false;
        }
    }
}
