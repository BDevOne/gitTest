using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gitTeste.Models.Enums
{
    public class Enumerados
    {
        [Flags]
        public enum PermissoesUsuario
        {
            Nenhuma = 0,
            Editar = 1,
            Excluir = 2,
            Procurar = 3, 
            EditarProprioUsuario = 4 
        }

        public enum UsuarioTipo
        {
            Nenhum = 0,
            Administrador = 1,
            Master = 2,
            Operador = 3,
            Externo = 4,
        }

        public enum CampoPropriedade
        {
            NomeCampo = 1,
            IdadeCampo = 2,
            TipoUsuarioCampo = 3,
            DocumentoCampo = 4 
        }

        public enum OpcoesEscolhaTela
        {
            CadastrarUsuario = 1,
            LogarUsuario = 2,
        }

        public enum DocumentoEscolha
        {
            Cpf = 1,
            Rg = 2,
        }

    }
}