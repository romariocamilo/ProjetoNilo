using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using ProjetoNilo.Modelos;
using TechTalk.SpecFlow;

namespace ProjetoNilo.Auxiliares
{
    [Binding]
    public class PesquisaAuxiliar
    {
        private ChromeDriver _chromeDriver;
        private List<string> _resultadoPesquisaPorProduto;
        private List<Produto> _resultadoPesquisaPorProdutoViaSubMenu;
        private string _resultadoMensagemDeErro;

        public PesquisaAuxiliar()
        {
            _resultadoPesquisaPorProduto = new List<string>();
            _resultadoPesquisaPorProdutoViaSubMenu = new List<Produto>();
        }

        //----------------------------------Setup e TearDown------------------------------

        //Setup
        [Given(@"que esteja na tela principal do sistema")]
        public void GivenQueEstejaNaTelaPrincipalDoSistema()
        {
            _chromeDriver = new ChromeDriver();

            _chromeDriver.Manage().Window.Maximize();
            _chromeDriver.Navigate().GoToUrl("http://practice.automationtesting.in/");
        }

        //Teardown
        [AfterScenario]
        public void ThenFecharOBrowser()
        {
            _chromeDriver.Close();
            _chromeDriver.Dispose();
        }

        //----------------------------------Métodos compartilhados------------------------------

        // Pesquisa por um produto
        [When(@"eu efetuar uma pesquisa pelo produto '([^']*)'")]
        public void WhenEuEfetuarUmaPesquisaPeloProduto(string hTML)
        {
            _chromeDriver.FindElement(By.Id("s")).SendKeys(hTML);
            _chromeDriver.FindElement(By.Id("s")).Submit();
        }

        //----------------------------------Testes------------------------------

        // Cenário: Pesquisa pelo produto HTML
        [Then(@"o validar se o sistema retornou o produto '([^']*)'")]
        public void ThenOValidarSeOSistemaRetornouOProduto(string produtoBlouseEsperado)
        {
            var listaDeProdutosDaConsulta = _chromeDriver.FindElements(By.ClassName("post-title"));

            foreach (var produtoDaConsulta in listaDeProdutosDaConsulta)
            {
                _resultadoPesquisaPorProduto.Add(produtoDaConsulta.Text);
            }

            string tituloProduto = _resultadoPesquisaPorProduto.FirstOrDefault(p => p.ToString() == produtoBlouseEsperado);

            if (tituloProduto != null)
                Assert.AreEqual(produtoBlouseEsperado, tituloProduto);
            else
                Assert.Fail($"Produto {produtoBlouseEsperado} não listado no retorno");
        }

        // Cenário: Pesquisa pelo produto inexistente
        [Then(@"o validar se o sistema retornou a mensagem '([^']*)'")]
        public void ThenOValidarSeOSistemaRetornouAMensagem(string mensageErroEsperada)
        {
            _resultadoMensagemDeErro = _chromeDriver.FindElement(By.ClassName("page-title")).Text;

            Assert.AreEqual(mensageErroEsperada, _resultadoMensagemDeErro);
        }

        // Cenário: Acessar produto via submenu HTML
        [When(@"eu clicar no submenu '([^']*)'")]
        public void WhenEuClicarNoSubmenu(string subMenu)
        {
            _chromeDriver.FindElement(By.Id("menu-item-40")).Click();

            var listaSubMenus = _chromeDriver.FindElements(By.CssSelector("a[href='http://practice.automationtesting.in/product-category/html/']"));

            foreach (var submenuLista in listaSubMenus)
            {
                if (submenuLista.Text == subMenu) submenuLista.Click();
            }
        }

        [Then(@"validar se o curso '([^']*)' apareceu na lista de cursos")]
        public void ThenValidarSeOCursoApareceuNaListaDeCursos(string cursoEsperado)
        {
            var listaDeProdutosDaConsulta = _chromeDriver.FindElements(By.TagName("h3"));

            foreach (var produtoDaConsulta in listaDeProdutosDaConsulta)
            {
                Produto produto = new Produto
                {
                    Titulo = produtoDaConsulta.Text.Split('\n')[0].Trim(),
                };

                _resultadoPesquisaPorProdutoViaSubMenu.Add(produto);
            }

            var produto2 = _resultadoPesquisaPorProdutoViaSubMenu.FirstOrDefault(p => p.Titulo == cursoEsperado);

            if (produto2 != null)
                Assert.AreEqual(cursoEsperado, produto2.Titulo);
            else
                Assert.Fail($"Produto {cursoEsperado} não encontrado");
        }
    }
}