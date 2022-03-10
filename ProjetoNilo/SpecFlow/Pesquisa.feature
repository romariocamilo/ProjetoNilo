#language: pt-br

Funcionalidade: PesquisaPorProduto

Cenario: Pesquisa pelo produto Blouse
	Dado que esteja na tela principal do sistema
	Quando eu efetuar uma pesquisa pelo produto 'HTML'
	Entao o validar se o sistema retornou o produto 'Blouse'

	Cenario: Pesquisa pelo produto inexistente
	Dado que esteja na tela principal do sistema
	Quando eu efetuar uma pesquisa pelo produto 'itemNãoExistente'
	Entao o validar se o sistema retornou a mensagem 'SEARCH RESULTS FOR: ITEMNÃOEXISTENTE'

	Cenario: Acessar produto Thinking in HTML via submenu HTML
	Dado que esteja na tela principal do sistema
	Quando eu clicar no submenu 'HTML'
	Entao validar se o curso 'Thinking in HTML' apareceu na lista de cursos