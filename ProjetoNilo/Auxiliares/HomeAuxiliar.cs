using System.Collections.Generic;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using ProjetoNilo.Modelos;

namespace ProjetoNilo.Auxiliares
{
    public class HomeAuxiliar
    {
        private ChromeDriver _chromeDriver;
        public List<string> resultadoPesquisaPorProduto { get; private set; }
        public List<Produto> resultadoPesquisaPorProdutoViaSubMenu { get; private set; }
        public string resultadoMensagemDeErro { get; private set; }


        //[SetUp]
        //public void Setup()
        //{
        //   _chromeDriver = new ChromeDriver();
        //}

        public HomeAuxiliar()
        {
            resultadoPesquisaPorProduto = new List<string>();
            resultadoPesquisaPorProdutoViaSubMenu = new List<Produto>();
        }

        public void PesquisaPorPordutoHtml(string produto)
        {
            _chromeDriver = new ChromeDriver();

            _chromeDriver.Manage().Window.Maximize();
            _chromeDriver.Navigate().GoToUrl("http://practice.automationtesting.in/");
            _chromeDriver.FindElement(By.Id("s")).SendKeys(produto);
            _chromeDriver.FindElement(By.Id("s")).Submit();

            var listaDeProdutosDaConsulta = _chromeDriver.FindElements(By.ClassName("post-title"));

            foreach(var produtoDaConsulta in listaDeProdutosDaConsulta)
            {
                resultadoPesquisaPorProduto.Add(produtoDaConsulta.Text);
            }

            _chromeDriver.Close();
            _chromeDriver.Dispose();
        }

        public void PesquisaPorPordutoInexistente(string produto)
        {
            _chromeDriver = new ChromeDriver();

            _chromeDriver.Manage().Window.Maximize();
            _chromeDriver.Navigate().GoToUrl("http://practice.automationtesting.in/");
            _chromeDriver.FindElement(By.Id("s")).SendKeys(produto);
            _chromeDriver.FindElement(By.Id("s")).Submit();

            resultadoMensagemDeErro = _chromeDriver.FindElement(By.ClassName("page-title")).Text;

            _chromeDriver.Close();
            _chromeDriver.Dispose();
        }

        public void AcessarPordutoViaSubMenuHtml(string subMenu)
        {
            _chromeDriver = new ChromeDriver();

            _chromeDriver.Manage().Window.Maximize();
            _chromeDriver.Manage().Window.Maximize();
            _chromeDriver.Navigate().GoToUrl("http://practice.automationtesting.in/");
            _chromeDriver.FindElement(By.Id("menu-item-40")).Click();

            var listaSubMenus = _chromeDriver.FindElements(By.CssSelector("a[href='http://practice.automationtesting.in/product-category/html/']"));

            foreach (var submenuLista in listaSubMenus)
            {
                if (submenuLista.Text == subMenu) submenuLista.Click();
            }

            var listaDeProdutosDaConsulta = _chromeDriver.FindElements(By.TagName("h3"));

            foreach (var produtoDaConsulta in listaDeProdutosDaConsulta)
            {
                Produto produto = new Produto
                {
                    Titulo = produtoDaConsulta.Text.Split('\n')[0].Trim(),
                };

                resultadoPesquisaPorProdutoViaSubMenu.Add(produto);
            }

            _chromeDriver.Close();
            _chromeDriver.Dispose();
        }

        //[TearDown]
        //public void Mata()
        //{
        //    _chromeDriver.Close();
        //    _chromeDriver.Dispose();
        //}
    }
}