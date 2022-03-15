using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using TechTalk.SpecFlow;

using ProjetoNilo.Modelos;

namespace ProjetoNilo.Auxiliares
{
    [Binding]
    public class PesquisaAuxiliar
    {
        public static ChromeDriver chromeDriver { get; private set; }
        public static WebDriverWait esperaImplicita;

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
            chromeDriver = new ChromeDriver();
            esperaImplicita = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(30));

            chromeDriver.Manage().Window.Maximize();
            chromeDriver.Navigate().GoToUrl("http://practice.automationtesting.in/");
        }

        //Teardown
        [AfterScenario]
        public void ThenFecharOBrowser()
        {
            chromeDriver.Close();
            chromeDriver.Dispose();
        }

        //----------------------------------Métodos compartilhados------------------------------

        // Pesquisa por um produto
        [When(@"eu efetuar uma pesquisa pelo produto '([^']*)'")]
        public void WhenEuEfetuarUmaPesquisaPeloProduto(string hTML)
        {
            esperaImplicita.Until(condition => condition.FindElement(By.Id("s"))).SendKeys(hTML);
            esperaImplicita.Until(condition => condition.FindElement(By.Id("s"))).Submit();
        }

        //----------------------------------Testes------------------------------

        // Cenário: Pesquisa pelo produto HTML
        [Then(@"o validar se o sistema retornou o produto '([^']*)'")]
        public void ThenOValidarSeOSistemaRetornouOProduto(string produtoBlouseEsperado)
        {
            var listaDeProdutosDaConsulta = esperaImplicita.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.ClassName("post-title")));

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
            _resultadoMensagemDeErro = esperaImplicita.Until(condition => condition.FindElement(By.ClassName("page-title"))).Text;

            Assert.AreEqual(mensageErroEsperada, _resultadoMensagemDeErro);
        }

        // Cenário: Acessar produto via submenu HTML
        [When(@"eu clicar no submenu '([^']*)'")]
        public void WhenEuClicarNoSubmenu(string subMenu)
        {
            chromeDriver.FindElement(By.Id("menu-item-40")).Click();

            var listaSubMenus = esperaImplicita.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector("a[href='http://practice.automationtesting.in/product-category/html/']")));
            
            foreach (var submenuLista in listaSubMenus)
            {
                if (submenuLista.Text == subMenu) submenuLista.Click();
            }
        }

        [Then(@"validar se o curso '([^']*)' apareceu na lista de cursos")]
        public void ThenValidarSeOCursoApareceuNaListaDeCursos(string cursoEsperado)
        {
            var listaDeProdutosDaConsulta = esperaImplicita.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.TagName("h3")));


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