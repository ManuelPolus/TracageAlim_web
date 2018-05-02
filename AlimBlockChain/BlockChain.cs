using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlimBlockChain
{
    public class BlockChain
    {
        public int Difficulty { get; set; }

        public List<Block> Blocks;


        public BlockChain(int difficulty)
        {
            this.Difficulty = difficulty;
            Blocks = new List<Block>();
            Dictionary<string,string> data = new Dictionary<string, string>();
            data.Add("first block"," new block chain");
            // create the first block
            Block b = new Block(0, null, data);
            Miner.MineBlock(difficulty,b);
            Blocks.Add(b);
        }


        public Block LatestBlock()
        {
            return Blocks.ElementAt(Blocks.Count - 1);
        }

        public Block NewBlock(Dictionary<string, string> data)
        {
            var timeStamp = DateTime.Now;
            Block last = LatestBlock();
            return new Block(LatestBlock().Index + 1, last.Hash, data);
        }

        public void AddBlock(Block b)
        {
            if (b == null)
            {
                throw new NullReferenceException("block can't be null");
            }

            Miner.MineBlock(Difficulty, b);
            Blocks.Add(b);
        }


        public String toString()
        {
            StringBuilder builder = new StringBuilder();

            foreach (Block block in Blocks)
            {
                builder.AppendLine(block.ToString());
            }
            return builder.ToString();
        }

    }
}
