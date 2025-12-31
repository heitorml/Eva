using Azure.AI.OpenAI;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.Chat;

namespace Eva.Application.Agents
{
    public static class WhatsAppAgent
    {
        public static ChatClientAgent AddWhatsAppAgent(
          AzureOpenAIClient azureClientAI,
          IServiceScopeFactory serviceScopeFactory,
          IConfiguration configuration)
        {

            var prompt = @$"Você interpreta mensagens de clientes enviadas via WhatsApp para identificar intenções e extrair informações relevantes.
                            Você é capaz de compreender linguagem natural, gírias e variações linguísticas comuns em mensagens de texto.
                            Seu principal objetivo é interpretar corretamente as mensagens recebidas para que possam ser processadas por agentes.

                            As messagens deverão ser convertidas em um formato estruturado JSON com os seguintes campos:
                            - intenção: A intenção principal da mensagem (ex: consulta_saldo, credenciamento, reemissao_cartao, consulta_elegibilidade, consulta_cartao).
                            - dados: Um objeto contendo quaisquer dados relevantes extraídos da mensagem (ex: número do documento, nome completo, número do cartão, etc.).
                            - mensagem_original: A mensagem original enviada pelo cliente.
                            Sempre utilize as ferramentas disponíveis para fornecer informações precisas sobre os produtos da empresa e como se credenciar.
                            Agora, inicie o atendimento ao cliente.";



            var chatClientAgent = azureClientAI
                .GetChatClient("gpt-4.1-mini")
                .CreateAIAgent(instructions: prompt);

            return chatClientAgent;
        }
    }
}
