using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace AlimBlockChain
{
    public class Block
    {

        public int Index { get; set; }

        public string TimeStamp { get; set; }

        public string Hash { get; set; }

        public string PreviousHash { get; set; }

        public string Data { get; set; }

        public int Nonce { get; set; }

        public Block(int index, string previousHash, string data)
        {
            Index = index;
            TimeStamp = DateTime.Now.ToString("g");
            PreviousHash = previousHash;
            Data = data;
            Nonce = 0;
            Hash = BlockHasher.calculateHash(this);
        }


        public string Str()
        {
            return Index + TimeStamp + PreviousHash + Data + Nonce;
        }

       

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("Block #").Append(Index).Append(" [previousHash : ").Append(PreviousHash).Append("; ").
                Append("timestamp : ").Append(TimeStamp).Append("; ").Append("data : \n").Append(Data).Append(";\n ").
                Append("hash : ").Append(Hash).Append("]");

            return builder.ToString();
        }

        public object GetData()
        {
            var item = JsonConvert.DeserializeObject<object>(Data);
            return item;
        }

    }
}
