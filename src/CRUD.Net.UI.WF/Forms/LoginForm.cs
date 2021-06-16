using CRUD.Net.Domain.Apps;
using CRUD.Net.Domain.Entities;
using CRUD.Net.UI.WF.Helpers;
using System;
using System.Windows.Forms;

namespace CRUD.Net.UI.WF
{
    public partial class LoginForm : Form
    {
        private readonly IUsuarioApp _usuarioApp;
        private ValidateResponse _validateResponse;

        public string Login { get; private set; }

        public LoginForm()
        {
            _usuarioApp = (IUsuarioApp)Program.ServiceProvider.GetService(typeof(IUsuarioApp));
            _validateResponse = new ValidateResponse();
            InitializeComponent();
            buttonLogin.Click += new EventHandler(buttonLogin_Click);
            buttonSair.Click += new EventHandler(buttonSair_Click);
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            var auth = _usuarioApp.Authenticate(textBoxUsuario.Text, textBoxSenha.Text);
            if (auth == null)
            {
                _usuarioApp.Validate(new Usuario { Login = textBoxUsuario.Text, Senha = textBoxSenha.Text });
                if (!_validateResponse.CustomResponse())
                {
                    return;
                }

                DialogResult dialogResult = MessageBox.Show("Usuário não cadastrado. Deseja criar seu usuário?", "Registrar", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    _usuarioApp.Create(new Usuario { Login = textBoxUsuario.Text, Senha = textBoxSenha.Text });
                    if (_validateResponse.CustomResponse("Usuário criado com sucesso."))
                    {
                        ReturnOK();
                    }
                }
            }
            else
            {
                ReturnOK();
            }            
        }

        private void ReturnOK()
        {
            Login = textBoxUsuario.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
