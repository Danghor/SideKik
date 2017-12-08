﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SideKik
{
	public sealed class Request
	{
		public Guid ID { get; } = Encryption.CreateRequestGuid();
		public DateTime Created { get; } = DateTime.UtcNow;
		public RequestState State { get; private set; } = RequestState.Created;

		public void Answer(XmlNode answer)
		{
			State = RequestState.Answered;
			if (answer.Attributes["type"].Value != "error")
			{
				OnAnswer?.Invoke(answer);
			}
			else
			{
				OnError?.Invoke(answer);
			}
		}

		public event MessagePump.Callback OnAnswer;
		public event MessagePump.Callback OnError;
	}
}
