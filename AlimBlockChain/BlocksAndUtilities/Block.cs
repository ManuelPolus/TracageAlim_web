﻿using System;
using System.Collections.Generic;
using System.Text;


namespace AlimBlockChain
{
    public class Block
    {

        public int Index { get; set; }

        public string TimeStamp { get; set; }

        public string Hash { get; set; }

        public string PreviousHash { get; set; }

        public Dictionary<string,string> Data { get; set; }

        public int Nonce { get; set; }

        public Block(int index, string previousHash, Dictionary<string, string> data)
        {
            Index = index;
            TimeStamp = DateTime.Now.ToString("g");
            PreviousHash = previousHash;
            Data = data;
            Nonce = 0;
            Hash = BlockHasher.calculateHash(this);
        }

        private string getData()
        {
            string dataAssembled = string.Empty;
            foreach (var values in Data)
            {
                dataAssembled += values.Key + ":" + values.Value + ",";
            }

            return dataAssembled;
        }


        public string Str()
        {
            return Index + TimeStamp + PreviousHash + getData() + Nonce;
        }

       

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("Block #").Append(Index).Append(" [previousHash : ").Append(PreviousHash).Append("; ").
                Append("timestamp : ").Append(TimeStamp).Append("; ").Append("data : ").Append(getData()).Append("; ").
                Append("hash : ").Append(Hash).Append("]");

            return builder.ToString();
        }

    }
}
