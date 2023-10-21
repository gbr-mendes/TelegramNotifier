using Microsoft.Extensions.Options;
using TelegramNotifier.Domain.Interfaces;
using TelegramNotifier.Domain.ValueObjects;

namespace TelegramNotifier.Domain.Services
{
    public class TelegramMessageService : IMessageService
    {


        private readonly TelegramAppSettings _telegramSettings;

        public TelegramMessageService(IOptions<TelegramAppSettings> telegramSettings)
        {
            _telegramSettings = telegramSettings.Value;
        }

        public async void SendMessage(string message)
        {
            string baseUrl = $"https://api.telegram.org/bot{_telegramSettings.ApiToken}/sendMessage";

            using (HttpClient client = new HttpClient())
            {
                var parameters = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("chat_id", _telegramSettings.ChatId),
                new KeyValuePair<string, string>("text", message)
            });

                var response = await client.PostAsync(baseUrl, parameters);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                }
                else
                {
                    Console.WriteLine($"Erro: {response.StatusCode}");
                }
            }
        }
    }
}
