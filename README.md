### 29/08 - Correção - Erro multíplos cadastros de usuários.

Foi verificado e corrigido um erro de cadastro de usuário, onde ao informar um valor nulo, nenhuma mensagem era apresentada para indicar o motivo pelo qual não era possível prosseguir com o novo cadastro de usuário.

Foi adicionada uma mensagem mais amigável "Operação falhou, informe um valor válido!!". 

#

### 05/09 - Correção Validação da Idade.

Agora, ao tentar cadastrar a idade de um usuário menor de 18 anos, será exibida a mensagem: **"Não foi possível cadastrar idade. Para prosseguir com o cadastro, informe uma idade maior que 17 anos!"** Em seguida, será solicitado que você informe a idade de um usuário que tenha 18 anos ou mais.