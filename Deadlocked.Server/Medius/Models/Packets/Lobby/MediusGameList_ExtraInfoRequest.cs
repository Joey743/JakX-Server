using Deadlocked.Server.Stream;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Deadlocked.Server.Medius.Models.Packets.Lobby
{
	[MediusMessage(NetMessageTypes.MessageClassLobbyExt, MediusLobbyExtMessageIds.GameList_ExtraInfo)]
    public class MediusGameList_ExtraInfoRequest : BaseLobbyExtMessage
    {

		public override byte PacketType => (byte)MediusLobbyExtMessageIds.GameList_ExtraInfo;

        public ushort PageID;
        public ushort PageSize;

        public override void Deserialize(BinaryReader reader)
        {
            // 
            base.Deserialize(reader);

            //
            reader.ReadBytes(1);
            PageID = reader.ReadUInt16();
            PageSize = reader.ReadUInt16();
        }

        public override void Serialize(BinaryWriter writer)
        {
            // 
            base.Serialize(writer);

            // 
            writer.Write(new byte[1]);
            writer.Write(PageID);
            writer.Write(PageSize);
        }


        public override string ToString()
        {
            return base.ToString() + " " +
             $"PageID:{PageID}" + " " +
$"PageSize:{PageSize}";
        }
    }
}