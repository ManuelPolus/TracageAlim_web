using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlimBlockChain.BlocksAndUtilities
{
    public class ValidityGranter
    {
        public  BlockChain Chain { get; set; }

        public ValidityGranter(BlockChain chain)
        {
            Chain = chain;
        }


        public  bool IsFirstBlockValid()
        {
            Block firstBlock = Chain.Blocks.ElementAt(0);
            if
            (
                firstBlock.Index != 0
                ||
                firstBlock.PreviousHash != null
                ||
                firstBlock.Hash == null
                ||
                !BlockHasher.calculateHash(firstBlock).Equals(firstBlock.Hash)
            )
            {
                return false;
            }

            return true;
        }

        public  bool IsNewBlockValid(Block newBlock, Block previousBlock)
        {
            if (newBlock != null && previousBlock != null)
            {
                if (previousBlock.Index + 1 != newBlock.Index)
                {
                    return false;
                }

                if (newBlock.PreviousHash == null ||
                    !newBlock.PreviousHash.Equals(previousBlock.Hash))
                {
                    return false;
                }

                if (newBlock.Hash == null ||
                    !BlockHasher.calculateHash(newBlock).Equals(newBlock.Hash))
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        public  bool IsBlockChainValid()
        {
            if (!IsFirstBlockValid())
            {
                return false;
            }

            for (int i = 1; i < Chain.Blocks.Count; i++)
            {
                Block currentBlock = Chain.Blocks.ElementAt(i);
                Block previousBlock = Chain.Blocks.ElementAt(i - 1);

                if (!IsNewBlockValid(currentBlock, previousBlock))
                {
                    return false;
                }
            }

            return true;
        }

    }
}
