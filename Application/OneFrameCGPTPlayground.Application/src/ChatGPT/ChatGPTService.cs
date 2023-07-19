// <copyright file="ChatGPTService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Models;
using KocSistem.OneFrame.DesignObjects.Services;
using OneFrameCGPTPlayground.Application.Abstractions.ChatGPT;
using OpenAI_API;
using OpenAI_API.Chat;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Application.ChatGPT
{
    public class ChatGPTService : IChatGPTService
    {
        public ChatGPTService()
        {

        }

        public ChatGPTService(string apiKey)
        {
            Api_Key = apiKey;
        }

        public string Api_Key { get; set; }

        public async Task<ServiceResponse<string>> Compare(string sourceContent, string targetContent)
        {
            var response = await CompareTextFiles(sourceContent, targetContent).ConfigureAwait(false);

            return response;
        }

        private async Task<ServiceResponse<string>> CompareTextFiles(string text1, string text2)
        {
            OpenAIAPI api = new OpenAIAPI(Api_Key);
            string response = string.Empty;
            var serviceResponse = new ServiceResponse<string>(response);
            var chat = api.Chat.CreateConversation();
            chat.AppendUserInput("I've would like you to compare two different files.");
            chat.AppendUserInput("Here is the contents of the file 1:\n" + text1);
            chat.AppendUserInput("And here is the contents of the file 2:\n" + text2);

            try
            {
                var chatCompletion = await api.Chat.CreateChatCompletionAsync(model: "gpt-3.5-turbo-16k",
                    temperature: 0.5,
                    messages: chat.Messages.Select(m => new ChatMessage(m.Role, m.Content)).ToList()).ConfigureAwait(false);

                response = chatCompletion.Choices[0].Message.Content;
                serviceResponse.Result = response;
                serviceResponse.IsSuccessful = true;
            }
            catch (Exception e)
            {
                serviceResponse.Error = new ErrorInfo(e.Message);
                serviceResponse.IsSuccessful = false;
            }

            return serviceResponse;
        }
    }
}
