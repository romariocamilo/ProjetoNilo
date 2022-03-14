#language: pt-br

Funcionalidade: CadastroUsuario

Cenario: Cadastro de novo usuario
	Dado que esteja na tela principal do sistema
	Quando eu clicar no menu My Account
	E preencher o campo email Email address
	E preencher o campo password Password
	E clicar no botão REGISTER
	Entao o sistema exibe a mensagem 'Hello {0} (not {0}? Sign out)'