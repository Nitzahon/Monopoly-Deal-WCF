using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDWcfServiceLibrary
{
    internal class MessageManager
    {
        Queue<Message> messagesQueued = new Queue<Message>();
        Queue<Message> messagesDequeued = new Queue<Message>();
        Queue<Message> sentMessages = new Queue<Message>();
        Queue<Message> recievedMessages = new Queue<Message>();
        Message currentMessage = null;
    }
}