using System;

namespace CSOBRF_Validacoes
{
    public class ContasMatematicas
    {
        #region Atributos
        string novoValor;
        #endregion

        #region Retorna Conta com Margem (Calcula o preco de Venda Baseado no CustoXMargem)
        /// <summary>
        /// Calcula o valor de um valor x (preço custo) multiplicado por sua margem, retornando preço venda
        /// </summary>
        /// <param name="margem">Inteiro com a Margem de Lucro (ex: 40, para 40%)</param>
        /// <param name="valor">Valor de Custo (ex: 45,50) (quarenta e cinco reais e cinquenta cents)</param>
        /// <returns>Retorna Preço de Venda com sua margem de Lucro!</returns>

        public decimal calcularContaComMargem(int margem, decimal valor)
        {
            decimal valorCalculado;
            valorCalculado = (valor * margem) / 100;
            string valorParaTirar2CasasAposVirgula = Convert.ToString(valor + valorCalculado);
            return Convert.ToDecimal(limitaDecimalemDuasCasasAposVirgula(valorParaTirar2CasasAposVirgula));
        }//fim método
        #endregion

        #region Retorna Conta com Desconto (Calcula o preço com Desconto)
        /// <summary>
        /// Usado para calcular um desconto, recebe um valor e a porcentagem de desconto que tem que ser
        /// aplicado sobre esse valor, e retorna o valor já com o desconto
        /// </summary>
        /// <param name="desconto">Valor do Desconto (ex:10 para 10%)</param>
        /// <param name="valor">Valor do Produto onde será aplicado o Desconto (ex: 100,00)</param>
        /// <returns>Retorna o Desconto (ex: 90,00)</returns>
        public decimal calcularDesconto(int desconto, decimal valor)
        {
            
            decimal valorDoDesconto = (valor * desconto) / 100;
            decimal valorFinal = valor - valorDoDesconto;
            string valorParaTirar2CasasAposVirgula = Convert.ToString(valorFinal);
            return Convert.ToDecimal(limitaDecimalemDuasCasasAposVirgula(valorParaTirar2CasasAposVirgula));
        }//fim método
        #endregion

        #region Retorna Valor do Desconto (Calcula o Valor do Desconto e Retorna apenas ele)
        /// <summary>
        /// Usado para calcular um desconto, recebe um valor e a porcentagem de desconto que tem que ser
        /// aplicado sobre esse valor, e retorna o valor já com o desconto
        /// </summary>
        /// <param name="desconto">Valor do Desconto (ex:10 para 10%)</param>
        /// <param name="valor">Valor do Produto onde será aplicado o Desconto (ex: 100,00)</param>
        /// <returns>Retorna o Desconto (ex: 90,00)</returns>
        public decimal calcularValorDoDesconto(int desconto, decimal valor)
        {
            
            decimal valorDoDesconto = (valor * desconto) / 100;
            decimal valorFinal = valor - valorDoDesconto;
            decimal valorFinal2 = valor - valorFinal;
            string valorParaTirar2CasasAposVirgula = Convert.ToString(valorFinal2);
            return Convert.ToDecimal(limitaDecimalemDuasCasasAposVirgula(valorParaTirar2CasasAposVirgula));
        }//fim método
        #endregion

        #region Retorna Valor do Desconto Arredondando Terceira Casa Acima (Calcula o Valor do Desconto e Retorna apenas ele, Arredondando Acima)
        /// <summary>
        /// Usado para calcular um desconto, recebe um valor e a porcentagem de desconto que tem que ser
        /// aplicado sobre esse valor, e retorna o valor já com o desconto. Caso o Desconto venha com a 3 casa abaixo,
        /// ele retorna acima. Ex: 12,233 retorna 12,24 - Usado na sacanagem das impressoras fiscais
        /// </summary>
        /// <param name="desconto">Valor do Desconto (ex:10 para 10%)</param>
        /// <param name="valor">Valor do Produto onde será aplicado o Desconto (ex: 100,00)</param>
        /// <returns>Retorna o Desconto (ex: 90,00)</returns>
        public double calcularValorDoDescontoArredondaAcima(int desconto, double valor)
        {
            string verficarValor = Convert.ToString(valor);

            int posicao = verficarValor.IndexOf(",");

            if ((desconto > 0) && (posicao == verficarValor.Length - 5))
            {
                double valorDoDesconto = (valor * desconto) / 100;
                double valorFinal = valor - valorDoDesconto;

                int casasDecimais = 3;
                casasDecimais = (int)Math.Pow(10, casasDecimais);
                double valorFinalArredondado = Math.Ceiling(valorFinal * casasDecimais) / casasDecimais;

                return valorFinalArredondado;
            }
            else
            {
                double valorDoDesconto = (valor * desconto) / 100;
                double valorFinal = valor - valorDoDesconto;
                double resultado = this.arredondaQuartaCasaDecimalAcima(valorFinal);
                return resultado;
            }
        }//fim método
        #endregion

        #region Retorna Decimal Arredondando Terceira Casa Acima (Calcula o Valor do Desconto e Retorna apenas ele, Arredondando Acima)
        /// <summary>
        /// Usado para calcular um desconto, recebe um valor e a porcentagem de desconto que tem que ser
        /// aplicado sobre esse valor, e retorna o valor já com o desconto. Caso o Desconto venha com a 3 casa abaixo,
        /// ele retorna acima. Ex: 12,233 retorna 12,24 - Usado na sacanagem das impressoras fiscais
        /// </summary>
        /// <param name="desconto">Valor do Desconto (ex:10 para 10%)</param>
        /// <param name="valor">Valor do Produto onde será aplicado o Desconto (ex: 100,00)</param>
        /// <returns>Retorna o Desconto (ex: 90,00)</returns>
        public string arredondarTerceiraCasaAcima(string valor)
        {
            #region Verifiva se valor Decimal é deferente de ZERO(0)
            string verficarValor = valor;
            bool valida = false;


            for (int i = 0; i < verficarValor.Length; i++)
            {
                if (verficarValor.Substring(i, 1) == "," || verficarValor.Substring(i, 1) == ".")
                {
                    for (int x = 0; x <= 1; x++)
                    {
                        i++;
                        try
                        {
                            if (verficarValor.Substring(i, 1) != "0")
                            {
                                valida = true;
                            }

                        }
                        catch (Exception)
                        {
                            valida = false;
                        }
                    }
                }
            }
            #endregion

            #region Arredondamento
            if (valida == true)
            {
                int posicao = verficarValor.IndexOf(",");

                if (posicao == verficarValor.Length - 4)
                {
                    double valorFinal = Convert.ToDouble(valor);

                    int casasDecimais = 2;
                    casasDecimais = (int)Math.Pow(10, casasDecimais);
                    double valorFinalArredondado = Math.Ceiling(valorFinal * casasDecimais) / casasDecimais;

                    return valorFinalArredondado.ToString();
                }
                else
                {
                    double valorFinal = Convert.ToDouble(valor);
                    double resultado = this.arredondaQuartaCasaDecimalAcima(valorFinal);
                    return resultado.ToString();
                }
            }
            else
            {

                return valor;
            }

            #endregion

        }//fim método
        #endregion

        #region Retorna Valor Arredondando Terceira Casa Acima e deixando apenas duas casas
        /// <summary>
        /// Usado para retornar o valor arredondado com duas casas decimais apenas, se existir 3 casasdecimais,
        /// ele incremental +1 na Segunda casa decimal e tira a Terceira casa decimal.
        /// Exemplo:
        /// 0.842 (antes)
        /// 0.85  (depois)
        /// </summary>        
        /// <param name="valor">Valor do Produto onde será aplicado o Desconto (ex: 100,00)</param>
        /// <returns>Retorna o valor arredondado</returns>
        public double arredondaTerceiraCasaDecimalAcima(double valor)
        {
            string verficarValor = Convert.ToString(valor);

            int posicao = verficarValor.IndexOf(",");

            if (posicao == verficarValor.Length - 4)
            {
                int casasDecimais = 2;
                casasDecimais = (int)Math.Pow(10, casasDecimais);
                double valorFinalArredondado = Math.Ceiling(valor * casasDecimais) / casasDecimais;
                return valorFinalArredondado;
            }
            else
            {
                return valor;
            }
        }//fim método
        #endregion

        #region Retorna Decimal Arredondando Quarta Casa Acima (Calcula o Valor do Desconto e Retorna apenas ele, Arredondando Acima)
        /// <summary>
        /// Usado para calcular um desconto, recebe um valor e a porcentagem de desconto que tem que ser
        /// aplicado sobre esse valor, e retorna o valor já com o desconto. Caso o Desconto venha com a 3 casa abaixo,
        /// ele retorna acima. Ex: 12,233 retorna 12,24 - Usado na sacanagem das impressoras fiscais
        /// </summary>
        /// <param name="desconto">Valor do Desconto (ex:10 para 10%)</param>
        /// <param name="valor">Valor do Produto onde será aplicado o Desconto (ex: 100,00)</param>
        /// <returns>Retorna o Desconto (ex: 90,00)</returns>
        public string arredondarQuartaCasaAcima(string valor)
        {
            #region Verifiva se valor Decimal é deferente de ZERO(0)
            string verficarValor = valor;
            bool valida = false;


            for (int i = 0; i < verficarValor.Length; i++)
            {
                if (verficarValor.Substring(i, 1) == "," || verficarValor.Substring(i, 1) == ".")
                {
                    for (int x = 0; x <= 2; x++)
                    {
                        i++;
                        try
                        {
                            if (verficarValor.Substring(i, 1) != "0")
                            {
                                valida = true;
                            }

                        }
                        catch (Exception)
                        {
                            valida = false;
                        }
                    }
                }
            }
            #endregion

            #region Arredondamento
            if (valida == true)
            {
                int posicao = verficarValor.IndexOf(",");

                if (posicao == verficarValor.Length - 5)
                {
                    double valorFinal = Convert.ToDouble(valor);

                    int casasDecimais = 3;
                    casasDecimais = (int)Math.Pow(10, casasDecimais);
                    double valorFinalArredondado = Math.Ceiling(valorFinal * casasDecimais) / casasDecimais;

                    return valorFinalArredondado.ToString();
                }
                else
                {
                    double valorFinal = Convert.ToDouble(valor);
                    double resultado = this.arredondaQuartaCasaDecimalAcima(valorFinal);
                    return resultado.ToString();
                }
            }
            else
            {

                return valor;
            }

            #endregion

        }//fim método
        #endregion

        #region Retorna Valor Arredondando Quarta Casa Acima e deixando apenas três casas
        /// <summary>
        /// Usado para retornar o valor arredondado com duas casas decimais apenas, se existir 3 casasdecimais,
        /// ele incremental +1 na Segunda casa decimal e tira a Terceira casa decimal.
        /// Exemplo:
        /// 0.842 (antes)
        /// 0.85  (depois)
        /// </summary>        
        /// <param name="valor">Valor do Produto onde será aplicado o Desconto (ex: 100,00)</param>
        /// <returns>Retorna o valor arredondado</returns>
        public double arredondaQuartaCasaDecimalAcima(double valor)
        {
            string verficarValor = Convert.ToString(valor);

            int posicao = verficarValor.IndexOf(",");

            if (posicao == verficarValor.Length - 5)
            {
                int casasDecimais = 3;
                casasDecimais = (int)Math.Pow(10, casasDecimais);
                double valorFinalArredondado = Math.Ceiling(valor * casasDecimais) / casasDecimais;
                return valorFinalArredondado;
            }
            else
            {
                return valor;
            }
        }//fim método
        #endregion

        #region "Método para tratamento de arredondamento de números com três casas decimais"
        /// <summary>
        /// Método para tratamento de arredondamento de números com três casas decimais
        /// </summary>
        /// <param name="valor">Enviar valor a ser arredondado</param>
        /// <returns>Se der erro ao tentar arredondar retorna -1</returns>
        public string ajustarArredondamentoTresCasasDecimais(string valor)
        {
            try
            {
                valor = this.ajustarDuasCasasDecimais(valor);
                int posicao = valor.IndexOf(",");

                if (posicao != -1)
                {
                    this.novoValor = valor;
                    for (int indice = valor.Length - 1; indice > posicao + 3; indice--)
                    { // varrendo o valor e verificando valores entre 6 e 9 
                        int caracter = Convert.ToInt32(novoValor.Substring(indice, 1));
                        if (caracter > 5 && caracter < 10)
                        {
                            caracter = Convert.ToInt32(novoValor.Substring(indice - 1, 1));
                            this.novoValor = this.novoValor.Substring(0, indice - 1);
                            this.novoValor = this.novoValor + Convert.ToString(Convert.ToInt32(caracter) + 1);// somando
                        }
                        else
                        {
                            this.novoValor = this.novoValor.Substring(0, indice);
                        }
                    }
                    return novoValor;
                }
                else
                {
                    return valor;
                }
            }
            catch
            {
                return "-1";
            }
        }
        #endregion

        #region "Método para tratamento de arredondamento de números"
        /// <summary>
        /// Se der erro ao tentar arredondar retorna -1;
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public string ajustarArredondamento(string valor)
        {
            try
            {
                valor = this.ajustarDuasCasasDecimais(valor);
                int posicao = valor.IndexOf(",");

                if (posicao != -1)
                {
                    this.novoValor = valor;
                    for (int indice = valor.Length - 1; indice > posicao + 2; indice--)
                    { // varrendo o valor e verificando valores entre 6 e 9 
                        int caracter = Convert.ToInt32(novoValor.Substring(indice, 1));
                        if (caracter > 5 && caracter < 10)
                        {
                            caracter = Convert.ToInt32(novoValor.Substring(indice - 1, 1));
                            this.novoValor = this.novoValor.Substring(0, indice - 1);
                            this.novoValor = this.novoValor + Convert.ToString(Convert.ToInt32(caracter) + 1);// somando
                        }
                        else
                        {
                            this.novoValor = this.novoValor.Substring(0, indice);
                        }
                    }
                    return novoValor;
                }
                else
                {
                    return valor;
                }
            }
            catch{
                return "-1";
            }
        }
        #endregion

        #region "Método para tratamento de arredondamento de números QuatroCasas "
        /// <summary>
        /// Se der erro ao tentar arredondar retorna -1;
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public string ajustarArredondamentoQuatroCasasDecimais(string valor)
        {
            try
            {
                valor = this.ajustarDuasCasasDecimais(valor);
                int posicao = valor.IndexOf(",");

                if (posicao != -1)
                {
                    this.novoValor = valor;
                    for (int indice = valor.Length - 1; indice > posicao + 4; indice--)
                    { // varrendo o valor e verificando valores entre 6 e 9 
                        int caracter = Convert.ToInt32(novoValor.Substring(indice, 1));
                        if (caracter > 5 && caracter < 10)
                        {
                            caracter = Convert.ToInt32(novoValor.Substring(indice - 1, 1));
                            this.novoValor = this.novoValor.Substring(0, indice - 1);
                            this.novoValor = this.novoValor + Convert.ToString(Convert.ToInt32(caracter) + 1);// somando
                        }
                        else
                        {
                            this.novoValor = this.novoValor.Substring(0, indice);
                        }
                    }
                    return novoValor;
                }
                else
                {
                    return valor;
                }
            }
            catch
            {
                return "-1";
            }
        }
        #endregion

        #region Verifica se valor é um INTEIRO (ex: 10, 2, 5, 90, 900, 9000, etc)
        /// <summary>
        /// Verifica se o valor recebido é um inteiro, se não há letras e etc
        /// </summary>
        /// <param name="valor">Valor a ser testado - converter para string</param>
        /// <returns>Retorna True se for Inteiro, False se não for</returns>
        public bool verificaSeEInteiro(string valor)
        {
            ulong resultadoConversao;
            if (valor != "")
            {

                //  Tenta converter o texto na textbox para float
                if ((ulong.TryParse(valor.ToString(), out resultadoConversao) == true))
                {
                    try
                    {
                        ulong teste = Convert.ToUInt64(valor);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return false;
        }//fim rotina que verifica se é inteiro
        #endregion

        #region Verifica se valor é um DECIMAL (ex: 15,00 - 18,00)
        /// <summary>
        /// Verifica se o valor recebido é um inteiro, se não há letras e etc
        /// </summary>
        /// <param name="valor">Valor a ser testado - converter para string</param>
        /// <returns>Retorna True se for DECIMAL, False se não for DECIMAL</returns>
        public bool verificaSeEDecimal(string valor)
        {
            if (valor != "")
            {
                if (tentaConverterEmDecimal(valor) == true && verificaSeTemVirgula(valor) == true)
                {
                    return true;
                }


                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return false;
        }//fim rotina que verifica se é inteiro
        #endregion

        #region Verifica se valor é uma Data Válida (ex: 12/12/2008 ou 12/12/2008 11:11:11)
        /// <summary>
        /// Verifica se o valor recebido é uma Data Válida. P.S: Observe que a Data é Validada no esquema
        /// brasileiro (ex: 12/12/2008 ou 12/12/2008 11:11:11), ou seja? Datas com - ou ainda com mês no lugar
        /// de dias (como no formato americano) serão negadas no método, também datas sem 2 digitos (1/1/2008)
        /// serão negadas! Meses Inexistentes (mês 13) e dias (dia 32>) também serão negados! Somente será
        /// aceita data válida e no formato 11/11/2009 ou 11/11/2009 11:11:11
        /// </summary>
        /// <param name="valor">Valor a ser testado - converter para string</param>
        /// <returns>Retorna True se for uma Data Válida, False se não for</returns>
        public bool verificaSeEData(string valor)
        {
            bool retorno = true;
            if (valor.Length < 10)
            {
                retorno = false;
            }

            if (valor.Length > 18)
            {
                retorno = false;
            }

            try
            {
                int dias = Convert.ToInt32(valor.Substring(0, 2));
                if (dias > 31)
                {
                    retorno = false;
                }

                int mes = Convert.ToInt32(valor.Substring(3, 2));
                if (mes > 12)
                {
                    retorno = false;
                }

                if (valor.Substring(2, 1) != "/" && valor.Substring(5, 1) != "/")
                {
                    retorno = false;
                }


                DateTime data = Convert.ToDateTime(valor);
                int ano = data.Year;

                if (ano < 1800)
                {
                    retorno = false;
                }
                return retorno;
            }
            catch
            {
                return false;
            }
        }//fim rotina que verifica se é inteiro
        #endregion

        #region Verifica Decimal - Limita Decimal em Duas Casas Após a Virgula
        /// <summary>
        /// Percorre a STRING e limita as casas depois da virgula (ou Ponto) em duas casas decimais
        /// Por exemplo, se receber 22,222 retorna 22,22 - após isso converter pra decimal o retorno
        /// </summary>
        /// <param name="texto">String a ser verificada</param>
        /// <returns>Retorna String com casas decimais limitadas (2 Casas)</returns>

        public string limitaDecimalemDuasCasasAposVirgula(string texto)
        {
            int tam = texto.Length;
            string auxtxt = texto;
            string textoRetorno = "";


            for (int i = 0; i < tam; i++)
            {
                if (auxtxt.Substring(i, 1) == ".")
                {
                    auxtxt = auxtxt.Replace(".", ",");
                }

                textoRetorno = textoRetorno + auxtxt.Substring(i, 1);

                if (auxtxt.Substring(i, 1) == ",")
                {
                    i++;
                    for (int x = 0; x < 2; x++)
                    {
                        if (auxtxt.Substring(i, 1).Length < 2)
                        {
                            auxtxt = auxtxt + "0";
                        }
                        textoRetorno = textoRetorno + auxtxt.Substring(i, 1);
                        i++;
                    }

                    i = tam;
                }
            }
            return textoRetorno;
        }//fim VerificarDecimal
        #endregion

        #region Verifica Decimal - Limita Decimal em Quatro Casas Após a Virgula
        /// <summary>
        /// Percorre a STRING e limita as casas depois da virgula (ou Ponto) em duas casas decimais
        /// Por exemplo, se receber 22,222 retorna 22,22 - após isso converter pra decimal o retorno
        /// </summary>
        /// <param name="texto">String a ser verificada</param>
        /// <returns>Retorna String com casas decimais limitadas (2 Casas)</returns>

        public string limitaDecimalemQuatroCasasAposVirgula(string texto)
        {
            int tam = texto.Length;
            string auxtxt = texto;
            string textoRetorno = "";


            for (int i = 0; i < tam; i++)
            {
                if (auxtxt.Substring(i, 1) == ".")
                {
                    auxtxt = auxtxt.Replace(".", ",");
                }

                textoRetorno = textoRetorno + auxtxt.Substring(i, 1);

                if (auxtxt.Substring(i, 1) == ",")
                {
                    i++;
                    for (int x = 0; x < 2; x++)
                    {
                        if (auxtxt.Substring(i, 1).Length < 4)
                        {
                            auxtxt = auxtxt + "0";
                        }
                        textoRetorno = textoRetorno + auxtxt.Substring(i, 1);
                        i++;
                    }

                    i = tam;
                }
            }
            return textoRetorno;
        }//fim VerificarDecimal
        #endregion

        #region Método "Para ajustar string em 3 casas Decimais ***JP***"
        /// <summary>
        /// Este método retorna string ajustada!!!
        /// Irregularidades tratadas:
        /// - Quando valores sem zero na segunda casa decimal, Exemplo: 4,4 ele acrescenta um 0, ficando 4,400
        /// - Quando valores sem "," e consequentemente os "000", Exemplo: 1 ele acrescenta um .000, ficando 1.000
        /// </summary>
        /// <param name="valorAjuste">passar a string a ser ajustada</param>
        /// <returns>Retorna string ajustada</returns>
        public string ajustarTresCasasDecimais(string valorAjuste)
        {
            //this.casasDecimais = "";                   
            //for(int contador =0; contador<numeroCasasDecimais;contador++){ //Colocando o número
            //this.casasDecimais = "0";                                  //de casas decimais na variável
            //}
            int posicao = valorAjuste.IndexOf(",");
            if (posicao > 0)
            {
                if (posicao == valorAjuste.Length - 2)
                {
                    valorAjuste = valorAjuste + "00";
                }

                if (posicao == valorAjuste.Length - 3)
                {
                    valorAjuste = valorAjuste + "0";
                }
                return valorAjuste;
            }
            else
            {
                valorAjuste = valorAjuste + ",000";
                return valorAjuste;
            }

        }
        #endregion

        #region Método ajusta e arredonda três casas decimais para impressoras fiscais ECF
        /// <summary>
        /// Ajusta tres casas (por exemplo, 1,00 para 1,000) e depois arredonda tres casas
        /// inclusive arredondando para cima (usado nas impressoras Fiscais para arredondamento)
        /// </summary>
        /// <param name="valorAjuste">passar a string a ser ajustada</param>
        /// <returns>Retorna string ajustada</returns>
        public string ajustarEArredondaTresCasasDecimaisParaECF(string valorAjuste)
        {
            valorAjuste = arredondarTerceiraCasaAcima(valorAjuste);
            int posicao = valorAjuste.IndexOf(",");
            if (posicao > 0)
            {
                if (posicao == valorAjuste.Length - 2)
                {
                    valorAjuste = valorAjuste + "00";
                }

                if (posicao == valorAjuste.Length - 3)
                {
                    valorAjuste = valorAjuste + "0";
                }
                return valorAjuste;
            }
            else
            {
                valorAjuste = valorAjuste + ",000";
                return valorAjuste;
            }
        }
        #endregion

        #region Método ajusta e arredonda três casas decimais para impressoras fiscais ECF Sem Aumento Valor
        /// <summary>
        /// Ajusta tres casas (por exemplo, 1,00 para 1,000). Mantem o valor caso venha
        /// com valor 3 casas (por exemplo, 1,333 sera mantido 1,333).
        /// </summary>
        /// <param name="valorAjuste">passar a string a ser ajustada</param>
        /// <returns>Retorna string ajustada</returns>
        public string ajustarTresCasasDecimaisECF(string valorAjuste)
        {            
            int posicao = valorAjuste.IndexOf(",");
            if (posicao > 0)
            {
                if (posicao == valorAjuste.Length - 2)
                {
                    valorAjuste = valorAjuste + "00";
                }

                if (posicao == valorAjuste.Length - 3)
                {
                    valorAjuste = valorAjuste + "0";
                }
                return valorAjuste;
            }
            else
            {
                valorAjuste = valorAjuste + ",000";
                return valorAjuste;
            }
        }
        #endregion

        #region Método ajusta e arredonda duas casas decimais para impressoras fiscais ECF Sem Aumento Valor
        /// <summary>
        /// Ajusta tres casas (por exemplo, 1,00 para 1,000). Mantem o valor caso venha
        /// com valor 3 casas (por exemplo, 1,333 sera mantido 1,333).
        /// </summary>
        /// <param name="valorAjuste">passar a string a ser ajustada</param>
        /// <returns>Retorna string ajustada</returns>
        public string ajustarDuasCasasDecimaisSemAumentarValorParaECF(string valorAjuste)
        {            
            int posicao = valorAjuste.IndexOf(",");
            int posicaoFinal = valorAjuste.LastIndexOf(",");

            if (posicaoFinal == 2)
            {
                valorAjuste = valorAjuste + "0";
            }
            if (posicao > 0)
            {
                for (int i = posicao; i < valorAjuste.Length; i++)
                {
                    i++;
                    if (i > 3)
                    {
                        //valorAjuste = valorAjuste.Substring(0, posicao + 3);
                    }

                }
                return valorAjuste;
            }
            else
            {
                valorAjuste = valorAjuste + ",00";
                return valorAjuste;
            }
        }
        #endregion

        #region Método "Para ajustar string em 2 casas Decimais ***JP***"
        /// <summary>
        /// Este método retorna string ajustada!!!
        /// Irregularidades tratadas:
        /// - Quando valores sem zero na segunda casa decimal, Exemplo: 4,4 ele acrescenta um 0, ficando 4,400
        /// - Quando valores sem "," e consequentemente os "000", Exemplo: 1 ele acrescenta um .000, ficando 1.000
        /// </summary>
        /// <param name="valorAjuste">passar a string a ser ajustada</param>
        /// <returns>Retorna string ajustada</returns>
        public string ajustarDuasCasasDecimais(string valorAjuste)
        {
            if (valorAjuste == "")
            {
                valorAjuste = "0,00";
                return valorAjuste;
            }
            else
            {
                int posicao = valorAjuste.IndexOf(",");

                if (posicao > 0)
                {
                    if (posicao == valorAjuste.Length - 1)
                    {
                        valorAjuste = valorAjuste + "00";
                    }
                    if (posicao == valorAjuste.Length - 2)
                    {
                        valorAjuste = valorAjuste + "0";
                    }

                    return valorAjuste;
                }
                else
                {
                    valorAjuste = valorAjuste + ",00";
                    return valorAjuste;
                }
            }
        }
        #endregion

        #region Método "Para ajustar string em 2 casas Decimais ***JP***"
        /// <summary>
        /// Este método retorna string ajustada!!!
        /// Irregularidades tratadas:
        /// - Quando valores sem zero na segunda casa decimal, Exemplo: 4,4 ele acrescenta um 0, ficando 4,400
        /// - Quando valores sem "," e consequentemente os "000", Exemplo: 1 ele acrescenta um .000, ficando 1.000
        /// </summary>
        /// <param name="valorAjuste">passar a string a ser ajustada</param>
        /// <returns>Retorna string ajustada</returns>
        public string ajustarQuatroCasasDecimais(string valorAjuste)
        {
            if (valorAjuste == "")
            {
                valorAjuste = "0,00";
                return valorAjuste;
            }
            else
            {
                int posicao = valorAjuste.IndexOf(",");

                if (posicao > 0)
                {
                    if (posicao == valorAjuste.Length - 1)
                    {
                        valorAjuste = valorAjuste + "0000";
                    }
                    if (posicao == valorAjuste.Length - 2)
                    {
                        valorAjuste = valorAjuste + "000";
                    }

                    if (posicao == valorAjuste.Length - 3)
                    {
                        valorAjuste = valorAjuste + "00";
                    }

                    if (posicao == valorAjuste.Length - 4)
                    {
                        valorAjuste = valorAjuste + "0";
                    }


                    return valorAjuste;
                }
                else
                {
                    valorAjuste = valorAjuste + ",0000";
                    return valorAjuste;
                }
            }
        }
        #endregion

        #region Tenta Converter em Decimal (Método PRIVADO e apenas Interno)
        /// <summary>
        /// Método interno usado pelo Verifica se é Decimal, esse verifica se é possível converter
        /// Ou seja, caso haja uma letra ele já retorna falso
        /// </summary>
        /// <param name="valor">String</param>
        /// <returns></returns>
        private bool tentaConverterEmDecimal(string valor)
        {
            decimal resultadoConversao;
            if ((decimal.TryParse(valor.ToString(), out resultadoConversao) == true))
            {
                return true;
            }
            else
            {
                return false;
            }
        }//fim tentaConverterEmDecimal
        #endregion

        #region Verifica se Há virgula na String - O que caracteriza um Decimal (Método PRIVADO e apenas Interno)
        /// <summary>
        /// Método que verifica se há uma virgula na string e retorna True caso sim
        /// </summary>
        /// <param name="valor">String</param>
        /// <returns></returns>
        private bool verificaSeTemVirgula(string valor)
        {
            int tam = valor.Length;
            for (int i = 0; i < tam; i++)
            {
                if (valor.Substring(i, 1) == ",")
                {
                    return true;
                }
            }
            return false;


        }//fim verifica se tem virgula   
        #endregion

        #region Calcula Valor de Impostos
        /// <summary>
        /// Retorna o valor liquido de um imposto (produto - imposto)
        /// </summary>
        /// <param name="valorProduto">Valor do produto (ex: 50,00 para 50 reais)</param>
        /// <param name="valorImposto">Valor do Imposto (ex: 18,00 para 18% (icms))</param>
        /// <returns>Retorna Apenas valor do Imposto</returns>
        public decimal calcularValorImposto(decimal valorProduto, decimal valorImposto)
        {
            //variavel = (variavel * 10) / 100;
            decimal valorCalculado;
            valorCalculado = (valorProduto * valorImposto) / 100;
            return valorCalculado;            
        }//fim método
        #endregion

        #region Ajusta para 3 Casas Decimais (ECF)
        public string ajustarTresCasasDecimaisSemAumentarValorParaECF(string valorAjuste)
        {
            int posicao = valorAjuste.IndexOf(",");
            if (posicao > 0)
            {
                if (posicao == (valorAjuste.Length - 2))
                {
                    valorAjuste = valorAjuste + "00";
                }
                if (posicao == (valorAjuste.Length - 3))
                {
                    valorAjuste = valorAjuste + "0";
                }
                return valorAjuste;
            }
            valorAjuste = valorAjuste + ",000";
            return valorAjuste;
        }
        #endregion
    }//fim classe
}//fim namespace