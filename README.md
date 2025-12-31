# 🛒 EVA - IA - Assitente de atendimento e credenciamento de vendas 

O projeto tem como efetuar atendimento por meio um assistente inteligente orquestrado por um Agente de IA que usa um LLM hospedado no Github model.
Sua função é auxiliar no credenciamento de vendas, respondendo perguntas frequentes e guiando os usuários através do processo de venda.

Não se trata de um chatbot comum, mas sim de um sistema avançado que utiliza técnicas de Processamento de Linguagem Natural (NLP) para entender e responder às necessidades dos usuários de forma eficaz. 

Esta é uma abordagem que conbina a integração com whatsapp business API para comunicação, um modelo de linguagem grande (LLM) para compreensão e geração de respostas, e um agente de IA para gerenciar o fluxo de interação.

Microserviços criados:

- `Eva.WhatsApp` - Serviço responsável pela integração com a API do WhatsApp Business, gerenciando o envio e recebimento de mensagens.
- `Eva.Worker` - Serviço de processamento em segundo plano que lida com tarefas assíncronas, como o processamento de mensagens recebidas e o envio de respostas.


Abaixo segue os detalhes da implementação 


---
### 🔄 EVA Agente de IA

O agente de IA é responsável por gerenciar o fluxo de interação com o usuário. Ele utiliza um modelo de linguagem grande (LLM) hospedado no GitHub Model para compreender as mensagens recebidas e gerar respostas apropriadas.

- Também é responsável por guiar o usuário através do processo de credenciamento de vendas, fazendo perguntas relevantes e fornecendo informações úteis.
- Interage com os outros microserviços para enviar e receber mensagens via WhatsApp Business API.
- Decide quando iniciar, finalizar ou abandonar uma conversa com o usuário, gerenciando o estado da interação de forma eficiente.
- Também pode solicitar o credenciamento de vendas quando apropriado, garantindo que o processo seja conduzido de maneira fluida e eficaz.

### 🔄 Libs Mágicas

- **GitHub Model**: Utilizado para hospedagem do modelo de linguagem grande (LLM) que compreende e gera respostas.
- **OpenAI GPT-4**: Utilizado para complementar o modelo de linguagem, forne
- **Microsoft.Extensions.AI.OpenAI**: Framework para construção de agentes de IA, facilitando a integração com o LLM e a gestão do fluxo de interação.
- **Microsoft.Agents.AI**: 

### 🔄 Eventos

- `AccreditationRequested` 
- `ConversationAbandoned` 
- `ConversationFinalized` 
- `ConversationStarted` 

---

### Sobre a mensageria 

Os eventos são transmitos através do messageBroker RabbitMq, a implementação utiliza o MasstransitV8 (https://masstransit.io/quick-starts).
- **MassTransitV8**: biblioteca de mensageria para comunicação entre microsserviços
- **ServiceBus**: broker de mensagens usado como transporte

---
### Sobre a aborgem arquitetural

Para esta solução foi escolhida a Clean Architecture que traz os benefícios

- Separação de responsabilidades em 4 camadas principais
- Independência da infraestrutura
- Inversão de dependência via interfaces
- Facilidade para testes unitários e mocks

## 📁 Projetos

| Projeto                       | Camada          | Descrição                                                        |
|-------------------------------|-----------------|------------------------------------------------------------------|
| `Eva.WhatsApp`                | Presentation    | API de pedidos para receber pedidos e enviar para o fornecedor   |
| `Eva.Worker`                  | Presentation    | API para CRUD básico des revendas                                |
| `Application.*`               | 2-Application   | Casos de uso, interfaces                                         |
| `Domain.*`                    | 3-Domain        | Entidades, enums, regras puras                                   |
| `Infrastructure.*`            | 4-Infrastructure| Repositórios, contextos, MongDb                                  |
| `*.Tests`                     | Tests           | Testes unitários com xUnit e Moq                                 |

---

## 🛠️ Tecnologias e Padrões Utilizados

### 🧼 Clean Architecture
- Separação de responsabilidades em 4 camadas principais
- Independência da infraestrutura
- Inversão de dependência via interfaces
- Facilidade para testes unitários e mocks

### 🟣 .NET 10
- Última versão LTS da plataforma .NET
- Alto desempenho e suporte a APIs modernas


### 🔁 Worker Service
- Utilizado para processamentos em background com o `Eva.Worker`
- Ideal para filas, cron jobs ou mensageria

### 🧼 Resilience 
- Polly - Biblioteca de politicas de resiliencia em chamadas Http
- Politicas de retentativas exponencial
- Circuit Breaker
- Timeout

### 📦 Refit 
- Para comunicação entre a `Eva.Worker` e `WhatsApp.Api`
- Abstrai toda a implementação do HttpClient 

### ✔️ Fail Fast Validation 
- Utiliza a biblioteca FluentValidation para efetuar validações de requisições

### 🗂️ Repositorio NoSQL 
- Utilização do MongoDb para armazenamento não relacional dos documentos  

### 📦 MassTransit + ServiceBus
- **MassTransit**: biblioteca de mensageria para comunicação entre microsserviços
- **ServiceBus**: broker de mensagens usado como transporte

### 🧪 xUnit + Moq
- **xUnit**: framework de testes unitários
- **Moq**: criação de mocks de dependências para testes

### 📈 Monitoramento Jeager
- a definir

### 📊 Coverlet + Cobertura + Azure Pipelines
- **Coverlet**: biblioteca para análise de cobertura de código
- Geração de relatórios no formato `cobertura.xml` (compatível com CI)

### 📈 GitHub Actions (Em desenvolvimento)
- CI/CD automatizado
- Build, testes, publicação e upload de artefatos
- Geração de resultados de testes e cobertura de código


---

## ✅ Pré-requisitos

Certifique-se de que os seguintes softwares estejam instalados em sua máquina:

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Docker](https://www.docker.com/) 
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/)
- [Postman]

---
  

## 👨‍💻 Autor

Desenvolvido com 💻 por **Heitor Machado**

- 📧 Email: machado.loureiro@gmail.com  
- 💼 LinkedIn: [linkedin.com/in/heitor-machado](https://www.linkedin.com/in/heitor-machado-45725982/)  
- 🐙 GitHub: [github.com/heitorml](https://github.com/heitorml)  

Sinta-se à vontade para contribuir, abrir issues ou dar uma ⭐ no repositório!




