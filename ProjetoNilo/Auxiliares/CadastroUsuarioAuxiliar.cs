using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ProjetoNilo.Uteis;
using TechTalk.SpecFlow;

namespace ProjetoNilo.Auxiliares
{
    [Binding]
    public class CadastroUsuarioAuxiliar
    {
        private string _emailUsuario;
        private string _nomeUsuario;

        public CadastroUsuarioAuxiliar()
        {
            _emailUsuario = GeradorDeDados.RetornaEmailValidoAleatorio();
            _nomeUsuario = _emailUsuario.Substring(0, 22);
        }

        [When(@"eu clicar no menu My Account")]
        public void WhenEuClicarNoMenuMyAccount()
        {
            var listaDeProdutosDaConsulta = PesquisaAuxiliar._chromeDriver.FindElement(By.Id("menu-item-50"));
            listaDeProdutosDaConsulta.Click();
        }



        [When(@"preencher o campo email Email address")]
        public void WhenPreencherOCampoEmail()
        {
            var listaDeProdutosDaConsulta = PesquisaAuxiliar._chromeDriver.FindElement(By.Id("reg_email"));
            listaDeProdutosDaConsulta.SendKeys(_emailUsuario);
        }


        [When(@"preencher o campo password Password")]
        public void WhenPreencherOCampoPassword()
        {
            var listaDeProdutosDaConsulta = PesquisaAuxiliar._chromeDriver.FindElement(By.Id("reg_password"));
            listaDeProdutosDaConsulta.SendKeys(_emailUsuario);
        }


        [When(@"clicar no botão REGISTER")]
        public void WhenClicarNoBotao()
        {
            var listaDeProdutosDaConsulta = PesquisaAuxiliar._chromeDriver.FindElement(By.Name("register"));
            listaDeProdutosDaConsulta.Click();
        }

        [Then(@"o sistema exibe a mensagem '([^']*)'")]
        public void ThenOSistemaExibeAMensagem(string p0)
        {
            var listaDeProdutosDaConsulta = PesquisaAuxiliar._chromeDriver.FindElements(By.TagName("p"));
            string fraseFormatada = string.Format(p0, _nomeUsuario);
            var fraseDeCadastro = listaDeProdutosDaConsulta.FirstOrDefault(p => p.Text == string.Format(p0, _nomeUsuario));

            if (fraseDeCadastro != null)
            {
                Assert.AreEqual(fraseFormatada, fraseDeCadastro.Text);
            }
            else
            {
                Assert.Fail($"A frase {string.Format(p0, _nomeUsuario)} não foi exibida na tela de login");
            }
        }
    }
}
