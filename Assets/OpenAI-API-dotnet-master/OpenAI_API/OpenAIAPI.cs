using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI_API
{
	/// <summary>
	/// Entry point to the OpenAPI API, handling auth and allowing access to the various API endpoints
	/// </summary>
	public class OpenAIAPI
	{
		/// <summary>
		/// The API authentication information to use for API calls
		/// </summary>
		public APIAuthentication Auth { get; set; }

		/// <summary>
		/// Creates a new entry point to the OpenAPI API, handling auth and allowing access to the various API endpoints
		/// </summary>
		/// <param name="apiKeys">The API authentication information to use for API calls, or <see langword="null"/> to attempt to use the <see cref="APIAuthentication.Default"/>, potentially loading from environment vars or from a config file.</param>
		public OpenAIAPI(APIAuthentication apiKeys = null)
		{
			this.Auth = apiKeys.ThisOrDefault();
			Completions = new CompletionEndpoint(this);
			Models = new ModelsEndpoint(this);
		}

		/// <summary>
		/// Text generation is the core function of the API. You give the API a prompt, and it generates a completion. The way you “program” the API to do a task is by simply describing the task in plain english or providing a few written examples. This simple approach works for a wide range of use cases, including summarization, translation, grammar correction, question answering, chatbots, composing emails, and much more (see the prompt library for inspiration).
		/// </summary>
		public CompletionEndpoint Completions { get; }

        /// <summary>
        /// The API endpoint for querying available Engines/models
        /// </summary>
        public ModelsEndpoint Models { get; }
	}
}
