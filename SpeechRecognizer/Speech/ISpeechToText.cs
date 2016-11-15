using System;
using System.Threading.Tasks;

namespace Listener
{
	public interface ISpeechToText
	{
		Task<string> SpeechToTextAsync();
	}
}
