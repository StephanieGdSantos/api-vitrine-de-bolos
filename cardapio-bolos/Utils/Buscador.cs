using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace subsequencia_mais_longa.Services
{
    public class Buscador
    {
        public static List<string> BuscarPorMaiorStringComum(string[] palavrasDisponiveis, string palavraProcurada)
        {
            var resultados = new List<(string Palavra, int Valor)>();

            foreach (var palavra in palavrasDisponiveis)
            {
                var subsequenciaComum = new int[palavraProcurada.Length + 1, palavra.Length + 1];

                for (int i = 1; i < palavraProcurada.Length + 1; i++)
                {
                    for (int j = 1; j < palavra.Length + 1; j++)
                    {
                        if (palavraProcurada[i - 1] == palavra[j - 1])
                        {
                            subsequenciaComum[i, j] = subsequenciaComum[i - 1, j - 1] + 1;
                        }
                        else
                        {
                            subsequenciaComum[i, j] = Math.Max(subsequenciaComum[i - 1, j], subsequenciaComum[i, j - 1]);
                        }
                    }
                }

                var maiorValor = subsequenciaComum[palavraProcurada.Length, palavra.Length];
                resultados.Add((palavra, maiorValor));
            }

            var numeroDeOpcoes = 2;
            if (resultados.Count == 1)
            {
                numeroDeOpcoes = 1;
            }
            else if (resultados.Count == 0)
            {
                numeroDeOpcoes = 0;
            }
            return resultados.OrderByDescending(r => r.Valor).Take(numeroDeOpcoes).Select(r => r.Palavra).ToList();
        }

        public static List<string> BuscarPorMaiorProporcaoSemelhante(string[] palavrasDisponiveis, string palavraProcurada)
        {
            var resultados = new List<(string Palavra, double Proporcao)>();

            foreach (var palavra in palavrasDisponiveis)
            {
                var subsequenciaComum = new int[palavraProcurada.Length + 1, palavra.Length + 1];

                for (int i = 1; i < palavraProcurada.Length + 1; i++)
                {
                    for (int j = 1; j < palavra.Length + 1; j++)
                    {
                        if (palavraProcurada[i - 1] == palavra[j - 1])
                        {
                            subsequenciaComum[i, j] = subsequenciaComum[i - 1, j - 1] + 1;
                        }
                        else
                        {
                            subsequenciaComum[i, j] = Math.Max(subsequenciaComum[i - 1, j], subsequenciaComum[i, j - 1]);
                        }
                    }
                }

                var maiorValor = subsequenciaComum[palavraProcurada.Length, palavra.Length];
                double proporcao = maiorValor / (double)palavra.Length;
                resultados.Add((palavra, proporcao));
            }

            var numeroDeOpcoes = 2;
            if (resultados.Count == 1)
            {
                numeroDeOpcoes = 1;
            }
            else if (resultados.Count == 0)
            {
                numeroDeOpcoes = 0;
            }
            return resultados.OrderByDescending(r => r.Proporcao).Take(numeroDeOpcoes).Select(r => r.Palavra).ToList();
        }

        public List<string> BuscarNomesSemelhantes(string[] palavrasDisponiveis, string palavraProcurada)
        {
            var nomesProximos = BuscarPorMaiorStringComum(palavrasDisponiveis, palavraProcurada);
            var nomesProporcionalmenteProximos = BuscarPorMaiorProporcaoSemelhante(palavrasDisponiveis, palavraProcurada);

            var listaDeNomes = nomesProximos.Concat(nomesProporcionalmenteProximos).Distinct().ToList();

            return listaDeNomes.Select(nome => nome.ToLower()).ToList();
        }
    }
}
