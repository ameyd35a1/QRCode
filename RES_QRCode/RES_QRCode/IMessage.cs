using System;
using System.Collections.Generic;
using System.Text;

namespace RES_QRCode
{
    public interface IMessage
    {
        void LongMessage(string message);

        void ShortMessage(string message);
    }
}
