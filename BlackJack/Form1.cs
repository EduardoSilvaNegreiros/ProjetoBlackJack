using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BlackJack
{
    public partial class Form1 : Form
    {
        // Lista para armazenar os valores das cartas
        List<string> baralho = new List<string> { "As", "Dois", "Tres", "Quatro", "Cinco", "Seis", "Sete", "Oito", "Nove", "Dez", "Valete", "Dama", "Rei" };
        Random random = new Random(); // Para sortear as cartas

        // Arrays para armazenar as imagens das cartas
        string[] cartasJogador1 = new string[5];
        string[] cartasJogador2 = new string[5];

        // Contadores de pontuação
        int pontosJogador1 = 0;
        int pontosJogador2 = 0;

        // Flags para controlar se o jogador parou
        bool jogador1Parou = false;
        bool jogador2Parou = false;

        public Form1()
        {
            InitializeComponent();
        }

        // Método para distribuir cartas para o jogador
        private void DistribuirCarta(int jogador)
        {
            string carta = baralho[random.Next(baralho.Count)]; // Sorteia uma carta
            if (jogador == 1)
            {
                for (int i = 0; i < cartasJogador1.Length; i++)
                {
                    if (cartasJogador1[i] == null)
                    {
                        cartasJogador1[i] = carta;
                        AtualizarImagem(jogador, i, carta);
                        pontosJogador1 += CalcularPontos(carta); // Adiciona pontos para o jogador 1
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < cartasJogador2.Length; i++)
                {
                    if (cartasJogador2[i] == null)
                    {
                        cartasJogador2[i] = carta;
                        AtualizarImagem(jogador, i, carta);
                        pontosJogador2 += CalcularPontos(carta); // Adiciona pontos para o jogador 2
                        break;
                    }
                }
            }
        }

        // Método para atualizar a imagem da carta no PictureBox correspondente
        private void AtualizarImagem(int jogador, int indice, string carta)
        {
            string caminhoImagem = $"Cartas/{carta}.png"; // Caminho da imagem com base no nome da carta
            if (jogador == 1)
            {
                switch (indice)
                {
                    case 0:
                        picCartaJogador1_1.Image = Image.FromFile(caminhoImagem);
                        break;
                    case 1:
                        picCartaJogador1_2.Image = Image.FromFile(caminhoImagem);
                        break;
                    case 2:
                        picCartaJogador1_3.Image = Image.FromFile(caminhoImagem);
                        break;
                    case 3:
                        picCartaJogador1_4.Image = Image.FromFile(caminhoImagem);
                        break;
                    case 4:
                        picCartaJogador1_5.Image = Image.FromFile(caminhoImagem);
                        break;
                }
            }
            else
            {
                switch (indice)
                {
                    case 0:
                        picCartaJogador2_1.Image = Image.FromFile(caminhoImagem);
                        break;
                    case 1:
                        picCartaJogador2_2.Image = Image.FromFile(caminhoImagem);
                        break;
                    case 2:
                        picCartaJogador2_3.Image = Image.FromFile(caminhoImagem);
                        break;
                    case 3:
                        picCartaJogador2_4.Image = Image.FromFile(caminhoImagem);
                        break;
                    case 4:
                        picCartaJogador2_5.Image = Image.FromFile(caminhoImagem);
                        break;
                }
            }
        }

        // Método para calcular os pontos de uma carta
        private int CalcularPontos(string carta)
        {
            switch (carta)
            {
                case "As":
                    return 11; 
                case "Dois":
                    return 2;
                case "Tres":
                    return 3;
                case "Quatro":
                    return 4;
                case "Cinco":
                    return 5;
                case "Seis":
                    return 6;
                case "Sete":
                    return 7;
                case "Oito":
                    return 8;
                case "Nove":
                    return 9;
                case "Dez":
                    return 10;
                case "Valete":
                case "Dama":
                case "Rei":
                    return 10; // Valete, Dama e Rei valem 10 pontos
                default:
                    return 0; // Caso a carta seja inválida ou não reconhecida
            }
        }

        // Eventos de clique dos botões para pedir carta
        private void btnPedirJogador1_Click(object sender, EventArgs e)
        {
            if (!jogador1Parou) // Se o jogador 1 não parou
            {
                DistribuirCarta(1); // Distribui carta para o jogador 1
                lblPontosJogador1.Text = $"Pontos: {pontosJogador1}"; // Atualiza os pontos do jogador 1
                VerificarSeBusted(1); // Verifica se o jogador 1 estourou
            }
        }

        private void btnPedirJogador2_Click(object sender, EventArgs e)
        {
            if (!jogador2Parou) // Se o jogador 2 não parou
            {
                DistribuirCarta(2); // Distribui carta para o jogador 2
                lblPontosJogador2.Text = $"Pontos: {pontosJogador2}"; // Atualiza os pontos do jogador 2
                VerificarSeBusted(2); // Verifica se o jogador 2 estourou
            }
        }

        // Verifica se o jogador estourou
        private void VerificarSeBusted(int jogador)
        {
            if (jogador == 1 && pontosJogador1 > 21)
            {
                MessageBox.Show("Jogador 1 estourou! O jogador 2 vence.");
                btnPedirJogador1.Enabled = false; // Desabilita o botão de pedir para o jogador 1
                btnPedirJogador2.Enabled = false; // Desabilita o botão de pedir para o jogador 2
                btnMotrarVencedor.Enabled = true; // Habilita o botão para mostrar vencedor
            }
            else if (jogador == 2 && pontosJogador2 > 21)
            {
                MessageBox.Show("Jogador 2 estourou! O jogador 1 vence.");
                btnPedirJogador1.Enabled = false; // Desabilita o botão de pedir para o jogador 1
                btnPedirJogador2.Enabled = false; // Desabilita o botão de pedir para o jogador 2
                btnMotrarVencedor.Enabled = true; // Habilita o botão para mostrar vencedor
            }
        }

        // Exibe o vencedor ao final da rodada
        private void btnMotrarVencedor_Click(object sender, EventArgs e)
        {
            if (pontosJogador1 > pontosJogador2 && pontosJogador1 <= 21)
            {
                MessageBox.Show("Jogador 1 venceu!");
            }
            else if (pontosJogador2 > pontosJogador1 && pontosJogador2 <= 21)
            {
                MessageBox.Show("Jogador 2 venceu!");
            }
            else
            {
                MessageBox.Show("Empate!");
            }
        }

        // Inicia um novo jogo
        private void btnNovoJogo_Click(object sender, EventArgs e)
        {
            pontosJogador1 = 0;
            pontosJogador2 = 0;
            lblPontosJogador1.Text = $"Pontos: {pontosJogador1}";
            lblPontosJogador2.Text = $"Pontos: {pontosJogador2}";

            // Limpar as cartas (remover as imagens das PictureBox)
            picCartaJogador1_1.Image = null;
            picCartaJogador1_2.Image = null;
            picCartaJogador1_3.Image = null;
            picCartaJogador1_4.Image = null;
            picCartaJogador1_5.Image = null;

            picCartaJogador2_1.Image = null;
            picCartaJogador2_2.Image = null;
            picCartaJogador2_3.Image = null;
            picCartaJogador2_4.Image = null;
            picCartaJogador2_5.Image = null;

            // Resetar variáveis de controle
            for (int i = 0; i < cartasJogador1.Length; i++)
            {
                cartasJogador1[i] = null; // Limpar as cartas dos jogadores
            }
            for (int i = 0; i < cartasJogador2.Length; i++)
            {
                cartasJogador2[i] = null;
            }

            // Reabilitar os botões
            btnPedirJogador1.Enabled = true;
            btnPedirJogador2.Enabled = true;
            btnMotrarVencedor.Enabled = false;

            // Resetar os flags de parar (caso o jogador tenha parado anteriormente)
            jogador1Parou = false;
            jogador2Parou = false;
        }

        private void btnPararJogador1_Click_1(object sender, EventArgs e)
        {
            // Marcar que o jogador 1 parou de pedir cartas
            jogador1Parou = true;

            // Desabilitar o botão de pedir carta para o jogador 1
            btnPedirJogador1.Enabled = false;

            // Verificar se ambos os jogadores pararam
            VerificarFimDeJogo();
        }

        private void btnPararJogador2_Click_1(object sender, EventArgs e)
        {
            // Marcar que o jogador 2 parou de pedir cartas
            jogador2Parou = true;

            // Desabilitar o botão de pedir carta para o jogador 2
            btnPedirJogador2.Enabled = false;

            // Verificar se ambos os jogadores pararam
            VerificarFimDeJogo();
        }

        private void VerificarFimDeJogo()
        {
            // Se ambos os jogadores pararam, o jogo terminou e podemos verificar os pontos
            if (jogador1Parou && jogador2Parou)
            {
                // Mostrar o vencedor, levando em consideração que ninguém pode ter estourado
                btnMotrarVencedor.Enabled = true; // Habilitar botão para mostrar vencedor
            }
        }
    }
}
