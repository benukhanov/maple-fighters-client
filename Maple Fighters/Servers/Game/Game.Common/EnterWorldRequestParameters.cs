﻿using System.IO;
using CommonCommunicationInterfaces;

namespace Shared.Game.Common
{
    public struct EnterWorldRequestParameters : IParameters
    {
        public int CharacterIndex;

        public EnterWorldRequestParameters(int characterIndex)
        {
            CharacterIndex = characterIndex;
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(CharacterIndex);
        }

        public void Deserialize(BinaryReader reader)
        {
            CharacterIndex = reader.ReadInt32();
        }
    }
}