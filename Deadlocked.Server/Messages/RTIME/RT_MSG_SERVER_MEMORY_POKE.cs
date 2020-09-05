﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Deadlocked.Server.Messages.RTIME
{
    [Message(RT_MSG_TYPE.RT_MSG_SERVER_MEMORY_POKE)]
    public class RT_MSG_SERVER_MEMORY_POKE : BaseMessage
    {
        public override RT_MSG_TYPE Id => RT_MSG_TYPE.RT_MSG_SERVER_MEMORY_POKE;

        public uint Address = 0;
        public byte[] Payload;

        public override void Deserialize(BinaryReader reader)
        {
            Address = reader.ReadUInt32();
            int len = reader.ReadInt32();
            Payload = reader.ReadBytes(len);
        }

        protected override void Serialize(BinaryWriter writer)
        {
            writer.Write(Address);
            writer.Write(Payload?.Length ?? 0);
            if (Payload != null)
                writer.Write(Payload);
        }

        public override string ToString()
        {
            return base.ToString() + " " +
                $"Address:{Address:X8} " +
            $"Payload:{BitConverter.ToString(Payload)}";
        }


        public static List<RT_MSG_SERVER_MEMORY_POKE> FromPayload(uint address, byte[] payload)
        {
            int i = 0;
            var msgs = new List<RT_MSG_SERVER_MEMORY_POKE>();

            while (i < payload.Length)
            {
                int len = (payload.Length - i);
                if (len > MediusConstants.MEDIUS_MESSAGE_MAXLEN)
                    len = MediusConstants.MEDIUS_MESSAGE_MAXLEN;

                // 
                var msg = new RT_MSG_SERVER_MEMORY_POKE()
                {
                    Address = (uint)(address + i),
                    Payload = new byte[len]
                };

                // 
                Array.Copy(payload, i, msg.Payload, 0, len);

                // 
                msgs.Add(msg);

                i += len;
            }


            return msgs;
        }
    }
}