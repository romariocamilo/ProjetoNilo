ProjetoNilo

Para execução do projeto siga os passos abaixo:

Passo 1:
Instale o dotnetcore de acordo com a versão local do S.O.
Link para download: https://dotnet.microsoft.com/en-us/download/dotnet/6.0
![image](https://user-images.githubusercontent.com/40321935/158284097-dd717450-08c9-47a6-b57e-a051e2e932fc.png)


Passo 2:
Crie no Disco C(Raiz) uma pasta com nome "Nilo";
Clone o repositório do git dentro da pasta criada na etapa anterior
Link do repositório: https://github.com/romariocamilo/ProjetoNilo

Exemplo comando git:
git clone https://github.com/romariocamilo/ProjetoNilo.git

![image](https://user-images.githubusercontent.com/40321935/158284151-4d603987-0b43-4730-8a16-09780419163d.png)

Passo 3:
Acesse a pasta "net6.0" localizada no caminho "C:\Nilo\ProjetoNilo\ProjetoNilo\bin\Debug\net6.0" via CMD atráves do comando abaixo (Windows)
Comando: cd C:\Nilo\ProjetoNilo\ProjetoNilo\bin\Debug\net6.0

Passo 4:
Já na pasta "net6.0" execute o comando abaixo via CMD para execução dos testes
Comando: dotnet test ProjetoNilo.dll --logger "console;verbosity=detailed"

Exemplo de saída:
![image](https://user-images.githubusercontent.com/40321935/158284037-1e4b1b02-41dd-486c-a8ed-d02f94f6fefa.png)
