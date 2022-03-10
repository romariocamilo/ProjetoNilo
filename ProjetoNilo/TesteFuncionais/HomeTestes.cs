using System.Linq;

using NUnit.Framework;

using ProjetoNilo.Auxiliares;

namespace ProjetoNilo.TesteFuncionais
{
    public class HomeTestes
    {
        private HomeAuxiliar _homeAuxiliar;

        public HomeTestes() => _homeAuxiliar = new HomeAuxiliar();


        [TestCase("HTML", "Blouse", TestName = "Pesquisa pelo produto HTML")]
        public void PesquisaPorPordutoHtmlTest(string produtoPesquisado, string produtoProcurado)
        {
            _homeAuxiliar.PesquisaPorPordutoHtml(produtoPesquisado);

            string tituloProduto = _homeAuxiliar.resultadoPesquisaPorProduto.FirstOrDefault(p => p.ToString() == produtoProcurado);

            Assert.True(tituloProduto == null, $"Pesquisa pelo produto {produtoPesquisado}");
        }

        [TestCase("itemNãoExistente", "SEARCH RESULTS FOR: ITEMNÃOEXISTENTE", TestName = "Pesquisa pelo produto inexistente")]
        public void PesquisaPorPordutoInexistenteTest(string produtoPesquisado, string mensagemDeErro)
        {
            _homeAuxiliar.PesquisaPorPordutoInexistente(produtoPesquisado);

            Assert.AreEqual(mensagemDeErro, _homeAuxiliar.resultadoMensagemDeErro);
        }

        [TestCase("HTML", "Thinking in HTML", TestName = "Acessar produto via submenu HTML")]
        public void AcessarPordutoViaSubMenuHtmlTest(string submenu, string produtoEncontrado)
        {
            _homeAuxiliar.AcessarPordutoViaSubMenuHtml(submenu);

            var produto = _homeAuxiliar.resultadoPesquisaPorProdutoViaSubMenu.FirstOrDefault(p => p.Titulo == produtoEncontrado);

            Assert.AreEqual(produtoEncontrado, produto.Titulo);
        }
    }
}
