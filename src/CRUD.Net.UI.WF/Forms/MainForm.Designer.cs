
namespace CRUD.Net.UI.WF
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripCadastros = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripFornecedores = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripProdutos = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSair = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripUsuario = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripCadastros,
            this.toolStripSair});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1182, 28);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // toolStripCadastros
            // 
            this.toolStripCadastros.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripFornecedores,
            this.toolStripSeparator1,
            this.toolStripProdutos});
            this.toolStripCadastros.Name = "toolStripCadastros";
            this.toolStripCadastros.Size = new System.Drawing.Size(88, 24);
            this.toolStripCadastros.Text = "Cadastros";
            // 
            // toolStripFornecedores
            // 
            this.toolStripFornecedores.Name = "toolStripFornecedores";
            this.toolStripFornecedores.Size = new System.Drawing.Size(181, 26);
            this.toolStripFornecedores.Text = "Fornecedores";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(178, 6);
            // 
            // toolStripProdutos
            // 
            this.toolStripProdutos.Name = "toolStripProdutos";
            this.toolStripProdutos.Size = new System.Drawing.Size(181, 26);
            this.toolStripProdutos.Text = "Produtos";
            // 
            // toolStripSair
            // 
            this.toolStripSair.Name = "toolStripSair";
            this.toolStripSair.Size = new System.Drawing.Size(48, 24);
            this.toolStripSair.Text = "Sair";
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripUsuario});
            this.statusStrip.Location = new System.Drawing.Point(0, 927);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1182, 26);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripUsuario
            // 
            this.toolStripUsuario.Name = "toolStripUsuario";
            this.toolStripUsuario.Size = new System.Drawing.Size(62, 20);
            this.toolStripUsuario.Text = "Usuario:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 953);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripCadastros;
        private System.Windows.Forms.ToolStripMenuItem toolStripFornecedores;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripProdutos;
        private System.Windows.Forms.ToolStripMenuItem toolStripSair;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripUsuario;
    }
}

