using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjAtendimento
{
    
    public partial class Form1 : Form
    {
        private Senhas senhas = new Senhas();
        private Guiches guiches = new Guiches();

        private Button btnGerar;
        private ListBox lstSenhas;
        private Label lblQtdGuichesText;
        private Label lblQtdGuiches;
        private Button btnAdicionarGuiche;
        private Label lblGuiche;
        private TextBox txtGuiche;
        private Button btnChamar;
        private ListBox lstAtendimentos;
        private Button btnListarSenhas;
        private Button btnListarAtendimentos;

        private void InitializeUI()
        {
            this.Text = "Gerenciamento de Senhas";
            this.Size = new Size(700, 380);
            this.StartPosition = FormStartPosition.CenterScreen;

            btnGerar = new Button();
            btnGerar.Text = "Gerar";
            btnGerar.Size = new Size(100, 30);
            btnGerar.Location = new Point(20, 50);
            btnGerar.Click += btnGerar_Click;
            this.Controls.Add(btnGerar);

            lstSenhas = new ListBox();
            lstSenhas.Size = new Size(200, 150);
            lstSenhas.Location = new Point(20, 100);
            this.Controls.Add(lstSenhas);

            btnListarSenhas = new Button();
            btnListarSenhas.Text = "Listar Senhas";
            btnListarSenhas.Size = new Size(200, 30);
            btnListarSenhas.Location = new Point(20, 260);
            btnListarSenhas.Click += btnListarSenhas_Click;
            this.Controls.Add(btnListarSenhas);

            lblQtdGuichesText = new Label();
            lblQtdGuichesText.Text = "Qtd. Guichês:";
            lblQtdGuichesText.Size = new Size(80, 20);
            lblQtdGuichesText.Location = new Point(240, 100);
            this.Controls.Add(lblQtdGuichesText);

            lblQtdGuiches = new Label();
            lblQtdGuiches.Text = "0";
            lblQtdGuiches.Size = new Size(40, 40);
            lblQtdGuiches.Location = new Point(300, 120);
            lblQtdGuiches.Font = new Font(lblQtdGuiches.Font.FontFamily, 24);
            this.Controls.Add(lblQtdGuiches);

            btnAdicionarGuiche = new Button();
            btnAdicionarGuiche.Text = "Adicionar";
            btnAdicionarGuiche.Size = new Size(150, 30);
            btnAdicionarGuiche.Location = new Point(240, 180);
            btnAdicionarGuiche.Click += btnAdicionarGuiche_Click;
            this.Controls.Add(btnAdicionarGuiche);

            lblGuiche = new Label();
            lblGuiche.Text = "Guichê:";
            lblGuiche.Size = new Size(50, 20);
            lblGuiche.Location = new Point(500, 20);
            this.Controls.Add(lblGuiche);

            txtGuiche = new TextBox();
            txtGuiche.Size = new Size(100, 30);
            txtGuiche.Location = new Point(565, 20);
            this.Controls.Add(txtGuiche);

            btnChamar = new Button();
            btnChamar.Text = "Chamar";
            btnChamar.Size = new Size(100, 30);
            btnChamar.Location = new Point(565, 50);
            btnChamar.Click += btnChamar_Click;
            this.Controls.Add(btnChamar);

            lstAtendimentos = new ListBox();
            lstAtendimentos.Size = new Size(239, 150);
            lstAtendimentos.Location = new Point(425, 100);
            this.Controls.Add(lstAtendimentos);

            btnListarAtendimentos = new Button();
            btnListarAtendimentos.Text = "Listar Atendimentos";
            btnListarAtendimentos.Size = new Size(239, 30);
            btnListarAtendimentos.Location = new Point(425, 260);
            btnListarAtendimentos.Click += btnListarAtendimentos_Click;
            this.Controls.Add(btnListarAtendimentos);
        }

        public Form1()
        {
            InitializeComponent();
            InitializeUI();
        }
        private void btnGerar_Click(object sender, EventArgs e)
        {
            senhas.gerar();
            atualizarListaSenhas();
        }

        private void btnAdicionarGuiche_Click(object sender, EventArgs e)
        {
            int novoId = guiches.getListaGuiches().Count() + 1;
            Guiche novoGuiche = new Guiche(novoId);
            guiches.adicionar(novoGuiche);

            lblQtdGuiches.Text = guiches.getListaGuiches().Count().ToString();
        }

        private void btnChamar_Click(object sender, EventArgs e)
        {
            try
            {
                int idGuiche = int.Parse(txtGuiche.Text);
                List<Guiche> lista = guiches.getListaGuiches();
                Guiche guiche = lista.FirstOrDefault(g => g.getId() == idGuiche);

                if (guiche != null)
                {
                    bool sucesso = guiche.chamar(senhas.getFilaSenhas());
                    if (sucesso)
                    {
                        atualizarListaSenhas();
                        atualizarListaAtendimentos(guiche);
                    }
                    else
                    {
                        MessageBox.Show("Nenhuma senha disponível para chamar.", "Atenção");
                    }
                }
                else
                {
                    MessageBox.Show("Guichê não encontrado.", "Erro");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao chamar: " + ex);
            }

        }

        private void btnListarSenhas_Click(object sender, EventArgs e)
        {
            atualizarListaSenhas();
        }

        private void btnListarAtendimentos_Click(object sender, EventArgs e)
        {
            try
            {
                int idGuiche = int.Parse(txtGuiche.Text);
                List<Guiche> lista = guiches.getListaGuiches();
                Guiche guiche = lista.FirstOrDefault(g => g.getId() == idGuiche);

                if (guiche != null)
                {
                    atualizarListaAtendimentos(guiche);
                }
                else
                {
                    MessageBox.Show("Guichê não encontrado.", "Erro");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Erro ao listar atendimentos: " + err);
            }

        }

        private void atualizarListaSenhas()
        {
            lstSenhas.Items.Clear();
            foreach (var senha in senhas.getFilaSenhas())
            {
                lstSenhas.Items.Add(senha.dadosParciais());
            }
        }

        private void atualizarListaAtendimentos(Guiche guiche)
        {
            lstAtendimentos.Items.Clear();
            foreach (var atendimento in guiche.getAtendimentos())
            {
                lstAtendimentos.Items.Add(atendimento.dadosCompletos());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
