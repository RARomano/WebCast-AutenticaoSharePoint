# Webcast - Exemplos de Autenticação no SharePoint
O objetivo desse exemplo é demonstrar diferentes formas de autenticar no SharePoint. Abordarei, principalmente, exemplos que não dependem de usuário e senha do usuário.

## Azure Active Directory (Utilizando ADAL e OAuth)
Nesse exemplo, utilizarei o Azure AD para emitir um token OAuth e, com esse token, conectar no SharePoint.

A biblioteca ADAL (Active Directory Authentication Library) é utilizada para conseguir um **access token** para acesso a um ou mais recursos do tenant. Para conhecer o projeto, clique nesse link: [Azure AD Authentication Library for .NET](https://msdn.microsoft.com/en-us/library/azure/jj573266.aspx).

Você pode fazer todas as chamadas aos endpoints manualmente, mas essa biblioteca facilita bastante o trabalho. O Andrew Connell tem vários artigos que explicam como fazer esse processo passo-a-passo: [Looking at the Different OAuth2 Flows Supported in AzureAD for Office 365 APIs](http://www.andrewconnell.com/blog/looking-at-the-different-oauth2-flows-supported-in-azuread-for-office-365-apis) ou veja o artigo da Microsoft falando sobre os tipos de autenticação do Azure AD [Authentication Scenarios for Azure AD](https://azure.microsoft.com/en-us/documentation/articles/active-directory-authentication-scenarios/).

### Preparando o ambiente
1. Crie Uma Solução do Tipo ASP.NET MVC e copie a URL que essa aplicação será iniciada. (Nesse exemplo: http://localhost:58689/)
![MVC](https://cloud.githubusercontent.com/assets/12012898/9564817/3aafcaa6-4e88-11e5-996f-d5ab3c0dd07e.png)
1. Abra o [portal de gerenciamento do Azure](https://manage.windowsazure.com/) e clique em Active Directory.
1. Escolha o domínio
![Domínio](https://cloud.githubusercontent.com/assets/12012898/9564863/dad31a90-4e8a-11e5-8708-24bc945ae095.png)
1. No menu superior, escolha **Aplicativos** e depois clique em **Adicionar**.
![Aplicativos](https://cloud.githubusercontent.com/assets/12012898/9564869/45595046-4e8b-11e5-9a83-84a34806ca5a.png)
1. Clique em **Adicionar uma aplicação que minha empresa esteja desenvolvendo**
![Aplicativo](https://cloud.githubusercontent.com/assets/12012898/9564874/8c06200a-4e8b-11e5-8cb2-f134fcf9dbfa.png)
1. Dê um nome para o seu aplicativo e escolha a opção Web API
![Nome](https://cloud.githubusercontent.com/assets/12012898/9564881/e8c1b08e-4e8b-11e5-80b6-cd0b0b179bad.png)
1. Digite uma URL para logon da aplicação (coloque a URL criada na primeita etapa) e uma URL que identificará a sua aplicação posteriormente (também poderá ser alterado depois).
![Identificação](https://cloud.githubusercontent.com/assets/12012898/9564880/e895684e-4e8b-11e5-9d4a-5de5bf65dd10.png)
1. Clique em configurar
![Configurar]("https://cloud.githubusercontent.com/assets/12012898/9565185/33786270-4e97-11e5-9f31-3c98f166f220.png)
1. Clique em URL de resposta e adicione a url http://localhost:58689/home/token. Em chaves, escolha 1 ano e em permissões, clique em **Adicionar Aplicativo** e escolha Office 365 SharePoint Online. Delegue as permissões necessárias para a sua aplicação e clique em salvar. Anote os valores de Client ID e Client Secret gerados.
![Configurações adicionais](https://cloud.githubusercontent.com/assets/12012898/9565194/56936778-4e97-11e5-8504-2aaa3e921fea.png)
1. Clique em Exibir pontos de entrada e copie a primeira URL.
![Pontos de Entrada](https://cloud.githubusercontent.com/assets/12012898/9565207/bdc359b2-4e97-11e5-8a48-78ade248fa84.png)
1. Coloque os valores obtidos nas etapas anteriores no código que está em **Controllers/HomeController**
```C#
		/// Url copiada do pontos de entrada, somente até o guid após login.microsoftonline.com
		/// exemplo: https://login.microsoftonline.com/B1EC3377-86A0-43EE-8305-FE1B1B3AE270
		string authority = "";

		/// Url do Tenant que deseja acessar
		string resource = "";

		/// Client ID
		string clientId = "";

		/// Client Secret
		string clientSecret = "";

		/// Url de Redirect
		string redirectUrl = "http://localhost:58689/Home/Token";
```


