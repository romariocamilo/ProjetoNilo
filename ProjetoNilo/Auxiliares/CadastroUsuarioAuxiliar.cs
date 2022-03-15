using System.Linq;

using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

using ProjetoNilo.Uteis;
using SeleniumExtras.WaitHelpers;

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
            var submenuMinhaConta  = PesquisaAuxiliar.esperaImplicita.Until(condition => condition.FindElement(By.Id("menu-item-50")));
            submenuMinhaConta.Click();
        }



        [When(@"preencher o campo email Email address")]
        public void WhenPreencherOCampoEmail()
        {
            var cpEmail = PesquisaAuxiliar.esperaImplicita.Until(condition => condition.FindElement(By.Id("reg_email")));
            cpEmail.SendKeys(_emailUsuario);
        }


        [When(@"preencher o campo password Password")]
        public void WhenPreencherOCampoPassword()
        {
            var cpSenha = PesquisaAuxiliar.esperaImplicita.Until(condition => condition.FindElement(By.Id("reg_password")));

            cpSenha.SendKeys(_emailUsuario);
        }


        [When(@"clicar no botão REGISTER")]
        public void WhenClicarNoBotao()
        {
            var btnRegistrar = PesquisaAuxiliar.esperaImplicita.Until(condition => condition.FindElement(By.Name("register")));

            btnRegistrar.Click();
        }

        [Then(@"o sistema exibe a mensagem '([^']*)'")]
        public void ThenOSistemaExibeAMensagem(string p0)
        {
            var listaParagrafos = PesquisaAuxiliar.esperaImplicita.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.TagName("p")));

            string fraseFormatada = string.Format(p0, _nomeUsuario);
            var elementoParagrafoObtido = listaParagrafos.FirstOrDefault(p => p.Text == string.Format(p0, _nomeUsuario));

            if (elementoParagrafoObtido != null)
            {
                Assert.AreEqual(fraseFormatada, elementoParagrafoObtido.Text);
            }
            else
            {
                Assert.Fail($"A frase {string.Format(p0, _nomeUsuario)} não foi exibida na tela de login");
            }
        }
    }
}
