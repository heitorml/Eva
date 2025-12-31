using Azure.AI.OpenAI;
using Eva.Application.Tools;
using Eva.Infrastructure.InternalSerices.Accreditation;
using Eva.Infrastructure.InternalSerices.Balance;
using Eva.Infrastructure.InternalSerices.Eligibility;
using Eva.Infrastructure.InternalSerices.HTTP;
using Eva.Infrastructure.InternalSerices.Reissue;
using Eva.Infrastructure.InternalSerices.SearchCard;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.Chat;
using System.Net;

namespace Eva.Application.Agents
{
    public static class EvaAgent
    {
        public static ChatClientAgent AddAgentEva(
            AzureOpenAIClient azureClientAI,
            IServiceScopeFactory serviceScopeFactory,
            IConfiguration configuration)
        {
            var faq = @"";

            var prompt = @$"
                    Você é um agente inteligente e prestativo especializado em efetuar atendimentos á clientes da Empresa. 
                    Seu principal objetivo é auxiliar os clientes com suas dúvidas e necessidades relacionadas aos produtos e serviços oferecidos pela empresa, garantindo uma experiência positiva e eficiente.
                    Sempre utilize as ferramentas disponíveis para fornecer informações precisas sobre os produtos da empresa e como se credenciar.
                    Quando o cliente entrar em contato, cumprimente-o de forma cordial, se apresente e pergunte como você pode ajudar listando as opções disponíveis.

                    Seja direto e claro nas respostas usando as ferramentas disponíbilizadas para enriquecer suas respostas.
                    Nunca invente informações ou dados que não estejam disponíveis nas ferramentas.
                    Mantenha a confidencialidade dos dados dos clientes e siga as políticas de privacidade da empresa.
                    Utilize o histórico da conversa  para manter o contexto do atendimento.

                    Ferramentas disponíveis:    
                    - consultaCartao.GetCardAync: Obtém dados do cartão, contrato e usuário. O cartão é um produto.
                    - consultaElegibilidade.GetEligibilityAsync: Faz a consulta de elegibilidade através do número do documento do cliente.
                    - consultaSaldo.GetBalanceAccountAsync: Faz a consulta de saldo do cartão através do número do documento do cliente (esta ferramenta não necessita de consultar de elegiilidade do cliente).
                    - credenciamento.AccreditationAsync: Faz o credenciamento do cliente (esta ferramenta só poderá ser usada após o sucesso da elegiilidade do cliente).
                    - reemissao.RequestReissueAsync: Faz a confirmação da solicitação de Reemissão de um cartão (esta ferramenta só poderá ser usada após o sucesso da elegiilidade do cliente).

                    Sempre que possivel responda com base nos dados obtidos das ferramentas.
                    Sempre que possível, utilize as ferramentas para obter informações precisas antes de responder ao cliente.
                    Sempre que uma ferramenta for ultilizada, explique ao cliente o que foi feito e os resultados obtidos.
    
                    Sempre que possível, utilize as informações do FAQ para enriquecer suas respostas.
                    FAQ: {faq}

                    Lembre-se de agir sempre em conformidade com as políticas da empresa e as leis vigentes.
                    Seja um agente confiável e eficiente, focado em proporcionar a melhor experiência possível para os clientes.
                    
                    Retire campos desnecessários para o cliente.

                    Responda apenas em Português Brasileiro e com no maximo 3 parágrafos curtos.

                    Em caso de falha, forneça uma mensagem clara em Português Brasileiro com orientações sobre os próximos passos que o cliente deve seguir com no máximo 1 parágrafo curto.

                    Agora, inicie o atendimento ao cliente.";

            ServiceCollection services = new();
            
            InjectionDependecy(configuration, services);
            
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            SearchCardsTool searchCardsTool = serviceProvider.GetRequiredService<SearchCardsTool>();
            ElegibilityTool elegibilityTool = serviceProvider.GetRequiredService<ElegibilityTool>();
            BalanceAccountTool balanceAccountTool = serviceProvider.GetRequiredService<BalanceAccountTool>();
            AccreditationTool accreditationTool = serviceProvider.GetRequiredService<AccreditationTool>();
            ReissueTool reissueTool = serviceProvider.GetRequiredService<ReissueTool>();

            var chatClientAgent = azureClientAI
                .GetChatClient("gpt-4.1-mini")
                .CreateAIAgent(instructions: prompt,
                    tools: [
                        AIFunctionFactory.Create(searchCardsTool.GetCardAync, "consultaCartao"),
                        AIFunctionFactory.Create(elegibilityTool.GetEligibilityAsync, "consultaElegibilidade"),
                        AIFunctionFactory.Create(balanceAccountTool.GetBalanceAccountAsync, "ConsultaSaldo"),
                        AIFunctionFactory.Create(accreditationTool.AccreditationAsync, "credenciamento"),
                        AIFunctionFactory.Create(reissueTool.RequestReissueAsync, "reemissaoreemissao")
                   ],
                   services: serviceProvider);

            return chatClientAgent;


        }

        private static void InjectionDependecy(IConfiguration configuration, ServiceCollection services)
        {
            services.AddHttpClient<IHttpService, HttpService>()
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    var handler = new HttpClientHandler
                    {
                        UseCookies = true,
                        CookieContainer = new CookieContainer()
                    };
                    return handler;
                });

            services.AddSingleton<IConfiguration>(configuration);

            services.AddScoped<SearchCardsTool>();
            services.AddScoped<ElegibilityTool>();
            services.AddScoped<BalanceAccountTool>();
            services.AddScoped<AccreditationTool>();
            services.AddScoped<ReissueTool>();


            services.AddScoped<ISearchCardsService, SearchCardsService>();
            services.AddScoped<IReissueService, ReissueService>();
            services.AddScoped<IBalanceAccountService, BalanceAccountService>();
            services.AddScoped<IEligibilityService, EligibilityService>();
            services.AddScoped<IAccreditationService, AccreditationService>();
        }
    }
}
