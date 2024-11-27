using System.Text.RegularExpressions;

namespace CrossCutting.Utils
{
    public static class CNPJValidator
    {
        /// <summary>
        /// Verifica se um CNPJ é válido.
        /// </summary>
        /// <param name="cnpj">CNPJ a ser validado.</param>
        /// <returns>True se for válido, False caso contrário.</returns>
        public static bool IsValid(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            // Remove caracteres não numéricos
            cnpj = Regex.Replace(cnpj, "[^0-9]", "");

            // O CNPJ deve ter 14 dígitos
            if (cnpj.Length != 14)
                return false;

            // Verifica se todos os dígitos são iguais (ex: 11111111111111 é inválido)
            if (new string(cnpj[0], 14) == cnpj)
                return false;

            // Cálculo do primeiro dígito verificador
            var peso1 = new int[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var soma1 = 0;
            for (int i = 0; i < 12; i++)
                soma1 += (cnpj[i] - '0') * peso1[i];

            var resto1 = soma1 % 11;
            var digito1 = resto1 < 2 ? 0 : 11 - resto1;

            // Verifica o primeiro dígito
            if (digito1 != cnpj[12] - '0')
                return false;

            // Cálculo do segundo dígito verificador
            var peso2 = new int[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var soma2 = 0;
            for (int i = 0; i < 13; i++)
                soma2 += (cnpj[i] - '0') * peso2[i];

            var resto2 = soma2 % 11;
            var digito2 = resto2 < 2 ? 0 : 11 - resto2;

            // Verifica o segundo dígito
            if (digito2 != cnpj[13] - '0')
                return false;

            return true;
        }
    }
}
